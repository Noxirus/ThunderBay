﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    GameObject target;
    NavMeshAgent navigation;
    GameObject[] trees;
    GameObject player;

    void Awake()
    {
        navigation = GetComponent<NavMeshAgent>();
        trees = GameObject.FindGameObjectsWithTag("Tree");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnEnable()
    {
        ChooseTarget();
    }

    void ChooseTarget()
    {
        int targetChoice = Random.Range(0, 2);
        if(targetChoice == 0)
        {
            int treeChoice = Random.Range(0, trees.Length);
            target = trees[treeChoice];
        }
        else
        {
            target = player;
        }
        navigation.SetDestination(target.transform.position);
    }
}
