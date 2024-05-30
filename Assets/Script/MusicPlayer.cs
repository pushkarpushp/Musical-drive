using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

public class MusicPlayer : MonoBehaviour
{
  public bool isMusicLoading = true;
  public enum SelectedPlayList
  {
    EXPLORE,
    TOP_MINTED,
    MY_MINTS
  }
  public SelectedPlayList selectedPlayList;

  private string nextCursor = "";

  public List<Types.Item> items = new List<Types.Item>();
  public int currentItemIndex = 0;

  public Types.Item selectedItem = null;

  public ToastManager toastManager;


  public async Task GetMusicItems()
  {
    switch (selectedPlayList)
    {
      case SelectedPlayList.EXPLORE:
        await getExploreMusicItems();
        break;
      case SelectedPlayList.TOP_MINTED:
        await getTopMintedMusicItems();
        break;
      case SelectedPlayList.MY_MINTS:
        await getMyMintsMusicItems();
        break;
    }
  }

  public async void SetSelectedPlayList(SelectedPlayList playlist)
  {
    if (playlist == selectedPlayList) return;
    selectedPlayList = playlist;
    items = new List<Types.Item>();
    nextCursor = "";
    currentItemIndex = 0;
    selectedItem = null;

    if (Wallet.lensProfile == null)
    {
      // todo show pop up message instead of Log message
      Debug.Log("Lens Profile not logged in");
      return;
    }

    await GetMusicItems();
  }


  public async void RefreshPlayList()
  {
    items = new List<Types.Item>();
    nextCursor = "";
    currentItemIndex = 0;
    selectedItem = null;

    await GetMusicItems();
  }

  public async void PlayNext()
  {
    if (items.Count > 0 && currentItemIndex < items.Count - 1)
    {
      currentItemIndex++;
      selectedItem = items[currentItemIndex];
    }

    // if the selected item is one of the last 3 items, load more items
    if (currentItemIndex >= items.Count - 3)
    {
      await GetMusicItems();
    }

  }

  public void PlayPrevious()
  {
    if (items.Count > 0 && currentItemIndex > 0)
    {
      currentItemIndex--;
      selectedItem = items[currentItemIndex];
    }

  }

  public async void PlayAtIndex(int index)
  {
    if (items.Count > 0 && index >= 0 && index < items.Count)
    {
      currentItemIndex = index;
      selectedItem = items[currentItemIndex];
    }

    // if the selected item is one of the last 3 items, load more items
    if (currentItemIndex >= items.Count - 3)
    {
      await GetMusicItems();
    }

  }

  // Start is called before the first frame update
  void Start()
  {
  }

  public void SelectExploreMusicPlaylist()
  {
    SetSelectedPlayList(SelectedPlayList.EXPLORE);
  }

  public void SelectTopMintedMusicPlaylist()
  {
    SetSelectedPlayList(SelectedPlayList.TOP_MINTED);
  }

  public void SelectMyMintsMusicPlaylist()
  {
    SetSelectedPlayList(SelectedPlayList.MY_MINTS);
  }

  // Update is called once per frame
  void Update()
  {
  }

  public async Task getTopMintedMusicItems()
  {
    await getExploreMusicItems("TOP_COLLECTED_OPEN_ACTION");
  }

  public async Task getMyMintsMusicItems()
  {
    if (Wallet.lensProfile == null)
    {
      toastManager.SetMessage("Lens Profile not logged in");
      return;
    }
    await getMyMintedPublicationItems();
  }


  public async Task getMyMintedPublicationItems()
  {
    if (Wallet.lensProfile == null) return;

    string query = @"
    query Publications($request: PublicationsRequest!, $hasActedRequest2: PublicationOperationsActedArgs, $hasReactedRequest2: PublicationOperationsReactionArgs) {
  publications(request: $request) {
    items {
       ... on Post {
        id
        by {
          handle {
            fullHandle
          }
          metadata {
            picture {
              ... on ImageSet {
                optimized {
                  uri
                }
              }
            }
          }
        }
        metadata {
          ... on AudioMetadataV3 {
            title
            rawURI
            asset {
              artist
              audio {
                optimized {
                  uri
                }
              }
              cover {
                optimized {
                  uri
                }
              }
            }
          }
        }
        openActionModules {
          ... on SimpleCollectOpenActionSettings {
            type
            amount {
              value
            }
          }
        }
        operations {
          hasActed(request: $hasActedRequest2) {
            value
          }
          canAct
          hasReacted(request: $hasReactedRequest2)
        }
      }
    }
    pageInfo {
      next
    }
  }
}
    
    ";

    var variables = new
    {
      request = new
      {
        cursor = nextCursor.Length > 0 ? nextCursor : (string)null,
        limit = "Ten",
        where = new
        {
          actedBy = Wallet.lensProfile.Id,
          publicationTypes = "POST",
          metadata = new
          {
            mainContentFocus = "AUDIO"
          }
        }
      },
      hasReactedRequest2 = new
      {
        type = "UPVOTE"
      }
    };

    string response = await GraphQL.Instance.PostGraphQLRequest(query, variables);

    var settings = new JsonSerializerSettings
    {
      NullValueHandling = NullValueHandling.Ignore
    };

    Types.PublicationsRoot data = JsonConvert.DeserializeObject<Types.PublicationsRoot>(response, settings);

    if (data?.Data?.Publications?.Items != null)
    {
      items.AddRange(data.Data.Publications.Items);
      nextCursor = data.Data.Publications.PageInfo.Next;
    }

    if (items != null && items.Count > 0 && selectedItem == null)
    {
      selectedItem = items[currentItemIndex];
    }

  }

  public async Task getExploreMusicItems(string orderBy = "LATEST")
  {
    string query = @"
query ExplorePublications($request: ExplorePublicationRequest!, $hasActedRequest2: PublicationOperationsActedArgs, $hasReactedRequest2: PublicationOperationsReactionArgs) {
  explorePublications(request: $request) {
    items {
      ... on Post {
        id
        by {
          handle {
            fullHandle
          }
          metadata {
            picture {
              ... on ImageSet {
                optimized {
                  uri
                }
              }
            }
          }
        }
        metadata {
          ... on AudioMetadataV3 {
            title
            rawURI
            asset {
              artist
              audio {
                optimized {
                  uri
                }
              }
              cover {
                optimized {
                  uri
                }
              }
            }
          }
        }
        openActionModules {
          ... on SimpleCollectOpenActionSettings {
            type
            amount {
              value
            }
          }
        }
        operations {
          hasActed(request: $hasActedRequest2) {
            value
          }
          canAct
          hasReacted(request: $hasReactedRequest2)
        }
      }
    }
    pageInfo {
      next
    }
  }
}

        ";

    var variables = new
    {
      request = new
      {
        cursor = nextCursor.Length > 0 ? nextCursor : (string)null,
        limit = "Ten",
        orderBy,
        where = new
        {
          publicationTypes = "POST",
          metadata = new
          {
            mainContentFocus = "AUDIO"
          },
          since = 1709164800
        }
      },
      hasReactedRequest2 = new
      {
        type = "UPVOTE"
      }
    };
    string response = await GraphQL.Instance.PostGraphQLRequest(query, variables);

    var settings = new JsonSerializerSettings
    {
      NullValueHandling = NullValueHandling.Ignore
    };

    Types.ExplorePublicationsRoot data = JsonConvert.DeserializeObject<Types.ExplorePublicationsRoot>(response, settings);

    if (data?.Data?.ExplorePublications?.Items != null)
    {
      items.AddRange(data.Data.ExplorePublications.Items);
      nextCursor = data.Data.ExplorePublications.PageInfo.Next;
    }

    if (items != null && items.Count > 0 && selectedItem == null)
    {
      selectedItem = items[currentItemIndex];
    }

  }

}
