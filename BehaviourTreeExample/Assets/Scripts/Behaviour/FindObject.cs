using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class FindObject : BTNode
{
    private string _targetObject;
    private Transform[] _objectList;
    public FindObject(string targetObject, Transform[] objectList)
    {
        _targetObject = targetObject;
        _objectList = objectList;
    }

    public override NodeState Evaluate()
    {
        var selectedObject = _objectList[Random.Range(0, _objectList.Length - 1)];
        
        if (selectedObject != null)
        { 
            SetData(_targetObject, selectedObject);
            
            _state = NodeState.SUCCESS;
            return _state;
        }
        _state = NodeState.FAILURE;
        return _state;
    }
}
