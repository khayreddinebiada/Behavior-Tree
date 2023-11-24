using System;
using System.Diagnostics.Contracts;

namespace HCEngine.BT.Generic
{
    public class Foreach : Composite
    {
        protected override NodeState Evaluate()
        {
            foreach (INode child in Children)
            {
                if (child.IsNull())
                    throw new NullReferenceException(nameof(child));
                Contract.EndContractBlock();

                child.Tick();
            }

            return NodeState.Success;
        }
    }
}