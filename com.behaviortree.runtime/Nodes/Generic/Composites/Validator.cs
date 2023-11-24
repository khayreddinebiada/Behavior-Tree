using System.Diagnostics.Contracts;

namespace HCEngine.BT.Generic
{
    public class Validator : Composite
    {
        private int _version = 0;

        public override void OnStarted()
        {
            _version = Version;
        }

        protected override NodeState Evaluate()
        {
            for (int i = 0; i < CountChildren; i++)
            {
                if (_version != Version)
                    return NodeState.Abort;

                INode current = this[i];

                if (current.IsNull())
                    throw new System.NullReferenceException(nameof(Child));
                Contract.EndContractBlock();

                NodeState newState = current.Tick();
                if (newState != NodeState.Success)
                    return newState;
            }

            return NodeState.Success;
        }
    }
}