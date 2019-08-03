using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	
	public bool flag;
    // Start is called before the first frame update
    void Start()
    {
		
		flag = false;
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.L)) {
			flag = false;
		}
    }

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {

		
			//	Debug.Log ("hello fdsf Activated Checkpoint" + transform.position);
			//flag = true;

			//flag = true;


		}



	}
}
