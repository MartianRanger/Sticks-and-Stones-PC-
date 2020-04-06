using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeechBubble : Speech //Different speech bubble type that is only used by enemies
{
    public float thrust = 1.0f;
    public Rigidbody rb;

    public GameObject effect;

    public void CreateSpeechBubble(AudioClip recording) //Was originally going to be Load Sound method, but didn't work with recordings. Stores recording into speech bubble.
    {
        thisSound = GetComponent<AudioSource>();
        thisSound.clip = recording;
        Debug.Log(thisSound.clip.length + " " + thisSound.clip.name);
        LoadDamage();

    }

    // Start is called before the first frame update
    public override void LoadSound()
    {

    }
    void OnCollisionEnter(Collision other) //Method is needed to determine whether or not it hits an enemy
    {
        PlaySound(); //Plays sound at an actual position, which will be whether it hits a collider
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyAI>().TakeDamage(damage);
            rb = other.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(0, 0, thrust, ForceMode.Impulse);

            Debug.Log("Cloud: " + damage);

        }
        Destroy(gameObject); //Then object is destroyed
        GameObject explosion = Instantiate(effect, transform.position, Quaternion.identity) as GameObject;
        Destroy(explosion, 2);

    }

    public void LoadDamage() //Calcuates the damage for each bubble 
    {
        damage = (int)(2 * thisSound.clip.length);
        Debug.Log("DAMGE!" + damageFactor);
    }
}
