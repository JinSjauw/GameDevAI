using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class MoveToTask : BTNode
{
    private Transform _agent;
    private Transform _target;
    private string _targetKey;
    private Animator _animator;
    private float _speed;
    public MoveToTask(Transform agent, string target, float speed)
    {
        _agent = agent;
        _targetKey = target;
        _animator = agent.GetComponentInChildren<Animator>();
        _speed = speed;
    }
    
    public MoveToTask(Transform agent, Transform targetTransform)
    {
        _agent = agent;
        _target = targetTransform;
        _animator = agent.GetComponentInChildren<Animator>();
    }
        
    public override NodeState Evaluate()
    {
        _target = (Transform)GetData(_targetKey);
        
        if (_target == null)
        {
            _state = NodeState.FAILURE;
            return _state;
        }
        
        float distance = Vector3.Distance(_agent.position, _target.position);
        
        if (distance <= 1f)
        {
            //_animator.SetBool("isWalking", false);
            //_animator.SetBool("isIdle", true);
            
            Debug.Log(" Target Reached");
            
            _state = NodeState.SUCCESS;
            return _state;
        }
        else if(distance > 0.01f)
        {
            _animator.SetBool("isWalking", true);
            _agent.position = Vector3.MoveTowards(_agent.position, _target.position, _speed * Time.deltaTime);
            Debug.Log("Walkin!: " + _target.position);
            _agent.LookAt(_target.position);
        }
        
        _state = NodeState.RUNNING;
        return _state;
    }
}
