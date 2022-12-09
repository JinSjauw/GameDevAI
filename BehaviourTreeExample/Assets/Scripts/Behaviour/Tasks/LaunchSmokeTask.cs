using System.Collections;
using System.Collections.Generic;
using BehaviourTree;
using UnityEngine;
using UnityEngine.AI;

public class LaunchSmokeTask : BTNode
{
    private ObjectPool _objectPool;
    private Transform _origin;
    private Transform _target;
    private GameObject _smokeBomb;
    private float _velocity;
    
    private float _waitTime = 2f; // in seconds
    private float _waitCounter = 0f;
    private bool _waiting = false;
    
    public LaunchSmokeTask(ObjectPool objectPool, Transform origin, Transform target, GameObject smokeBomb, float velocity)
    {
        _origin = origin;
        _target = target;
        _smokeBomb = smokeBomb;
        _velocity = velocity;
        _objectPool = objectPool;
    }

    public override NodeState Evaluate()
    {
        if (_smokeBomb == null)
        {
            _state = NodeState.FAILURE;
            return _state;
        }
        
        //Launch Grenade once
        
        //Launch grenade
        Debug.Log("LAUNCHING SMOKE");
        GameObject smokeGrenade = _objectPool.GetObject(_smokeBomb);
        
        smokeGrenade.GetComponent<Rigidbody>().isKinematic = false;
        smokeGrenade.transform.position = _origin.position;
        smokeGrenade.transform.LookAt(_target);
        
        Rigidbody rb = smokeGrenade.GetComponent<Rigidbody>();
        rb.velocity = _velocity * smokeGrenade.transform.forward;
        rb.velocity += _velocity * smokeGrenade.transform.up;
        
        _waiting = true;

        SetRootData("nextCover", true);
        _state = NodeState.SUCCESS;
        return _state;
    }
}
