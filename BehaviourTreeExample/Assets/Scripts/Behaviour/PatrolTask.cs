using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviourTree;
public class PatrolTask : BTNode
{
   private Transform _transform;
   private Transform[] _wayPoints;
   private Animator _animator;
   
   private int _currentWaypointIndex = 0;

   private float _waitTime = 1f; // in seconds
   private float _waitCounter = 0f;
   private bool _waiting = false;
   
   public PatrolTask(Transform[] wayPoints, Transform transform)
   {
      _transform = transform;
      _wayPoints = wayPoints;
      _animator = transform.GetComponentInChildren<Animator>();
   }

   public override NodeState Evaluate()
   {
      if (_waiting)
      {
         _animator.SetBool("isWalking", false);
         _animator.SetBool("isIdle", true);

         _waitCounter += Time.deltaTime;
         if (_waitCounter >= _waitTime)
         {
            _waiting = false;
         }
      }
      else
      {
         //Return waypoint;
         Transform wp = _wayPoints[_currentWaypointIndex];
         if (Vector3.Distance(_transform.position, wp.position) < 0.05f)
         {
            _transform.position = wp.position;
            _waitCounter = 0f;
            _waiting = true;
            
            _animator.SetBool("isWalking", false);
            _animator.SetBool("isIdle", true);
            
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _wayPoints.Length;

            _state = NodeState.SUCCESS;
            return _state;
         }
         else
         {
            _animator.SetBool("isWalking", true);
            _animator.SetBool("isIdle", false);
            
            _transform.position = Vector3.MoveTowards(
               _transform.position,
               wp.position, GuardTree.speed * Time.deltaTime);
            _transform.LookAt(wp.position);
         }
      }
      _state = NodeState.RUNNING;
      return _state;
   }
}
