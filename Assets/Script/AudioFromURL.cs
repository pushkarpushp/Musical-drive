using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class AudioFromURL : MonoBehaviour
{
    public string audioUrl = ""; // URL to your audio file
    private AudioSource audioSource;
    public Slider audioSlider; // Reference to the UI Slider
    public TextMeshProUGUI elapsedTimeText; // Reference to the UI Text for elapsed time
    public TextMeshProUGUI durationText; // Reference to the UI Text for total duration

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        setMusic(audioUrl);
    }

    void Update()
    {
        if (audioSource.clip != null)
        {
            if (audioSource.isPlaying)
            {
                // Update slider value
                audioSlider.value = audioSource.time / audioSource.clip.length;

                // Update elapsed time text
                elapsedTimeText.text = FormatTime(audioSource.time);
            }

            // Update total duration text
            durationText.text = FormatTime(audioSource.clip.length);
        }
    }

    public void setMusic(string url)
    {
        StartCoroutine(PlayAudioFromURL(url));
    }

    IEnumerator PlayAudioFromURL(string url)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                audioSource.clip = clip;
                //audioSource.Play();
                // Optionally, reset the slider to 0 at the start of playing
                audioSlider.value = 0;
            }
        }
    }

    // Helper method to format time into minutes:seconds
    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time % 60F);
        return string.Format("{0:0}:{1:00}", minutes, seconds);
    }
}
