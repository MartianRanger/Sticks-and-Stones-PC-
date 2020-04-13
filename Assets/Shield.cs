using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Shield : MonoBehaviour
{
    [SerializeField]
    GameObject shield;

    public Vector3 startingPosition;
    bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = shield.transform.position;
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider col)
    {
        Debug.Log("IS ACTIVE");
        if(!isActive)
        {
            isActive = true;
            shield.transform.position = transform.position + new Vector3(0, 100, 0);
        }
    }
    void OnTriggerExit(Collider col)
    {
        isActive = false;
        shield.transform.position = startingPosition;
    }
}
