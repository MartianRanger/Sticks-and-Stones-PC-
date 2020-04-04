using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimation : MonoBehaviour //Animation script solely for the main menu
{
    public GameObject speechBubble; //Prefab variable
    public Vector3 spawnValues; //Maximum and minimum values of where the bubbles can be created
    public int spawnCounter; //How many you spawn
    public int randomFactor; //How much they are separated by

    // Start is called before the first frame update
    void Start()
    {
        for (int spawn = 0;spawn< spawnCounter; spawn++) //Loop for spawning prefabs
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), Random.Range(-spawnValues.y, spawnValues.y), 1);
            Instantiate(speechBubble, spawnPosition, gameObject.transform.rotation);            
        }
    }
    
}
