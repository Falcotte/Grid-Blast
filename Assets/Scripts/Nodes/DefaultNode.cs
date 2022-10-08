using UnityEngine;

namespace GridBlast.GridSystem.Nodes
{
    public class DefaultNode : Node, IClickable
    {
        public void Click()
        {
            Debug.Log("clicked");
        }
    }
}
