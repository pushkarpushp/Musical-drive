using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoadImageFromUrl : MonoBehaviour
{
    public string url;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DownloadAndLoadImageOnCanvas(url));
    }


    public void SetUrl(string url)
    {
        this.url = url;
        StartCoroutine(DownloadAndLoadImageOnCanvas(url));
    }


    IEnumerator DownloadAndLoadImageOnCanvas(string url)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
        }
        else
        {
            Texture2D webTexture = DownloadHandlerTexture.GetContent(request);

            // Adjust texture settings for better quality
            webTexture.filterMode = FilterMode.Bilinear; // or FilterMode.Trilinear for even better quality
            webTexture.anisoLevel = 9; // Increase anisotropic level
            webTexture.wrapMode = TextureWrapMode.Clamp; // Ensure proper wrapping

            gameObject.GetComponent<RawImage>().texture = webTexture;
        }
    }
}
