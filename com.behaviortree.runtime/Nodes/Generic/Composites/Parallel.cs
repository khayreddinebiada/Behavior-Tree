using System;
using System.Diagnostics.Contracts;

namespace HCEngine.BT.Generic
{
    public class Parallel : Composite
    {
        public Policy SuccessPolicy { get; set; }
        public Policy FailurePolicy { get; set; }

        public Parallel(Policy successPolicy)
            : this(successPolicy, Policy.Non) { }

        public Parallel(Policy successPolicy, Policy failurePolicy)
            : base()
        {
            SuccessPolicy = successPolicy;
            FailurePolicy = failurePolicy;
        }

        public override void OnStarted()
        {
            for (int i = 0; i < CountChildren; i++)
            {
                INode current = this[i];

                if (current.IsNull())
                    throw new NullReferenceException(nameof(current));
                Contract.EndContractBlock();

                current.Tick();
            }
        }

        protected override NodeState Evaluate()
        {
            int totalCount = 0;
            int successCount = 0;
            int failuresCount = 0;

            for (int i = 0; i < CountChildren; i++)
            {
                INode current = this[i];

                if (current.IsNull())
                    throw new NullReferenceException(nameof(current));
                Contract.EndContractBlock();

                NodeState nodeState = current.State;
                if (nodeState == NodeState.Running)
                    nodeState = current.Tick();

                if (nodeState == NodeState.Abort)
                    return NodeState.Abort;

                if (nodeState == NodeState.Success)
                {
                    if (SuccessPolicy == Policy.RequireOne)
                        return NodeState.Success;
                    successCount++;
                }

                if (nodeState == NodeState.Failure)
                {
                    if (FailurePolicy == Policy.RequireOne)
                        return NodeState.Failure;
                    failuresCount++;
                }

                totalCount++;
            }

            if (SuccessPolicy == Policy.RequireAll && successCount == totalCount)
                return NodeState.Success;

            if (FailurePolicy == Policy.RequireAll && failuresCount == totalCount)
                return NodeState.Failure;

            return NodeState.Running;
        }
    }
}