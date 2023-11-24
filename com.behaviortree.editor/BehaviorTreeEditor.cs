using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace HCEngine.BT.Editor
{
    public class BehaviorTreeEditor : EditorWindow
    {
        private float Zoom = 1f;

        private float xStartPosition => 20 * Zoom;
        private float xDistanceBetween => 110 * Zoom;
        private float yStartPosition => 20 * Zoom;
        private float yDistanceBetween => 100 * Zoom;

        private float width => 110 * Zoom;
        private float height => 80 * Zoom;


        private List<NodeEditor> _nodes = new List<NodeEditor>();
        private List<Connection> _connections = new List<Connection>();

        private GUIStyle _nodeStyleSuccess;
        private GUIStyle _nodeStyleRunning;
        private GUIStyle _nodeStyleFailure;
        private GUIStyle _nodeStyleAbort;

        private GUIStyle _inPointStyle;
        private GUIStyle _outPointStyle;

        [MenuItem("Window/Behavior Tree")]
        private static void OpenWindow()
        {
            BehaviorTreeEditor window = GetWindow<BehaviorTreeEditor>();
            window.titleContent = new GUIContent("Behavior Tree");
        }

        private void OnEnable()
        {
            _nodeStyleFailure = NodeStype("builtin skins/darkskin/images/node0.png");
            _nodeStyleRunning = NodeStype("builtin skins/darkskin/images/node1.png");
            _nodeStyleSuccess = NodeStype("builtin skins/darkskin/images/node3.png");
            _nodeStyleAbort = NodeStype("builtin skins/darkskin/images/node6.png");

            _inPointStyle = new GUIStyle();
            _inPointStyle.border = new RectOffset(4, 4, 0, 0);

            _outPointStyle = new GUIStyle();
            _outPointStyle.border = new RectOffset(4, 4, 0, 0);

            DisplayerTree.OnTreeChanged += DrawTree;

            DrawTree(DisplayerTree.Root);
        }


        private GUIStyle NodeStype(string image)
        {
            GUIStyle newStyle = new GUIStyle();

            newStyle.normal.background = EditorGUIUtility.Load(image) as Texture2D;
            newStyle.border = new RectOffset(12, 12, 12, 12);
            newStyle.fontStyle = FontStyle.Bold;
            newStyle.fontSize = 11;
            newStyle.alignment = TextAnchor.MiddleCenter;
            newStyle.normal.textColor = Color.white;
            newStyle.contentOffset = new Vector2(0, 0);

            return newStyle;
        }

        private void OnDisable()
        {
            DisplayerTree.OnTreeChanged -= DrawTree;
        }

        private void OnGUI()
        {
            DrawNodes();

            DrawConnections();

            Repaint();
        }

        private void DrawNodes()
        {
            if (DisplayerTree.Root != null)
            {
                for (int i = 0; i < _nodes.Count; i++)
                {
                    _nodes[i].Draw();
                }
            }
        }

        private void DrawConnections()
        {
            if (_connections != null)
            {
                for (int i = 0; i < _connections.Count; i++)
                {
                    _connections[i].Draw();
                }
            }
        }

        private void DrawTree(INode treeRoot)
        {
            if (treeRoot == null)
                return;

            _nodes.Clear();

            int width = treeRoot.Width();

            float xPosition = (width / 2) * xDistanceBetween;
            float yPosition = yStartPosition;

            Vector2 position = new Vector2(xPosition, yPosition);

            NodeEditor root = new NodeEditor(treeRoot, position, this.width, height,
                _nodeStyleFailure, _nodeStyleSuccess, _nodeStyleRunning, _nodeStyleAbort,
                _inPointStyle, _outPointStyle);
            _nodes.Add(root);
            AddNode(root, xStartPosition, xDistanceBetween, yStartPosition, yDistanceBetween, 1);
        }

        private void AddNode(NodeEditor node, float xStartPosition, float xDistanceBetween,
            float yStartPosition, float yDistanceBetween, int depth)
        {
            if (node.Node is IComposite)
            {
                IComposite composite = (IComposite)node.Node;
                int width = 0;
                for (int i = 0; i < composite.CountChildren; i++)
                {
                    int newWidth = composite[i].Width();

                    float xPosition = (width + newWidth / 2) * xDistanceBetween + xStartPosition;
                    float yPosition = depth * yDistanceBetween + yStartPosition;

                    Vector2 position = new Vector2(xPosition, yPosition);

                    NodeEditor child = new NodeEditor(composite[i], position, this.width, height,
                        _nodeStyleFailure, _nodeStyleSuccess, _nodeStyleRunning, _nodeStyleAbort,
                        _inPointStyle, _outPointStyle);

                    _nodes.Add(child);
                    _connections.Add(new Connection(node.OutPoint, child.InPoint));
                    AddNode(child, xStartPosition + width * xDistanceBetween, xDistanceBetween, yStartPosition, yDistanceBetween, depth + 1);

                    width += newWidth;
                }
            }
            else if (node.Node is IDecorator)
            {
                int totalWidth = node.Node.Width();

                float xPosition = (totalWidth / 2) * xDistanceBetween + xStartPosition;
                float yPosition = depth * yDistanceBetween + yStartPosition;

                Vector2 position = new Vector2(xPosition, yPosition);

                NodeEditor child = new NodeEditor(((IDecorator)node.Node).Child, position, width, height,
                    _nodeStyleFailure, _nodeStyleSuccess, _nodeStyleRunning, _nodeStyleAbort,
                    _inPointStyle, _outPointStyle);

                _nodes.Add(child);
                _connections.Add(new Connection(node.OutPoint, child.InPoint));
                AddNode(child, xStartPosition, xDistanceBetween, yStartPosition, yDistanceBetween, depth + 1);
            }
        }
    }
}