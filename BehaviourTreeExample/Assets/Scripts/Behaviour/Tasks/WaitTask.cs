using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class WaitTask : BTNode
{
    private float _waitTime;
    private float _waitCounter;
    
    public WaitTask(float waitTime)
    {
        _waitTime = waitTime;
        _waitCounter = 0f;
    }

    public override NodeState Evaluate()
    {
        _waitCounter += Time.deltaTime;
        Debug.Log(_waitCounter);
        if (_waitCounter >= _waitTime)
        {
            _waitCounter = 0f;
            Debug.Log("TIMER IS OVER");
            _state = NodeState.SUCCESS;
            return _state;
        }
        
        _state = NodeState.RUNNING;
        return _state;
    }
}