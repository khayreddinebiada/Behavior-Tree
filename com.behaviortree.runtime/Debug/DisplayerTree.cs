using System;
using System.Diagnostics.Contracts;

namespace HCEngine.BT
{
    public static class DisplayerTree
    {
        public static event Action<INode> OnTreeChanged;
        public static INode Root { get; private set; }

        public static void Display(BehaviorTree tree)
        {
            Display(tree.Root);
        }

        public static void Display(INode root)
        {
            if (root == null)
                throw new NullReferenceException($"The object of name: {nameof(root)} has a null value!...");
            Contract.EndContractBlock();

            Root = root;
            OnTreeChanged?.Invoke(root);
        }
    }
}