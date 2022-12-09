using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class FindObject : BTNode
{
    private string _targetObject;
    private Transform[] _objectList;
    Transform selectedObject = null;
    public FindObject(string targetObject, Transform[] objectList)
    {
        _targetObject = targetObject;
        _objectList = objectList;
    }

    public override NodeState Evaluate()
    {
        if (selectedObject == null)
        {
           selectedObject = _objectList[Random.Range(0, _objectList.Length)];
           SetRootData(_targetObject, selectedObject);
           _state = NodeState.SUCCESS;
           return _state;
        }
        if (selectedObject != null)
        {
            _state = NodeState.SUCCESS;
            return _state;
        }
        _state = NodeState.FAILURE;
        return _state;
    }
}
