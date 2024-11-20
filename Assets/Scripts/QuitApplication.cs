using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuitApplication : MonoBehaviour
{
    void Update()
    {
        ApplicationQuit();
    }
    void ApplicationQuit()
    {
        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Debug.Log("esc pressed");
            Application.Quit();
        }
    }
}
