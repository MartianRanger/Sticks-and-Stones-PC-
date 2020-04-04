using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    //This is a script that basically handles a lot of the UI stuff

    public void OpenPanel(GameObject panel) //Panel is basically used to group a bunch of UI buttons together so they can be a menu and navigate without going to different scenes
    {
        if (panel != null) //Basically if the chosen panel is null, it turns it on or vice versa.
        {
            bool isActive = panel.activeSelf;
            panel.SetActive(!isActive);
        }
    }
}
