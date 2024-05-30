using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CopyText : MonoBehaviour
{
    public ToastManager toastManager;
    // Start is called before the first frame update

    public void Copy()
    {
        toastManager.SetMessage("Copied!");
        TextEditor te = new()
        {
            text = gameObject.GetComponent<TMP_Text>().text
        };
        te.SelectAll();
        te.Copy();
    }
}
