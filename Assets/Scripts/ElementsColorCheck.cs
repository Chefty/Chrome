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
        //Setup base list of materials from every element on our planet
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
        if (m_elementsMaterials != null && m_elementsMaterials.Count != 0)
        {
            //For each element's material in our base list, we will check if it's a color match with the current brush material
            for (int i = 0; i < m_elementsMaterials.Count; i++)
            {
                StartCoroutine(CheckElementsColorMatchCoroutine(elementName, paintingToolMaterial, elementMeshRendererRef));
            }
        }
    }

    private IEnumerator CheckElementsColorMatchCoroutine(string elementName, Material paintingToolMaterial, MeshRenderer elementMeshRendererRef)
    {
        Material tmpElementMaterial;
        //Check over all possible color code we defined as element's expected color
        //If it's a match we set the designed element to rightful default matching material
        if (paintingToolMaterial.name == "Grey" && (elementName.Contains("Building") || elementName.Contains("Mountain")))
        {
            print("Building !");
            tmpElementMaterial = FindElementDefaultMaterial(elementName);
            if (tmpElementMaterial != null)
                elementMeshRendererRef.sharedMaterial = tmpElementMaterial;
        }
        else if (paintingToolMaterial.name == "Green" &&
                (elementName.Contains("Tree") || elementName.Contains("Bird") || elementName.Contains("Hill")))
        {
            tmpElementMaterial = FindElementDefaultMaterial(elementName);
            if (tmpElementMaterial != null)
                elementMeshRendererRef.sharedMaterial = tmpElementMaterial;
        }
        else if (paintingToolMaterial.name == "Brown" && (elementName.Contains("Horse")))
        {
            tmpElementMaterial = FindElementDefaultMaterial(elementName);
            if (tmpElementMaterial != null)
                elementMeshRendererRef.sharedMaterial = tmpElementMaterial;
        }
        else if (paintingToolMaterial.name == "Red" &&
                (elementName.Contains("Bird") || elementName.Contains("Flower")))
        {
            tmpElementMaterial = FindElementDefaultMaterial(elementName);
            if (tmpElementMaterial != null)
                elementMeshRendererRef.sharedMaterial = tmpElementMaterial;
        }
        else if (paintingToolMaterial.name == "Purple" && (elementName.Contains("Flower")))
        {
            tmpElementMaterial = FindElementDefaultMaterial(elementName);
            if (tmpElementMaterial != null)
                elementMeshRendererRef.sharedMaterial = tmpElementMaterial;
        }
        else if (paintingToolMaterial.name == "Yellow" &&
                (elementName.Contains("Flower") || elementName.Contains("Desert")))
        {
            tmpElementMaterial = FindElementDefaultMaterial(elementName);
            if (tmpElementMaterial != null)
                elementMeshRendererRef.sharedMaterial = tmpElementMaterial;
        }
        else if (paintingToolMaterial.name == "Blue" &&
                (elementName.Contains("Flower") || elementName.Contains("Ocean") || elementName.Contains("Snow")))
        {
            tmpElementMaterial = FindElementDefaultMaterial(elementName);
            if (tmpElementMaterial != null)
                elementMeshRendererRef.sharedMaterial = tmpElementMaterial;
        }
        else //Brush's color and element's color are not matching, so we set it to the current color on the brush
        {
            elementMeshRendererRef.sharedMaterial = paintingToolMaterial;
        }
        yield return null;
    }

    private Material FindElementDefaultMaterial(string elementName)
    {
        foreach (var elementMaterial in m_elementsMaterials)
        {
            if (elementName.Contains(elementMaterial.Key))
            {
                return elementMaterial.Value;
            }
        }
        return null;
    }
}
