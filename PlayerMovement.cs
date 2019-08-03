using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour
{
	Rigidbody2D rb;
	public float speed;
	public float jumpheight;
	public float dirX;

	public float hurtimpactx;
	public float hurtimpacty;
	//public int health;
	public Animator anim;
	public bool ishurt;
	public bool isdead;
	public bool ispunching;
	public bool iskicking;
	public bool isslinging;
	public bool isjumping;
	public bool isfalling;
    public bool isidle;
	bool facingRight = true;
	Vector3 localScale;
	public Transform firePoint;
	public GameObject ninjaStar;
	public GameObject punchtrail;
	public bool isAxisInUse=false;
	private float shoot;
	public GameObject sling;
	//public float playerHealth;
	
	public int bullet;
	public bool healthloss;

    public FindClosestEnemy fce;

    public EnemyPatrol enemyP;
    //EnemyPatrol enemyP = GameObject.FindObjectsOfType<EnemyPatrol>();
    

    public HaatacholaEnemy patenemy;
	public CameraShake camsha;

    public float punchDelay;
    public float kickDelay;
    public float slingDelay;

    public bool canpunch;
    public bool cankick;
    public bool cansling;
    //public patrolEnemyCollider penemy;
    //public RandomPatrol penemyP;
    GameManager gm;
    public GameObject boomeffect;
    public GameObject weapon;
    public Transform weaponPoint;
    public WeaponPickup weapick;
    //public bool pickkorse;
    public bool issticking;
    public float weapontime;
    public bool hoy;
    public bool ispaused;
    public SceneManagement sm;
    //public ChangeColor cc;
    private Renderer[] r;
    [SerializeField]
    private Color colortochangeto = Color.white;
    public bool turnwhite;
    public float t;

    private void Start()
	{
        
        turnwhite = false;
        AudioListener.pause = false;
        //pickkorse = false;
        hoy = false;
        //isidle = true;
		anim = GetComponent<Animator>();
		rb = GetComponent < Rigidbody2D>();
		localScale = transform.localScale;
        gm = GetComponent<GameManager>();
        sm = FindObjectOfType<SceneManagement>();
        //cc = GetComponent<ChangeColor>();
        r = GetComponentsInChildren<Renderer>();
        bullet = 10;
        weapontime = 7;
		//health = 10;
		healthloss = false;
        canpunch = true;
        cankick = true;
        cansling = true;
        weapick = FindObjectOfType<WeaponPickup>();
        //EnemyPatrol enemyp = (EnemyPatrol)FindObjectOfType(typeof(EnemyPatrol));
        //enemyP = FindObjectOfType<EnemyPatrol>();
        //penemy = GetComponent<patrolEnemyCollider>();
        //penemyP = GetComponent<RandomPatrol>();
        camsha = GetComponent<CameraShake>();
        //enemyP.yomama = false;
        enemyP = FindObjectOfType<EnemyPatrol>();
        //EnemyPatrol enemyP = (EnemyPatrol)FindObjectOfType(typeof(EnemyPatrol));
        //enemyP = GameObject.FindWithTag("Enemy").GetComponent<EnemyPatrol>();
        //enemyP = fce.closestEnemy;

        if (enemyP)
            Debug.Log("enemy found: " + enemyP.name);
        
       

        //patenemy = FindObjectOfType<HaatacholaEnemy>();
        ispaused = false;
        isdead = false;

    }

   
    void Update()
	{
        //enemyP = FindObjectOfType<EnemyPatrol>();
        /*
		enemyP = FindObjectOfType<EnemyPatrol>();
		penemy = FindObjectOfType<patrolEnemyCollider>();
		penemyP = FindObjectOfType<RandomPatrol>();
        */
        //enemy.yomama = false;
        //if (enemyP.yomama)
		if(gm.lifedecreased)///DAMAGE TAKE
        {
            if (!isdead)
            {

              healthloss = true;
              gm.lifedecreased = false;
                //gm.decreaseLife();
                //Debug.Log("enemy marse");
                //anim.SetTrigger("ishurt");
                // StartCoroutine("Hurt");//ishurt true kore
                //ishurt = true;

                StartCoroutine(TurnWhite());

                //boom effect+ sound 

                FindObjectOfType<SoundManager>().Play("Player punch");
                Vector3 boomoffset = new Vector3(1, 6, 0);
                var boominstance = Instantiate(boomeffect, transform.position + boomoffset, transform.rotation);
                Destroy(boominstance, 0.3f);
                //ishurt = true;
                //anim.SetBool("ishurt",true);
                Hurt();
                HurtFalse(1);
            }
        }
		else if (!enemyP.yomama)
        {
			healthloss = false;
		}

		if(gm.playerhealth <= 0 ){
			isdead = true;
            enemyP.attacking = false;
			enemyP.yomama = false;
            ishurt = false;
			anim.SetBool("isdead", true);
            enemyP.anim.SetBool("ispunching", false);
            enemyP.enispunching = false;//ar ghushabe na 
			//penemyP.anim.SetBool("ispunching", false);
			//penemyP.enispunching = false;//ar ghushabe na
			StartCoroutine("Death");
            //levelmanager.RespawnPlayer ();
            //Time.timeScale = 0;
            // ispaused = true;
            //AudioListener.volume = 0;
            AudioListener.pause = true;
        }
		else if(gm.playerhealth>0)
		{
            //AudioListener.pause = !AudioListener.pause;
            //isdead = false;
        }

#if UNITY_STANDALONE_WIN
        if (Input.GetKey(KeyCode.Space) && !isdead && rb.velocity.y==0)///jump PC
        {
            rb.AddForce(Vector2.up * jumpheight * 10);
            isjumping = true;
        }
        else
        {
            isjumping = false;
        }
#endif

#if UNITY_ANDROID
        if (CrossPlatformInputManager.GetButtonDown("Jump") && !isdead && rb.velocity.y == 0)
        {
			rb.AddForce(Vector2.up * jumpheight*10);
			isjumping = true;
		}
		else
		{
			isjumping = false;
		}
#endif
        SetAnimationState();
		if (!isdead)
		{
            //anim.SetBool("isdead", false);
#if UNITY_STANDALONE_WIN
            dirX = Input.GetAxisRaw("Horizontal") * speed;///PC
#endif

#if UNITY_ANDROID
            dirX = CrossPlatformInputManager.GetAxis("Horizontal") * speed;// ANDROID
#endif
        }

		if (isdead)
		{
			anim.SetBool("isdead", true);
            isdead = false;
        }


        /*
        else if(levelmanager.respawned)
        {
            anim.SetBool("isdead", false);
        }
        */

#if UNITY_STANDALONE_WIN
        if (!isdead && canpunch && Input.GetKey(KeyCode.Z))///MELEE PUNCH
		{
            
            //FindObjectOfType<SoundManager>().Play("Player punch");
			ispunching = true;
			punchtrail.SetActive(true);
			camsha.shouldShake = true;
            canpunch = false;
            StartCoroutine(PunchDelay());
         if (weapick.pickedweapon)///when stick replaces punch
            {
                issticking = true;
                ispunching = false;

            }
            else if(!weapick.pickedweapon && weapontime == 0)
            {
                canpunch = true;
            }
		}
		else
		{
			ispunching = false;
            issticking = false;
		}
#endif

#if UNITY_ANDROID
        if (!isdead && canpunch && CrossPlatformInputManager.GetButtonDown("Punch") && !sm.ispaused)///MELEE PUNCH
		{
            
            //FindObjectOfType<SoundManager>().Play("Player punch");
            ispunching = true;
            punchtrail.SetActive(true);
            camsha.shouldShake = true;
            canpunch = false;
            StartCoroutine(PunchDelay());
            Debug.Log("choti");
            if (WeaponPickup.pickedweapon)///when stick replaces punch
            {
                Debug.Log("choti2");
                hoy = true;
                issticking = true;
                ispunching = false;
                weapon.SetActive(true);
               

            }
          
            /*
           else if(!weapick.pickedweapon && weapontime == 0)
           {
               Debug.Log("choti3");
               hoy = false;
             
               weapon.SetActive(false);
               weapontime = 7;
           }




           else if (!weapick.pickedweapon)
           {
               canpunch = true;
           }
           */


        }
        else///always chole
        {
            //Debug.Log("choti4");
            ispunching = false;
            issticking = false;
            //weapon.SetActive(false);
        }
#endif

#if UNITY_STANDALONE_WIN
        if (!isdead && cankick && Input.GetKey(KeyCode.X))///MELEE KICK
		{
            //FindObjectOfType<SoundManager>().Play("Player kick");
			iskicking = true;
			camsha.shouldShake = true;
            cankick = false;
            StartCoroutine(KickDelay());
		}
		else//////pera
		{
			iskicking = false;
		}
#endif

#if UNITY_ANDROID
        if (!isdead && cankick && CrossPlatformInputManager.GetButtonDown("Kick") && !sm.ispaused)///MELEE KICK
		{
            //FindObjectOfType<SoundManager>().Play("Player kick");
            iskicking = true;
            camsha.shouldShake = true;
            cankick = false;
            StartCoroutine(KickDelay());
        }
        else//////pera
        {
            iskicking = false;
        }
#endif

#if UNITY_STANDALONE_WIN
        if (!isdead && cansling && Input.GetKeyDown(KeyCode.C)){//////SLINGSHOT

			//if (isAxisInUse == false) {
			isslinging = true;
			Shoot ();
			//isAxisInUse = true;
			//	}
			//Fire();
		}
		else
		{
			isslinging = false;
		}
#endif

#if UNITY_ANDROID
        if (!isdead && cansling && CrossPlatformInputManager.GetButtonDown("Sling") && !sm.ispaused)
        {//////SLINGSHOT

            //if (isAxisInUse == false) {
            isslinging = true;
            Shoot();
            cansling = false;
            StartCoroutine(SlingDelay());
            //isAxisInUse = true;
            //	}
            //Fire();
        }
        else
        {
            isslinging = false;
        }
#endif
        //if (shoot == 0) {
        //	isAxisInUse = false;
        //}

    }

    void FixedUpdate()
    {
        if (!ishurt)
        {
            rb.velocity = new Vector2(dirX, rb.velocity.y);
            
        }

        if (dirX == 0)
        {
          //  isidle = true;

        }
	}

	void LateUpdate()
	{
		CheckWhereToFace();
	}

	void SetAnimationState()///ANIMATION LOGICS
	{
		
		if (dirX == 0)
		{
			anim.SetBool("iswalking", false);
			anim.SetBool("isfalling", false);

		}

		if (rb.velocity.y == 0)
		{
			anim.SetBool("isjumping", false);
			anim.SetBool("isfalling", false);

		}
		else if (rb.velocity.y > 0)
		{
			anim.SetBool("isjumping", true);
			anim.SetBool("isfalling", false);
		}
		else if (rb.velocity.y < 0)
		{
			anim.SetBool("isjumping", false);
			anim.SetBool("isfalling", true);
		}

		if (Mathf.Abs(dirX) > 0 && rb.velocity.y == 0)
		{
			anim.SetBool("iswalking", true);
			punchtrail.SetActive(false);
		}
		else
		{
			anim.SetBool("iswalking", false);
		}

		if (ispunching)               
		{
			anim.SetBool("ispunching", true);
		}
		else
		{
			anim.SetBool("ispunching", false);
		}

        if (issticking)
        {
            anim.SetBool("issticking", true);
        }
        else
        {
            anim.SetBool("issticking", false);
        }

        if (iskicking)                                
		{
			anim.SetBool("iskicking", true);
		}
		else
		{
			anim.SetBool("iskicking", false);
		}

		if (isslinging)
		{
			anim.SetBool("isslinging", true);
		}
		else
		{
			anim.SetBool("isslinging", false);
		}


		if (ishurt)
		{
			anim.SetBool("ishurt", true);
		}
		else
		{
			anim.SetBool("ishurt",false);
		}

                                                             
	}

	void CheckWhereToFace()
	{
		if (dirX > 0)
			facingRight = true;
		else if (dirX < 0)
			facingRight = false;

		if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
			localScale.x *= -1;

		transform.localScale = localScale;

	}

	/* void OnTriggerEnter2D(Collider2D col)/////////HEALTH CONTROL++++++++++++++++++++++
    {
        if (col.gameObject.tag.Equals("enemypunch") && health > 0)
        {
			healthloss = true;
            //health -= 1;
            Debug.Log("enemy marse");
            //anim.SetTrigger("ishurt");
            StartCoroutine("Hurt");
        }

        
    }
	void OnTriggerExit2D(Collider2D col)
	{
		healthloss = false;
	}*/

	void Hurt()
	{
		//ishurt = true;
        //rb.velocity = Vector2.zero;//player k thamay dicche.. should do this on enemy instead
        Debug.Log("hala");
        
        if (facingRight)
			rb.AddForce(new Vector2(-hurtimpactx, hurtimpacty));
		else
			rb.AddForce(new Vector2(hurtimpactx, hurtimpacty));
        
        //yield return new WaitForSeconds(0f);
        Invoke("HurtFalse", 0.2f);
    }

    IEnumerator HurtFalse(float time)
    {
        yield return new WaitForSeconds(time);
        ishurt = false;

        // Code to execute after the delay
    }

    void HurtFalsem()
    {
      
    }

	private IEnumerator Death()
	{
		yield return new WaitForSeconds(0f);
        //isdead = true;
		//anim.SetBool("isdead", false);
		//Destroy(gameObject);
		
        isdead = false;

	}

	public void Shoot(){
		//if (FindObjectOfType<LevelManager> ().bullet > 0) {
		//if(bullet>0 ){
		if(BulletCount.scorevalue>0 )
		{
			//FindObjectOfType<LevelManager> ().Takebullet();
			//shoot=1;
			//bullet--;
			BulletCount.scorevalue--;
          
            //	anim.SetBool ("Attack", true);
            sling.SetActive(true);
            StartCoroutine("SlingHide");
            FindObjectOfType<SoundManager>().Play("Slingshot");

            //Instantiate (ninjaStar, firePoint.position, firePoint.rotation*Quaternion.Euler(Random.Range(2,30),0,0));
            Instantiate(ninjaStar, firePoint.position, firePoint.rotation);
        //Instantiate (bulletshell, shellpoint.position, shellpoint.rotation);
            //	Instantiate(bulletshell,shellpoint.position,Quaternion.Euler(0f,0f,Random.Range(0f,360f)));
            //	shellrb.AddForce (transform.right* 2f,ForceMode2D.Impulse);


            //Debug.Log (bullet);
            //}
            //}
        }
        else
        {
            //no ammo sound hobe
        }
	}

    IEnumerator SlingHide()
    {
        yield return new WaitForSeconds(2f);
        sling.SetActive(false);
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

    IEnumerator PunchDelay()
    {
        yield return new WaitForSeconds(punchDelay);
        canpunch = true;
    }

    IEnumerator KickDelay()
    {
        yield return new WaitForSeconds(kickDelay);
        cankick = true;
    }

    IEnumerator SlingDelay()
    {
        yield return new WaitForSeconds(slingDelay);
        cansling = true;
    }

    public void GiveWeapon()
    {
        if (WeaponPickup.pickedweapon)
        {
            //Instantiate(weapon, weaponPoint.position, weaponPoint.rotation);
            weapon.SetActive(true);
            StartCoroutine("WeaponTimer");
            //Debug.Log("thanus");
        }
    }

    IEnumerator WeaponTimer()
    {
       
        yield return new WaitForSeconds(weapontime);
        canpunch = true;
        weapontime = 7;
        //Debug.Log("chosha");
        weapon.SetActive(false);
        issticking = false;
        WeaponPickup.pickedweapon = false;
        //pickkorse = false;

        //ispunching = true;
    } //weapon diye bari dibe not punch+ weapon anim play instead of punch


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
