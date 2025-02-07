using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;

public class GetWayPoint : BTNode
{
    private Transform _agent;
    private float _range;
    private Transform[] _wayPoints;
    private Transform _currentWaypoint;
    private int _wayPointIndex = 0;
    
    public GetWayPoint(Transform agent, Transform[] waypoints, float range)
    {
        _agent = agent;
        _range = range;
        _wayPoints = waypoints;
    }

    public override NodeState Evaluate()
    {
        if (_wayPoints.Length <= 0)
        {
            _state = NodeState.FAILURE;
            return _state;
        }
        
        if (_currentWaypoint != null && Vector3.Distance(_agent.position, _currentWaypoint.position) >= _range)
        {
            _state = NodeState.SUCCESS;
            return _state;
        }

        _currentWaypoint = _wayPoints[_wayPointIndex];
        SetRootData("waypoint", _currentWaypoint);
        _wayPointIndex = (_wayPointIndex + 1) % _wayPoints.Length;
        
        _state = NodeState.SUCCESS;
        return _state;
    }
}
