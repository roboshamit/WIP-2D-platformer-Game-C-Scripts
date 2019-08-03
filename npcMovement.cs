using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcMovement : MonoBehaviour
{

    public Animator anim;
    public bool npcwalking;
    public bool facingLeft;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        npcwalking = true;
        facingLeft = true;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (npcwalking)
        {
            anim.SetBool("iswalking", true);

            if (facingLeft)
            {
                this.transform.Translate(Vector3.left * (Time.deltaTime * moveSpeed));
            }
            else if (!facingLeft)
            {
                this.transform.Translate(Vector3.right * (Time.deltaTime * moveSpeed));
            }

        }
        else
        {
            anim.SetBool("iswalking", false);
        }

    }
}
