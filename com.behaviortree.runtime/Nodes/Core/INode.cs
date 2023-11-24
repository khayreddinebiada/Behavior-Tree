namespace HCEngine.BT
{
    public interface INode
    {
        public string Name { get; set; }
        public NodeState State { get; }
        public IDecorator Parent { get; set; }

        public void Cancel();
        public NodeState Tick();
        public string DebugTree();
    }
}