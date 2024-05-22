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

    private Toggle toggle;

    public bool isPlaying = false;

    public void SetMusic()
    {
        Debug.Log("SetMusic");
        // Debug.Log("Toogle is on" + value);

        musicPlayer.PlayAtIndex(index);
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("RenderToggleItem Start" + item);
        toggle = GetComponent<Toggle>();

        // toggle.onValueChanged.AddListener((value) =>
        // {
        //     Debug.Log("Toogle is on" + value);
        //     if (value)
        //     {
        //         musicPlayer.PlayAtIndex(index);
        //     } else {

        //     }
        // });

        // get title gameobject which is the child of this gameobject
        TextMeshProUGUI title = transform.Find("Title").GetComponent<TextMeshProUGUI>();

        var newSongName = Helper.ShortenString(item.Metadata.Title ?? item.Metadata.Content, 40);

        title.text = newSongName;


        TextMeshProUGUI artist = transform.Find("Artist").GetComponent<TextMeshProUGUI>();


        var newArtistName = musicPlayer.selectedItem.Metadata.Asset.Artist ?? musicPlayer?.selectedItem?.By?.Handle?.FullHandle;

        artist.text = newArtistName;

        loadImageFromUrl.SetUrl(item.Metadata.Asset.Cover.Optimized.Uri);

        // GameObject Panel = transform.Find("Panel").gameObject;

        // // RawImage is the child of Panel
        // RawImage rawImage = Panel.transform.Find("RawImage").GetComponent<RawImage>();

        // rawImage.GetComponent<LoadImageFromUrl>().SetUrl(item.Metadata.Asset.Cover.Optimized.Uri);
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

        // if (toggle.isOn)
        // {
        //     Debug.Log("Toogle is on Update" + toggle.isOn);
        //     Debug.Log("at index" + index);

        //     musicPlayer.PlayAtIndex(index);
        //     return;
        // }
    }

    // Update is called once per frame
    // void FixedUpdate()
    // {
    //     if (musicPlayer.currentItemIndex != index)
    //     {
    //         toggle.isOn = false;
    //     }
    // }
}
