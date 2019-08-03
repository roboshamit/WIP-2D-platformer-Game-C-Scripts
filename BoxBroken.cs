using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBroken : MonoBehaviour
{
    private Rigidbody2D[] childrenRbs;
    private float randomTorque, randomDirX, randomDirY;
    
    void Start()
    {
        childrenRbs = GetComponentsInChildren<Rigidbody2D>();
        foreach(Rigidbody2D rigbody2d in childrenRbs)
        {
            FindObjectOfType<SoundManager>().Play("Box Break");
            randomTorque = Random.Range(-500f, 500f);
            randomDirX = Random.Range(-1000f, 1000f);
            randomDirY = Random.Range(-1000f, 1000f);
            rigbody2d.AddTorque(randomTorque);
            rigbody2d.AddForce(new Vector2(randomDirX, randomDirY));
            Destroy(gameObject, 3f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
