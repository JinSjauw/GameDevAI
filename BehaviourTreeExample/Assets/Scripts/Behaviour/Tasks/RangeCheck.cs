using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class RangeCheck : BTNode
{
    private Transform _origin;
    private Transform _target;
    private string _targetKey;
    private float _range;

    public RangeCheck(Transform origin, string targetKey, float range)
    {
        _origin = origin;
        _targetKey = targetKey;
        _range = range;
    }
    
    public override NodeState Evaluate()
    {
        Debug.Log("Checking Range");
        _target = (Transform)GetData(_targetKey);
        
        if (_target == null)
        {
            _state = NodeState.FAILURE;
            return _state;
        }

        if (_target.GetComponent<IDamageable>().isDead)
        {
            _state = NodeState.FAILURE;
            return _state;
        }
        
        //Check distance
        float distance = Vector3.Distance(_origin.position, _target.position);
        if (distance < _range)
        {
            SetRootData(_targetKey, _target);
            _state = NodeState.SUCCESS;
            return _state;
        }
        else
        {
            _state = NodeState.FAILURE;
            return _state;
        }
    }
}
