using System.Diagnostics.Contracts;

namespace HCEngine.BT.Generic
{
    public class RepearUntilSuccess : Decorator
    {
        protected override NodeState Evaluate()
        {
            if (Child.IsNull())
                throw new System.NullReferenceException(nameof(Child));
            Contract.EndContractBlock();

            if (Child.Tick() == NodeState.Success)
                return NodeState.Success;

            return NodeState.Running;
        }
    }
}