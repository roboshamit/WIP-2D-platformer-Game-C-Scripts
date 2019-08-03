using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cameraController : MonoBehaviour
{
    public PlayerMovement player;
    //public GameObject bus;
    public bool isFollowing;
    public float xOffset;
    public float yOffset;
    string sceneName;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        isFollowing = true;
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowing)
        {
            transform.position = new Vector3(player.transform.position.x + xOffset, player.transform.position.y + yOffset, transform.position.z);

        }

    }

}
