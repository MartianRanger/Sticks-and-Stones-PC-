using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour { //At this point, it's just a script for a health power up. If I have more time later, I will add more items this will apply to like the bombs for example.

    public string type; //Again "type" is for later down the road.

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(Time.deltaTime * 50, 0, 0)); //For animation purposes
	}

    void OnTriggerEnter (Collider other) //If a player collides into it, they will receive a health bonus
    {
        if(other.CompareTag("Player"))
        {
            Pickup(other);
            other.GetComponent<CustomVRControls>().currentHealth += 25;
        }
    }
    void Pickup(Collider player)
    {
        Destroy(gameObject);
    }
}
