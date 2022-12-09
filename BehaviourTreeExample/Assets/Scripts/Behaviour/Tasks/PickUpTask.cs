using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class PickUpTask : BTNode
{
    private Transform _agent;
    private Transform _agentContainer;
    private Transform _target;
    private string _targetKey;
    private Animator _animator;

    //private Animator _animator;
    public PickUpTask(Transform agent, Transform agentContainer, string targetKey)
    {
        _agent = agent;
        _agentContainer = agentContainer;
        _targetKey = targetKey;
        _animator = agent.GetComponentInChildren<Animator>();
    }

    public override NodeState Evaluate()
    {
        _target = (Transform)GetData(_targetKey);
        if (_target.IsChildOf(_agentContainer))
        {
            _state = NodeState.SUCCESS;
            return _state;
        }
        //Check if in range;
        float distance = Vector3.Distance(_agent.position, _target.position);
        if (distance <= 1f)
        {
            _target.parent = _agentContainer;
            _target.localPosition = Vector3.zero;
            _target.GetComponent<Rigidbody>().isKinematic = true;
            _target.GetComponent<MeshCollider>().enabled = false;
            _target.rotation = _agentContainer.rotation;
            _target.up = _agentContainer.forward;
            _target.localRotation *= Quaternion.Euler(0, -70 ,0);
            _target.localScale = new Vector3(-1, 1, 1);
            
            _state = NodeState.SUCCESS;
            return _state;
        }

        _state = NodeState.RUNNING;
        return _state;
    }
}
