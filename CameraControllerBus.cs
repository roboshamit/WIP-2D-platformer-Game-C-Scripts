using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraControllerBus : MonoBehaviour
{
   
    public GameObject bus;
    public bool isFollowing;
    public float xOffset;
    public float yOffset;
    string sceneName;

    // Use this for initialization
    void Start()
    {
        
        isFollowing = true;
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

    }

    // Update is called once per frame
    void Update()
    {

        if (sceneName == "Bus")
        {
            transform.position = new Vector3(bus.transform.position.x + xOffset, bus.transform.position.y + yOffset, transform.position.z);

        }
        //bus scene e thakle camera bus k target korbe

    }
}
