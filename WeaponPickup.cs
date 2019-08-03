using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public float speedRotate;
    public static bool pickedweapon;
    // Start is called before the first frame update
    void Start()
    {
        pickedweapon = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (!pickedweapon)
        {
            transform.Rotate(Vector3.down * speedRotate * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            //Debug.Log("I love pasa");
            pickedweapon = true;
            FindObjectOfType<PlayerMovement>().GiveWeapon();
            gameObject.SetActive(false);
        }


    }
}
