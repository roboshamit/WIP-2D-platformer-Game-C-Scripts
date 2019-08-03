using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
	
	public Animator anim;
	public PlayerMovement player;

	public EnemyPatrol enemy;
	//public RandomPatrol penemy;
	public BoxCollider2D enpunchcol;
	public bool flag=false;
	public bool punchistriggered;
	public bool collidertrigger = false;
	public bool hocche = false;
	public int time;
	public bool yomama;
	//public float savedTime = 0;
	//public float delayTime = 2;
	//public int count;
	// Start is called before the first frame update

	void Start()
	{
		anim = GetComponent<Animator>();
		//player = FindObjectOfType<PlayerMovement> ();
		//enemy = FindObjectOfType<EnemyPatrol>();
		//penemy = FindObjectOfType<RandomPatrol>();
		//defend.enabled = true;
		enpunchcol.enabled = true;
		time = 0;
		punchistriggered = false;
		yomama = false;

	}
	void FixedUpdate()
	{
		//StartCoroutine(activation());
	}
	// Update is called once per frame
	void Update()
	{

		//Invoke("punchEnable", 2);
		//punchEnable();
		
		
		/*else{
			hocche = false;
		}*/

	}

	

	

	



}
