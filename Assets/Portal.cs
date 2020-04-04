using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

    public Transform player;
    public Transform portal;

    private bool playerCollision = false;
	// Use this for initialization
	void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" || other.tag == "Enemy")
        {
            playerCollision = true;
        }
    }
    void Update()
    {
        if (playerCollision)
        {
            Vector3 portalDistance = player.position - transform.position;
            float product = Vector3.Dot(transform.up, portalDistance); //Basically multiplies two values together

            if (product < 0f) //If player has moved crossed the portal
            {
                //code to teleport the player
                float rotationDiff = -Quaternion.Angle(transform.rotation, portal.rotation);
                rotationDiff += 180;
                player.Rotate(Vector3.up, rotationDiff);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalDistance;
                player.position = portal.position + positionOffset;

                playerCollision = false;
            }
        }
    }
}
