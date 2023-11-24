using System;
using System.Diagnostics.Contracts;

namespace HCEngine.BT.Generic
{
    public class RepeatUntilAll : Composite
    {
        public NodeState RequiredState { get; set; }

        public RepeatUntilAll(NodeState requiredState)
            : base()
        {
            RequiredState = requiredState;
        }

        protected override NodeState Evaluate()
        {
            int totalCount = 0;
            int doneCount = 0;

            for (int i = 0; i < CountChildren; i++)
            {
                INode current = this[i];

                if (current.IsNull())
                    throw new NullReferenceException(nameof(current));
                Contract.EndContractBlock();

                NodeState nodeState = current.Tick();

                if (nodeState == NodeState.Abort)
                    return NodeState.Abort;

                if (nodeState == RequiredState)
                {
                    doneCount++;
                }

                totalCount++;
            }

            if (totalCount == doneCount)
                return NodeState.Success;

            return NodeState.Running;
        }
    }
}