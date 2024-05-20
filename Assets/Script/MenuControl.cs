using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControl : MonoBehaviour
{
    public GameObject explore;
    public GameObject topMinted;
    public GameObject myMinted;
    
    public void Explore()
    {
        explore.SetActive(true);
        topMinted.SetActive(false);
        myMinted.SetActive(false);
    }

    public void Topminted()
    {
        explore.SetActive(false);
        topMinted.SetActive(true);
        myMinted.SetActive(false);
    }

    public void MyMinted()
    {
        explore.SetActive(false);
        topMinted.SetActive(false);
        myMinted.SetActive(true);
    }
}
