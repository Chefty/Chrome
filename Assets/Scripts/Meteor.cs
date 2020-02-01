using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{   
    public float speed = 5f;
    float CurrentTime = 0f;

    public float interval = 1f;
    // Start is called before the first frame update

    Vector3 originalPosition;
    void Start()
    {
        originalPosition = transform.localPosition;

    }

    // Update is called once per frame
    void Update()
    {
        CurrentTime += Time.deltaTime;
        transform.localPosition = originalPosition + (new Vector3(0,0,Mathf.Sin(CurrentTime * speed))* interval);
    }

}
