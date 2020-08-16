using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this is class to handle object class.
public class CharacterAnimationController : MonoBehaviour
{
    public Animator animateController;

    public virtual void animateIdle() {
    }

    public virtual void animateMove()
    {

    }

    public virtual void animateAttack()
    {

    }

    public virtual void animateDamage()
    {

    }

    public virtual void animateDie()
    {

    }
}
