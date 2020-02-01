using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ColorComponent : MonoBehaviour
{
    private MeshRenderer m_meshRenderer;
    private Animator m_animator;
    private AudioSource m_audioSource;
    private Collider m_collider;
    private ElementsColorCheck m_elementsColorCheck;

    // Start is called before the first frame update
    void Start()
    {
        m_meshRenderer = GetComponent<MeshRenderer>();
        m_animator = GetComponent<Animator>();
        m_audioSource = GetComponent<AudioSource>();
        m_collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Brush is interacting with planet elements
        if (other.CompareTag("PaintingTool") && gameObject.CompareTag("PlanetElements"))
        {
            Material paintingToolMaterial = other.GetComponent<MeshRenderer>().sharedMaterial;
            if (m_meshRenderer.sharedMaterial != paintingToolMaterial)
            {
                ElementsColorCheck.instance.CheckElementsColorMatch(gameObject.transform.parent.name, paintingToolMaterial, m_meshRenderer);
            }
        }
        if (other.CompareTag("ColorPicker")) //Getting colors from meteor palette with the brush
        {
            Material paintingMaterial = other.GetComponent<MeshRenderer>().sharedMaterial;
            if (m_meshRenderer.sharedMaterial != paintingMaterial)
            {
                m_meshRenderer.sharedMaterial = paintingMaterial;
            }
        }
    }
}
