using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediKitManager : MonoBehaviour
{
    private BayMaxController m_BayMaxController;

    [SerializeField]
    private GameObject m_Charger;

    private const int HEALTH_LAYER = 13;
    private const int PLAYER_LAYER = 12;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hii");
        if (collision.gameObject.layer == PLAYER_LAYER)
        {
            m_BayMaxController.FillBayMaxHealth();
        }
    }
    

    public IEnumerator ReCreateCharger()
    {
        yield return new WaitForSeconds(10.0f);
        Instantiate(m_Charger);
    }
}
