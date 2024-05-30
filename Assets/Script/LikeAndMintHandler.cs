using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;

public class LikeAndMintHandler : MonoBehaviour
{
    public MusicPlayer musicPlayer;

    public Types.Item currentSelectedItem;

    public GameObject notLikedIcon;
    public GameObject notMintedIcon;

    public GameObject likedIcon;
    public GameObject mintedIcon;

    public GameObject MintButton;

    public ToastManager toastManager;

    public bool liked = false;
    public bool minted = false;

    public Wallet wallet;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentSelectedItem == musicPlayer.selectedItem) return;

        currentSelectedItem = musicPlayer.selectedItem;

        if (currentSelectedItem == null)
        {
            notLikedIcon.SetActive(true);
            likedIcon.SetActive(false);
            notMintedIcon.SetActive(true);
            mintedIcon.SetActive(false);
            liked = false;
            minted = false;
            return;

        };

        if (currentSelectedItem.Operations.HasReacted)
        {
            notLikedIcon.SetActive(false);
            likedIcon.SetActive(true);
            liked = true;
        }
        else
        {
            notLikedIcon.SetActive(true);
            likedIcon.SetActive(false);
            liked = false;
        }

        if (currentSelectedItem.Operations.HasActed.Value)
        {
            notMintedIcon.SetActive(false);
            mintedIcon.SetActive(true);
            minted = true;
        }
        else
        {
            notMintedIcon.SetActive(true);
            mintedIcon.SetActive(false);
            minted = false;
        }


        if (Wallet.lensProfile != null && currentSelectedItem?.Operations?.CanAct == "YES" && currentSelectedItem?.OpenActionModules[0]?.Type == "SimpleCollectOpenActionModule" && currentSelectedItem?.OpenActionModules[0]?.Amount?.Value == "0")
        {
            MintButton.SetActive(true);
        }
        else
        {
            MintButton.SetActive(false);
        }
    }

    public void Like()
    {
        if (liked) return;

        if (Wallet.lensProfile == null) return;

        toastManager.SetMessage("Liked the song!");

        // todo , for now just 
        liked = true;
        notLikedIcon.SetActive(false);
        likedIcon.SetActive(true);

        LikePublication();
    }

    public void Mint()
    {
        if (minted) return;

        toastManager.SetMessage("Minted the song!");

        // todo , for now just 
        minted = true;
        notMintedIcon.SetActive(false);
        mintedIcon.SetActive(true);

        ActOnSimpleCollectOpenAction(currentSelectedItem.Id);

    }

    public async void LikePublication()
    {
        var query = @"
        mutation AddReaction($request: ReactionRequest!) {
  addReaction(request: $request)
}";


        var variables = new
        {
            request = new
            {
                reaction = "UPVOTE",
                @for = currentSelectedItem.Id
            }
        };

        string response = await GraphQL.Instance.PostGraphQLRequest(query, variables);

        Debug.Log("response " + response);

        if (response != null)
        {
            Debug.Log("Success liking");
        }
        else
        {
            Debug.Log("Failed liking");

        }
    }

    public async void ActOnSimpleCollectOpenAction(string publicationId)
    {
        var query = @"
    mutation ActOnOpenAction($request: ActOnOpenActionLensManagerRequest!) {
  actOnOpenAction(request: $request) {
    ... on LensProfileManagerRelayError {
      reason
    }
    ... on RelaySuccess {
      txHash
      txId
    }
  }
}
    ";

        var variables = new
        {
            request = new
            {
                actOn = new
                {
                    simpleCollectOpenAction = true
                },
                @for = publicationId
            }
        };

        string response = await GraphQL.Instance.PostGraphQLRequest(query, variables);

        var settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        Types.ActOnOpenActionRoot actOnOpenActionRoot = JsonConvert.DeserializeObject<Types.ActOnOpenActionRoot>(response, settings);

        Debug.Log("response " + response);
        Debug.Log("actOnOpenActionRoot " + actOnOpenActionRoot);

        if (actOnOpenActionRoot.Data.ActOnOpenAction.TxHash != null)
        {
            Debug.Log("Success minting");
        }
        else
        {
            Debug.Log("Failed minting");
            Debug.Log("reason given " + actOnOpenActionRoot.Data.ActOnOpenAction.Reason);
        }
    }
}
