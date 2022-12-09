using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class FindCover : BTNode
{
    private Transform[] _coverPoints;
    private Transform _player;
    private Transform _target;
    private LayerMask _obstructionLayer;
    private bool nextCoverReady = true;
    
    public FindCover(Transform[] coverPoints, Transform player, Transform target, LayerMask obstructionLayer)
    {
        _coverPoints = coverPoints;
        _player = player;
        _target = target;
        _obstructionLayer = obstructionLayer;
    }

    public override NodeState Evaluate()
    {
        //If smoke Not thrown yet continue;
        if (GetData("nextCover") != null)
        {  
            nextCoverReady = (bool)GetData("nextCover");
        }
        
        if (!nextCoverReady)
        {
            _state = NodeState.SUCCESS;
            return _state;
        }

        //Iterate through the Array
        //Choose one based proximity to the player and LOS on the ENEMY
        List<Transform> safePoints = new List<Transform>();
        for (int i = 0; i < _coverPoints.Length; i++)
        {
            Transform coverPoint = _coverPoints[i];
            Vector3 direction = (_target.position - coverPoint.position).normalized;
            float distanceToTarget = Vector3.Distance(coverPoint.position, _target.position);

            if (Physics.Raycast(coverPoint.position, direction, distanceToTarget, _obstructionLayer))
            {
                safePoints.Add(coverPoint);
            }
        }
        
        Debug.Log(safePoints.Count + " " + _coverPoints.Length);
        Transform bestCover = null;
        foreach (Transform point in safePoints)
        {
            if (bestCover == null)
            {
                bestCover = point;
            }
            
            float distanceA = Vector3.Distance(point.position, _player.position);
            float distanceB = Vector3.Distance(bestCover.position, _player.position);

            if (distanceA < distanceB)
            {
                bestCover = point;
            }
        }

        if (bestCover != null)
        {
            SetRootData("cover", bestCover);
            SetRootData("nextCover", false);
            Debug.Log("Set Cover!");
            _state = NodeState.SUCCESS;
            return _state;
        }
        
        _state = NodeState.FAILURE;
        return _state;
    }
}
