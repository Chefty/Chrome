using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ColorComponent : MonoBehaviour
{
    private Renderer m_renderer;
    private Collider m_collider;
    public delegate void ColoredComponenEventHandler();
    public event ColoredComponenEventHandler ColoredComponent;

    public bool isDone;

    // Start is called before the first frame update
    void Start()
    {
        m_renderer = GetComponent<Renderer>();
        m_collider = GetComponent<Collider>();
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
