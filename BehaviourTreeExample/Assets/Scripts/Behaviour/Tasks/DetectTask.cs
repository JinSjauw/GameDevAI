using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;
using UnityEngine.UIElements;

public class DetectTask : BTNode
{
    private Transform _agent;
    private Transform _target;
    private string _targetKey;
    private float _range;
    private bool detectedOnce = false;

    public DetectTask(Transform agent, string targetKey, float range)
    {
        _agent = agent;
        _range = range;
        _targetKey = targetKey;
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
        if (distance < _range)
        {
            detectedOnce = true;
            _state = NodeState.SUCCESS;
            return _state;
        }
        else if(distance > _range && detectedOnce)
        {
            _state = NodeState.SUCCESS;
            return _state;
        }
        
        _state = NodeState.FAILURE;
        return _state;
    }
}
