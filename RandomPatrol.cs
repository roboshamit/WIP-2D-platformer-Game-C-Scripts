using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPatrol : MonoBehaviour
{
	Rigidbody2D rb;
	public Transform target;
	public Animator anim;
	public float engageDistance;
	public float attackDistance;
	public float moveSpeed;
	private bool facingLeft = true;
	public bool attacking;
	public bool engaging;
	public bool enishurt;
	public bool enisdead;
	public bool enispunching=false;
	public bool eniswalking;
	public bool enisidle;

	public int enemyHealth;
	public PlayerMovement player;
	public patrolEnemyCollider encol;
	//public float attackDamage;
	public float hurtimpactx;
	public float hurtimpacty;
	//public float moveSpeed;
	public bool moveRight;
	//private Animator anim;
	public Transform wallCheck;
	public float wallCheckRadius;
	public LayerMask whatIsWall;
	private bool hittingWall;
	private bool notAtEdge;
	public Transform edgeCheck;
	public bool death;

	//public float attackDamage;

	void Start()
	{
		//encol = FindObjectOfType<EnemyCollider>();
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		//player = FindObjectOfType<PlayerMovement>();
		enisidle = true;
		eniswalking = false;
		enishurt = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (enemyHealth <= 0)
		{

			///age death anim then delay then destroy
			/////genjam- destroy na korle enemy kaj kortei thake so
			///1. delay te destroy kora jabena 
			///2. delay er age shob kaj off korte hobe deathanim baade :3
			enishurt = false;
			enisdead = true;
			death = true;
			eniswalking = false;
			enisidle = false;
			enispunching = false;
			engaging = false;
			attacking = false;
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
		//anim.SetBool("isIdle", true);
		//anim.SetBool("isAttacking", false);
		//enispunching = false;

		else if ((Vector3.Distance(target.position, this.transform.position) < engageDistance ) )
		{

			engaging = true;
			enisidle = false;
			eniswalking = true;

			//get the direction of the target
			Vector3 direction = target.position - this.transform.position;
			if (Mathf.Sign(direction.x) == 1 && facingLeft)
				flip();
			else if (Mathf.Sign(direction.x) == -1 && !facingLeft)
				flip();

			if (direction.magnitude >= attackDistance )///FAR
			{
				attacking = false;
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

				attacking = true;
				enispunching = true;
				eniswalking = false;
				anim.SetBool("iswalking", false);
				anim.SetBool("ispunching", true);

				//Debug.DrawLine(target.position, this.transform.position, Color.red);
				//anim.SetBool("isAttacking", true);
			}
			else
			{

				attacking = false;
				enispunching = false;
				anim.SetBool("ispunching", false);
				eniswalking = true;
				//eniswalking = false;
			}
		}
		else if(Vector3.Distance(target.position, this.transform.position) > engageDistance) //default/idle
		{
			attacking = false;
			enispunching = false;
			eniswalking = true;
			hittingWall = Physics2D.OverlapCircle (wallCheck.position, wallCheckRadius, whatIsWall);
			//notAtEdge=Physics2D.OverlapCircle (edgeCheck.position, wallCheckRadius, whatIsWall);
			if (hittingWall)
				moveRight = !moveRight;
			if (moveRight) {
				transform.localScale = new Vector3 (1f, 1f, 1f);
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveSpeed, GetComponent<Rigidbody2D> ().velocity.y);
			} else {
				transform.localScale = new Vector3 (-1f, 1f, 1f);
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (-moveSpeed, GetComponent<Rigidbody2D> ().velocity.y);
			}

			//	Debug.DrawLine(target.position, this.transform.position, Color.green);
		}

	}

	private IEnumerator Death()
	{
		yield return new WaitForSeconds(5f);
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
		if (enemyHealth > 0)
		{
			enishurt = true;
			enemyHealth -= damageToGive;
			Invoke("SetHurtBool", 0.3f);
			/* if (!facingLeft)
                rb.AddForce(new Vector2(-hurtimpactx, hurtimpacty));
            else
                rb.AddForce(new Vector2(hurtimpactx, hurtimpacty));*/
		}

	}

	private void SetHurtBool()
	{
		enishurt = false;
	}

	private void DeathDelay()
	{
		Destroy(gameObject);
		//enisdead = false;
	}

}





/*
 if(enemy.engaging == false)
		{
			enemy.eniswalking = true;
			hittingWall = Physics2D.OverlapCircle (wallCheck.position, wallCheckRadius, whatIsWall);
			notAtEdge=Physics2D.OverlapCircle (edgeCheck.position, wallCheckRadius, whatIsWall);
			if (hittingWall || !notAtEdge)
				moveRight = !moveRight;
			if (moveRight) {
				transform.localScale = new Vector3 (1f, 1f, 1f);
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (Speed, GetComponent<Rigidbody2D> ().velocity.y);
			} else {
				transform.localScale = new Vector3 (-1f, 1f, 1f);
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (-Speed, GetComponent<Rigidbody2D> ().velocity.y);
			}
		}
 */
