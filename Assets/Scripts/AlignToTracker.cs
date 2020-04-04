using UnityEngine;
using System.Collections;

public class AlignToTracker : MonoBehaviour
{

    public Transform hmdOrientation; // In the inspector add the "OVRCameraRig/CenterEyeAnchor"

    private Vector3 tempRot;

    // Update is called once per frame
    void Update()
    {
        tempRot = new Vector3(5, 0, 0);
        // Hack to align avatar head-bone to tracker orientation
        //tempRot.x = -hmdOrientation.localEulerAngles.y;
        tempRot.y = hmdOrientation.localEulerAngles.z;
        tempRot.z = -hmdOrientation.localEulerAngles.x;

        // Update head rotation
        transform.localEulerAngles = tempRot;

    }

}
