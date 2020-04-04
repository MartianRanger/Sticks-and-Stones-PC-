using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitAroundPlayer : MonoBehaviour //Animation script solely for the main menu
{
    public float speed; //Look up definition of speed and this is it
    public Vector3 originPoint; //The point they will orbit around
    public int randomFactor; //Choice to determine if they will move in one direction or another

    // Start is called before the first frame update
    void Start()
    {
        originPoint = new Vector3(-2.0f, 0.0f, -5.0f); //Hardcode in, just in case because I was having some issues
        speed = Random.Range(5, 10); //Random speed 
        randomFactor = Random.Range(1, 10); //Random Factor
    }

    // Update is called once per frame
    void Update()
    {
        if (randomFactor < 5 && randomFactor > 1) //If random is between 1 and 5, go forward
        {
            transform.RotateAround(originPoint, Vector3.up, speed * Time.deltaTime);
        }
        else //If not, go back
        {
            transform.RotateAround(originPoint, Vector3.down, speed * Time.deltaTime);
        }
    }
}
