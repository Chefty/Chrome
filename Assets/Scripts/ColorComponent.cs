using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ColorComponent : MonoBehaviour
{
    private Renderer m_renderer;
    public Animator m_animator;
    public AudioSource[] m_audioSources;
    private Collider m_collider;
    public delegate void ColoredComponenEventHandler();
    public event ColoredComponenEventHandler ColoredComponent;

    public bool isDone;

    // Start is called before the first frame update
    void Start()
    {
        m_renderer = GetComponent<Renderer>();
        if (m_animator == null)
        {
            m_animator = GetComponent<Animator>();
        }
        m_audioSources = GetComponentsInChildren<AudioSource>();
        m_collider = GetComponent<Collider>();
        // Mean animator has to be on parent's GO
        if (gameObject.transform.parent != null)
            m_animator = gameObject.transform.parent.GetComponent<Animator>();
    }
    public void Colored()
    {
        isDone = true;
        OnComponentColored();
    }

    protected virtual void OnComponentColored()
    {
        ColoredComponent?.Invoke();
    }

    public void ForceFinish()
    {
        StartAnimation();
        StartAudio();

        if (isDone)
            return;
        else
            isDone = true;

        if (m_renderer != null)
        {
            Material defaultElementMaterial;
            //Trying to get default material of current GameObject
            if (gameObject.transform.parent.name == "World")
            {
                defaultElementMaterial = ElementsColorCheck.instance.FindElementDefaultMaterial(gameObject.name);
            }
            else
            {
                defaultElementMaterial = ElementsColorCheck.instance.FindElementDefaultMaterial(gameObject.transform.parent.name);
            }
            //Trying to apply default material on current GameObject
            if (defaultElementMaterial != null)
            {
                m_renderer.sharedMaterial = defaultElementMaterial;
            }
            else
            {
                print("Couldn't set default material on planet element " + gameObject.name);
                return;
            }
        }
        else
        {
            print("Renderer compoenent is missing on planet element " + gameObject.name);
            return;
        }
    }

    void StartAnimation()
    {
        //Start current GameObject animation (disabled by default)
        if (m_animator != null)
        {
            m_animator.enabled = true;
        }
        //If current gameObject is a bird we also start his orbit
        if (gameObject.transform.parent.name.Contains("Bird"))
        {
            gameObject.transform.parent.parent.GetComponent<Orbite>().enabled = true;
        }
    }

    void StartAudio()
    {
        if (m_audioSources.Length > 0)
        {
            for (int i = 0; i < m_audioSources.Length; i++)
            {
                m_audioSources[i].Play();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Brush is interacting with planet elements
        if (other.CompareTag("PaintingTool") && gameObject.CompareTag("PlanetElements"))
        {
            if (isDone)
                return;
            Material paintingToolMaterial = other.GetComponent<MeshRenderer>().sharedMaterial;
            if (m_renderer != null &&
                m_renderer.sharedMaterial != paintingToolMaterial)
            {
                if (gameObject.name.Contains("Ocean") || gameObject.name.Contains("Biome"))
                {
                    ElementsColorCheck.instance.CheckElementsColorMatch(
                        gameObject.name,
                        paintingToolMaterial,
                        m_renderer);
                }
                else
                {
                    ElementsColorCheck.instance.CheckElementsColorMatch(
                        gameObject.transform.parent.name,
                        paintingToolMaterial,
                        m_renderer);
                }
            }
        }
        if (other.CompareTag("ColorPicker")) //Getting colors from meteor palette with the brush
        {
            Material paintingMaterial = other.GetComponent<MeshRenderer>().sharedMaterial;
            if (m_renderer.sharedMaterial != paintingMaterial)
            {
                m_renderer.sharedMaterial = paintingMaterial;
            }
        }
    }
}
