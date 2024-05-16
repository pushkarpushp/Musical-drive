using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class shakeObject : MonoBehaviour
{
    public List<Transform> objectReactingToBasses, objectReactingToNB, objectReactingToMids, objectReactingToHigh;

    [SerializeField] float t = 0.1f;

    // Update is called once per frame
    void FixedUpdate()
    {
        makeObjectsShakescale();
    }

    void makeObjectsShakescale()
    {
        foreach (Transform obj in objectReactingToBasses)
        {
            obj.localScale = Vector3.Lerp(obj.localScale, new Vector3(1, MusicManager.instance.getFrequenciesDiapason(0, 7, 10), 1), t);
        }
        foreach (Transform obj in objectReactingToNB)
        {
            obj.localScale = Vector3.Lerp(obj.localScale, new Vector3(1, MusicManager.instance.getFrequenciesDiapason(7, 15, 100), 1), t);
        }
        foreach (Transform obj in objectReactingToMids)
        {
            obj.localScale = Vector3.Lerp(obj.localScale, new Vector3(1, MusicManager.instance.getFrequenciesDiapason(15, 30, 200), 1), t);
        }
        foreach (Transform obj in objectReactingToHigh)
        {
            obj.localScale = Vector3.Lerp(obj.localScale, new Vector3(1, MusicManager.instance.getFrequenciesDiapason(30, 32, 1000), 1), t);
        }
    }
}
