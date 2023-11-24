using System;
using System.Diagnostics.Contracts;

namespace HCEngine.BT.Generic
{
    public class ReturnSuccess : Decorator
    {
        protected override NodeState Evaluate()
        {

            if (Child.IsNull())
                throw new NullReferenceException(nameof(Child));
            Contract.EndContractBlock();

            Child.Tick();
            return NodeState.Success;
        }
    }
}