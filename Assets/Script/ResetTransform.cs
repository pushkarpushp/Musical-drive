using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTransform : MonoBehaviour
{
    private Quaternion originalRotation;
    private bool shouldResetRotation = true;
    public float resetSpeed = 1.0f;

    void Start()
    {
        // Store the original rotation at the start
        originalRotation = transform.rotation;
    }

    void Update()
    {
        if (shouldResetRotation)
        {
            // Smoothly interpolate from current rotation to the original rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, originalRotation, Time.deltaTime * resetSpeed);

            // Check if the rotation is almost back to the original rotation
            if (Quaternion.Angle(transform.rotation, originalRotation) < 0.01f)
            {
                transform.rotation = originalRotation;
                shouldResetRotation = false;
            }
        }

        // Trigger the reset process with the "R" key
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetRotation();
        }
    }

    // Method to start resetting the rotation
    public void ResetRotation()
    {
        shouldResetRotation = true;
    }
}
