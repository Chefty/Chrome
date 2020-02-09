using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBiomesStatus : MonoBehaviour
{
    public bool m_isDone;

    private GameObject m_credits;
    private List<Biome> m_biomesInstances;
    private Animator m_environmentAnimator;
    private int m_numberOfBiomesCompleted;

    // Start is called before the first frame update
    void Start()
    {
        m_isDone = false;
        m_biomesInstances = new List<Biome>();
        m_biomesInstances.AddRange(gameObject.transform.GetChild(0).GetComponentsInChildren<Biome>());
        m_environmentAnimator = GetComponent<Animator>();
        m_credits = GameObject.Find("Menu").transform.GetChild(1).gameObject;
    }

    //Debug purpose only
    //private void Update()
    //{
    //    if (m_isDone)
    //        StartCoroutine(EndGameCO());
    //}

    public void BiomeCompleted()
    {
        m_numberOfBiomesCompleted++;
        if (m_numberOfBiomesCompleted == m_biomesInstances.Count)
            StartCoroutine(EndGameCO());
    }

    private IEnumerator EndGameCO()
    {
        m_isDone = true;
        m_environmentAnimator.SetTrigger("GameComplete");
        yield return new WaitForSeconds(3f);
        m_credits.SetActive(true);
    }
}
