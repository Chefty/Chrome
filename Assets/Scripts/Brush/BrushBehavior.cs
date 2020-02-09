using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrushBehavior : MonoBehaviour
{
    public AudioSource source;
    public ParticleSystem particles;
    public Material BurstMat;

    //private void Update()
    //{
    //    particles.Play();
    //}
    private void Start()
    {
        Application.targetFrameRate = -1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ColorPicker"))
        {
            PlayFeedback(other);
        }
    }

    void PlayFeedback(Collider other)
    {
        BurstMat.SetColor("_TintColor", other.GetComponent<MeshRenderer>().sharedMaterial.color);
        particles.Play();
    }
}
