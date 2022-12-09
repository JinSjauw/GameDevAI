using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;
using UnityEngine.AI;
using TMPro;
using Tree = BehaviourTree.Tree;

public class NinjaTree : Tree
{
    private ObjectPool _objectPool;
    [SerializeField] private GameObject _smokeBomb;
    [SerializeField] private Transform _playerTransform, _guardTransform, ninjaHand;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private TextMeshProUGUI _stateText;
    [SerializeField] private float _range;
    [SerializeField] private Transform[] _coverPoints;
    [SerializeField] private LayerMask _obstructionLayer = 1 << 8;
    protected override BTNode InitTree()
    {
        //Creating a objectPool for smokes
        _objectPool = FindObjectOfType<ObjectPool>();
        for (int i = 0; i < 5; i++)
        {
           GameObject smoke = _objectPool.GetObject(_smokeBomb);
           smoke.SetActive(false);
        }

        BTNode root = new Selector(new List<BTNode>()
      {
          new Sequence(new List<BTNode>
          {
              new SetState(transform, AgentState.ALERT, _stateText),
              //Check if player is Attacked/Chased
              new CheckState(_guardTransform, AgentState.ATTACKING),
              //Find Cover
              new FindCover(_coverPoints, _playerTransform, _guardTransform, _obstructionLayer),
              new SetState(transform, AgentState.ATTACKING, _stateText),
              new MoveToTask(_agent, "cover"),
              //Throw Smokebomb 
              new LaunchSmokeTask(_objectPool, ninjaHand, _guardTransform, _smokeBomb, 4)
          }),
            
          //Follow Player as fallback
          new Sequence(new List<BTNode>
          {
              new SetState(transform, AgentState.PATROLLING, _stateText),
              new MoveToTask(_agent, _playerTransform, _range),
          })
      });
      return root;
   }
}
