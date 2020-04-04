using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {
    //Script to control doors in the arena level

    public Transform bottom; //Bottom of the doorway
    public Transform top; //Top of the doorway

    public float doorSpeed = 2f; //How fast the door moves
	// Use this for initialization
	void Start () {

    }

    void OnTriggerEnter(Collider other) //OnTrigger says that if either an enemy or the player goes near it, it opens the door
    {
        if(other.gameObject.tag=="Player" || other.gameObject.tag == "Enemy") //Checks both tags
        {
            StopCoroutine("CloseDoor");
            StartCoroutine("OpenDoor");
        }
    }
    void OnTriggerExit(Collider other) //OnTrigger says that if either an enemy or the player goes near it, it opens the door
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy") //Checks both tags
        {
            StartCoroutine("CloseDoor");
            StopCoroutine("OpenDoor");
        }
    }
    IEnumerator OpenDoor()
    {
        while(Vector3.Distance (transform.position, top.position) > 0.1f) //While the distance from the floor to the door is greater than 0.1f...
        {
            transform.position = Vector3.Lerp(transform.position, top.position, doorSpeed * Time.deltaTime); //..The door moves up, opening it.

            yield return null;
        }
    }
    IEnumerator CloseDoor() 
    {
        while (Vector3.Distance(transform.position, bottom.position) > 0.1f) //While the distance from the floor to the door is less than 0.1f...
        {
            transform.position = Vector3.Lerp(transform.position, bottom.position, doorSpeed * Time.deltaTime); //..The door moves down, closing it.

            yield return null;
        }
    }
}
