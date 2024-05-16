using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayer : MonoBehaviour
{
    [SerializeField] Transform resetTransform;

    [SerializeField] GameObject player;

    [SerializeField] Camera playerHead;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("ResetPosition", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetPosition()

    {

        var rotationAngleY = playerHead.transform.rotation.eulerAngles.y - resetTransform.transform.rotation.eulerAngles.y;

        player.transform.Rotate(0, -rotationAngleY, 0);



        var distanceDiff = resetTransform.position - playerHead.transform.position;

        player.transform.position += distanceDiff;

    }
}
