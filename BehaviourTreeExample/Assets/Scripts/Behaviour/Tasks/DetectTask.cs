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
    private bool detectedOnce = false;

    public DetectTask(Transform agent, string targetKey)
    {
        _agent = agent;
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
        if (distance < GuardTree.detectRange)
        {
            detectedOnce = true;
            _state = NodeState.SUCCESS;
            return _state;
            //Need to make this an FOV
            /*Collider[] colliders = Physics.OverlapSphere(_agent.position, GuardTree.detectRange, _targetLayer);
        
            if (colliders.Length > 0)
            {
                //Debug.Log("Target Set! : " + colliders[0].transform);
                SetRootData("target", colliders[0].transform);
                detectedOnce = true;
                _state = NodeState.SUCCESS;
                return _state;
            }*/
        }
        else if(distance > GuardTree.detectRange && detectedOnce)
        {
            //Debug.Log("Out of range");
            _state = NodeState.SUCCESS;
            return _state;
        }
        
        _state = NodeState.FAILURE;
        return _state;
    }
}
