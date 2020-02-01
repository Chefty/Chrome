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
        if (other.CompareTag("PaintingTool") && gameObject.CompareTag("PlanetElements"))
        {
            Material paintingToolMaterial = other.GetComponent<MeshRenderer>().sharedMaterial;
            if (m_meshRenderer.sharedMaterial != paintingToolMaterial)
            {
                print("Set painting material");
                ElementsColorCheck.instance.CheckElementsColorMatch(gameObject.transform.parent.name, paintingToolMaterial, m_meshRenderer);
            }
        }
        if (other.CompareTag("ColorPicker"))
        {
            Material paintingMaterial = other.GetComponent<MeshRenderer>().sharedMaterial;
            if (m_meshRenderer.sharedMaterial != paintingMaterial)
            {
                print("Get painting material");
                m_meshRenderer.sharedMaterial = paintingMaterial;
            }
        }
    }
}
