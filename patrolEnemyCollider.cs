using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrolEnemyCollider : MonoBehaviour
{
	
	public Animator anim;
	public PlayerMovement player;

	public RandomPatrol enemy;
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

		if(enemy.death)
		{
			if (enemy.enisdead)
			{
				enemy.enishurt = false;
				anim.SetBool("isdead", true);
				//Debug.Log("CHULL");
			}
			else
			{
				anim.SetBool("isdead", false);
			}
		}
		else if(!enemy.death)
		{

			EnemyAnimation();

		}
		//Invoke("punchEnable", 2);
		//punchEnable();
		if (enemy.attacking == true)
		{
			flag = true;

		}
		else
		{
			flag = false;

		}

		if (flag)//working
		{
			//StartCoroutine(punchdelay());
			if(time > 0){
				enpunchcol.enabled = false;
				yomama = false;
				time++;
				if(time == 35)
					time = 0;
			}
			if(time == 0){
				enpunchcol.enabled = true;
				yomama = true;
				time++;

			}

		}
		if(player.ispunching == true && hocche == true){
			enemy.GiveDamage(4);
			print("ki re bhai");
			//	hocche = true;
		}
		if(player.iskicking == true && hocche == true){
			enemy.GiveDamage(4);
			print("ki re bhai");
			//	hocche = true;
		}
		/*else{
			hocche = false;
		}*/

	}

	void OnTriggerEnter2D(Collider2D col)
	{
		//print("Touch hoccheeeee mamaaaaaaaaaaaaaaaaaaaaaa");
		if (col.tag == "Player")
		{
			collidertrigger = true;
			//Debug.Log("Touch hoccheeeee mamaaaaaaaaaaaaaaaaaaaaaa");
			hocche = true;
			//enemy.GiveDamage(5);

			//	}
			punchistriggered = true;
			/*if (defend.enabled == true)///DEFEND CODE
            {
                defend.enabled = false;
                yield return new WaitForSeconds(3);
                if (defend.enabled == false)
                {
                    defend.enabled = true;
                    yield return new WaitForSeconds(3);
                }
            }*/

		}



	}


	void OnTriggerExit2D(Collider2D col)
	{
		punchistriggered = false;
		collidertrigger = false;
		hocche = false;
	}

	IEnumerator punchdelay()
	{


		if (flag == true)
		{
			Debug.Log("ahare");
			enpunchcol.enabled = true;
			enemy.enispunching = true;
			enemy.enisidle = false;
			//yield return new WaitForSeconds(2);

			if (enpunchcol.enabled == true)//genjam eikhanei
			{
				Debug.Log("gebon");
				enpunchcol.enabled = false;
				punchistriggered = false;
				enemy.enispunching = false;
				enemy.enisidle = true;
				yield return new WaitForSeconds(2);
				enpunchcol.enabled = true;
				punchistriggered = true;
				enemy.enispunching = true;
				enemy.enisidle = false;
			}
		}
		//yield return new WaitForSeconds(2);
	}

	void EnemyAnimation()
	{
		if (enemy.eniswalking )
		{
			anim.SetBool("iswalking", true);

		}
		else
		{
			anim.SetBool("iswalking", false);
		}

		if (player.anim.GetBool("isdead"))//player morle ar hatbe na 
		{
			anim.SetBool("iswalking", false);
			enemy.moveSpeed = 0;
		}

		if (enemy.enispunching ) //player morle ar marbena 
		{
			anim.SetBool("ispunching", true);
		}
		else
		{
			anim.SetBool("ispunching", false);
		}

		if (player.anim.GetBool("isdead"))
		{
			anim.SetBool("ispunching", false);
		}


		if (enemy.enishurt)
		{
			anim.SetBool("ishurt", true);
		}
		else
		{
			anim.SetBool("ishurt", false);
		}


		/*if (enemy.enisidle)
        {
            anim.SetBool("isidle", true);
        }
        else
        {
            anim.SetBool("isidle", false);
        }*/
	}
}
