using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueTrigger : MonoBehaviour
{
	public dialogue dial;
    
	public void TriggerDialogue()
	{
		FindObjectOfType<dialogueManager>().StartDialogue(dial);
	}
}
