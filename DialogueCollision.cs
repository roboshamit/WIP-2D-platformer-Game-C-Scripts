using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCollision : MonoBehaviour
{
    public bool dtriggered;
    // Start is called before the first frame update
	void Start()
    {
        dtriggered = false;

	}

    // Update is called once per frame
    void Update()
    {
        
    }
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player" && !dtriggered) {
			GetComponent<dialogueTrigger>().TriggerDialogue();
            dtriggered = true;
		}

	}
}
