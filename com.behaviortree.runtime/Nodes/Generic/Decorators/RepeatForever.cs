using System.Diagnostics.Contracts;

namespace HCEngine.BT.Generic
{
    public class RepeatForever : Decorator
    {
        protected override NodeState Evaluate()
        {
            if (Child.IsNull())
                throw new System.NullReferenceException(nameof(Child));
            Contract.EndContractBlock();

            Child.Tick();
            return NodeState.Running;
        }
    }
}