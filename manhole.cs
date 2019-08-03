using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manhole : MonoBehaviour
{
    public cameraController cam;
    public GameManager gm;

    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            gm.DecreaseLife(5);
            //Debug.Log("porse");
            cam.isFollowing = false;
			//levelManager.RespawnPlayer ();
        }
    }
}
