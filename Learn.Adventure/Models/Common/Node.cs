namespace Learn.Adventure.Models.Common
{
    public class Node
    {
        public string Text { get; set; }
        public Node YesNode { get; set; }
        public Node NoNode { get; set; }
    }
}