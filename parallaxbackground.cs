using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxbackground : MonoBehaviour
{
    public float scrollspeed;
    public float distancecover;
    Vector2 startpos;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float newpos = Mathf.Repeat(Time.time * scrollspeed, distancecover);
        transform.position = startpos + Vector2.right * newpos;
    }
}
