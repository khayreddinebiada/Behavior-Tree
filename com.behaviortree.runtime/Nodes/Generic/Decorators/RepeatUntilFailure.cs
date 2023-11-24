using System.Diagnostics.Contracts;

namespace HCEngine.BT.Generic
{
    public class RepeatUntilFailure : Decorator
    {
        protected override NodeState Evaluate()
        {
            if (Child.IsNull())
                throw new System.NullReferenceException(nameof(Child));
            Contract.EndContractBlock();

            if (Child.Tick() == NodeState.Failure)
                return NodeState.Failure;

            return NodeState.Running;
        }
    }
}