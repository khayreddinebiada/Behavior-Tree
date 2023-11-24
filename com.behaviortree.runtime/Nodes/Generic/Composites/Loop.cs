using System;
using System.Diagnostics.Contracts;

namespace HCEngine.BT.Generic
{
    public class Loop : Decorator
    {
        private readonly int _totalCount;
        private readonly NodeState _stateRequired;

        private int _counter;

        public Loop(int totalCount)
        {
            _totalCount = totalCount;
            _stateRequired = NodeState.Success;
        }

        public Loop(NodeState stateRequired, int totalCount)
        {
            _totalCount = totalCount;
            _stateRequired = stateRequired;
        }

        public override void OnStarted()
        {
            base.OnStarted();
            _counter = 0;
        }

        public override string DebugTree()
        {
            return $"{Name}\nCount {_counter}/{_totalCount}";
        }

        protected override NodeState Evaluate()
        {
            if (_totalCount <= _counter)
                return _stateRequired;

            if (Child.IsNull())
                throw new NullReferenceException(nameof(Child));
            Contract.EndContractBlock();

            NodeState state = Child.Tick();

            if (state == _stateRequired)
            {
                _counter++;
                if (_totalCount <= _counter)
                    return _stateRequired;

                return NodeState.Running;
            }

            return state;
        }
    }
}