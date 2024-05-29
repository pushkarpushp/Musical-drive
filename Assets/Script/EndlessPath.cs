using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessPath : MonoBehaviour
{
    public GameObject RoadSection;
    public float SpawnDistance;

    private void OnTriggerEnter(Collider other)
    {
       // if (other.gameObject.CompareTag("Trigger"))
        //{
       //     Instantiate(RoadSection, new Vector3(0, 0, SpawnDistance), Quaternion.identity);
       // }
    }
}
