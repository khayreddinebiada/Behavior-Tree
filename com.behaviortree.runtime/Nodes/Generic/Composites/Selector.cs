using System;
using System.Diagnostics.Contracts;

namespace HCEngine.BT.Generic
{
    public sealed class Selector : Composite
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
                    throw new NullReferenceException(nameof(current));
                Contract.EndContractBlock();


                NodeState newState = current.Tick();
                if (newState != NodeState.Failure)
                    return newState;

                _currentIndex++;
            }

            return NodeState.Failure;
        }
    }
}
