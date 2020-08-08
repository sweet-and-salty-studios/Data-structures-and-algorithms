using System.Collections.Generic;
using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public class GridManager : Singelton<GridManager>
    {
        #region VARIABLES

        [Space]
        [Header("Data Structure Settigs")]
        public ALGORITHM_TYPE AlgorithmType;

        [Space]
        [Header("Algorithm Settings")]
        public DATA_STRUCTURE_TYPE DataStructureType;

        [Space]
        [Header("Node References")]
        public Node NodePrefab;
        public Node StartNode;
        public Node TargetNode;
        public Transform NodeParent;

        private Node[,] grid;
        private Vector2 gridWorldSize;
        private Vector2Int GridSize;

        private readonly float cellSize = 42;

        private List<Node> openList = new List<Node>();
        private List<Node> closedList = new List<Node>();

        #endregion VARIABLES

        #region PROPERTIES

        #endregion PROPERTIES

        #region UNITY_FUNCTIONS

        private void Awake()
        {
            gridWorldSize = new Vector2(Screen.width, Screen.height);

            GridSize.x = Mathf.RoundToInt(gridWorldSize.x / cellSize);
            GridSize.y = Mathf.RoundToInt(gridWorldSize.y / cellSize);
        }

        private void Start()
        {
            CreateGrid();
        }

        #endregion UNITY_FUNCTIONS

        #region CUSTOM_FUNCTIONS

        private void CreateGrid()
        {
            grid = new Node[GridSize.x, GridSize.y];

            for(int x = 0; x < grid.GetLength(0); x++)
            {
                for(int y = 0; y < grid.GetLength(1); y++)
                {
                    var newNode = Instantiate(NodePrefab, NodeParent);

                    newNode.name = $"Node {x} , {y}";

                    grid[x, y] = newNode;
                }
            }
        }

        public Node GetNodeFromPoint(Vector2 point)
        {
            return null;
        }

        public void ChangeNodeType(Node node)
        {
            if(node.Type == NODE_TYPE.START)
            {
                node.ChangeType(NODE_TYPE.WALKABLE);
                StartNode = null;
                return;
            }


            if(node.Type == NODE_TYPE.TARGET)
            {
                node.ChangeType(NODE_TYPE.WALKABLE);
                TargetNode = null;
                return;
            }

            if(StartNode == null)
            {
                StartNode = node;
                StartNode.ChangeType(NODE_TYPE.START);
                return;
            }

            if(TargetNode == null)
            {
                TargetNode = node;
                TargetNode.ChangeType(NODE_TYPE.TARGET);
                return;
            }

            node.ChangeType(node.Type == NODE_TYPE.WALKABLE ? NODE_TYPE.UN_WALKABLE : NODE_TYPE.WALKABLE);
        }

        public void ClearGrid()
        {
            if(grid == null || grid.Length == 0)
            {
                return;
            }

            for(int x = 0; x < grid.GetLength(0); x++)
            {
                for(int y = 0; y < grid.GetLength(1); y++)
                {
                    if(grid[x, y].Type == NODE_TYPE.WALKABLE)
                    {
                        continue;
                    }

                    grid[x, y].ChangeType(NODE_TYPE.WALKABLE);           
                }

                StartNode = null;
                TargetNode = null;
            }
        }

        public void BreathFirstSearch(Node[,] graph)
        {
            var n = graph.Length;
            var visitedNodes = new List<Node>(graph.Length);


        }

        public void Dijkstra(Node[,] grid, Node originNode, Node destination)
        {
           
        }

        #endregion CUSTOM_FUNCTIONS
    }
}

