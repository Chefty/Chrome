using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderActivator : MonoBehaviour
{
    public int AmountPerFrame;

    void Start()
    {
        StartCoroutine(ActivateCollider());
    }

    IEnumerator ActivateCollider()
    {
        Physics.autoSimulation = false;
        Collider[] cols = GetComponentsInChildren<Collider>();

        for (int i = 0; i < cols.Length; i++)
        {
            cols[i].enabled = true;

            if (i % AmountPerFrame == 0)
            {
                yield return new WaitForEndOfFrame();
            }
        }

        Physics.autoSimulation = true;
    }
}
