using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBehaviour : StateMachineBehaviour
{
    [SerializeField]
    int maxnum = 2;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo info, int layerIndex)
    {
        animator.SetInteger("Random", Random.Range(0, maxnum));
    }
}