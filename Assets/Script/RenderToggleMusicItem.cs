using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RenderToggleItem : MonoBehaviour
{
    public Types.Item item;
    public MusicPlayer musicPlayer;
    public int index;

    public GameObject background;

    public LoadImageFromUrl loadImageFromUrl;

    public bool isPlaying = false;

    public void SetMusic()
    {
        musicPlayer.PlayAtIndex(index);
    }

    // Start is called before the first frame update
    void Start()
    {

        // get title gameobject which is the child of this gameobject
        TextMeshProUGUI title = transform.Find("Title").GetComponent<TextMeshProUGUI>();

        var newSongName = Helper.ShortenString(item.Metadata.Title ?? item.Metadata.Content, 40);

        title.text = newSongName;


        TextMeshProUGUI artist = transform.Find("Artist").GetComponent<TextMeshProUGUI>();


        var newArtistName = item.Metadata.Asset.Artist ?? item.By?.Handle?.FullHandle;

        artist.text = newArtistName;

        loadImageFromUrl.SetUrl(item.Metadata.Asset.Cover.Optimized.Uri);
    }

    void Update()
    {

        if (musicPlayer.currentItemIndex == index && !isPlaying)
        {
            background.SetActive(true);
            isPlaying = true;
            return;
        }

        if (musicPlayer.currentItemIndex != index && isPlaying)
        {
            background.SetActive(false);
            isPlaying = false;
            return;
        }
    }
}
