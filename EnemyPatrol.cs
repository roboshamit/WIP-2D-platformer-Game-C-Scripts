using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPatrol : MonoBehaviour
{
    Rigidbody2D rb;
    
    public Animator anim;
    public float engageDistance;
	public float attackDistance;
	public float moveSpeed;
	public bool facingLeft;
    public bool attacking;
    public bool engaging;
    public bool enishurt;
    public bool enisdead;
    public bool enispunching;
    public bool eniswalking;
    public bool enisidle;
    public bool enisnut;
    public bool death;
    public int enemyHealth;

    public static PlayerMovement player;
    
    //public float attackDamage;
    public float hurtimpactx;
    public float hurtimpacty;
    public bool flag = false;
    public BoxCollider2D enpunchcol;
    public int time;
    public bool yomama;
    public bool punchistriggered;
    public bool collidertrigger = false;
    public bool hocche = false;
    public GameManager gm;
    public bool triggerbool=false;
    public GameObject boomeffect;
    public GameObject slapeffect;
    public Transform target;
    GameObject playera;
    public bool haswhistled;

    public GameObject minus10;
    public GameObject minus20;
    public GameObject minus30;
    public GameObject minus50;
    public bool colwithplayer;

    public SceneManagement sm;
    private Renderer[] r;
    [SerializeField]
    private Color colortochangeto = Color.white;
    public bool turnwhite;
    public float t;
    public int damagecounter;

    void Start()
    {
        damagecounter = 0;
        turnwhite = false;
        r = GetComponentsInChildren<Renderer>();
        gm = FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sm = FindObjectOfType<SceneManagement>();
        //NinjaController sling = FindObjectOfType<NinjaController>();

		enispunching = false;
        enisidle = true;
        eniswalking = false;
        enishurt = false;
        enisnut = false;
        enpunchcol.enabled = true;
        time = 0;
        punchistriggered = false;
        yomama = false;
        player = FindObjectOfType<PlayerMovement>();
        haswhistled = false;

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
       
       
        ///ENEMY DAMAGE TAKE 
        if (player.ispunching == true && hocche == true)
        {
           
            GiveDamage(2);
            StartCoroutine(TurnWhite());
            Vector3 minusoffset = new Vector3(0, 8, 0);
            var minus20instance= Instantiate(minus20,transform.position+minusoffset,transform.rotation);
            Destroy(minus20instance, 1.3f);
            this.GetComponent<Rigidbody2D>().constraints &= ~RigidbodyConstraints2D.FreezePositionX;
            //	hocche = true;
        }
        if (player.iskicking == true && hocche == true)
        {
            GiveDamage(3);
            StartCoroutine(TurnWhite());
            Vector3 minusoffset = new Vector3(0, 8, 0);
            var minus30instance = Instantiate(minus30, transform.position + minusoffset, transform.rotation);
            Destroy(minus30instance, 1.3f);
            this.GetComponent<Rigidbody2D>().constraints &=~ RigidbodyConstraints2D.FreezePositionX;
            //	hocche = true;
        }
        if (player.issticking == true && hocche == true)
        {
            GiveDamage(5);
            StartCoroutine(TurnWhite());
            Vector3 minusoffset = new Vector3(0, 8, 0);
            var minus50instance = Instantiate(minus50, transform.position + minusoffset, transform.rotation);
            Destroy(minus50instance, 1.3f);
            
            //	hocche = true;
        }
        if(!player.ispunching && !player.iskicking && colwithplayer)
        {
            this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
        }
      /*  if (sling.slinghit)
        {
            Vector3 minusoffset = new Vector3(0, 8, 0);
            var minus10instance = Instantiate(minus10, transform.position + minusoffset, transform.rotation);
            Destroy(minus10instance, 1.3f);
        }
     */
        if (death)
        {
            if (enisdead)
            {
                enishurt = false;
                anim.SetBool("isdead", true);
                
                //Debug.Log("CHULL");
            }
            else
            {
                anim.SetBool("isdead", false);
            }
        }
        else if (!death)
        {

            EnemyAnimation();

        }

        if (enemyHealth <= 0)
		{

            ///age death anim then delay then destroy
            /////genjam- destroy na korle enemy kaj kortei thake so
            ///1. delay te destroy kora jabena 
            ///2. delay er age shob kaj off korte hobe deathanim baade :3
            FindObjectOfType<SoundManager>().Play("Enemy Death");
          
            enishurt = false;
            enisnut = false;
            enisdead = true;
            death = true;
            eniswalking = false;
            enisidle = false;
            enispunching = false;
            engaging = false;
            attacking = false;
            anim.SetBool("iswalking", false);
            anim.SetBool("ishurt", false);
            anim.SetBool("isnut", false);
            anim.SetBool("isdead", true);

            StartCoroutine("Death");

            Invoke("SetHurtBool", 0.3f);
            Invoke("SetNutBool", 0.7f);
            //Invoke("DeathDelay", 5f);
            //Destroy(gameObject, 2f);
            //Destroy(gameObject);

            //Instantiate (deathEffect, transform.position, transform.rotation);
            //ScoreManager.AddPoints (pointsOnDeath);

        }
        else if ((Vector3.Distance(target.position, this.transform.position) < engageDistance ) )
        {
            
            engaging = true;
            enisidle = false;
            eniswalking = true;
           
            //get the direction of the target
            Vector3 direction = target.position - this.transform.position;
            if (transform.localScale.x > 0 )
            {
                if (target.transform.position.x < this.transform.position.x && facingLeft)
                    flip();
                if (target.transform.position.x > this.transform.position.x && !facingLeft)
                    flip();
            }
            /*
                  if (Mathf.Sign(direction.x) == -1 && facingLeft)
                      flip();
                  else if (Mathf.Sign(direction.x) == 1 && !facingLeft)
                      flip();
                  */



            if (engaging && !haswhistled)
            {
                Whistle();
            }

            if (direction.magnitude >= attackDistance )///FAR
			{
                attacking = false;
                /*if (Mathf.Sign(direction.x) == -1 && facingLeft)
					flip();
				else if (Mathf.Sign(direction.x) == 1 && !facingLeft)
					flip();
               */
                //eniswalking = true;
                //Debug.DrawLine(target.position, this.transform.position, Color.yellow);

                if (target.transform.position.x < this.transform.position.x)
                {
                    this.transform.Translate(Vector3.left * (Time.deltaTime * moveSpeed));
                }
                else if (target.transform.position.x > this.transform.position.x)
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
				/*if (Mathf.Sign(direction.x) == -1 && facingLeft)
					flip();
				else if (Mathf.Sign(direction.x) == 1 && !facingLeft)
					flip();*/
                attacking = true;
                hocche = true;
                triggerbool = true;
                // enispunching = true;
                eniswalking = false;
				anim.SetBool("iswalking", false);
				anim.SetBool("ispunching", true);

                //Debug.DrawLine(target.position, this.transform.position, Color.red);
                //anim.SetBool("isAttacking", true);
            }
            else
            {
				
                attacking = false;
                hocche = false;
                triggerbool = false;
                //	enispunching = false;
                anim.SetBool("ispunching", false);
                //eniswalking = false;
            }
        }
        else if (Vector3.Distance(target.position, this.transform.position) > engageDistance) //default/idle
        {
            enispunching = false;
            eniswalking = false;

            //	Debug.DrawLine(target.position, this.transform.position, Color.green);
        }
        //anim.SetBool("isIdle", true);
        //anim.SetBool("isAttacking", false);
        //enispunching = false;
        if (moveSpeed == 0 && !attacking && !enispunching)
        {
           // enisidle = true;

            Debug.Log("pasa");
        }
        ///////////////////////enemycolliderSAL
        if (attacking == true)
        {
            flag = true;

        }
        else
        {
            flag = false;

        }

        if (triggerbool)//working
        {
            //StartCoroutine(punchdelay());
            if (time > 0 )
            {
                enpunchcol.enabled = false;
                yomama = false;
                time++;
				anim.SetBool("ispunching", false);
				//enispunching = false;
                player.ishurt = false;
                if (time == 60)
                    time = 0;
            }
            if (time == 0 && !sm.ispaused)
            {
                enpunchcol.enabled = true;
				anim.SetBool("ispunching", true);
                //enispunching = true;
                if (damagecounter>0)
                {
                    gm.DecreaseLife(1);
                    
                }
                damagecounter++;
                yomama = true;
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
        if (this.enemyHealth > 0)
        {  
           
            this.enemyHealth -= damageToGive;
           

            //punch or kick khaise decide 
            if (player.ispunching)
            {
                //this.enishurt = true;
                FindObjectOfType<SoundManager>().Play("Player punch");
                Vector3 boomoffset = new Vector3(0, 8, 0);
                var slapinstance = Instantiate(slapeffect, transform.position + boomoffset, transform.rotation);
                Destroy(slapinstance, 0.3f);

            }
            if (player.iskicking)
            {
                FindObjectOfType<SoundManager>().Play("Player kick");
               //this.enisnut = true;

                if (!facingLeft)
                    rb.AddForce(new Vector2(hurtimpactx, hurtimpacty));
                else
                    rb.AddForce(new Vector2(-hurtimpactx, hurtimpacty));

                Vector3 boomoffset = new Vector3(0, 5, 0);
                var boominstance = Instantiate(boomeffect, transform.position + boomoffset, transform.rotation);
                Destroy(boominstance, 0.3f);

                //anim.SetBool("isnut", true);
            }
            
            Invoke("SetHurtBool", 0.3f);
            Invoke("SetNutBool", 0.8f);
            //FindObjectOfType<SoundManager>().Play("Enemy Death");

            
        }
        else if (this.enemyHealth <= 0)
        {
            
        }
    }

    private void SetHurtBool()
    {
        enishurt = false;
    }

    private void SetNutBool()
    {
        enisnut = false;
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

        if (col.gameObject.tag.Equals("box"))
        {
            anim.SetBool("iswalking", false);
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
        if (eniswalking)
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


        if (enishurt)
        {
            anim.SetBool("ishurt", true);
        }
        else
        {
            anim.SetBool("ishurt", false);
        }

        if (enisnut)
        {
            anim.SetBool("isnut", true);
        }
        else
        {
            anim.SetBool("isnut", false);
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
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            colwithplayer = true;
        }

    }

	void OnTriggerStay2D(Collider2D other)
	{
		GameObject objectcollided = other.gameObject;
		if (objectcollided.CompareTag("playerp"))
		{
			hocche = true;
			//FindObjectOfType<GameManager>().DecreaseLife(1);
			triggerbool = true;
            //Debug.LogError("jhakanaka");

        }
        else if (objectcollided.CompareTag("Untagged"))
        {
             //Debug.LogError("Hit an object with an undefined tag");
           
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
		triggerbool = false;
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
            enispunching = true;
            enisidle = false;
            //yield return new WaitForSeconds(2);

            if (enpunchcol.enabled == true)//genjam eikhanei
            {
                Debug.Log("gebon");
                //enpunchcol.enabled = false;
                punchistriggered = false;
                enispunching = false;
                enisidle = true;
                yield return new WaitForSeconds(5);
                enpunchcol.enabled = true;
                punchistriggered = true;
                enispunching = true;
                enisidle = false;
            }
        }
        //yield return new WaitForSeconds(2);
    }

    void Whistle()
    {
        FindObjectOfType<SoundManager>().Play("Whistle1");
        haswhistled = true;
    }

    public void Slingimpactshow()
    {
        Vector3 minusoffset = new Vector3(0, 8, 0);
        var minus10instance = Instantiate(minus10, transform.position + minusoffset, transform.rotation);
        Destroy(minus10instance, 0.7f);

        Vector3 boomoffset = new Vector3(0, 5, 0);
        var boominstance = Instantiate(boomeffect, transform.position + boomoffset, transform.rotation);
        Destroy(boominstance, 0.3f);
    }

    IEnumerator TurnWhite()
    {
        foreach (Renderer rr in r)
            rr.material.color = colortochangeto;
        yield return new WaitForSeconds(t);
        foreach (Renderer rr in r)
            rr.material.color = Color.white;
        t = 0.15f;

    }



}
