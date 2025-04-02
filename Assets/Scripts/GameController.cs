using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private BayMaxController m_Player;

    private void Awake()
    {
        m_Player = GameObject.Find("BayMax Controller").GetComponent<BayMaxController>();
    }
    void OnEnable()
    {
        InputManager.s_KeyPressed += KeyPressed;
        InputManager.s_MouseDrag += MouseMovement;
        InputManager.s_MouseClick += MouseClick;
    }

    void OnDisable()
    {
        InputManager.s_KeyPressed -= KeyPressed;
        InputManager.s_MouseDrag -= MouseMovement;
        InputManager.s_MouseClick -= MouseClick;
    }

    void KeyPressed(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.W:
            {
                m_Player.MoveForward();
                break;
            }

            case KeyCode.S:
            {
                m_Player.MoveBackward();
                break;
            }

            case KeyCode.A:
            {
                m_Player.MoveLeft();
                break;
            }

            case KeyCode.D:
            {
                m_Player.MoveRight();
                break;
            }
        }
    }

    void MouseMovement(float mouseX, float mouseY)
    {
        m_Player.MouseMovement(mouseX, mouseY);
    }

    void MouseClick(InputManager.MouseClick leftClick)
    {
        m_Player.Fire();
    }
}
