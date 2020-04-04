using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {
    public GameObject toObject;
    public Vector3 origPoint;
    float distance;
    bool reached = false;
    public GameObject player;
    public void Start()
    {
        origPoint = transform.position;
    }

    public void Update()
    {
        if (!reached)
        {
            distance = Vector3.Distance(transform.position, toObject.transform.position);
            if (distance > .1)
            {
                move(transform.position, toObject.transform.position);
            }
            else
            {
                reached = true;
            }
        }
        else
        {
            distance = Vector3.Distance(transform.position, origPoint);
            if (distance > .1)
            {
                move(transform.position, origPoint);
            }
            else
            {
                reached = false;
            }
        }
    }

    void move(Vector3 pos, Vector3 towards)
    {
        transform.position = Vector3.MoveTowards(pos, towards, .1f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            player.transform.parent = transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            player.transform.parent = transform;
        }
    }
    /*
    public Transform platformMover;
    public Transform startingPosition;
    public Transform endingPosition;
    public float speed;

	// Use this for initialization
	void Start () {
        PlatformMover();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void PlatformMover()
    {

    }*/
}
