using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
	public SpawnerTrigger spawn;
	public GameObject enemyprefab1,enemyprefab2;
    private GameObject eninstance;
    float randX;
	Vector2 whereToSpawn;
	public float spawnRate = 2f;
	float nextSpawn = 0.0f;
    int whattospawn;

    // Start is called before the first frame update
    void Start()
    {
		spawn = FindObjectOfType<SpawnerTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
		if(spawn.flag){
		if(Time.time > nextSpawn)
		{
			
            whattospawn = Random.Range(1, 3);
            
            whereToSpawn = new Vector2(transform.position.x, transform.position.y);

                switch (whattospawn)
                {
                    case 1:
                        eninstance = Instantiate(enemyprefab1, whereToSpawn, Quaternion.identity);
                        break;
                    case 2:
                        eninstance = Instantiate(enemyprefab2, whereToSpawn, Quaternion.identity);
                        break;
                }
                nextSpawn = Time.time + spawnRate;
            }
		}
    }
}
