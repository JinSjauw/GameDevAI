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

   public static float detectRange = 5f;
   public static float speed = 2f;

   protected override BTNode InitTree()
   {
      BTNode root = new Selector(new List<BTNode>()
      {
         new Sequence(new List<BTNode>
         {
            //Detect Player
            new DetectTask(guardTransform, playerTransform, playerMask),
            
            //Checking if agent has weapon
            /*new Selector(new List<BTNode>
            {
               new Inverter(new HasObject("weapon")),
               new FindObject("weapon", weaponsArray),
            }),
            new Sequence(new List<BTNode>
            {
               //Attacking/Chasing Nodes
            }),*/
            
            new MoveToTask(guardTransform, "target")
         }),
         new Sequence(new List<BTNode>
         {
            new PatrolTask(wayPoints, guardTransform)
         })
         //new PatrolTask(wayPoints, guardTransform),
      });

      return root;
   }
}
