using System.Collections;
using System.Collections.Generic;

using BehaviourTree;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using Tree = BehaviourTree.Tree;

public class GuardTree : Tree
{
   [SerializeField] private Transform _guardTransform, _guardHand, _guardEyes;
   [SerializeField] private NavMeshAgent _guardAgent;
   [SerializeField] private TextMeshProUGUI _stateText;
   [SerializeField] private Transform[] _wayPoints;
   [SerializeField] private Transform[] _weaponsArray;
   [SerializeField] private float _attackRange = 4f;
   [SerializeField] private int _attackDamage;
   [SerializeField] private LayerMask _playerMask = 1 << 6;
   [SerializeField] private LayerMask _obstacleMask = 1 << 8;
   [SerializeField] private float _detectRange = 8f;

   protected override BTNode InitTree()
   {
      BTNode root = new Selector(new List<BTNode>()
      {
         new Sequence(new List<BTNode>
         {
            //Detect Player
            new SetState(_guardTransform, AgentState.IDLE, _stateText),
            new Selector(new List<BTNode>
            {
               new DetectTask(_guardTransform, "target", _detectRange),
               new FOV(_guardEyes, 120f, _detectRange, _playerMask, _obstacleMask),
            }),
            new SetState(_guardTransform, AgentState.ALERT, _stateText),
            
            //Checking if agent has weapon
            //Retrieve weapon
            new Selector(new List<BTNode>
            {
               new HasObject(_guardHand, "weapon"),
               new Sequence(new List<BTNode>
               {
                  new FindObject("weapon", _weaponsArray),
                  new MoveToTask(_guardAgent, "weapon"),
                  new PickUpTask(_guardTransform,_guardHand, "weapon"),
               })
            }),
            
            new Selector(new List<BTNode>
            {
               //Attack
               new Sequence(new List<BTNode>
               {
                  new FOV(_guardEyes, 360f, _attackRange, _playerMask, _obstacleMask),
                  new SetState(_guardTransform, AgentState.ATTACKING, _stateText),
                  new AttackTask(_guardTransform, "target", _attackRange, _attackDamage),
               }),
               
               //Chase
               new Sequence(new List<BTNode>
               {
                  new FOV(_guardEyes, 120f, _detectRange, _playerMask, _obstacleMask),
                  new SetState(_guardTransform, AgentState.CHASING, _stateText),
                  new MoveToTask(_guardAgent, "target", _attackRange - .5f),
               }),
            })
         }),
         //Fallback PatrolTask
         new Sequence(new List<BTNode>
         {
            new Inverter(new CheckState(_guardTransform, AgentState.ATTACKING)),
            new SetState(_guardTransform, AgentState.PATROLLING, _stateText),
            new GetWayPoint(_guardTransform, _wayPoints, 1f),
            new MoveToTask(_guardAgent, "waypoint"),
         })
      });
      return root;
   }
}
