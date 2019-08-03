using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaController : MonoBehaviour
{
	public float speed;
	public PlayerMovement player;
	public int damageToGive;
	public EnemyPatrol enemy;
	public HaatacholaEnemy patenemy;
    public bool slinghit;
    // Start is called before the first frame update
    void Start()
    {
		enemy = FindObjectOfType<EnemyPatrol> ();
		patenemy = FindObjectOfType<HaatacholaEnemy> ();
		player = FindObjectOfType<PlayerMovement> ();
        slinghit = false;
		if (player.transform.localScale.x < 0) {
			speed = -speed;
			//rotationSpeed = -rotationSpeed;
		}
    }

    // Update is called once per frame
    void Update()
    {
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (speed, GetComponent<Rigidbody2D> ().velocity.y);
    }

	void OnTriggerEnter2D(Collider2D other)
	{
        GameObject objectcollided = other.gameObject;
        
        if (objectcollided.CompareTag("enbodysling")) // If the object is an enemy
        {
            
                
                objectcollided.GetComponentInParent<EnemyPatrol>().GiveDamage(1);//main enemyr component nibe
                slinghit = true;
                objectcollided.GetComponentInParent<EnemyPatrol>().Slingimpactshow();
                Destroy(gameObject);

        }

		if (objectcollided.CompareTag("PEnemy")) // If the object is an enemy
		{
            if (objectcollided.CompareTag("enbodysling"))
            {
                Debug.Log("kola");
                objectcollided.GetComponent<HaatacholaEnemy>().GiveDamage(1);
                slinghit = true;
                //objectcollided.GetComponent<HaatacholaEnemy>().GiveDamage(1);
                Destroy(gameObject);
            }
		}
        /*
        if (other.tag == "Enemy"){
			//other.GetComponent<enemyHealthManager> ().GiveDamage (5);
			Destroy(gameObject);
			enemy.GiveDamage (5);
		}
        */
	} 
}
