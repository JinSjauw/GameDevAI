using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;
using Tree = BehaviourTree.Tree;

public class CheckState : BTNode
{
    private Transform _target;
    private AgentState _checkedState;

    public CheckState(Transform target, AgentState checkedState)
    {
        _target = target;
        _checkedState = checkedState;
    }
    
    public override NodeState Evaluate()
    {
        if (_target == null)
        {
            _state = NodeState.FAILURE;
            return _state;
        }

        Tree targetAgent = _target.GetComponent<Tree>();
        
        if (targetAgent != null)
        {
            if (targetAgent._AgentState == _checkedState)
            {
                _state = NodeState.SUCCESS;
                return _state;
            }
        }
        _state = NodeState.FAILURE;
        return _state;
    }
}
