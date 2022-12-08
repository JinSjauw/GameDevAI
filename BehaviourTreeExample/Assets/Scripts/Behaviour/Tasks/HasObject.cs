using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class HasObject : BTNode
{
    private string _targetObject;
    public HasObject(string targetObject)
    {
        _targetObject = targetObject;
    }

    public override NodeState Evaluate()
    {
        var targetObject = GetData(_targetObject);
        
        if (targetObject != null)
        {
            //Debug.Log("Has Object! " + targetObject);
            _state = NodeState.SUCCESS;
            return _state;
        }

        _state = NodeState.FAILURE;
        return _state;
    }
}
