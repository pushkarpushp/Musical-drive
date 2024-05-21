using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
    // Start is called before the first frame update
    public static string ShortenString(string str, int maxLength)
    {
        if (str.Length > maxLength)
        {
            return str.Substring(0, maxLength) + "...";
        }
        return str;
    }
}
