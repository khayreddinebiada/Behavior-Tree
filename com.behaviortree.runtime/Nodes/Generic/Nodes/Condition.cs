using System;
using System.Diagnostics.Contracts;

namespace HCEngine.BT.Generic
{
    public class Condition : Node
    {
        private Func<bool> _condition;

        public Condition(Func<bool> condition) : base()
        {
            if (condition == null)
                throw new ArgumentNullException($"Object Name: {nameof(condition)}");
            Contract.EndContractBlock();

            _condition = condition;
        }

        public override string DebugTree()
        {
            return $"Condition\n{Name}";
        }

        protected override NodeState Evaluate()
        {
            return _condition.Invoke() ? NodeState.Success : NodeState.Failure;
        }
    }
}