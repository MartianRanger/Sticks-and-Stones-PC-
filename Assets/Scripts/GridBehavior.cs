using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBehavior : MonoBehaviour //Specific script only for the cave / lava environment level
{
    public int gridX, gridZ; //Length and width of grid
    public GameObject prefab; //Platform
    public float gridOffset = 1f; //How much each platform is separated from each other

    void Start() //At the start it will create a "grid" of platforms 
    {
        for (int x = 0; x < gridX; x++) //Go through length
        {
            for (int z = 0; z < gridZ; z++) //Go through width
            {
                Vector3 spawnPosition = new Vector3(x * gridOffset, 0, z * gridOffset); //Spawn position
                Instantiate(prefab, spawnPosition, Quaternion.identity); // Instantiates prefab
            }

        }

    }
}