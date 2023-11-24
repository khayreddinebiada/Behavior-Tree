using System.Diagnostics.Contracts;

namespace HCEngine.BT.Generic
{
    public sealed class Sequence : Composite
    {
        private int _currentIndex = 0;
        private int _version = 0;

        public override void OnStarted()
        {
            _currentIndex = 0;
            _version = Version;
        }

        protected override NodeState Evaluate()
        {
            while (_currentIndex < CountChildren)
            {
                if (_version != Version)
                    return NodeState.Abort;

                INode current = this[_currentIndex];
                if (current.IsNull())
                    throw new System.NullReferenceException(nameof(Child));
                Contract.EndContractBlock();

                NodeState newState = current.Tick();
                if (newState != NodeState.Success)
                    return newState;

                _currentIndex++;
            }

            return NodeState.Success;
        }
    }
}
