using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKey)
        {
            InitiateGame();
        }
    }

    void InitiateGame()
    {
        SceneManager.LoadScene(1);
    }
}
