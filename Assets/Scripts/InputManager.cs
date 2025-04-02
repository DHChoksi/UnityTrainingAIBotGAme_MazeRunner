using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    public enum MouseClick
    {
        leftClick,
        rightClick
    }

    public static Action<KeyCode> s_KeyPressed;
    public static Action<float, float> s_MouseDrag;
    public static Action<MouseClick> s_MouseClick;
    
    void Update()
    { 
        if (Input.GetKey(KeyCode.W))
        { 
            s_KeyPressed?.Invoke(KeyCode.W);
        }

        if (Input.GetKey(KeyCode.S))
        {
            s_KeyPressed?.Invoke(KeyCode.S);
        }

        if (Input.GetKey(KeyCode.A))
        {
            s_KeyPressed?.Invoke(KeyCode.A);
        }

        if (Input.GetKey(KeyCode.D))
        {
            s_KeyPressed?.Invoke(KeyCode.D);
        }

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (mouseX != 0 || mouseY != 0)
        {
            s_MouseDrag?.Invoke(mouseX, mouseY);
        }

        if (Input.GetMouseButton(0))
        {
            s_MouseClick?.Invoke(MouseClick.leftClick);
        }
    }
}
