using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMover : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    //public GameObject RoadSection;
    //public GameObject attachPoint;
    //public Transform parent;
    /*void Start()
    {
        Invoke("spawnroad", 20f);
    }*/

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0,0, speed) * Time.deltaTime;
        Invoke("DestroyPlatform", 80f);
    }
    private void OnTriggerEnter(Collider other)
    {
        // if (other.gameObject.CompareTag("Trigger"))
        //{
        //     Instantiate(RoadSection, new Vector3(0, 0, SpawnDistance), Quaternion.identity);
        // }
    }
    private void DestroyPlatform()
    {
            Destroy(gameObject);
       
    }
   
}
