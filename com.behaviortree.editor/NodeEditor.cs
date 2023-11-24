using System.Text;
using UnityEngine;

namespace HCEngine.BT.Editor
{
    public class NodeEditor
    {
        public INode Node { get; private set; }
        public Rect Rect { get; private set; }

        public ConnectionPoint InPoint { get; private set; }
        public ConnectionPoint OutPoint { get; private set; }

        public GUIStyle FailureStyle { get; private set; }
        public GUIStyle SuccessStyle { get; private set; }
        public GUIStyle RunningStyle { get; private set; }
        public GUIStyle AbortStyle { get; private set; }

        public NodeEditor(INode node, Vector2 position, float width, float height,
            GUIStyle failureStyle, GUIStyle successStyle, GUIStyle runningStyle, GUIStyle abortedStyle,
            GUIStyle inPointStyle, GUIStyle outPointStyle)
        {
            Rect = new Rect(position.x, position.y, width, height);
            Node = node;

            FailureStyle = failureStyle;
            SuccessStyle = successStyle;
            RunningStyle = runningStyle;
            AbortStyle = abortedStyle;

            InPoint = new ConnectionPoint(this, ConnectionPointType.In, inPointStyle);
            OutPoint = new ConnectionPoint(this, ConnectionPointType.Out, outPointStyle);
        }

        public void Draw()
        {
            InPoint.Draw();
            OutPoint.Draw();
            string text;
            if (Node == null)
                return;

            if (Node.Name != null)
            {
                text = Node.DebugTree();
            }
            else
                text = "Node";

            switch (Node.State)
            {
                case NodeState.Success:
                    GUI.Box(Rect, text, SuccessStyle);
                    break;
                case NodeState.Failure:
                    GUI.Box(Rect, text, FailureStyle);
                    break;
                case NodeState.Running:
                    GUI.Box(Rect, text, RunningStyle);
                    break;
                case NodeState.Abort:
                    GUI.Box(Rect, text, AbortStyle);
                    break;
            }
        }
    }
}