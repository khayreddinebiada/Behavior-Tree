namespace HCEngine.BT
{
    public abstract class Node : INode
    {
        public string Name { get; set; }

        public IDecorator Parent { get; set; }

        public NodeState State { get; private set; } = NodeState.Failure;

        public NodeState Tick()
        {
            if (State != NodeState.Running)
                OnStarted();

            State = Evaluate();

            if (State != NodeState.Running)
                OnFinished(State);

            return State;
        }
        
        public void Cancel()
        {
            if (State != NodeState.Running)
                return;

            State = NodeState.Failure;
            OnCancelled();
        }

        public virtual string DebugTree()
        {
            return Name;
        }

        protected abstract NodeState Evaluate();
        public virtual void OnStarted() { }
        public virtual void OnFinished(NodeState result) { }
        public virtual void OnCancelled() { }
    }
}