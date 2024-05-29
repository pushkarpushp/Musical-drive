using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMover : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public GameObject RoadSection;
    public GameObject attachPoint;
    public Transform parent;
    void Start()
    {
        Invoke("spawnroad", 20f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0,0, speed) * Time.deltaTime;
        Invoke("DestroyPlatform", 80f);
    }

    private void DestroyPlatform()
    {
            Destroy(gameObject);
       
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
