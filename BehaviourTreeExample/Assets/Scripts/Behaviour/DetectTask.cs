using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;
using UnityEngine.UIElements;

public class DetectTask : BTNode
{
    private Transform _transform;
    private Transform _player;
    private LayerMask _playerLayer;
    public DetectTask(Transform transform, Transform player, LayerMask playerLayer)
    {
        _transform = transform;
        _player = player;
        _playerLayer = playerLayer;
    }

    public override NodeState Evaluate()
    {
        //Debug.Log("DetectTask");
        if (Vector3.Distance(_transform.position, _player.position) < GuardTree.DetectRange)
        {
            Collider[] colliders = Physics.OverlapSphere(-_transform.position, GuardTree.DetectRange, _playerLayer);
            
            if (colliders.Length > 0)
            {
                parent.parent.SetData("target", colliders[0].transform);
                state = NodeState.SUCCESS;
                Debug.Log("Player Found");
                return state;
            }
            state = NodeState.FAILURE;
            return state;
        }

        state = NodeState.FAILURE;
        return state;
    }
}
