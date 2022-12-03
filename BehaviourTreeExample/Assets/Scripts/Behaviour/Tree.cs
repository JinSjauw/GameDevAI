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
            root = RunTree();
        }

        private void Update()
        {
            if (root != null)
            {
                root.Evaluate();
            }
        }
        protected abstract BTNode RunTree();
    }
}

