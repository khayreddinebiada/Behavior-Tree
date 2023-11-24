using System.Collections.Generic;

namespace HCEngine.BT
{
    public interface IComposite : IDecorator
    {
        public int CountChildren { get; }

        public INode this[int index] { get; }
        public IEnumerable<INode> Children { get; }

        public void Attach(IEnumerable<INode> children);
    }
}