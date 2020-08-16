using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public int progressCount;
    private float currentTime = 0;

    public AudioSource musicPiece1;
    public AudioSource musicPiece2;
    public AudioSource musicPiece3;
    public AudioSource musicPiece4;
    public AudioSource musicPiece5;
    public AudioSource musicPiece6;
    public AudioSource musicPiece7;
    //Only thing that needs to hook it up to the other script that keeps track of progress, however we decide to do that
    private void Start()
    {
        progressCount = 1;
    }
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            progressCount++;
            currentTime = 0;
        }
        if (progressCount == 2)
        {
            currentTime += Time.deltaTime;
            musicPiece2.volume = currentTime;
        }
        if (progressCount == 3)
        {
            currentTime += Time.deltaTime;
            musicPiece3.volume = currentTime;
        }
        if (progressCount == 4)
        {
            currentTime += Time.deltaTime;
            musicPiece4.volume = currentTime;
        }
        if (progressCount == 5)
        {
            currentTime += Time.deltaTime;
            musicPiece5.volume = currentTime;
        }
        if (progressCount == 6)
        {
            currentTime += Time.deltaTime;
            musicPiece6.volume = currentTime;
        }
        if (progressCount == 7)
        {
            currentTime += Time.deltaTime;
            musicPiece7.volume = currentTime;
        }
        //This is so the incrementation doesn't get crazy high values
        if (currentTime > 1)
        {
            currentTime = 1;
        }
    }
}
