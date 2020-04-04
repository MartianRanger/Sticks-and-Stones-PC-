using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
    public GameObject prefab;
    public float prefabSize = 9000000f;
    public int prefabNumber = 1;
    public int explosionRadius;
    public float explosionForce = 1f;
    float prefabDistance;
    Vector3 explosion;
	// Use this for initialization
	void Start () {
        prefabDistance = prefabSize * prefabNumber / 2; //Calculates explosion distance
        explosion = new Vector3(prefabDistance, prefabDistance, prefabDistance); //new explosion vector created from the last line
	}
	public void Explode()
    {
        Debug.Log("EXPLOSION!");
        gameObject.SetActive(false); // makes gameObject dissapear

        for (int x = 0; x < prefabNumber; x++) //loop to create pieces in x, y, z coordinates
        {
            for (int y = 0; y < prefabNumber; y++)
            {
                for (int z = 0; z < prefabNumber; z++)
                {
                    createObject(x, y, z);
                }
            }
        }

        Vector3 explosionPos = transform.position; //explosion position
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius); //get colliders in the vicinity of explosion
        foreach (Collider hit in colliders) //moves objects that are affected by explosion vincinity
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }
    }
    void createObject(int x, int y, int z)
    {
        //Create debris
        GameObject debris = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
        //sets scale and position
        debris.transform.position = transform.position + new Vector3(prefabSize * x, prefabSize * y, prefabSize * z) - explosion;
        debris.transform.localScale = new Vector3(5, 5, 5);

        Destroy(debris, Random.Range(0.5f, 5f));


        debris.AddComponent<Rigidbody>(); //adds rigidbody (might be useless in some areas)
        debris.GetComponent<Rigidbody>().mass = prefabSize; //sets mass
    }
}
