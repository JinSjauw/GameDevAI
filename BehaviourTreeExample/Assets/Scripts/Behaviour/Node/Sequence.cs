using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public class Sequence : BTNode
    {
        public Sequence() : base() { }

        public Sequence(List<BTNode> children) : base(children) { }

        public override NodeState Evaluate()
        {
            bool anyChildRunning = false;

            foreach (BTNode node in _children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        _state = NodeState.FAILURE;
                        return _state;
                    case NodeState.SUCCESS:
                        _state = NodeState.SUCCESS;
                        continue;
                    case NodeState.RUNNING:
                        anyChildRunning = true;
                        _state = NodeState.RUNNING;
                        return _state;
                    default:
                        _state = NodeState.SUCCESS;
                        return _state;
                }
            }

            _state = anyChildRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return _state;
        }
    }    
}
