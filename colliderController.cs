using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colliderController : MonoBehaviour
{
    public BoxCollider2D punchcol;
	public BoxCollider2D kickcol;
	public bool punch = false;
	//public BoxCollider2D crouch;

    public PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerMovement>();
		kickcol.enabled = false;
        punchcol.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (player.ispunching== true)
        {
            punchcol.enabled = true;
            //print("Punch hocche");
			punch = true;

        }
        else
        {
            punchcol.enabled = false;
			punch = false;
        }

		if (player.iskicking== true)
		{
			kickcol.enabled = true;
			//print("Kick hocche");

		}
		else
		{
			kickcol.enabled = false;
		}
    }
}
