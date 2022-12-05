using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class MoveToTask : BTNode
{
    private Transform _transform;
    private Animator _animator;
    public MoveToTask(Transform transform)
    {
        _transform = transform;
        transform.GetComponentInChildren<Animator>();
    }
        
    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        Debug.Log("MoveToTask : transform " + target);

        //_animator.SetBool("isWalking", true);
        //_animator.SetBool("isIdle", false);
        if (Vector3.Distance(_transform.position, target.position) > 0.01f)
        {
            _transform.position = Vector3.MoveTowards(_transform.position, target.position, GuardTree.speed * Time.deltaTime);
            _transform.LookAt(target.position);
        }
        state = NodeState.RUNNING;
        return state;
    }
}
