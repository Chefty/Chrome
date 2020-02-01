using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabSize : MonoBehaviour
{
    public Transform Earth;

    /// <summary>
    /// Custom Collider offset relative to the earth's ground.
    /// </summary>
    public float Offset;

    /// <summary>
    /// Min max earth size.
    /// </summary>
    public Vector2 SizeRange;

    /// <summary>
    /// The left hand.
    /// </summary>
    public Transform GrabHand;

    /// <summary>
    /// Obj pos at startup.
    /// </summary>
    Vector3 OriginalPosition;

    /// <summary>
    /// Transform pos at last frame.
    /// </summary>
    Vector3 lastpos;

    void Start()
    {
        OriginalPosition = transform.position;
    }

    private void Update()
    {
        HandleGrab();
    }

    void HandleGrab()
    {
        if (transform.position != OriginalPosition)
        {
            RescaleEarth();

            if (transform.position != lastpos)
            {
                transform.position = OriginalPosition;
            }

            lastpos = transform.position;
        }
    }

    void RescaleEarth()
    {
        float RescaleMagnitude = (GrabHand.position - OriginalPosition).magnitude - Offset;
        Earth.localScale = Vector3.one * (Mathf.Clamp(RescaleMagnitude, SizeRange.x, SizeRange.y));
        transform.localScale = Earth.localScale + (Vector3.one * Offset);
    }
}
