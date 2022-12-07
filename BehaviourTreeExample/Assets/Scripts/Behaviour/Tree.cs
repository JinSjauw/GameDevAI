using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public abstract class Tree : MonoBehaviour
    {
        private BTNode root = null;
        
        protected void Start()
        {
            root = InitTree();
        }

        private void Update()
        {
            if (root != null)
            {
                root.Evaluate();
                //Debug.Log(root.GetData("target"));
            }
        }
        protected abstract BTNode InitTree();
    }
}

