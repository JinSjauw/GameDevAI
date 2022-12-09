using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class HasObject : BTNode
{
    private Transform _container;
    private string _targetObject;
    public HasObject(Transform container, string targetObject)
    {
        _container = container;
        _targetObject = targetObject;
    }

    public override NodeState Evaluate()
    {
        Transform targetObject = (Transform)GetData(_targetObject);
        
        if (targetObject != null && targetObject.IsChildOf(_container))
        {
            Debug.Log("Has Object! " + targetObject);
            _state = NodeState.SUCCESS;
            return _state;
        }

        _state = NodeState.FAILURE;
        return _state;
    }
}
