using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Biome : MonoBehaviour
{
    public List<ColorComponent> Instances;
    bool isDone;

    private void Start()
    {
        Instances = new List<ColorComponent>();
        Instances.AddRange(GetComponentsInChildren<ColorComponent>());
    }

    private void Update()
    {
        if (isDone)
            return;

        int AmountFinished = 0;

        for (int i = 0; i < Instances.Count; i++)
        {
            if (Instances[i].isDone)
            {
                AmountFinished++;

                if (AmountFinished >= Instances.Count / 2)
                {
                    Validate();
                }
            }
        }
    }

    private void Validate()
    {
        if (isDone)
            return;

        // Activate animations
        isDone = true;

        for (int i = 0; i < Instances.Count; i++)
        {
            print(Instances[i].name);
            Instances[i].ForceFinish();
        }

        GetComponent<ColorComponent>().ForceFinish();
    }
}
