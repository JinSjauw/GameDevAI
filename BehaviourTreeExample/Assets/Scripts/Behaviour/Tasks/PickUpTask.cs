using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class PickUpTask : BTNode
{
    private Transform _agent;
    private Transform _target;
    private string _targetKey;
    private Animator _animator;

    //private Animator _animator;
    public PickUpTask(Transform agent, string targetKey)
    {
        _agent = agent;
        _targetKey = targetKey;
        _animator = agent.GetComponentInChildren<Animator>();
        //_target = (Transform)GetData(target);
    }

    public override NodeState Evaluate()
    {
        _target = (Transform)GetData(_targetKey);
        if (_target.parent == _agent)
        {
            _state = NodeState.SUCCESS;
            return _state;
        }
        //Check if in range;
        float distance = Vector3.Distance(_agent.position, _target.position);
        if (distance <= 1f)
        {
            Debug.Log("Picked Up Weapon");
            _target.parent = _agent;
            _state = NodeState.SUCCESS;
            return _state;
        }

        _state = NodeState.RUNNING;
        return _state;
    }
}
