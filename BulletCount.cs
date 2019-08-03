using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletCount : MonoBehaviour
{
	public static int scorevalue;
	//public PlayerMovement playa;
	public Text bulletText;

	// Use this for initialization
	void Start () {
        //playa = GetComponent<PlayerMovement> ();
        scorevalue = 10;
        bulletText = GetComponent<Text>();
        bulletText.color = Color.white;
		//scorevalue = playa.bullet;
	}

	// Update is called once per frame
	void Update () {
        bulletText.text = scorevalue.ToString();
		if (scorevalue <= 0) {
			bulletText.color = Color.red;
		}
		if ( scorevalue>0) {
			bulletText.color = Color.white;

		}
		
	}
}
