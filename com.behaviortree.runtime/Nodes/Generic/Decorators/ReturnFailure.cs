using System.Diagnostics.Contracts;

namespace HCEngine.BT.Generic
{
    public class ReturnFailure : Decorator
    {
        protected override NodeState Evaluate()
        {

            if (Child.IsNull())
                throw new System.NullReferenceException(nameof(Child));
            Contract.EndContractBlock();

            Child.Tick();
            return NodeState.Failure;
        }
    }
}