using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Turret : MonoBehaviour
{
    Transform player;
    public Transform gunEnd;
    public GameObject bullet;
    public float navigationUpdate;
    private float navigationTime = 0;
    private float rotationSpeed = 3.0f;
    private float moveSpeed = 3.0f;
    public Transform speechBubbleSpawn;
    // Use this for initialization

    public Animator enemyAnimator; //Variable for animator controller

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; //Finds player position in scene
    }
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        enemyAnimator.SetInteger("distanceFromPlayer", Mathf.RoundToInt(distance));

        transform.LookAt(player.transform.position);

        //Debug.Log(distance);
    }

    /*IEnumerator Shooting()
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
    }*/
}
