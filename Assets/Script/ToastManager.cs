using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ToastManager : MonoBehaviour
{
    public string message;
    public TMPro.TextMeshPro text;
    public GameObject toastBox;
    // Start is called before the first frame update


    public void SetMessage(string message)
    {
        this.message = message;
        if (message == null)
        {
            toastBox.SetActive(false);
            text.text = "";
        }
        else
        {
            toastBox.SetActive(true);
            text.text = message;
            Invoke("disableToastBox", 3f);
        }
    }

    public void disableToastBox()
    {
        toastBox.SetActive(false);
    }
}
