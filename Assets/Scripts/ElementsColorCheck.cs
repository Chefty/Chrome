using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ElementsColorCheck : MonoBehaviour
{
    public static ElementsColorCheck instance;
    public List<GameObject> elements;
    
    public Dictionary<string, Material> m_elementsMaterials;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_elementsMaterials = new Dictionary<string, Material>();
        if (elements != null && elements.Count != 0)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                m_elementsMaterials.Add(elements[i].name, elements[i].GetComponentInChildren<MeshRenderer>().sharedMaterial);
            }
        }
    }


    public void CheckElementsColorMatch(string elementName, Material paintingToolMaterial, MeshRenderer elementMeshRendererRef)
    {
        print("101");
        if (m_elementsMaterials != null && m_elementsMaterials.Count != 0)
        {
            for (int i = 0; i < m_elementsMaterials.Count; i++)
            {
                StartCoroutine(CheckElementsColorMatchCoroutine(elementName, paintingToolMaterial, elementMeshRendererRef));
            }
        }
    }

    private IEnumerator CheckElementsColorMatchCoroutine(string elementName, Material paintingToolMaterial, MeshRenderer elementMeshRendererRef)
    {
        print("102");
        //Switch over all possible color code we defined as elements expected color
        switch (paintingToolMaterial.name)
        {
            case "Grey":
                print("103: " + elementName + " - " + elementName.Contains("Building"));
                if (elementName.Contains("Building") || elementName.Contains("Mountains"))
                {
                    print("104");
                    Material tmpElementMaterial = FindElementDefaultMaterial(elementName);
                    //Get the default matching material based on designed element name
                    if (tmpElementMaterial != null)
                    {
                        print("105");
                        elementMeshRendererRef.sharedMaterial = tmpElementMaterial;
                    }
                }
                else
                {
                    print("107");
                    elementMeshRendererRef.sharedMaterial = paintingToolMaterial;
                }
                break;
            case "Green":
                if (elementName.Contains("Tree"))
                {
                }
                else
                {
                    elementMeshRendererRef.sharedMaterial = paintingToolMaterial;
                }
                break;
            default:
                elementMeshRendererRef.sharedMaterial = paintingToolMaterial;
                break;
        }
        yield return null;
    }

    private Material FindElementDefaultMaterial(string elementName)
    {
        foreach (var elementMaterial in m_elementsMaterials)
        {
            print("105.5: " + elementMaterial.Key + " - " + elementName);
            if (elementName.Contains(elementMaterial.Key))
            {
                print("106.5");
                return elementMaterial.Value;
            }
        }
        return null;
    }
}
