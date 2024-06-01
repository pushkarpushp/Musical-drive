using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlRotation : MonoBehaviour
{
    // The target GameObject whose rotation you want to change
    public GameObject target;

    void Update()
    {
        if (target != null)
        {
            // Copy the rotation of this GameObject to the target GameObject
            target.transform.rotation = transform.rotation;
        }
    }
}
