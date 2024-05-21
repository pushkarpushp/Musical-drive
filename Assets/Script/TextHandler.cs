using System;
using System.Collections;
using System.Collections.Generic;
using Meta.WitAi.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextHandler : MonoBehaviour
{
    public TextMeshProUGUI songNameText;
    public TextMeshProUGUI artistNameText;

    public MusicPlayer musicPlayer;

    private string SongName;
    private string ArtistName;
    // Start is called before the first frame update
    void Start()
    {
    }

    void updateText()
    {
        var newSongName = musicPlayer.selectedItem.Metadata.Title ?? Helper.ShortenString(musicPlayer.selectedItem.Metadata.Content, 40);

        if (newSongName != SongName)
        {
            SongName = newSongName;
            songNameText.text = SongName;
        }


        var newArtistName = musicPlayer.selectedItem.Metadata.Asset.Artist ?? musicPlayer?.selectedItem?.By?.Handle?.FullHandle;

        if (newArtistName != ArtistName)
        {
            ArtistName = newArtistName;
            artistNameText.text = ArtistName;
        }

    }

    // Update is called once per frame
    void Update()
    {
        updateText();
    }
}
