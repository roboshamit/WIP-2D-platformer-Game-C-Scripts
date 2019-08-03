using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPickup : MonoBehaviour {
    public float speedRotate;
    // Use this for initialization
    void Start () {

	}

	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.down * speedRotate * Time.deltaTime);
    }
	void OnTriggerEnter2D(Collider2D other){

		if (other.tag == "Player") {
			//other.GetComponent<AudioSource>().Play();﻿
			FindObjectOfType<GameManager> ().Givebullet();///hurt method call
			//Instantiate (bloodParticleObject, transform.position, transform.rotation);
			Destroy (gameObject);
			//Bulletcount.scorevalue += 3;
		}


	}
}
