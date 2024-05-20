using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenu : MonoBehaviour
{
    public GameObject musicMenu;


    public void OpenMusicMenu()
    {
        if (musicMenu.activeSelf)
        {
            musicMenu.SetActive(false);
        }
        else
        {
            musicMenu.SetActive(true);
        }
    }
}
