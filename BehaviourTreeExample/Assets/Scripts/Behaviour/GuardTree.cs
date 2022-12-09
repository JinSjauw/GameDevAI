using System.Collections;
using System.Collections.Generic;

using BehaviourTree;
using UnityEngine;
using UnityEngine.AI;
using Tree = BehaviourTree.Tree;

public class GuardTree : Tree
{
   public Transform guardTransform, guardHand, guardEyes;
   public NavMeshAgent guardAgent;
   public Transform[] wayPoints;
   public Transform[] weaponsArray;
   public float attackRange = 4f;
   public LayerMask playerMask = 1 << 6;
   public LayerMask obstacleMask = 1 << 8;

   public static float detectRange = 8f;

   protected override BTNode InitTree()
   {
      BTNode root = new Selector(new List<BTNode>()
      {
         new Sequence(new List<BTNode>
         {
            //Detect Player
            //new DetectTask(guardTransform, playerTransform, playerMask),
            new Selector(new List<BTNode>
            {
               new DetectTask(guardTransform, "target"),
               new FOV(guardEyes, 120f, detectRange, playerMask, obstacleMask),
            }),
            
            //Checking if agent has weapon
            //Retrieve weapon
            new Selector(new List<BTNode>
            {
               new HasObject(guardHand, "weapon"),
               new Sequence(new List<BTNode>
               {
                  new FindObject("weapon", weaponsArray),
                  new MoveToTask(guardAgent, "weapon"),
                  new PickUpTask(guardTransform,guardHand, "weapon"),
               })
            }),
            
            new Sequence(new List<BTNode>
            {
               new FOV(guardEyes, 120f, detectRange, playerMask, obstacleMask),
               new Selector(new List<BTNode>
               {
                  //Attack()
                  new AttackTask(guardTransform, "target", attackRange, 20),
                  //Chase
                  new MoveToTask(guardAgent, "target", attackRange),
               }),
            }),

         }),
         new Sequence(new List<BTNode>
         {
            new GetWayPoint(guardTransform, wayPoints),
            new MoveToTask(guardAgent, "waypoint"),
         })
      });
      return root;
   }
}
