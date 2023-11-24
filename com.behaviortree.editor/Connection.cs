using UnityEditor;
using UnityEngine;

namespace HCEngine.BT.Editor
{
    public class Connection
    {
        public ConnectionPoint inPoint;
        public ConnectionPoint outPoint;

        public Connection(ConnectionPoint inPoint, ConnectionPoint outPoint)
        {
            this.inPoint = inPoint;
            this.outPoint = outPoint;
        }

        public void Draw()
        {
            Handles.DrawLine(
                inPoint.Rect.center,
                outPoint.Rect.center
                //inPoint.Rect.center + Vector2.left * 50f,
                //outPoint.Rect.center - Vector2.left * 50f,
                //Color.white,
                //null,
                //2f
            );
        }
    }
}