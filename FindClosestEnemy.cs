using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClosestEnemy : MonoBehaviour
{

   

    // Update is called once per frame
    void Update () {
		FindClosestEnemyaha ();
	}

	void FindClosestEnemyaha()
	{
		float distanceToClosestEnemy = Mathf.Infinity;
        EnemyPatrol closestEnemy = null;
        EnemyPatrol[] allEnemies = GameObject.FindObjectsOfType<EnemyPatrol>();

		foreach (EnemyPatrol currentEnemy in allEnemies) {
			float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
			if (distanceToEnemy < distanceToClosestEnemy) {
				distanceToClosestEnemy = distanceToEnemy;
				closestEnemy = currentEnemy;
			}
		}

		//Debug.DrawLine (this.transform.position, closestEnemy.transform.position);
	}


}
