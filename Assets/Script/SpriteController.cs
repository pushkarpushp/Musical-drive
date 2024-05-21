using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    public GameObject Play;
    public GameObject Pause;
    public bool isPlaying = true;
    public AudioSource PlaySource;
    // Start is called before the first frame update
    void Start()
    {
        Play.SetActive(true);
        Pause.SetActive(false);
    }

    // Update is called once per frame
    public void SwitchPlay()
    {
        if (!isPlaying)
        {
            PlaySource.Play();
            Play.SetActive(false);
            Pause.SetActive(true);
            isPlaying = true;
        }
        else
        {
            PlaySource.Pause();
            Play.SetActive(true);
            Pause.SetActive(false);
            isPlaying = false;
        }
    }

    void Update()
    {
        if (isPlaying)
        {
            Play.SetActive(false);
            Pause.SetActive(true);
        }
        else
        {
            Play.SetActive(true);
            Pause.SetActive(false);
        }
    }




}
