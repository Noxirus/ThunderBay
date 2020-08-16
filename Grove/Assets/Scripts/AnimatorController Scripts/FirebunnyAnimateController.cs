using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FirebunnyAnimateController : CharacterAnimationController
{
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }

    public override void animateMove()
    {
        animateController.SetFloat("Speed", agent.velocity.magnitude);
    }

    public override void animateAttack()
    {
        animateController.SetTrigger("AttackTrigger");
    }

    public override void animateDamage()
    {
        animateController.SetTrigger("DamageTrigger");
    }

    public override void animateDie()
    {
        animateController.SetTrigger("DieTrigger");
    }
}
