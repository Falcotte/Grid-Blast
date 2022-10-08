using UnityEngine;
using TMPro;

namespace GridBlast.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private GridSystem.Grid grid;

        [SerializeField] private TMP_InputField gridSizeInputField;

        private void Start()
        {
            gridSizeInputField.text = grid.GridSize.ToString();
        }

        public void SetGridSize(string gridSize)
        {
            int gridSizeValue;

            bool success = int.TryParse(gridSize, out gridSizeValue);

            if(success)
            {
                grid.GridSize = gridSizeValue;

                // If grid size change fails, this will ensure that the inputField text shows the correct grid size
                gridSizeInputField.text = grid.GridSize.ToString();
            }
        }

        public void ResetGrid()
        {
            grid.ResetGrid();
        }
    }
}
