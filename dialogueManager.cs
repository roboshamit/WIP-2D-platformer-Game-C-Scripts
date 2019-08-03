using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogueManager : MonoBehaviour
{
	public Text nameText;
	public Text dialogueText;
	private Queue<string> sentences;
	public Animator animator;
    public SceneManagement sm;
    public float dt;

    // Start is called before the first frame update
    void Start()
    {
		sentences = new Queue<string>();
        sm = FindObjectOfType<SceneManagement>();
        sm.ispaused = false;
    }

    // Update is called once per frame
	public void StartDialogue(dialogue dial)
	{
		animator.SetBool("IsOpen", true);
        //sm.ispaused = true;
        StartCoroutine(PauseDelay());
       

		//Debug.Log("Starting Conversation with " + dial.name);
		nameText.text = dial.name;
		sentences.Clear();

		foreach(string sentence in dial.sentences)
		{
			sentences.Enqueue(sentence);

        }

        DisplayNextSentence();


	}

	public void DisplayNextSentence()
	{
		if(sentences.Count == 0)
		{
			EndDialogue();
            sm.ispaused = false;
			return;
		}

		string sentence = sentences.Dequeue();
		//dialogueText.text = sentence;
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach(char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
        StartCoroutine(DialogTimer());

    }

	public void EndDialogue()
	{
		Debug.Log("End of Conversation");
		animator.SetBool("IsOpen", false);
	}

    IEnumerator DialogTimer()//ek sentence sesh hobe.. porer sentence dekhabe
    {
       
        yield return new WaitForSeconds(dt);
        dt = 5f;

    }

    IEnumerator PauseDelay()//initial delay so box can popup
    {

        yield return new WaitForSeconds(dt);
        sm.PauseGame();
        dt = 2f;

    }


}
