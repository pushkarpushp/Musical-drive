using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class AudioFromURL : MonoBehaviour
{
    public SpriteController spriteController;
    public MusicPlayer musicPlayer;
    public string audioUrl = ""; // URL to your audio file
    private AudioSource audioSource;
    public Slider audioSlider; // Reference to the UI Slider
    public TextMeshProUGUI elapsedTimeText; // Reference to the UI Text for elapsed time
    public TextMeshProUGUI durationText; // Reference to the UI Text for total duration

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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

        if (musicPlayer.selectedItem.Metadata.Asset.Audio.Optimized.Uri != audioUrl)
        {
            audioUrl = musicPlayer.selectedItem.Metadata.Asset.Audio.Optimized.Uri;
            Debug.Log("Audio URL: " + audioUrl);
            setMusic(audioUrl);
        }
    }

    public void setMusic(string url)
    {
        StartCoroutine(PlayAudioFromURL(url));
    }

    IEnumerator PlayAudioFromURL(string url)
    {

        musicPlayer.isMusicLoading = true;
        var audioType = AudioType.MPEG;

        if (url.EndsWith(".wav"))
        {
            audioType = AudioType.WAV;
        }

        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, audioType))
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
                audioSource.Play();
                // Optionally, reset the slider to 0 at the start of playing
                audioSlider.value = 0;
                spriteController.isPlaying = true;
            }

            musicPlayer.isMusicLoading = false;
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
