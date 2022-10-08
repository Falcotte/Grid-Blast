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
    }
}
