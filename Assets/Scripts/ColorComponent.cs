using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ColorComponent : MonoBehaviour
{
    private MeshRenderer m_meshRenderer;
    private SkinnedMeshRenderer m_SkinedmeshRenderer;
    public Animator m_animator;
    private AudioSource m_audioSource;
    private Collider m_collider;
    private ElementsColorCheck m_elementsColorCheck;

    public bool isDone;

    public void Finish()
    {
        isDone = true;

        StartAnimation();
    }

    public void ForceFinish()
    {
        // TODO SAFE ?
        if (isDone)
        {
            return;
        }

        isDone = true;

        var mat = ElementsColorCheck.instance.FindElementDefaultMaterial(gameObject.transform.parent.name);
        print(mat == null ? "OK" : "KO");

        if (gameObject.transform.parent.name == "World")
        {
            mat = ElementsColorCheck.instance.FindElementDefaultMaterial(gameObject.name);
        }


        if (mat == null)
        {
            return;
        }
        if (m_meshRenderer != null)
        {
            m_meshRenderer.sharedMaterial = mat;
        }
        else
        {
            m_SkinedmeshRenderer.sharedMaterial = mat;
        }

        StartAnimation();
    }

    void StartAnimation()
    {
        if (m_animator != null)
        {
            m_animator.SetBool("ShouldAnimate", true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_SkinedmeshRenderer = GetComponent<SkinnedMeshRenderer>();
        m_meshRenderer = GetComponent<MeshRenderer>();
        if (m_animator == null)
        {
            m_animator = GetComponent<Animator>();
        }
        m_audioSource = GetComponent<AudioSource>();
        m_collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Brush is interacting with planet elements
        if (other.CompareTag("PaintingTool") && gameObject.CompareTag("PlanetElements"))
        {
            Material paintingToolMaterial = other.GetComponent<MeshRenderer>().sharedMaterial;
            if (m_meshRenderer != null &&
                m_meshRenderer.sharedMaterial != paintingToolMaterial)
            {
                if (gameObject.name.Contains("Ocean") || gameObject.name.Contains("Terre"))
                {
                    ElementsColorCheck.instance.CheckElementsColorMatch(
                        gameObject.name,
                        paintingToolMaterial,
                        m_meshRenderer);
                }
                else
                {
                    ElementsColorCheck.instance.CheckElementsColorMatch(
                        gameObject.transform.parent.name,
                        paintingToolMaterial,
                        m_meshRenderer);
                }
            }
            else if (m_SkinedmeshRenderer != null &&
                m_SkinedmeshRenderer.sharedMaterial != paintingToolMaterial)
            {
                ElementsColorCheck.instance.smr_CheckElementsColorMatch(
                    gameObject.transform.parent.name,
                    paintingToolMaterial,
                    m_SkinedmeshRenderer);

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
