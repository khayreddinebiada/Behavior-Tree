using System;
using System.Diagnostics.Contracts;

namespace HCEngine.BT.Generic
{
    public class Action : Node
    {
        private Func<NodeState> _action;

        public Action(Func<NodeState> action)
        {
            if (action == null)
                throw new ArgumentNullException($"Object Name: {nameof(action)}");
            Contract.EndContractBlock();

            _action = action;
        }

        public override string DebugTree()
        {
            return $"Action\n{Name}";
        }

        protected override NodeState Evaluate()
        {
            return _action.Invoke();
        }
    }
}