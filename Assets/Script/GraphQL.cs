using UnityEngine;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine.Networking;

public class GraphQL : MonoBehaviour
{
    private const string GraphQLUrl = "https://api-v2.lens.dev";

    public static GraphQL Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public async Task<string> PostGraphQLRequest(string query, object variables)
    {
        // Create the JSON payload
        var payload = new
        {
            query = query.Replace("\n", ""),
            variables
        };

        string jsonPayload = JsonConvert.SerializeObject(payload);
        var request = new UnityWebRequest(GraphQLUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonPayload);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Create a TaskCompletionSource to represent the operation
        var tcs = new TaskCompletionSource<string>();

        // Start the request
        var asyncOp = request.SendWebRequest();

        // Wait for completion
        asyncOp.completed += (AsyncOperation op) =>
        {
            // Check for errors
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(request.error);
                tcs.SetException(new System.Exception(request.error));
            }
            else
            {
                tcs.SetResult(request.downloadHandler.text);
            }
        };

        // Return the task
        return await tcs.Task;
    }

}
