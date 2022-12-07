using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BehaviourTree
{
    public enum NodeState
    {
        RUNNING = 0,
        SUCCESS = 1, 
        FAILURE = 2
    }

    public class BTNode
    {
        protected NodeState _state;

        public BTNode _parent;
        protected List<BTNode> _children = new List<BTNode>();
        
        private Dictionary<string, object> _dataContext = new Dictionary<string, object>();
        
        public BTNode()
        {
            _parent = null;
        }

        public BTNode(List<BTNode> children)
        {
            foreach (BTNode child in children)
            {
                Attach(child);
            }
        }

        private void Attach(BTNode node)
        {
            node._parent = this;
            _children.Add(node);
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;

        public Dictionary<string, object> GetDataContext()
        {
            return _dataContext;
        }
        
        public void SetData(string key, object value)
        {
            _dataContext[key] = value;
        }
        
        public bool SetRootData(string key, object value)
        {
            BTNode node = _parent;
            
            while (node != null)
            {
                if (node._parent == null)
                {
                    node.SetData(key, value);
                    return true;
                }
                else
                {
                    node = node._parent;
                }
            }
            
            return false;
        }

        public object GetData(string key)
        {
            object value = null;
            if (_dataContext.TryGetValue(key, out value))
            {
                return value;
            }

            BTNode node = _parent;
            while ( node != null)
            {
                value = node.GetData(key);
                if (value != null)
                {
                    return value;
                }
                node = node._parent;
            }
            return null;
        }
        
        public bool ClearData(string key)
        {
            if (_dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;
            }

            BTNode node = _parent;
            while ( node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                {
                    return true;
                }
                node = node._parent;
            }
            return false;
        }

    }
}


