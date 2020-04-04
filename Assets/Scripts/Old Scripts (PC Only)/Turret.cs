using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Turret : MonoBehaviour
{
    Transform player;
    public Transform gunEnd;
    public GameObject bullet;
    private NavMeshAgent nav;
    public float navigationUpdate;
    private float navigationTime = 0;
    private float rotationSpeed = 3.0f;
    private float moveSpeed = 3.0f;
    public Transform speechBubbleSpawn;
    // Use this for initialization
    void Awake()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);//search target right at beginning, at a rate of .5 seconds
        nav = GetComponent<NavMeshAgent>();
        nav.Warp(transform.position);
        nav.enabled = true;
    }
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (player != null)
        {
            if (distance <= 110 && distance >= 15)
            {
                /*
                navigationTime += Time.deltaTime;
                if (navigationTime > navigationUpdate)
                {
                    if(!nav.isOnNavMesh)
                    {
                        navigationTime = 0;
                        nav.enabled = false;
                        nav.enabled = true;
                        Debug.Log(nav.isOnNavMesh);
                    }
                    nav.SetDestination(player.position);
                }*/
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position), rotationSpeed * Time.deltaTime);
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
                Debug.Log("CHASING");
            }
        }
    }

    // Update is called once per frame
    void UpdateTarget()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.LookAt(player.transform.position);
        gunEnd.LookAt(player.transform.position);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player Detected");
            StartCoroutine("Shooting");
        }

    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StopCoroutine("Shooting");
        }
    }
    IEnumerator Shooting()
    {
        while (true)
        {
            GameObject temp = new GameObject();
            //Destroy(temp, 2f);
            temp = Instantiate(bullet, gunEnd.position, gunEnd.rotation);
            temp.transform.parent = transform;
            float length = temp.GetComponent<AudioSource>().clip.length + 5;

            yield return new WaitForSeconds(length);
        }
    }
}
