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
    private bool isLoadingSong = false;
    public bool isPlaying = true;
    public bool isPaused = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySong()
    {
        if (!isPlaying)
        {
            audioSource.Play();
            spriteController.isPlaying = true;
            isPlaying = true;
            isPaused = false;
        }
    }

    public void PauseSong()
    {
        if (isPlaying)
        {
            audioSource.Pause();
            spriteController.isPlaying = false;
            isPlaying = false;
            isPaused = true;
        }
    }

    public void SwitchPlay()
    {
        if (!isPlaying)
        {
            PlaySong();
        }
        else
        {
            PauseSong();
        }
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

                isLoadingSong = false;
            }
            else if (!isLoadingSong && !isPaused)
            {
                musicPlayer.PlayNext();
                isLoadingSong = true;
            }

            // Update total duration text
            durationText.text = FormatTime(audioSource.clip.length);
        }

        if (musicPlayer.selectedItem.Metadata.Asset.Audio.Optimized.Uri != audioUrl)
        {
            audioUrl = musicPlayer.selectedItem.Metadata.Asset.Audio.Optimized.Uri;
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
                isPlaying = true;
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
