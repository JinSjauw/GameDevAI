using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public enum AgentState
    {
        IDLE = 0,
        PATROLLING = 1,
        ALERT = 2,
        CHASING = 3,
        ATTACKING = 4,
    }
    public abstract class Tree : MonoBehaviour
    {
        private BTNode _root = null;
        protected AgentState _agentState;
        public AgentState _AgentState{
            get 
            {
                return _agentState; 
            }
            set
            {
                _agentState = value;
            }
        }
        
        protected void Start()
        {
            _root = InitTree();
        }

        private void Update()
        {
            if (_root != null)
            {
                _root.Evaluate();
                //Debug.Log(root.GetData("target"));
            }
        }
        protected abstract BTNode InitTree();
    }
}

