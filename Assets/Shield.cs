using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Shield : MonoBehaviour
{
    [SerializeField]
    GameObject shield;

    public Vector3 startingPosition;
    bool isActive = false;
    public Transform bottom; //Bottom of the doorway
    public Transform top; //Top of the doorway

    public float doorSpeed = 2f; //How fast the door moves

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = new Vector3(0.0f, 24.0f, 0.0f);
        //bottom = shield.transform;
        //top = shield.transform;
        //top += 24;
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider col)
    {
        Debug.Log("IS ACTIVE");
        if(!isActive && col.gameObject.tag == "Player")
        {
            isActive = true;
            StopCoroutine("CloseShield");
            StartCoroutine("ActivateShield");
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            isActive = false;
            StopCoroutine("ActivateShield");
            StartCoroutine("CloseShield");
            Debug.Log("NOT ACTIVE");
        }
    }
    IEnumerator ActivateShield()
    {
        while (Vector3.Distance(shield.transform.position, top.position) > 0.1f) //While the distance from the floor to the door is greater than 0.1f...
        {
            shield.transform.position = Vector3.Lerp(shield.transform.position, top.position, doorSpeed * Time.deltaTime); //..The door moves up, opening it.

            yield return null;
        }
    }
    IEnumerator CloseShield()
    {
        while (Vector3.Distance(shield.transform.position, bottom.position) > 0.1f) //While the distance from the floor to the door is less than 0.1f...
        {
            shield.transform.position = Vector3.Lerp(shield.transform.position, bottom.position, doorSpeed * Time.deltaTime); //..The door moves down, closing it.

            yield return null;
        }
    }

}
