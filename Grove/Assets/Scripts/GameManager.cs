using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    List<PlantController> trees = new List<PlantController>();
    float growthPercentage = 0f;
    int phase = 0;
    SpawnManager spawnManager;

    void Start()
    {
        spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();
        GameObject[] tempTrees = GameObject.FindGameObjectsWithTag("Tree");
        for(int i = 0; i < tempTrees.Length; i++)
        {
            trees.Add(tempTrees[i].GetComponent<PlantController>());
        }
    }
    //TODO to be called by the plant after changing its state
    public void UpdateGameState()
    {
        float treesRegrown = 0f;
        for(int i = 0; i < trees.Count; i++)
        {
            if (!trees[i].burnt)
            {
                treesRegrown += 1f;
            }
        }
        growthPercentage = (treesRegrown / trees.Count) * 100;
        CheckToChangeEnemyState();
    }
    //TODO add a call to the spawn manager to trigger a boss wave at certain milestones
    void CheckToChangeEnemyState()
    {
        if(growthPercentage == 0)
        {
            //Game over
        }

        if(growthPercentage >= 7 && phase == 0)
        {
            //Enter phase 1
            phase = 1;
            spawnManager.IncreaseEnemiesPerWave(1);
        }
        else if(growthPercentage >= 25 && phase == 1)
        {
            //Enter phase 2
            phase = 2;     
        }
        else if(growthPercentage >= 50 && phase == 2)
        {
            //Enter phase 3
            phase = 3;
            spawnManager.IncreaseEnemiesPerWave(2);
        }
        else if(growthPercentage >= 75 && phase == 3)
        {
            //Enter phase 4
            phase = 4;
        }
        else if(growthPercentage == 100 && phase == 4)
        {
            //Final boss phase
            phase = 5;
        }
    }
}
