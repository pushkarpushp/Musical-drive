using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMover : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    void Start()
    {
        
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
}
