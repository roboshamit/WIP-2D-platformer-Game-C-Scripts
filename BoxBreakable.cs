using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBreakable : MonoBehaviour
{
    public static PlayerMovement player;
    // Start is called before the first frame update
    public int hitsNeeded = 2;
    public int hitsTaken;
    public GameObject brokenbox;
    public GameObject ammo;
    public GameObject weapon;
    int whattospawn;

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (collisionInfo.gameObject.tag == "playerp")
        {
            hitsTaken += 1;
            //Debug.Log("a collision occured, hitsTaken:" + hitsTaken);
            if (hitsTaken >= hitsNeeded)
            {
                Destroy(gameObject);
                //randomly instantiate ammo, life, weapon
                Instantiate(brokenbox, transform.position, Quaternion.identity);
                whattospawn = Random.Range(1, 2);
                switch (whattospawn) {

                    case 1:
                        Instantiate(ammo, transform.position, Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(weapon, transform.position, Quaternion.identity);
                        break;
                }
                
               
                //Instantiate(ammo, transform.position, Quaternion.identity);

            }
        }
    }


}
