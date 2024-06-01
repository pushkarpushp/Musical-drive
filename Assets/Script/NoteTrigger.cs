using NBitcoin.Protocol;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteTrigger : MonoBehaviour
{
    public AudioSource audioSource;
    public ParticleSystem particleSystem;
    public MeshRenderer meshRenderer;
    public FreWorkMeter freWorkMeter;
    private GameObject firework;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        firework = GameObject.FindGameObjectWithTag("Meter");
        freWorkMeter = firework.GetComponent<FreWorkMeter>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Trigger"))
        {
            NotePoint();
        }
    }
    public void NotePoint()
    {
       audioSource.Play();
        freWorkMeter.meter += 1f; 
        particleSystem.Play();
        meshRenderer.enabled = false;
        Invoke("DestroyNote", 2f);
    }

    private void DestroyNote()
    {
        Destroy(gameObject);
    }
}
