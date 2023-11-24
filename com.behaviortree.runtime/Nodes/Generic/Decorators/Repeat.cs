using System.Diagnostics.Contracts;

namespace HCEngine.BT.Generic
{
    public class Repeat : Decorator
    {
        public int Limit { get; set; }

        private int _count;
        private int _version = 0;

        public Repeat(int limit) : base()
        {
            Limit = limit;
        }

        public override void OnStarted()
        {
             _count = 0;
            _version = Version;
        }

        protected override NodeState Evaluate()
        {
            if (_version != Version)
                return NodeState.Abort;

            if (Child.IsNull())
                throw new System.NullReferenceException(nameof(Child));
            Contract.EndContractBlock();

            NodeState newState = Child.Tick();
            if (newState == NodeState.Failure)
                return NodeState.Failure;

            _count++;

            if (_count < Limit)
                return NodeState.Running;

            return NodeState.Success;
        }
    }
}