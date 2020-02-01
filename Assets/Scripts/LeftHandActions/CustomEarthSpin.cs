using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomEarthSpin : MonoBehaviour
{
    public Transform Earth;
    public float EarthRotSpeed;
    /// <summary>
    /// Time where the custom rotation will be applied after user quit doing it.
    /// </summary>
    public float CustomRotateTime;

    public float CurrentRotateTime;

    public bool IsCustomRotating;

    void Update()
    {
        if (IsCustomRotating)
        {
            CurrentRotateTime -= Time.deltaTime;
        }
        else
        {

        }

        GenericEarthRotation();
    }

    void GenericEarthRotation()
    {
        Earth.Rotate(Vector3.up * (EarthRotSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        IsCustomRotating = true;
        CurrentRotateTime = CustomRotateTime;
    }

    private void OnTriggerStay(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {
        CurrentRotateTime = CustomRotateTime;
        IsCustomRotating = true;

    }
}
