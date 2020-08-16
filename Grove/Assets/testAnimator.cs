using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NOTE: THIS SCRIPT WAS ADDED PURELY FOR TESTING PURPOSES
public class testAnimator : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown("w"))
        {
            anim.Play("Run", 0, 0f);
        }
        if (Input.GetKeyUp("w"))
        {
            anim.Play("Idle", 0, 0f);
        }
        if (Input.GetKeyDown("s"))
        {
            anim.Play("Backwards", 0, 0f);
        }
        if (Input.GetKeyUp("s"))
        {
            anim.Play("Idle", 0, 0f);
        }
        if (Input.GetKeyDown("e"))
        {
            anim.Play("Attack", 0, 0f);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.Play("TurnLeft90", 0, 0f);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.Play("TurnRight90", 0, 0f);
        }
        if (Input.GetKeyDown("f"))
        {
            anim.Play("Dying", 0, 0f);
        }
        if (Input.GetKeyDown("r"))
        {
            anim.Play("GettingHit", 0, 0f);
        }
    }
}
