using System.Collections.Generic;
using UnityEngine;

namespace HCEngine.BT
{
    public static class UtilityExtensions
    {
        internal static bool IsNull(this object obj)
        {
            return obj == null || obj.Equals(null);
        }

        internal static void Fill(this ICollection<int> collection, int count)
        {
            for (int i = 0; i < count; i++)
                collection.Add(i);
        }

        public static int Width(this BehaviorTree tree)
        {
            return tree.Root._Width();
        }

        public static int Width(this INode root)
        {
            return root._Width();
        }

        public static int Count(this BehaviorTree tree)
        {
            return tree.Root._Count(0);
        }

        public static int Count(this INode root)
        {
            return root._Count(0);
        }

        public static int Depth(this BehaviorTree tree)
        {
            return tree.Root._Depth(1);
        }

        public static int Depth(this INode root)
        {
            return root._Depth(1);
        }

        public static List<INode> ToList(this BehaviorTree tree, int fromDepth = -1, int length = int.MaxValue)
        {
            return tree.Root.ToList(fromDepth, length);
        }

        public static List<INode> ToList(this INode root, int fromDepth = -1, int length = int.MaxValue)
        {
            List<INode> nodes = new List<INode>();
            root._Iterate(ref nodes, 0, fromDepth, length);
            return nodes;
        }

        private static void _Iterate(this INode node, ref List<INode> nodes, int depth, int fromDepth, int length)
        {
            if (fromDepth + length <= depth)
                return;

            if (fromDepth <= depth)
                nodes.Add(node);

            ++depth;
            if (node is IComposite)
            {
                IComposite composite = (IComposite)node;
                foreach (INode child in composite.Children)
                {
                    _Iterate(child, ref nodes, depth, fromDepth, length);
                }
            }
            else if (node is IDecorator)
            {
                _Iterate(((IDecorator)node).Child, ref nodes, ++depth, fromDepth, length);
            }
        }

        private static int _Count(this INode node, int depth)
        {
            depth++;

            if (node is IComposite)
            {
                IComposite composite = (IComposite)node;
                foreach (INode child in composite.Children)
                {
                    return _Count(child, depth);
                }
            }
            else if (node is IDecorator)
            {
                return _Count(((IDecorator)node).Child, depth);
            }

            return depth;
        }

        private static int _Width(this INode node)
        {
            if (node is IComposite)
            {
                int result = 0;
                IComposite composite = (IComposite)node;
                foreach (INode child in composite.Children)
                {
                    result += _Width(child);
                }
                return result;
            }
            else if (node is IDecorator)
            {
                return _Width(((IDecorator)node).Child);
            }
            else
                return 1;
        }

        private static int _Depth(this INode node, int depth)
        {
            int resut = depth;
            if (node is IComposite)
            {
                IComposite composite = (IComposite)node;
                foreach (INode child in composite.Children)
                {
                    resut = Mathf.Max(resut, _Depth(child, depth + 1));
                }
            }
            else if (node is IDecorator)
            {
                resut = _Depth(((IDecorator)node).Child, depth + 1);
            }

            return resut;
        }
    }
}