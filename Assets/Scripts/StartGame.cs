using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StartGame : MonoBehaviour
{
    public GameObject m_startMenu;
    public GameObject m_environmentPrefab;
    public CustomEarthSpin m_customEarthSpin;
    public GameObject m_brush;
    private Animator m_environmentAnimator;
    private GameObject m_environment;
    
    private void OnTriggerEnter(Collider other)
    {
        m_brush.SetActive(true);
        if (other.GetComponentInParent<OVRCameraRig>())
        {
            if (m_startMenu != null)
                m_startMenu.SetActive(false);
            if (m_environmentPrefab != null)
            {
                GameObject tmpGO;
                Destroy(m_environment);
                tmpGO = Instantiate(m_environmentPrefab) as GameObject;
                m_environment = tmpGO;
                m_environmentAnimator = m_environment.GetComponent<Animator>();
                if (m_customEarthSpin != null)
                    m_customEarthSpin.Earth = m_environment.transform.GetChild(0).transform;
            }
            if (m_environmentAnimator != null)
                m_environmentAnimator.SetTrigger("GameStart");
        }
    }
}
