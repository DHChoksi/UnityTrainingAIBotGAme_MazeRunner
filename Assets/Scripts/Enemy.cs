using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float m_Demage = 5.0f;

    [SerializeField]
    private float m_EnemySpeed = 2.0f;

    [SerializeField]
    private float m_TurningSpeed = 25.0f;

    [SerializeField]
    private float m_MinimumDistance = 2.0f;

    [SerializeField]
    private float m_IdleStateTimeOut = 4.0f;
    
    [SerializeField]
    private float m_WalkingStateTimeOut = 15.0f;

    [SerializeField]
    private Text m_HealthText;

    [SerializeField]
    private GameObject m_Head;

    [SerializeField]
    private GameObject m_Mouth;

    private const float DIE_DELAY = 2.0f;
    private const int FULL_HEALTH = 20;
    private float m_HealthSpan = FULL_HEALTH;

    private const string IS_DYING = "IsDying";
    private const string IS_WALKING = "IsWalking";
    private const string IS_ATTACKING = "IsAttacking";

    private const int ENEMY_LAYER = 8;
    private const int OTHEROBJECTS_LAYER = 9;
    private const int WALL_LAYER = 10;
    private const int PLAYER_LAYER = 12;

    private int m_AllLayers = 1 << ENEMY_LAYER | 1 << OTHEROBJECTS_LAYER | 1 << WALL_LAYER;
    private bool m_PlayerInViewSite = false;
    private const int MINIMUM_ANGLE = 45;
    private const int MAXIMUM_ANGLE = 136;

    private const int WALKING_STATE_MINIMUM_TIME = 15;
    private const int WALKING_STATE_MEXIMUM_TIME = 25;

    private const int IDLE_STATE_MINIMUM_TIME = 3;
    private const int IDLE_STATE_MEXIMUM_TIME = 8;

    private float m_FinalAngle;

    private int m_ViewFieldRadius = 6;
    private int m_NumberOfRays = 91;

    private Animator m_Animator;

    [SerializeField]
    private Projectile m_ProjectileScript;

    public enum States
    {
        Idle,
        Walking,
        TurnAround,
        AttackPlayer,
        Dead
    }

    States m_CurrentState = States.Idle;
    States m_PreviousState;

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        detectplayer();
        m_HealthText.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 20.0f, gameObject.transform.position.z);

        if (m_CurrentState == States.Dead)
        {
            return;
        }

        else if (m_CurrentState == States.Idle)
        {
            m_Animator.SetBool(IS_WALKING, false);

            if (m_CurrentState != m_PreviousState)
            {
                m_IdleStateTimeOut = Random.Range(IDLE_STATE_MEXIMUM_TIME, IDLE_STATE_MINIMUM_TIME);
                m_PreviousState = m_CurrentState;
            }

            if (m_IdleStateTimeOut <= 0)
            {
                m_Animator.SetBool(IS_WALKING, true);
                m_CurrentState = States.Walking;
            }
            else
            {
                m_IdleStateTimeOut -= Time.deltaTime;
            }
        }

        else if (m_CurrentState == States.Walking)
        {
            m_Animator.SetBool(IS_WALKING, true);

            if (m_CurrentState != m_PreviousState)
            {
                m_WalkingStateTimeOut = Random.Range(WALKING_STATE_MINIMUM_TIME, WALKING_STATE_MEXIMUM_TIME);
                m_PreviousState = m_CurrentState;
            }

            if (m_WalkingStateTimeOut <= 0)
            {
                m_Animator.SetBool(IS_WALKING, false);
                m_CurrentState = States.Idle;
            }

            else
            {
                DetectObstracles();

                m_Animator.SetBool(IS_WALKING, true);

                Vector3 DeltaPosition = m_EnemySpeed * transform.forward * Time.deltaTime;
                DeltaPosition.y = 0.0f;
                transform.position += DeltaPosition;

                m_WalkingStateTimeOut -= Time.deltaTime;
            }
        }

        else if (m_CurrentState == States.TurnAround)
        {
            m_Animator.SetBool(IS_WALKING, false);

            float currentangle = transform.eulerAngles.y;

            if (m_CurrentState != m_PreviousState)
            {
                float randomangle = Random.Range(MINIMUM_ANGLE, MAXIMUM_ANGLE);
                m_FinalAngle = transform.eulerAngles.y + randomangle;

                if (m_FinalAngle > 360)
                {
                    m_FinalAngle -= 360;
                    currentangle = 360 - currentangle;
                }
            }

            currentangle += Time.deltaTime * m_TurningSpeed;
            transform.rotation = Quaternion.Euler(0.0f, currentangle, 0.0f);

            if (currentangle >= m_FinalAngle && m_PreviousState == m_CurrentState)
            {
                m_PreviousState = m_CurrentState;
                m_CurrentState = States.Walking;
            }

            m_PreviousState = m_CurrentState;
        }
        else if (m_CurrentState == States.AttackPlayer)
        {
            m_Animator.SetBool(IS_ATTACKING, true);
            
            RaycastHit hit;

            if (Physics.Raycast(m_Mouth.transform.position, m_Mouth.transform.forward, out hit, m_ViewFieldRadius))
            {
                m_PreviousState = m_CurrentState;

                if (hit.transform.gameObject.layer != PLAYER_LAYER)
                {
                    m_PlayerInViewSite = false;
                    m_Animator.SetBool(IS_ATTACKING, false);
                    m_CurrentState = States.Idle;
                }
                
                if (m_PlayerInViewSite == true)
                {
                    BayMaxController m_BayMaxControllerScript = hit.collider.gameObject.GetComponent<BayMaxController>();
                    m_BayMaxControllerScript.ReducePlayerHealth();
                }
            }
        }
    }

    void DetectObstracles()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, m_MinimumDistance, m_AllLayers))
        {
            m_CurrentState = States.TurnAround;
        }
    }

    public void ReduceHealth()
    {
        m_HealthSpan -= m_Demage;
        if (m_HealthSpan <= 0)
        {
            m_Animator.SetBool(IS_DYING, true);
            m_CurrentState = States.Dead;
            return;
        }
    }

    public void Destroy()
    {
        Destroy(gameObject, DIE_DELAY);
    }

    public void detectplayer()
    {
        float rayAngle = -45;
        RaycastHit hit;

        for (var index = 0; index <= m_NumberOfRays; index++)
        {
            Vector3 direction = Quaternion.AngleAxis(rayAngle, transform.up) * transform.forward;
            
            if (Physics.Raycast(transform.position, direction, out hit, m_ViewFieldRadius, 1 << PLAYER_LAYER))
            { 
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + rayAngle, transform.eulerAngles.z);
                m_PlayerInViewSite = true;
                m_CurrentState = States.AttackPlayer;
            }
            rayAngle++;
        }
    }
}