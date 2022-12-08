using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;
using UnityEngine.UIElements;

public class DetectTask : BTNode
{
    private Transform _agent;
    private Transform _target;
    private LayerMask _targetLayer;
    private bool detectedOnce = false;

    public DetectTask(Transform agent, Transform target, LayerMask targetLayer)
    {
        _agent = agent;
        _target = target;
        _targetLayer = targetLayer;
    }

    public override NodeState Evaluate()
    {
        float distance = Vector3.Distance(_agent.position, _target.position);
        if (distance < GuardTree.detectRange)
        {
            //Need to make this an FOV
            Collider[] colliders = Physics.OverlapSphere(_agent.position, GuardTree.detectRange, _targetLayer);
        
            if (colliders.Length > 0)
            {
                //Debug.Log("Target Set! : " + colliders[0].transform);
                SetRootData("target", colliders[0].transform);
                detectedOnce = true;
                _state = NodeState.SUCCESS;
                return _state;
            }
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
