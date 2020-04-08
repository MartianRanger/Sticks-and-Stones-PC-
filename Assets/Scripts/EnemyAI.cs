using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    //Script for enemy AI. Not a lot more to say

    private Vector3 startingPosition; //Where they start
    public Transform speechBubbleSpawn; //Where they will spawn the speech Bubble
    public GameObject speechBubble; //Speech Bubble Prefab

    public Transform player; //Variable to keep track of player's position
    public float speechBubbleSpeed = 0.25f; //Factor to force Speech Bubble forward
    public Animator enemyAnimator; //Variable for animator controller

    public int currentHealth = maxHealth; //Sets health on Start
    public const int maxHealth = 100; //Sets health for 100
    public Slider healthBar; //Connects to Slider so we can see his health

    public NavMeshAgent agent;
    public Transform destination;

    bool patrolWaiting;
    float totalWaitTime = 3f;

    float randomSwitch = 0.2f;
    public Transform[] patrolPoints;

    int currentPatrolIndex;
    bool traveling;
    bool waiting;
    bool patrolForward;
    float waitTimer;

    public float chaseRadius = 20f;
    public float facePlayer = 20f;
    public float distToPlayer = 5.0f;

    
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform; //Finds player position in scene
        healthBar.maxValue = maxHealth; //Sets health
        healthBar.value = currentHealth;

        agent = this.GetComponent<NavMeshAgent>();

        patrolPoints = GameObject.FindGameObjectWithTag("EnemySpawn").GetComponent<EnemySpawn>().patrolPoints;
        if (agent == null)
        {
            Debug.Log("No Nav mesh Here");
        }
        else
        {
            agent.enabled = true;

            if (patrolPoints != null && patrolPoints.Length >= 2)
            {
                currentPatrolIndex = 0;
                //SetDestination();
            }
        }
            
    }
      private void SetDestination()
      {
         if (patrolPoints != null)
         {
            Vector3 targetVector = patrolPoints[currentPatrolIndex].transform.position;
            agent.SetDestination(targetVector);
            traveling = true;
         }
      }
    //Possible methods to find player
    private Vector3 GetDirection()
    {
        Vector3 randomDirection = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
        return startingPosition + randomDirection * Random.Range(10f, 70f);
    }
    /*void UpdateTarget()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.LookAt(player.transform.position);
        gunEnd.LookAt(player.transform.position);
    }
    */
    public void TakeDamage(int amount) //General damage method. Used to be its own script, but experimenting with just placing them in here.
    {
        currentHealth -= amount; //Minus the amount specified in the amount
        Debug.Log("Current Health:" + currentHealth); //We always need more debug methods
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Destroy(gameObject);
            Debug.Log("Dead!");
        }
        enemyAnimator.SetInteger("currentHealth", currentHealth); //Has animator state controller keep track of health to know when to die
        healthBar.value = currentHealth; //Sets health bar value to current vvalue

    }

    public void ShootEvent() //Shoot event method connects to animation event for attacking
    {
        Debug.Log("Shooting");
        GameObject temp = new GameObject(); //Creates temporary object and sends them forward
        temp = Instantiate(speechBubble, speechBubbleSpawn.position, speechBubbleSpawn.rotation);

        temp.GetComponent<Rigidbody>().AddForce(speechBubbleSpawn.forward * speechBubbleSpeed, ForceMode.Impulse);

    }
    // Update is called once per frame
    void Update()
    {
        //Code to connect to animation controller to know when to change phases based on distance
        float distance = Vector3.Distance(transform.position, player.position);

        enemyAnimator.SetInteger("distanceFromPlayer", Mathf.RoundToInt(distance));
        //transform.LookAt(player.transform.position);

        //agent.SetDestination(destination.transform.position);

        if (distance > chaseRadius)
        {
            Patrol();
        }
        else if (distance <= chaseRadius)
        {
            //ChasePlayer();
            //AttackPlayer();
        }
        
    }

    private void Patrol()
    {
        if (traveling && agent.remainingDistance > agent.stoppingDistance)
        {
            traveling = false;

            if (patrolWaiting)
            {
                waiting = true;
                waitTimer = 0f;
            }
            else
            {
                ChangePatrolPoint();
                SetDestination();
            }
            //Move code
        }

        if (waiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= totalWaitTime)
            {
                waiting = false;
                ChangePatrolPoint();
                SetDestination();
            }
        }
    }
    private void ChangePatrolPoint()
    {
        if(Random.Range(0f, 1f) <= randomSwitch)
        {
            patrolForward = !patrolForward;
        }
        
        if(patrolForward)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
        else
        {
            if(--currentPatrolIndex<0)
            {
                currentPatrolIndex = patrolPoints.Length - 1;
            }
        }
    }
    private void ChasePlayer()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        enemyAnimator.SetInteger("distanceFromPlayer", Mathf.RoundToInt(distance));

        if(distance <= chaseRadius && distance > distToPlayer)
        {
            agent.SetDestination(player.position);
        }
        else if (distance <= distToPlayer)
        {
            agent.ResetPath();
        }
    }
    void AttackPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * facePlayer);
    }
}
