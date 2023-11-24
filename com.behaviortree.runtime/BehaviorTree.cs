namespace HCEngine.BT
{
    public class BehaviorTree
    {
        private IDecorator _root = null;
        public IDecorator Root
            => _root;

        public bool IsEnable { get; set; }
        public NodeState State
        {
            get
            {
                if (_root == null)
                    return NodeState.Abort;

                return _root.State;
            }
        }

        public BehaviorTree(IDecorator root)
        {
            IsEnable = true;
            _root = root;
        }

        public NodeState Tick()
        {
            if (_root.IsNull())
                throw new System.NullReferenceException("Your root have a null value!...");

            if (!IsEnable)
                return NodeState.Failure;

            return _root.Tick();
        }

        public void Cancel()
        {
            if (_root.IsNull())
                throw new System.NullReferenceException("Your root have a null value!...");

            _root.Cancel();
        }
    }
}
