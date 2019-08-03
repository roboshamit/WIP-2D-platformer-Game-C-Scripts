using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vehicleMovement : MonoBehaviour
{
    public float vehiclespeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * vehiclespeed * Time.deltaTime;
    }
}
