using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    public GameObject Play;
    public GameObject Pause;
    public bool isPlaying = true;
    // Start is called before the first frame update
    void Start()
    {
        Play.SetActive(true);
        Pause.SetActive(false);
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
