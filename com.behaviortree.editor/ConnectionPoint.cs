using UnityEngine;

namespace HCEngine.BT.Editor
{
    public enum ConnectionPointType { In, Out }
    public class ConnectionPoint
    {
        public Rect Rect;

        public ConnectionPointType type;

        public NodeEditor node;

        public GUIStyle style;

        public ConnectionPoint(NodeEditor node, ConnectionPointType type, GUIStyle style)
        {
            this.node = node;
            this.type = type;
            this.style = style;
            Rect = new Rect(0, 0, 10f, 10f);
        }

        public void Draw()
        {
            Rect.x = node.Rect.x + node.Rect.width * 0.5f;

            switch (type)
            {
                case ConnectionPointType.In:
                    Rect.y = node.Rect.y;
                    break;

                case ConnectionPointType.Out:
                    Rect.y = node.Rect.y + (node.Rect.height) - 10f;
                    break;
            }
        }
    }
}