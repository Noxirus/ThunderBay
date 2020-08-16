using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FireSlimeAnimateController : CharacterAnimationController
{
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public override void animateAttack()
    {
        animateController.SetTrigger("AttackTrigger");
    }

    public override void animateDie()
    {
        animateController.SetTrigger("DieTrigger");
    }

    public override void animateMove()
    {
        animateController.SetFloat("Speed", agent.velocity.magnitude);
    }

}
