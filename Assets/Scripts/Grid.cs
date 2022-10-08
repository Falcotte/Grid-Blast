using UnityEngine;

namespace GridBlast.Grid
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] private Node nodePrefab;

        [SerializeField] private int gridSize;
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

                    node.transform.localPosition = new Vector3(-(nodeSize / 2f) * (gridSize - 1) + nodeSize * x, playSpaceSize.y / 2f - nodeSize * (gridSize - .5f - y), 0f);
                    node.transform.localScale = Vector2.one * (nodeSize - padding);

                    node.name = $"Node[{x},{y}]";

                    grid[x, y] = node;
                }
            }
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
            float playSpaceHeight = Camera.main.orthographicSize * 2f;
            float playSpaceWidth = playSpaceHeight * Camera.main.aspect;

            playSpaceSize = new Vector2(playSpaceWidth - padding, playSpaceHeight - padding);
        }

        private void SetNodeSize()
        {
            if(Camera.main.aspect <= 1)
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

