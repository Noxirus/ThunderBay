using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    float splashTimer = 3f;
    bool readyToPlay = false;
    [SerializeField] GameObject aGJLogo = null;
    [SerializeField] GameObject teamLogo = null;
    [SerializeField] GameObject directionsSplash = null;

    private void Start()
    {
        StartCoroutine(TeamLogoSplashTimer());
    }

    IEnumerator TeamLogoSplashTimer()
    {
        yield return new WaitForSeconds(splashTimer);
        teamLogo.SetActive(false);
        StartCoroutine(AGJSplashTimer());
    }

    IEnumerator AGJSplashTimer()
    {
        aGJLogo.SetActive(true);
        yield return new WaitForSeconds(splashTimer);
        directionsSplash.SetActive(true);
        aGJLogo.SetActive(false);
        readyToPlay = true;
    }

    void Update()
    {
        if (Input.anyKey && readyToPlay)
        {
            InitiateGame();
        }
    }

    void InitiateGame()
    {
        SceneManager.LoadScene(1);
    }
}
