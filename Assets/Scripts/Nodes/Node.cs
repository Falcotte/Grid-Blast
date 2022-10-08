using System.Collections.Generic;
using UnityEngine;

namespace GridBlast.GridSystem.Nodes
{
    public abstract class Node : MonoBehaviour
    {
        [SerializeField] protected List<Node> neighbours = new List<Node>();
        public List<Node> Neighbours => neighbours;

        public void AddNeighbour(Node node)
        {
            neighbours.Add(node);
        }

        /// <summary>
        /// Returns all clicked neighbours of this node excluding this node itself
        /// </summary>
        /// <returns></returns>
        public List<Node> GetClickedNeighbours()
        {
            List<Node> clickedNeighbours = new List<Node>();
            List<Node> nodesToEvaluate = new List<Node>();

            foreach(var clickedNeighbour in neighbours.FindAll(x => !clickedNeighbours.Contains(x) && x.TryGetComponent(out IClickable clickable) && clickable.Clicked))
            {
                nodesToEvaluate.Add(clickedNeighbour);
            }

            while(nodesToEvaluate.Count > 0)
            {
                foreach(var clickedNeighbour in nodesToEvaluate[0].Neighbours.FindAll(x => x != this && !clickedNeighbours.Contains(x) && x.TryGetComponent(out IClickable clickable) && clickable.Clicked))
                {
                    nodesToEvaluate.Add(clickedNeighbour);
                }

                clickedNeighbours.Add(nodesToEvaluate[0]);
                nodesToEvaluate.RemoveAt(0);
            }

            return clickedNeighbours;
        }
    }
}
