using System.Diagnostics.Contracts;

namespace HCEngine.BT.Generic
{
    public class Inverter : Decorator
    {
        protected override NodeState Evaluate()
        {
            if (Child.IsNull())
                throw new System.NullReferenceException(nameof(Child));
            Contract.EndContractBlock();

            NodeState state = Child.Tick();
            switch (state)
            {
                case NodeState.Success:
                    return NodeState.Failure;
                case NodeState.Failure:
                    return NodeState.Success;
            }

            return state;
        }
    }
}