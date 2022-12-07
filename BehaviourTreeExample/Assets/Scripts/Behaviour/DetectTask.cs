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
        if (Vector3.Distance(_transform.position, _player.position) < GuardTree.detectRange)
        {
            //Debug.Log(Vector3.Distance(_transform.position, _player.position) + "Detect Range: " + GuardTree.detectRange);
            Collider[] colliders = Physics.OverlapSphere(_transform.position, GuardTree.detectRange, _playerLayer);
        
            if (colliders.Length > 0)
            {
                SetRootData("target", colliders[0].transform);
                Transform target = (Transform)GetData("target");
                _state = NodeState.SUCCESS;
                //Debug.Log("Player : " + target.name);
                return _state;
            }
        }
        else if(ClearData("target"))
        {
            Debug.Log("Clearing Target DATA!");
            //parent.parent.ClearData("target");
        }
        _state = NodeState.FAILURE;
        return _state;
    }
}
