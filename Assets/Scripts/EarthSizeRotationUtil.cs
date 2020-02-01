using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSizeRotationUtil : MonoBehaviour
{
    public float EarthRotSpeed;

    public Transform Earth;

    public float Offset;
    /// <summary>
    /// Min max earth size.
    /// </summary>
    public Vector2 SizeRange;

    public Transform GrabHand;

    /// <summary>
    /// Obj pos at startup.
    /// </summary>
    Vector3 OriginalPosition;

    /// <summary>
    /// Transform pos at last frame.
    /// </summary>
    Vector3 lastpos;

    bool isGrabbing;


    // +------------------------------------------+ 
    //              Custom Spin
    // +------------------------------------------+ 
    Vector3 currentControllerpos;
    Vector3 lastControllerpos;

    public float CustomSpinMaxForce;

    // < 0 == clockwise > 0 == counter-clockwise
    float currentSpinForce;

    void Update()
    {
        //HandleEarthRotation();
    }
}
