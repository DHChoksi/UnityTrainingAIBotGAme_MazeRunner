using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Projectile : MonoBehaviour
{
    private const int ENEMY_LAYER = 8;
    private const int PLAYER_LAYER = 12;
    private int m_DestroyTimer = 5;

    private bool m_FiredProjectile = false;
    
    [SerializeField]
    private float m_ShootingForce;
    
    private void Awake()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * m_ShootingForce);
    }
    
    void OnCollisionEnter(Collision collision)
    { 
        if (collision.gameObject.layer != ENEMY_LAYER && collision.gameObject.layer != PLAYER_LAYER)
        {
            m_FiredProjectile = true;       
        }
        
        if (collision.gameObject.layer == ENEMY_LAYER)
        {
            
            Enemy m_EnemyScript = collision.gameObject.GetComponent<Enemy>();
            
            if (m_FiredProjectile == false)
            {
                m_EnemyScript.ReduceHealth();
            }
        }
        Destroy();
    }

    public void Destroy()
    {
        Destroy(gameObject, m_DestroyTimer);
    }
}
