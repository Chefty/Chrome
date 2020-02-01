using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorComponent : MonoBehaviour
{
    private MeshRenderer m_meshRenderer;
    private Material m_material;
    private Animator m_animator;
    private AudioSource m_audioSource;
    private Collider m_collider;


    // Start is called before the first frame update
    void Start()
    {
        m_meshRenderer = GetComponent<MeshRenderer>();
        m_material = m_meshRenderer.sharedMaterial;
        m_animator = GetComponent<Animator>();
        m_audioSource = GetComponent<AudioSource>();
        m_collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PaintingTool") && gameObject.CompareTag("PlanetElements"))
        {
            Material paintingToolMaterial = other.GetComponent<MeshRenderer>().sharedMaterial;
            if (m_material != paintingToolMaterial)
            {
                print("Set painting material");
                m_meshRenderer.sharedMaterial = paintingToolMaterial;
                m_material = paintingToolMaterial;
            }
        }
        if (other.CompareTag("ColorPicker"))
        {
            Material paintingMaterial = other.GetComponent<MeshRenderer>().sharedMaterial;
            if (m_material != paintingMaterial)
            {
                print("Get painting material");
                m_meshRenderer.sharedMaterial = paintingMaterial;
                m_material = paintingMaterial;
            }
        }
    }
}
