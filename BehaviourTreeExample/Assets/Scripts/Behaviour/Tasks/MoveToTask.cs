using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;
using UnityEngine.AI;

public class MoveToTask : BTNode
{
    private NavMeshAgent _agent;
    private Transform _target;
    private float _range;
    private string _targetKey;
    private Animator _animator;
    private float _waitCounter = 0;
    public MoveToTask(NavMeshAgent agent, string target, float range)
    {
        _agent = agent;
        _targetKey = target;
        _range = range;
        _animator = agent.GetComponentInChildren<Animator>();
    }
    
    public MoveToTask(NavMeshAgent agent, string target)
    {
        _agent = agent;
        _targetKey = target;
        _range = 1f;
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

        float distance = Vector3.Distance(_agent.transform.position, _target.position);
        if (distance <= _range)
        {
            Debug.Log(" Target Reached " + _target.name);
            _animator.SetBool("isWalking", false);
            _state = NodeState.SUCCESS;
            return _state;
        }
        
        if(distance > 0.01f)
        {
            _animator.SetBool("isWalking", true);
            _agent.destination = _target.position;
        }
        
        _state = NodeState.RUNNING;
        return _state;
    }
}
