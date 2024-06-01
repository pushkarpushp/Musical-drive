using Org.BouncyCastle.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreWorkMeter : MonoBehaviour
{
    public GameObject gameObject1;
    public GameObject gameObject2;
    public GameObject gameObject3;
    public GameObject gameObject4;
    public GameObject gameObject5;
    public GameObject gameObject6;
    public GameObject gameObject7;
    public ParticleSystem particleSystem;
    public AudioSource audioSource;

    public float meter = 0f;
    // Start is called before the first frame update
    void Start()
    {
        gameObject1.SetActive(false);
        gameObject2.SetActive(false);
        gameObject3.SetActive(false);
        gameObject4.SetActive(false);
        gameObject5.SetActive(false);
        gameObject6.SetActive(false);
        gameObject7.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (meter == 0)
        {
            gameObject1.SetActive(false);
            gameObject2.SetActive(false);
            gameObject3.SetActive(false);
            gameObject4.SetActive(false);
            gameObject5.SetActive(false);
            gameObject6.SetActive(false);
            gameObject7.SetActive(false);
        }
        if (meter == 1) 
        {
            gameObject1.SetActive(true);
        }
        if (meter == 2)
        {
            gameObject2.SetActive(true);
        }
        if (meter == 3)
        {
            gameObject3.SetActive(true);
        }
        if (meter == 4)
        {
            gameObject4.SetActive(true);
        }
        if (meter == 5)
        {
            gameObject5.SetActive(true);
        }
        if (meter == 6)
        {
            gameObject6.SetActive(true);
        }
        if (meter == 7)
        {
            gameObject7.SetActive(true);
            particleSystem.Play();
            audioSource.Play();
            meter = 0;
        }
    }
}
