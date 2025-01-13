
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class RandomSequence : Node
    {
        public RandomSequence() : base() { }
        public RandomSequence(List<Node> children) : base(children) { }

        public override NodeState Evaluate()
        {
            int n = children.Count;  
            while (n > 1) {  
                n--;  
                int k = Random.Range(0,n+1);  
                var value = children[k];  
                children[k] = children[n];  
                children[n] = value;  
            } 
            	
            bool anyChildIsRunning = false;

            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        state = NodeState.FAILURE;
                        return state;
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.RUNNING:
                        anyChildIsRunning = true;
                        continue;
                    default:
                        state = NodeState.SUCCESS;
                        return state;
                }
            }

            state = anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return state;
        }

    }

}