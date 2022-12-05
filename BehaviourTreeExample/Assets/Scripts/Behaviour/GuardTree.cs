using System.Collections;
using System.Collections.Generic;

using BehaviourTree;
using UnityEngine;
using Tree = BehaviourTree.Tree;

public class GuardTree : Tree
{
   public Transform guardTransform, playerTransform;
   public Transform[] wayPoints;
   public LayerMask playerMask = 1 << 6;

   public static float DetectRange = 5f;
   public static float speed = 2f;
   
   protected override BTNode RunTree()
   {
      BTNode root = new Selector(new List<BTNode>()
      {
         new Sequence(new List<BTNode>
         {
            new DetectTask(guardTransform, playerTransform, playerMask),
            new MoveToTask(guardTransform)
         }),
         new PatrolTask(wayPoints, guardTransform),
      });
      
      return root;
   }
}
