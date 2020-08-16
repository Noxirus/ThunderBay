using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    List<PlantController> trees = new List<PlantController>();
    float growthPercentage = 0f;
    void Start()
    {
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
        Debug.Log(trees.Count);
        for(int i = 0; i < trees.Count; i++)
        {
            if (!trees[i].burnt)
            {
                treesRegrown += 1f;
            }
        }
        growthPercentage = (treesRegrown / trees.Count) * 100;
        Debug.Log("Game state updated : " + growthPercentage + " %");
    }
    //TODO add a call to the spawn manager to trigger a boss wave at certain milestones
}
