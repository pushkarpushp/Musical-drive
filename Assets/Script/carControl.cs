using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carControl : MonoBehaviour
{
    public float leftPositionX = -5f;   // Position on the left
    public float rightPositionX = 5f;   // Position on the right
    public float speed = 5f;            // Speed of movement

    public GameObject steering;
    public GameObject secondaryObject;  // Reference to the secondary object to rotate
    public float leftRotationY = -45f;  // Y rotation for the left turn
    public float rightRotationY = 45f;  // Y rotation for the right turn
    public float rotationDuration = 0.5f; // Duration to rotate back to 0

    private float targetPositionX;      // Target position on the x-axis
    private float targetRotationY;      // Target rotation on the y-axis
    private Coroutine resetRotationCoroutine;

    void Start()
    {
        // Initialize the target position to the current position
        targetPositionX = transform.position.x;
        // Initialize the target rotation to the current rotation
        targetRotationY = secondaryObject.transform.eulerAngles.y;
    }

    void Update()
    {
        // Check for input and set the target position and rotation
        if (Input.GetKeyDown(KeyCode.A) || steering.transform.localEulerAngles.y < -10f)
        {
            targetPositionX = leftPositionX;
            targetRotationY = leftRotationY;
            StartRotation();
        }
        if (Input.GetKeyDown(KeyCode.D) || steering.transform.localRotation.y > 10f)
        {
            targetPositionX = rightPositionX;
            targetRotationY = rightRotationY;
            StartRotation();
        }

        // Smoothly move the object towards the target position
        Vector3 currentPosition = transform.position;
        currentPosition.x = Mathf.Lerp(currentPosition.x, targetPositionX, speed * Time.deltaTime);
        transform.position = currentPosition;
    }

    private void StartRotation()
    {
        // If a reset rotation coroutine is already running, stop it
        if (resetRotationCoroutine != null)
        {
            StopCoroutine(resetRotationCoroutine);
        }

        // Start the rotation coroutine
        resetRotationCoroutine = StartCoroutine(RotateAndReset());
    }

    private IEnumerator RotateAndReset()
    {
        // Smoothly rotate the secondary object towards the target rotation
        Vector3 currentRotation = secondaryObject.transform.eulerAngles;
        float elapsedTime = 0f;

        while (elapsedTime < rotationDuration)
        {
            currentRotation.y = Mathf.LerpAngle(currentRotation.y, targetRotationY, (elapsedTime / rotationDuration));
            secondaryObject.transform.eulerAngles = currentRotation;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure it ends exactly at the target rotation
        currentRotation.y = targetRotationY;
        secondaryObject.transform.eulerAngles = currentRotation;

        // Smoothly rotate back to 0
        elapsedTime = 0f;
        while (elapsedTime < rotationDuration)
        {
            currentRotation.y = Mathf.LerpAngle(currentRotation.y, 0f, (elapsedTime / rotationDuration));
            secondaryObject.transform.eulerAngles = currentRotation;
            elapsedTime += Time.deltaTime;
           
            yield return null;
        }
        //Quaternion newLocalRotation = Quaternion.Euler(-65.941f, 0f, 0f);
        //steering.transform.localRotation = newLocalRotation;
        // Ensure it ends exactly at 0 rotation
        currentRotation.y = 0f;
        secondaryObject.transform.eulerAngles = currentRotation;
    }
}
