using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {
    public GameObject bomb;
    public float power = 10.0f;
    public float radius = 5.0f;
    public float force = 1.0f;

    public GameObject explosionPrefab;

    // Use this for initialization
    void Start () {
		
	}
	void FixedUpdate()
    {
        if (bomb == enabled)
        {
            Invoke("Detonate", 5);
        }
    }
	// Update is called once per frame
	void Detonate() {
        Vector3 explosionPosition = bomb.transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);
        foreach(Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(power, explosionPosition, radius, force, ForceMode.Impulse);
            }
        }
        GameObject explosion = (GameObject)Instantiate(explosionPrefab, transform.position, explosionPrefab.transform.rotation);
        Destroy(explosion, explosion.GetComponent<ParticleSystem>().main.startLifetimeMultiplier);

        Destroy(gameObject);
        Debug.Log("BOMB!");
	}
}
