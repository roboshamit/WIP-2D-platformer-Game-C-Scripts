using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour {
	Rigidbody2D rb;
	GameObject playa;
	Vector2 playaDirection;
	float timeStamp;
	bool flyToplaya;

	void Start()
	{
		rb = GetComponent<Rigidbody2D> ();
	}

	void Update()
	{
		if (flyToplaya) {
			playaDirection = - (transform.position - playa.transform.position).normalized;
			rb.velocity = new Vector2 (playaDirection.x, playaDirection.y) * 50f * (Time.time / timeStamp);
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.name.Equals ("Magnet")) {
			timeStamp = Time.time;
			playa= GameObject.Find ("Player");
			flyToplaya = true;
		}
	}



}
