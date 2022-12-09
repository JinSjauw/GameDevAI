using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class FOV : BTNode
{
    private float _radius;
    private float _angle;
    private Transform _origin;
    private Transform _target;

    private LayerMask _targetMask;
    private LayerMask _obstacleMask;
    private bool seeTarget;

    //Timer
    private float _waitTime = .2f; // in seconds
    private float _waitCounter = 0f;
    private bool _waiting = false;
    
    public FOV(Transform origin, float angle, float radius, LayerMask targetMask, LayerMask obstacleMask)
    {
        _origin = origin;
        _angle = angle;
        _radius = radius;
        _targetMask = targetMask;
        _obstacleMask = obstacleMask;
    }
    public override NodeState Evaluate()
    {
        if (_waiting)
        {
            _waitCounter += Time.deltaTime;
            if (_waitCounter >= _waitTime)
            {
                _waiting = false;
            }
        }
        else
        {
            //FOV Calculations
            //Debug.Log("DOING FOV CALC");
            Collider[] colliders = Physics.OverlapSphere(_origin.position, _radius, _targetMask);

            if (colliders.Length != 0)
            {
                _target = colliders[0].transform;
                Vector3 directionToTarget = (_target.position - _origin.position).normalized;

                if (Vector3.Angle(_origin.forward, directionToTarget) < _angle / 2)
                {
                    float distanceToTarget = Vector3.Distance(_origin.position, _target.position);

                    if (!Physics.Raycast(_origin.position, directionToTarget, distanceToTarget, _obstacleMask))
                    {
                        seeTarget = true;
                        SetRootData("target", _target);
                        //Do stuff here
                    }
                    else
                    {
                        seeTarget = false;
                        //Do stuff here
                    }
                }
                else
                {
                    seeTarget = false;
                }
            }else if (seeTarget)
            {
                seeTarget = false;
            }
        }
        
        switch (seeTarget)
        {
            case true:
                Debug.Log("SEES PLAYER");
                _state = NodeState.SUCCESS;
                return _state;
            case false:
                Debug.Log("DOESN'T SEE PLAYER");
                ClearData("target");
                _state = NodeState.FAILURE;
                return _state;
        }
    }
}
