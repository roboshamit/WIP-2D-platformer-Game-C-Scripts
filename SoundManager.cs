using UnityEngine;
using System;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static AudioClip ppunch, pkick, phurt, slingshot, edeath, bus, boxbreak;
    static AudioSource audiosrc;

    public Sound[] sounds;

    // Start is called before the first frame update
    void Start()
    {
        /*
        ppunch = Resources.Load<AudioClip>("Punch");
        pkick = Resources.Load<AudioClip>("Kick");
        phurt = Resources.Load<AudioClip>("Player Hurt");
        slingshot = Resources.Load<AudioClip>("Slingshot");
        edeath= Resources.Load<AudioClip>("Enemey Death");
        bus= Resources.Load<AudioClip>("Bus");

        audiosrc = GetComponent<AudioSource>();
        */
    }

    void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.source=gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    /*
    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "Punch":
                audiosrc.PlayOneShot(ppunch);
                break;
            case "Kick":
                audiosrc.PlayOneShot(pkick);
                break;
            case "Player Hurt":
                audiosrc.PlayOneShot(phurt);
                break;
            case "Slingshot":
                audiosrc.PlayOneShot(slingshot);
                break;
            case "Enemey Death":
                audiosrc.PlayOneShot(edeath);
                break;
            case "Bus":
                audiosrc.PlayOneShot(bus);
                break;


        }
    }
    */


}
