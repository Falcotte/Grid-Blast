namespace GridBlast.GridSystem.Nodes
{
    public interface IClickable
    {
        public bool Clicked { get; set; }

        public void Click();
    }
}
