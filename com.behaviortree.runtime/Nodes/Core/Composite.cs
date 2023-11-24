using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace HCEngine.BT
{
    public abstract class Composite : Node, IComposite
    {
        private readonly List<INode> _children = new();
        protected int Version { get; private set; }

        public INode this[int index]
            => _children[index];

        public int CountChildren
            => _children.Count;

        public IEnumerable<INode> Children
            => _children;

        public INode Child
        {
            get
            {
                if (_children.Count == 0)
                    return null;

                return _children[0];
            }
        }

        public Composite()
            : this(Array.Empty<INode>()) { }

        public Composite(IEnumerable<INode> children)
        {
            if (children == null)
                throw new ArgumentNullException($"Object Name: {nameof(children)}");
            Contract.EndContractBlock();

            Version = 0;

            Attach(children);
        }

        public void Attach(INode child)
        {
            Version++;
            child.Parent = this;
            _children.Add(child);
        }

        public void Attach(IEnumerable<INode> children)
        {
            Version++;
            foreach (INode node in children)
            {
                node.Parent = this;
                _children.Add(node);
            }
        }

        public bool Detach()
        {
            if (0 < _children.Count)
            {
                _children.RemoveAt(_children.Count - 1);
                Version++;
                return true;
            }

            return false;
        }

        public override void OnCancelled()
        {
            foreach (INode node in _children)
            {
                if (node.IsNull())
                    throw new NullReferenceException(nameof(node));
                Contract.EndContractBlock();

                node.Cancel();
            }
        }
    }
}
