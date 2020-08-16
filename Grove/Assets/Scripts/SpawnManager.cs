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

    void Start()
    {
        SpawnSetup();
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(enemySpawnTimer);
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
        StartCoroutine(SpawnEnemy());
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

    public void TriggerBossRound(int roundNumber)
    {

    }
}
