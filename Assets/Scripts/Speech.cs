using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Speech : MonoBehaviour //Created a main class to create multiple types of object (AKA inheritance)
{
    public AudioSource thisSound;
    public AudioClip sound;
    public GameObject speechBubble; //prefab model

    public int damageFactor = 5; //How much we multiply the object sounds
    public float speed = 5f; //How fast all speech bubbles
    public int damage; //Actual Damage Amount

    void Start()
    {
       GetComponent<Rigidbody>().velocity = transform.forward * 50; //Sends it immediatelly flying
    }

    public abstract void LoadSound(); //Created abstract method for all speech bubbles to use

    public void PlaySound() //Plays sound at an actual position, which will be whether it hits a collider
    {
        AudioSource.PlayClipAtPoint(thisSound.clip, transform.position, 0.2f);
    }

}
