using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnroad : MonoBehaviour
{
    public GameObject RoadSection;
    public GameObject attachPoint;
    public Transform parent;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger"))
        {
            spawnroad();
        }
    }
    public void spawnroad()
    {
        // Instantiate the RoadSection without setting the parent initially
        GameObject road = Instantiate(RoadSection);

        // Set the parent after instantiation
        road.transform.SetParent(parent, false);

        // Manually set the position to the attach point's position
        road.transform.position = attachPoint.transform.position;
    }
}
