using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenu : MonoBehaviour
{
    public GameObject musicMenu;
    public GameObject LoginMenu;

    public GameObject LoggedInMenu;

    public bool isMenuOpen = false;


    public void OpenMusicMenu()
    {
        if (musicMenu.activeSelf)
        {
            musicMenu.SetActive(false);
            LoginMenu.SetActive(false);
            LoggedInMenu.SetActive(false);
        }
        else
        {
            musicMenu.SetActive(true);
            if (Wallet.lensProfile == null)
            {
                LoginMenu.SetActive(true);
            }
            else
            {
                LoggedInMenu.SetActive(true);
            }
        }
    }

    public void ShowLoggedInMenu()
    {
        if (!musicMenu.activeSelf) return;

        if (Wallet.lensProfile == null)
        {
            LoginMenu.SetActive(true);
            LoggedInMenu.SetActive(false);
        }
        else
        {
            LoginMenu.SetActive(false);
            LoggedInMenu.SetActive(true);
        }
    }
}
