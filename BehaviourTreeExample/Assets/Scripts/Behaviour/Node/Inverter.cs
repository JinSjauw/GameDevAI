using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class Inverter : BTNode
{
    private BTNode _childNode;

    public Inverter(): base() {}
    
    public Inverter(BTNode childNode)
    {
        _childNode = childNode;
    }

    public override NodeState Evaluate()
    {
        NodeState childState = _childNode.Evaluate();
        _state = childState;
        
        if (childState == NodeState.SUCCESS)
        {
            _state = NodeState.FAILURE;
            Debug.Log("Returning False");
            return _state;
        }

        if (childState == NodeState.FAILURE)
        {
            _state = NodeState.SUCCESS;
            Debug.Log("Returning True");
            return _state;
        }

        return _state;
    }
}