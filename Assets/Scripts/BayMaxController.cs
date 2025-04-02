using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BayMaxController : MonoBehaviour
{
    [SerializeField]
    private float m_PlayerSpeed = 4.0f;

    [SerializeField]
    private float m_MouseSensitivity = 200.0f;
    
    [SerializeField]
    private GameObject m_BayMaxHead;
    
    [SerializeField]
    private GameObject m_Camera;
    
    [SerializeField]
    private GameObject Projectile;

    [SerializeField]
    private float m_Demage = 0.5f;

    [SerializeField]
    private MediKitManager m_MediKitManager;
   
    private const int FULL_HEALTH = 30;
    private float m_HealthSpan = FULL_HEALTH;

    private const int HEALTH_CHARGER = 30;
    
    private float m_MaximumRotationAngle = 45.0f;
    private float m_MinimumRotationAngle = -30.0f;

    private float m_HorizontalRotationAngle = 0.0f;
    private float m_VerticalRotationAngle = 0.0f;

    public void FillBayMaxHealth()
    {
        m_HealthSpan = HEALTH_CHARGER;
        StartCoroutine(m_MediKitManager.ReCreateCharger());
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void MoveForward()
    {
        Vector3 playerDeltaPosition = m_PlayerSpeed * transform.forward * Time.deltaTime;
        playerDeltaPosition.y = 0.0f;
        transform.position += playerDeltaPosition;
    }

    public void MoveBackward()
    {
        Vector3 playerDeltaPosition = m_PlayerSpeed * transform.forward * Time.deltaTime;
        playerDeltaPosition.y = 0.0f;
        transform.position -= playerDeltaPosition;
    }

    public void MoveLeft()
    {
        Vector3 playerDeltaPosition = m_PlayerSpeed * transform.right * Time.deltaTime;
        playerDeltaPosition.y = 0.0f;
        transform.position -= playerDeltaPosition;
    }

    public void MoveRight()
    {
        Vector3 playerDeltaPosition = m_PlayerSpeed * transform.right * Time.deltaTime;
        playerDeltaPosition.y = 0.0f;
        transform.position += playerDeltaPosition;
    }

    public void Fire()
    {
        GameObject projectile = Instantiate(Projectile, m_Camera.transform.position, m_Camera.transform.rotation);
    }

    public void MouseMovement(float mouseX, float mouseY)
    {
        m_HorizontalRotationAngle += m_MouseSensitivity * mouseX * Time.deltaTime;
        float verticalRotationDeltaAngle = m_MouseSensitivity * mouseY * Time.deltaTime; 
        m_VerticalRotationAngle = m_BayMaxHead.transform.eulerAngles.x;

        if (m_VerticalRotationAngle > 180)
        {
            m_VerticalRotationAngle -= 360;
        }
        m_VerticalRotationAngle -=verticalRotationDeltaAngle;

        m_VerticalRotationAngle = Mathf.Clamp(m_VerticalRotationAngle, m_MinimumRotationAngle, m_MaximumRotationAngle);
        m_BayMaxHead.transform.rotation = Quaternion.Euler(m_VerticalRotationAngle, m_BayMaxHead.transform.eulerAngles.y, m_BayMaxHead.transform.eulerAngles.z);
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, m_HorizontalRotationAngle, transform.eulerAngles.z);
    }

    public void ReducePlayerHealth()
    {
        m_HealthSpan -= m_Demage;

        if (m_HealthSpan <= 0)
        {
            Destroy();
            return;
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}