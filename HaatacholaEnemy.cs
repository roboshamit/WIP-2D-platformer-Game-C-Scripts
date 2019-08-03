using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaatacholaEnemy : MonoBehaviour
{
	Rigidbody2D rb;

	public Animator anim;
	public float engageDistance;
	public float attackDistance;
	public float moveSpeed;
	public bool facingLeft;
	public bool pattacking;
	public bool pengaging;
	public bool penishurt;
	public bool penisdead;
	public bool penispunching;
	public bool peniswalking;
	public bool penisidle;
	public bool pdeath;
	public int penemyHealth;

	public static PlayerMovement player;

	//public float attackDamage;
	public float hurtimpactx;
	public float hurtimpacty;
	public bool flag = false;
	public BoxCollider2D penpunchcol;
	public int time;
	public bool pyomama;
	public bool ppunchistriggered;
	public bool pcollidertrigger = false;
	public bool phocche = false;
	public GameManager gm;
	public bool ptriggerbool=false;
	public GameObject boomeffect;

	public bool moveRight;
	//private Animator anim;
	public Transform wallCheck;
	public float wallCheckRadius;
	public LayerMask whatIsWall;
	private bool hittingWall;
	private bool notAtEdge;
	public Transform edgeCheck;

	public Transform target;
	GameObject playera;

	void Start()
	{
		gm = FindObjectOfType<GameManager>();
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();

		penispunching = false;
		penisidle = true;
		peniswalking = false;
		penishurt = false;
		penpunchcol.enabled = true;
		time = 0;
		ppunchistriggered = false;
		pyomama = false;
		player = FindObjectOfType<PlayerMovement>();

		//player = Transform.FindWithTag("Player").GetComponent<PlayerMovement>();
		//player = Transform.FindWithTag("Player").transform;
		//player = Transform.Find("Player").gameObject;
		//player = PlayerMovement.Transform.Find("Player").gameObject;


		playera = GameObject.FindWithTag("playerp");
		target = playera.transform;
	}



	// Update is called once per frame
	void Update()
	{

		///SALMA ENEMYCOLLIDER 
		if (player.ispunching == true && phocche == true)
		{

			GiveDamage(2);       
			//	hocche = true;
		}
		if (player.iskicking == true && phocche == true)
		{
			GiveDamage(3);
			//	hocche = true;
		}

		if (pdeath)
		{
			if (penisdead)
			{
				penishurt = false;
				anim.SetBool("isdead", true);

				//Debug.Log("CHULL");
			}
			else
			{
				anim.SetBool("isdead", false);
			}
		}
		else if (!pdeath)
		{

			EnemyAnimation();

		}

		if (penemyHealth <= 0)
		{

			///age death anim then delay then destroy
			/////genjam- destroy na korle enemy kaj kortei thake so
			///1. delay te destroy kora jabena 
			///2. delay er age shob kaj off korte hobe deathanim baade :3
			FindObjectOfType<SoundManager>().Play("Enemy Death");
			penishurt = false;
			penisdead = true;
			pdeath = true;
			peniswalking = false;
			penisidle = false;
			penispunching = false;
			pengaging = false;
			pattacking = false;
			anim.SetBool("iswalking", false);
			anim.SetBool("ishurt", false);
			anim.SetBool("isdead", true);

			StartCoroutine("Death");

			Invoke("SetHurtBool", 0.3f);
			//Invoke("DeathDelay", 5f);
			//Destroy(gameObject, 2f);
			//Destroy(gameObject);

			//Instantiate (deathEffect, transform.position, transform.rotation);
			//ScoreManager.AddPoints (pointsOnDeath);

		}
		else if ((Vector3.Distance(target.position, this.transform.position) < engageDistance ) )
		{

			pengaging = true;
			penisidle = false;
			peniswalking = true;

			//get the direction of the target
			Vector3 direction = target.position - this.transform.position;
			if (Mathf.Sign(direction.x) == 1 && facingLeft)
				flip();
			else if (Mathf.Sign(direction.x) == -1 && !facingLeft)
				flip();

			if (direction.magnitude >= attackDistance )///FAR
			{
				pattacking = false;
				//eniswalking = true;
				//Debug.DrawLine(target.position, this.transform.position, Color.yellow);

				if (facingLeft)
				{
					this.transform.Translate(Vector3.left * (Time.deltaTime * moveSpeed));
				}
				else if (!facingLeft)
				{
					this.transform.Translate(Vector3.right * (Time.deltaTime * moveSpeed));
				}
			}
			else
			{
				//eniswalking = true;
			}

			if (direction.magnitude < attackDistance)///NEAR should punch
			{

				pattacking = true;
				phocche = true;
				ptriggerbool = true;
				// enispunching = true;
				peniswalking = false;
				anim.SetBool("iswalking", false);
				anim.SetBool("ispunching", true);

				//Debug.DrawLine(target.position, this.transform.position, Color.red);
				//anim.SetBool("isAttacking", true);
			}
			else
			{

				pattacking = false;
				phocche = false;
				ptriggerbool = false;
				//	enispunching = false;
				anim.SetBool("ispunching", false);
				//eniswalking = false;
			}
		}
		else if (Vector3.Distance(target.position, this.transform.position) > engageDistance) //default/idle
		{
			penispunching = false;
			peniswalking = true;
			hittingWall = Physics2D.OverlapCircle (wallCheck.position, wallCheckRadius, whatIsWall);
            
            //notAtEdge=Physics2D.OverlapCircle (edgeCheck.position, wallCheckRadius, whatIsWall);
            if (hittingWall)
            {
                moveRight = !moveRight;
                pengaging = false;
            }
            else if(hittingWall && pengaging)///wall hit korse engage o kortese
            {
                pengaging = false;
            }

			if (moveRight) {
				transform.localScale = new Vector3 (1f, 1f, 1f);
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveSpeed, GetComponent<Rigidbody2D> ().velocity.y);
			} else {
				transform.localScale = new Vector3 (-1f, 1f, 1f);
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (-moveSpeed, GetComponent<Rigidbody2D> ().velocity.y);
			}


			//	Debug.DrawLine(target.position, this.transform.position, Color.green);
		}
		//anim.SetBool("isIdle", true);
		//anim.SetBool("isAttacking", false);
		//enispunching = false;
		if (moveSpeed == 0 && !pattacking && !penispunching)
		{
			// enisidle = true;

			Debug.Log("pasa");
		}
		///////////////////////enemycolliderSAL
		if (pattacking == true)
		{
			flag = true;

		}
		else
		{
			flag = false;

		}

		if (ptriggerbool)//working
		{
			//StartCoroutine(punchdelay());
			if (time > 0)
			{
				penpunchcol.enabled = false;
				pyomama = false;
				time++;
				anim.SetBool("ispunching", false);
				//enispunching = false;
				player.ishurt = false;
				if (time == 60)
					time = 0;
			}
			if (time == 0)
			{
				penpunchcol.enabled = true;
				anim.SetBool("ispunching", true);
				//enispunching = true;
				gm.DecreaseLife(1);
				pyomama = true;
				//player.ishurt = true;
				time++;

			}

		}

	}

	private IEnumerator Death()
	{
		yield return new WaitForSeconds(2f);
		Destroy(gameObject);
	}

	private void flip()
	{
		facingLeft = !facingLeft;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1; 
		transform.localScale = theScale; 
	}

	public void GiveDamage(int damageToGive)
	{
		if (this.penemyHealth > 0)
		{   FindObjectOfType<SoundManager>().Play("Player punch");
			this.penishurt = true;
			this.penemyHealth -= damageToGive;
			Vector3 boomoffset = new Vector3(0,5,0);
			var boominstance = Instantiate(boomeffect, transform.position + boomoffset, transform.rotation);
			Destroy(boominstance, 0.3f);

			//punch or kick khaise decide 
			if (player.ispunching)
			{
				FindObjectOfType<SoundManager>().Play("Player punch");
			}
			if (player.iskicking)
			{
				FindObjectOfType<SoundManager>().Play("Player kick");
			}
			//FindObjectOfType<SoundManager>().Play("Enemy Death");
			Invoke("SetHurtBool", 0.3f);
			if (!facingLeft)
				rb.AddForce(new Vector2(-hurtimpactx, hurtimpacty));
			else
				rb.AddForce(new Vector2(hurtimpactx, hurtimpacty));
		}
		else if (this.penemyHealth <= 0)
		{

		}
	}

	private void SetHurtBool()
	{
		penishurt = false;
	}

	private void DeathDelay()
	{
		Destroy(gameObject);
		//enisdead = false;
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag.Equals("groundbus"))
		{
			this.transform.parent = col.transform;
		}
	}

	void OnCollisionExit2D(Collision2D col)
	{
		if (col.gameObject.tag.Equals("groundbus"))
		{
			this.transform.parent = null;
		}
	}

	void EnemyAnimation()
	{
		if (peniswalking)
		{
			anim.SetBool("iswalking", true);

		}
		else
		{
			anim.SetBool("iswalking", false);
		}

		if (player.isdead)//player morle ar hatbe na 
		{
			anim.SetBool("iswalking", false);
			moveSpeed = 0;
		}

		/* if (enispunching) //player morle ar marbena 
        {
            anim.SetBool("ispunching", true);
        }
        else
        {
            anim.SetBool("ispunching", false);
        }*/

		if (player.isdead)
		{
			anim.SetBool("ispunching", false);
		}


		if (penishurt)
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
	///SALMA ENEMYCOLLIDER
	void OnTriggerEnter2D(Collider2D other)
	{
		//print("Touch hoccheeeee mamaaaaaaaaaaaaaaaaaaaaaa");
		/* GameObject objectcollided = other.gameObject;
        if (objectcollided.CompareTag("Player"))
        {
            //FindObjectOfType<GameManager>().DecreaseLife(1);
            triggerbool = true;
            gm.DecreaseLife(1);
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
            }

        }*/



	}

	void OnTriggerStay2D(Collider2D other)
	{
		GameObject objectcollided = other.gameObject;
		if (objectcollided.CompareTag("playerp"))
		{
			phocche = true;
			//FindObjectOfType<GameManager>().DecreaseLife(1);
			ptriggerbool = true;
			//Debug.LogError("jhakanaka");

		}
		else if (objectcollided.CompareTag("Untagged"))
		{
			//Debug.LogError("Hit an object with an undefined tag");

		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		ptriggerbool = false;
		ppunchistriggered = false;
		pcollidertrigger = false;
		phocche = false;

	}

	IEnumerator punchdelay()
	{
		if (flag == true)
		{
			Debug.Log("ahare");
			penpunchcol.enabled = true;
			penispunching = true;
			penisidle = false;
			//yield return new WaitForSeconds(2);

			if (penpunchcol.enabled == true)//genjam eikhanei
			{
				Debug.Log("gebon");
				//enpunchcol.enabled = false;
				ppunchistriggered = false;
				penispunching = false;
				penisidle = true;
				yield return new WaitForSeconds(5);
				penpunchcol.enabled = true;
				ppunchistriggered = true;
				penispunching = true;
				penisidle = false;
			}
		}
		//yield return new WaitForSeconds(2);
	}

}
