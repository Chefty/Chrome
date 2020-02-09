using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSetuper : MonoBehaviour
{
    public Transform PencilCanvas;

    public Vector3 offsetFromPlayer;
    public Vector3 PlayerPosition;

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.One))
        {
            SetupScene();
        }
    }

    void SetupScene()
    {

    }


}
