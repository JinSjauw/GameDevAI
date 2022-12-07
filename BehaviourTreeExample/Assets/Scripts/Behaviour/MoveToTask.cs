using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class MoveToTask : BTNode
{
    private Transform _transform;
    private Transform _targetTransform;
    private string _targetKey;
    private Animator _animator;
    public MoveToTask(Transform transform, string target)
    {
        _transform = transform;
        _targetKey = target;
        _animator = transform.GetComponentInChildren<Animator>();
    }
    
    public MoveToTask(Transform transform, Transform targetTransform)
    {
        _transform = transform;
        _targetTransform = targetTransform;
        _animator = transform.GetComponentInChildren<Animator>();
    }
        
    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData(_targetKey);
        //Debug.Log("MoveToTask : transform " + target.name);

        if (target == null)
        {
            _state = NodeState.FAILURE;
            return _state;
        }
        
        //_animator.SetBool("isWalking", true);
        //_animator.SetBool("isIdle", false);
        if (Vector3.Distance(_transform.position, target.position) > 0.01f)
        {
            _animator.SetBool("isWalking", true);
            _transform.position = Vector3.MoveTowards(_transform.position, target.position, GuardTree.speed * Time.deltaTime);
            _transform.LookAt(target.position);
        }
        _state = NodeState.RUNNING;
        return _state;
    }
}
