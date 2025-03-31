using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class ActionCommands : MonoBehaviour
{
    public bool inputEnabled = false;
    public string inputString;
    private void Start()
    {
        if (GameObject.FindGameObjectsWithTag("Text").Length == 1)
        {
            gameObject.name = "Text1";
        }
        else
        {
            gameObject.name = "Text2";
        }
    }

    public void Input(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            inputString += context.action.name;
        }
    }
}
