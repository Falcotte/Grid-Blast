using UnityEngine;
using GridBlast.GridSystem.Nodes;

namespace GridBlast.GridSystem
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] private Node nodePrefab;

        [SerializeField] private int gridSize;
        public int GridSize
        {
            get { return gridSize; }
            set
            {
                gridSize = value;
                CheckGridSize();
            }
        }

        private Node[,] grid;

        [SerializeField] private float padding;

        private Vector2 playSpaceSize;
        private float nodeSize;

        private void Start()
        {
            CreateGrid();
        }

        public void CreateGrid()
        {
            CheckGridSize();

            SetPlaySpaceSize();
            SetNodeSize();

            grid = new Node[gridSize, gridSize];

            for(int x = 0; x < gridSize; x++)
            {
                for(int y = 0; y < gridSize; y++)
                {
                    Node node = Instantiate(nodePrefab, transform);

                    node.transform.localPosition = new Vector3(-(nodeSize / 2f) * (gridSize - 1) + nodeSize * x, playSpaceSize.y * .75f - nodeSize * (gridSize - .5f - y), 0f);
                    node.transform.localScale = Vector2.one * (nodeSize - padding);

                    node.name = $"Node[{x},{y}]";

                    grid[x, y] = node;
                }
            }
        }

        public void ResetGrid()
        {
            for(int x = 0; x < grid.GetLength(0); x++)
            {
                for(int y = 0; y < grid.GetLength(1); y++)
                {
                    Destroy(grid[x, y].gameObject);
                }
            }

            CreateGrid();
        }

        private void CheckGridSize()
        {
            if(gridSize < 1)
            {
                Debug.LogWarning("GridSize should not be set lower than 1");
                gridSize = 1;
            }
        }

        private void SetPlaySpaceSize()
        {
            // Lower quarter of screen space is reserved for UI
            float playSpaceHeight = Camera.main.orthographicSize * 4f / 3f;
            float playSpaceWidth = Camera.main.orthographicSize * 2f * Camera.main.aspect;

            playSpaceSize = new Vector2(playSpaceWidth - padding, playSpaceHeight - padding);
        }

        private void SetNodeSize()
        {
            // Lower quarter of screen space is reserved for UI
            if(Camera.main.aspect <= .75f)
            {
                nodeSize = playSpaceSize.x / gridSize;
            }
            else
            {
                nodeSize = playSpaceSize.y / gridSize;
            }
        }
    }
}

