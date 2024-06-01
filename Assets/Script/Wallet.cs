using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thirdweb;
using Newtonsoft.Json;
using TMPro;

public class Wallet : MonoBehaviour
{

  public MusicPlayer musicPlayer;
  public ToastManager toastManager;
  // Start is called before the first frame update
  public static string walletAddress;
  public static Types.Profile lensProfile = null;

  public static Types.Authenticate authenticate = null;

  // wallet address text mesh pro object list
  public List<TextMeshPro> walletAddressTexts;

  public OpenMenu openMenu;

  public LoadImageFromUrl logoImage;

  public TextMeshPro handleText;

  void Start()
  {
    // Connect to the wallet
    ConnectWallet();
  }



  public async void ConnectWallet()
  {
    // Reference to your Thirdweb SDK
    var sdk = ThirdwebManager.Instance.SDK;

    // Configure the connection
    var connection = new WalletConnection(
      provider: WalletProvider.LocalWallet,      // The wallet provider you want to connect to (Required)
      chainId: 137                               // The chain you want to connect to (Required)
                                                 // password: "myEpicPassword"                 // Used to encrypt your Local Wallet, defaults to device uid (Optional)
    );

    // Connect the wallet
    string address = await sdk.Wallet.Connect(connection);

    Debug.Log("address " + address);

    walletAddress = address;

    // Update the wallet address text mesh pro objects
    foreach (var text in walletAddressTexts)
    {
      text.text = Helper.ShortenString(address, 15);
    }

    CheckAndLoginLensProfile();
  }

  public async void CheckAndLoginLensProfile()
  {
    if (walletAddress == null)
    {
      musicPlayer.RefreshPlayList();
      return;
    };
    // check profiles for the wallet
    string query = @"
    query ProfilesManaged($request: ProfilesManagedRequest!) {
  profilesManaged(request: $request) {
    items {
      handle {
        fullHandle
      }
      id
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
  }
}
    ";

    var variables = new
    {
      request = new
      {
        @for = walletAddress
      }
    };

    string response = await GraphQL.Instance.PostGraphQLRequest(query, variables);

    var settings = new JsonSerializerSettings
    {
      NullValueHandling = NullValueHandling.Ignore
    };

    Types.ProfilesManagedRoot profilesManaged = JsonConvert.DeserializeObject<Types.ProfilesManagedRoot>(response, settings);

    var profiles = profilesManaged.Data.ProfilesManaged.Items;

    Debug.Log("profiles managed " + profiles);

    if (profiles.Count > 0)
    {
      SignInLensProfile(profiles[0]);
    }
    else
    {
      musicPlayer.RefreshPlayList();
    }
  }

  public async void SignInLensProfile(Types.Profile profile)
  {
    var query = @"
    query Challenge($request: ChallengeRequest!) {
  challenge(request: $request) {
    id
    text
  }
}
    ";

    var variables = new
    {
      request = new
      {
        @for = profile.Id,
        signedBy = walletAddress
      }
    };

    string response = await GraphQL.Instance.PostGraphQLRequest(query, variables);

    var settings = new JsonSerializerSettings
    {
      NullValueHandling = NullValueHandling.Ignore
    };

    Types.ChallengeRoot challengeRoot = JsonConvert.DeserializeObject<Types.ChallengeRoot>(response, settings);

    var challenge = challengeRoot.Data.Challenge.Text;
    var challengeId = challengeRoot.Data.Challenge.Id;

    var sdk = ThirdwebManager.Instance.SDK;

    var signature = await sdk.Wallet.Sign(challenge);

    var authenticateQuery = @"
mutation Authenticate($request: SignedAuthChallenge!) {
  authenticate(request: $request) {
    accessToken
    refreshToken
  }
}
    ";

    var authenticateVariables = new
    {
      request = new
      {
        id = challengeId,
        signature
      }
    };

    string authenticateResponse = await GraphQL.Instance.PostGraphQLRequest(authenticateQuery, authenticateVariables);

    var authenticateSettings = new JsonSerializerSettings
    {
      NullValueHandling = NullValueHandling.Ignore
    };

    Types.AuthenticateRoot authenticateRoot = JsonConvert.DeserializeObject<Types.AuthenticateRoot>(authenticateResponse, authenticateSettings);

    authenticate = authenticateRoot.Data.Authenticate;

    if (authenticate != null)
    {
      lensProfile = profile;
      Debug.Log("Logged in as " + lensProfile.Handle.FullHandle);
      toastManager.SetMessage("Logged in as " + lensProfile.Handle.FullHandle);
            musicPlayer.RefreshPlayList();
            handleText.text = lensProfile.Handle.FullHandle;
      logoImage.SetUrl(lensProfile.Metadata.Picture.Optimized.Uri);
            
            
            openMenu.ShowLoggedInMenu();
    }
        
    
       
        // ActOnSimpleCollectOpenAction("0xf71a-0x032a");
    }

  public void CopyAddress()
  {
    if (walletAddress == null) return;
    toastManager.SetMessage("Copied!");
    TextEditor te = new()
    {
      text = walletAddress
    };
    te.SelectAll();
    te.Copy();
  }
}



