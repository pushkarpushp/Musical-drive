using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ToastManager : MonoBehaviour
{
    public string message;
    public TMPro.TextMeshPro text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMPro.TextMeshPro>();
    }


    public void SetMessage(string message)
    {
        this.message = message;
        if (message == null)
        {
            gameObject.SetActive(false);
            text.text = "";
        }
        else
        {
            gameObject.SetActive(true);
            text.text = message;
        }
    }
}
