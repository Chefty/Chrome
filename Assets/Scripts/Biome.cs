using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biome : MonoBehaviour
{
    private List<ColorComponent> Instances;
    private Animator[] m_animators;
    private AudioSource[] m_audioSources;
    private bool isDone;
    private CheckBiomesStatus checkBiomesStatus;

    private void Start()
    {
        Instances = new List<ColorComponent>();
        Instances.AddRange(GetComponentsInChildren<ColorComponent>());
        for (int i = 0; i < Instances.Count; i++)
        {
            Instances[i].ColoredComponent += CheckPlanetElementsStatus;
        }
        // We get animators and audio sources in childrens only for biomes (contain all planet elements)
        m_animators = GetComponentsInChildren<Animator>();
        m_audioSources = GetComponentsInChildren<AudioSource>();
        checkBiomesStatus = GetComponentInParent<CheckBiomesStatus>();
    }

    public void CheckPlanetElementsStatus()
    {
        if (isDone)
            return;

        int AmountFinished = 0;

        for (int i = 0; i < Instances.Count; i++)
        {
            if (Instances[i].isDone)
            {
                AmountFinished++;

                if (AmountFinished >= Instances.Count / 2)  
                {
                    Validate();
                    return;
                } 
            }
        }
    }

    private void Validate()
    {
        for (int i = 0; i < Instances.Count; i++)
        {
            Instances[i].ForceFinish();
        }

        GetComponent<ColorComponent>().ForceFinish();
        StartAnimation();
        StartAudio();
        checkBiomesStatus.BiomeCompleted();
        isDone = true;
    }

    void StartAnimation()
    {
        //Start biome's animation (disabled by default)
        if (m_animators.Length > 0)
        {
            for (int i = 0; i < m_animators.Length; i++)
            {
                m_animators[i].enabled = true;
                // Enable orbite if animated GO is a bird
                if (m_animators[i].name.Contains("Bird"))
                    m_animators[i].GetComponentInParent<Orbite>().enabled = true;
            }
        }
    }

    void StartAudio()
    {
        //Start biome's audio (paused by default)
        if (m_audioSources.Length > 0)
        {
            for (int i = 0; i < m_audioSources.Length; i++)
            {
                m_audioSources[i].Play();
            }
        }
    }
}
