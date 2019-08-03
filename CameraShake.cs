using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public float power = 0.7f;
    public float duration = 1.0f;
    public Transform camerash;

    public float slowDownAmount = 1.0f;
    public bool shouldShake = false;

    Vector3 startPosition;
    float initialDuration;

    void Start()
    {

        camerash = Camera.main.transform;
        startPosition = camerash.localPosition;
        initialDuration = duration;

    }
    void Update()
    {

        if (shouldShake)
        {
            if (duration > 0)
            {
                camerash.localPosition = startPosition + Random.insideUnitSphere * power;
                duration -= Time.deltaTime * slowDownAmount;


            }
            else
            {
                shouldShake = false;
                duration = initialDuration;
                camerash.localPosition = startPosition;


            }

        }


    }

}
