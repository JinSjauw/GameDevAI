using System;
using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using TMPro;
using UnityEngine;
using Tree = BehaviourTree.Tree;

public class SetState : BTNode
{
    private Transform _target;
    private AgentState _targetState;
    private TextMeshProUGUI _stateText;
    
    public SetState(Transform target, AgentState targetState)
    {
        _target = target;
        _targetState = targetState;
    }
    
    public SetState(Transform target, AgentState targetState, TextMeshProUGUI stateText)
    {
        _target = target;
        _targetState = targetState;
        _stateText = stateText;
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
            targetAgent._AgentState = _targetState;
            
            if (_stateText != null)
            {
                string currentState = "";
                switch (targetAgent._AgentState)
                {
                    case AgentState.IDLE:
                        currentState = "Idle";
                        break;
                    case AgentState.PATROLLING:
                        currentState = "Patrolling";
                        break;
                    case AgentState.ALERT:
                        currentState = "Alert";
                        break;
                    case AgentState.CHASING:
                        currentState = "Chasing";
                        break;
                    case AgentState.ATTACKING:
                        currentState = "Attacking";
                        break;
                }
                _stateText.text = currentState;
            }
            _state = NodeState.SUCCESS;
            return _state;
        }
        _state = NodeState.FAILURE;
        return _state;
    }
}
