using System;

namespace HCEngine.BT
{
    public abstract class Decorator : Node, IDecorator
    {
        private INode _child;
        public INode Child => _child;

        protected int Version { get; private set; }

        public void Attach(INode child)
        {
            if (Child != null)
                throw new InvalidOperationException($"The decorator of name {Name} node are not support multiple children!...");

            Version++;
            _child = child;
            _child.Parent = this;
        }

        public bool Detach()
        {
            if (_child.IsNull())
            {
                Version++;
                _child = null;
                return true;
            }

            return false;
        }

        public override void OnCancelled()
        {
            _child.Cancel();
        }
    }
}