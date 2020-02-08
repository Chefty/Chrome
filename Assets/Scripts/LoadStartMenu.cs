using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadStartMenu : MonoBehaviour
{
    public GameObject m_startMenu;
    public GameObject m_creditsMenu;
    public GameObject m_brush;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<OVRCameraRig>())
        {
            m_brush.SetActive(false);
            m_startMenu.SetActive(true);
            m_creditsMenu.SetActive(false);
        }
    }
}
