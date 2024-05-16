using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarVibration : MonoBehaviour
{
    // Variables for controlling the vibration effect
    public float vibrationFrequency = 10f; // Frequency of the vibration
    public float baseVibrationIntensity = 0.1f; // Base intensity of the vibration
    public float maxRandomIntensityOffset = 0.05f; // Maximum random intensity offset

    private float timeCounter = 0f;

    void Update()
    {
        // Update the time counter
        timeCounter += Time.deltaTime;

        // Calculate the rotation angle using a sinusoidal function to simulate vibration
        float vibrationIntensity = baseVibrationIntensity + Random.Range(-maxRandomIntensityOffset, maxRandomIntensityOffset);
        float rotationAngle = Mathf.Sin(timeCounter * vibrationFrequency) * vibrationIntensity;

        // Apply the rotation to the car in the x-axis
        transform.rotation = Quaternion.Euler(rotationAngle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }
}
