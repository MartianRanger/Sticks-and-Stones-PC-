using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawn : MonoBehaviour //Script specifically for the space level to spawn platforms surrounding the player
{
    public GameObject prefab; //Platform prefab
    
    // Start is called before the first frame update
    void Start()
    {
        instantiateInCircle(prefab, transform.position, 100);
    }

    public void instantiateInCircle(GameObject obj, Vector3 location, int howMany) //Literally what the entire script is for
    {
        for (int i = 0; i < howMany; i++)
        {
            float radius = howMany;
            float angle = i * Mathf.PI * 2f / radius; //Math. Honestly had to look this up.
            Vector3 newPos = transform.position + (new Vector3(Mathf.Cos(angle) * radius, -2, Mathf.Sin(angle) * radius));
            var newPlatform = Instantiate(obj, newPos, Quaternion.Euler(0, 0, 0)); //Instantiates object
            newPlatform.transform.parent = transform;
        }
    }

}
