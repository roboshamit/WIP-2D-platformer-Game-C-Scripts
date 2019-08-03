using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RickshawMovement : MonoBehaviour
{
    public float speedRotate;
    public float vehiclespeed;
    public GameObject chaka1;
    public GameObject chaka2;
    public GameObject chaka3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        chaka1.transform.Rotate(Vector3.forward * speedRotate * Time.deltaTime);
        chaka2.transform.Rotate(Vector3.forward * speedRotate * Time.deltaTime);
        chaka3.transform.Rotate(Vector3.forward * speedRotate * Time.deltaTime);
        transform.position += transform.right * vehiclespeed * Time.deltaTime;
    }
}
