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
        
        //Increase rotation speed with time
        if (EarthRotSpeed < 5f)
            EarthRotSpeed += .02f * Time.deltaTime;
    }

    void GenericEarthRotation()
    {
        if (Earth != null)
            Earth.Rotate(new Vector3(1f, 1f, 0f) * (EarthRotSpeed * Time.deltaTime));
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
