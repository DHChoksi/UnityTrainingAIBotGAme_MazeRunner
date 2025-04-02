using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{   
    [SerializeField]
    private int m_NumberOfEnemies = 10;  
    
    [SerializeField]
    private GameObject EnemyController;

    [SerializeField]
    List<Transform> m_SpawnPointsList = new List<Transform>();

    void Start()
    {
        if (m_NumberOfEnemies <= m_SpawnPointsList.Count)
        {
            InitiateRandomSpawnIndex();
        }
        else
        {
            Debug.Log("Number of enemies must be <= "+ m_SpawnPointsList.Count);
        }
    }

    void InitiateRandomSpawnIndex()
    {
        int index, spawnIndex;

        for (index = 0; index < m_NumberOfEnemies; index++)
        {
            spawnIndex = Random.Range(0, m_SpawnPointsList.Count);
            SpawnEnemy(spawnIndex);
        }
    }

    void SpawnEnemy(int spawnIndex)
    {
        Instantiate(EnemyController, m_SpawnPointsList[spawnIndex].transform.position, Quaternion.identity);
        m_SpawnPointsList.RemoveAt(spawnIndex);
    }
}
