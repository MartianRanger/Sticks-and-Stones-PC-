﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpeechBubble : Speech //Different speech bubble type that is only used by enemies
{
    public AudioClip[] myClips; //Array to store clips for this episode

    public GameObject effect;
    // Awake is called before the first frame update

    Vector3 mDir;
    Rigidbody rigid;
    public float moveSpeed = 5f;
    void Awake()
    {
        LoadSound();
        rigid = GetComponent<Rigidbody>();
    }

    public override void LoadSound() //Takes a random clip from the Audio folder and places it inside our speech bubble
    {
        myClips = Resources.LoadAll<AudioClip>("Audio");
        thisSound = GetComponent<AudioSource>();
        RandomizeSfx(myClips);
        LoadDamage(); //Loads Damage necessary for attack
    }

    public void LoadDamage() //Calcuates the damage for each bubble 
    {
        damage = (int) thisSound.clip.length * 5;
        Debug.Log("DAMGE!" + damage);
    }

    public void RandomizeSfx(params AudioClip[] myClips) //Helps with randomization
    {
        //Generate a random number between 0 and the length of our array of clips passed in.
        int randomIndex = Random.Range(0, myClips.Length);

        //Set the clip to the clip at our randomly chosen index.
        thisSound.clip = myClips[randomIndex];
    }
    void OnCollisionEnter(Collision other) //Method is needed to determine whether or not it hits a player
    {
        if (!other.gameObject.CompareTag("Shield"))
        {
            PlaySound(); //Plays sound at an actual position, which will be whether it hits a collider
                         //GetComponent<Explosion>().Explode();
            if (other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<PlayerScript>().TakeDamage(damage);
                Debug.Log("Cloud: " + damage);
            }
            Destroy(gameObject); //Then object is destroyed
            GameObject explosion = Instantiate(effect, transform.position, Quaternion.identity) as GameObject;
            Destroy(explosion, 2);
        }
        else
        {
            Vector3 wallNormal = other.contacts[0].normal;
            mDir = Vector3.Reflect(rigid.velocity, wallNormal).normalized;
            rigid.velocity = mDir * moveSpeed;
            Destroy(gameObject, 10);
            GameObject explosion = Instantiate(effect, transform.position, Quaternion.identity) as GameObject;
            Destroy(explosion, 2);

        }
    }
}