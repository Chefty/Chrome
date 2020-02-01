using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchSound : MonoBehaviour
{
    AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.Play();
    }

    private void Update()
    {
        print(source.isPlaying);
    }
}
