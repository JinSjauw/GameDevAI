using System.Collections;
using System.Collections.Generic;

using BehaviourTree;
using UnityEngine;
using Tree = BehaviourTree.Tree;

public class GuardTree : Tree
{
   public Transform guardTransform, playerTransform;
   public Transform[] wayPoints;
   public Transform[] weaponsArray;
   public LayerMask playerMask = 1 << 6;

   public static float detectRange = 10f;
   public float speed = 2f;

   protected override BTNode InitTree()
   {
      BTNode root = new Selector(new List<BTNode>()
      {
         new Sequence(new List<BTNode>
         {
            //Detect Player
            new DetectTask(guardTransform, playerTransform, playerMask),
            
            //Checking if agent has weapon
            //Retrieve weapon
            new Selector(new List<BTNode>
            {
               new HasObject("weapon"),
               new FindObject("weapon", weaponsArray),
            }),
            
            new Sequence(new List<BTNode>
            {
               new MoveToTask(guardTransform, "weapon", speed),
               new PickUpTask(guardTransform, "weapon"),
            }),
            
            new MoveToTask(guardTransform, "target", speed)
            //if In FOV RANGE
            //if in attackRange 
            //Attack
            //not in range
            //chase
            
         }),
         //new WaitTask(1f),
         new Sequence(new List<BTNode>
         {
            new PatrolTask(wayPoints, guardTransform, speed)
         })
         //new PatrolTask(wayPoints, guardTransform),
      });
      return root;
   }
}
