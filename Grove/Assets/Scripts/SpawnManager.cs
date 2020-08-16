using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    GameObject[] enemySpawnAreas;
    float enemySpawnTimer = 5f;
    [SerializeField]GameObject enemyPrefab = null;
    List<GameObject> enemies = new List<GameObject>();
    [SerializeField]int numberOfMaxEnemies = 10;
    [SerializeField] int enemiesPerWave = 0;

    void Start()
    {
        SpawnSetup();
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(enemySpawnTimer);
        Debug.Log("Wave spawned");
        if(enemiesPerWave >= 0)
        {
            for (int i = 0; i < enemiesPerWave; i++)
            {
                SpawnSingleEnemy();
            }
        }
        StartCoroutine(SpawnWave());
    }

    void SpawnSingleEnemy()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            //TODO Potential edge case if there are all enemies on the field then this could continue forever
            if (!enemies[i].activeInHierarchy)
            {
                enemies[i].transform.position = RandomRespawn().transform.position;
                enemies[i].SetActive(true);
                break;
            }
        }
    }

    Transform RandomRespawn()
    {
        int randomRespawn = Random.Range(0, enemySpawnAreas.Length);
        return enemySpawnAreas[randomRespawn].transform;
    }

    void SpawnSetup()
    {
        Transform spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<Transform>();
        for(int i = 0; i < numberOfMaxEnemies; i++)
        {
            GameObject temporaryEnemy = Instantiate(enemyPrefab, spawnManager);
            enemies.Add(temporaryEnemy);
            temporaryEnemy.SetActive(false);
        }

        enemySpawnAreas = GameObject.FindGameObjectsWithTag("EnemySpawn");
    }

    public void IncreaseEnemiesPerWave(int amount)
    {
        enemiesPerWave = amount;
    }

    public void TriggerBossRound(int roundNumber)
    {

    }
}
