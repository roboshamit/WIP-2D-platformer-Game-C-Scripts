using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Animator anim;
    public float moveSpeed;
    public bool facingLeft;
    public bool meyeiswalking;
    public static PlayerMovement player;
    public Transform target;
    public Vector3 velocity;
    public Vector3 pos;
    GameObject playera;
    public float distwithplayer;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Awake()
    {
        pos = transform.position;
    }
    void Start()
    {
        meyeiswalking = false;
        anim = GetComponent<Animator>();
        playera = GameObject.FindWithTag("playerp");
        target = playera.transform;
        rb = playera.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //velocity = (transform.position - pos) / Time.deltaTime;
        //pos = transform.position;
        
        
        if (target.transform.position.x < this.transform.position.x && rb.velocity.x<0f)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x - distwithplayer, transform.position.y, transform.position.z), moveSpeed * Time.deltaTime);
            meyeiswalking = true;
        }
        else if(target.transform.position.x > this.transform.position.x && rb.velocity.x > 0f)
        {
               transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x + distwithplayer, transform.position.y, transform.position.z), moveSpeed * Time.deltaTime);
               meyeiswalking = true;
        }
        else
        {
            meyeiswalking = false;
        }
        
        if (target.localScale.x == 1)
        {
            Vector3 theScale = transform.localScale;
            theScale.x = 1;
            transform.localScale = theScale;
        }
        else if (target.localScale.x == -1)
        {
            Vector3 theScale = transform.localScale;
            theScale.x = -1;
            transform.localScale = theScale;
        }

        MeyeAnim();
    }

    void MeyeAnim()
    {
            if (meyeiswalking)
            {
                anim.SetBool("iswalking", true);
            }
            else if(!meyeiswalking)
            {
                anim.SetBool("iswalking", false);
            }
        }
}
