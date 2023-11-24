namespace HCEngine.BT
{
    public interface IDecorator : INode
    {
        public INode Child { get; }

        public void Attach(INode child);
        public bool Detach();
    }
}