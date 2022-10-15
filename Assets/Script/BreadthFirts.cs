using System.Collections.Generic;

public class BreadthFirts : PathFinding
{
    Queue<Nodo> closed = new Queue<Nodo>();
    Queue<Nodo> open = new Queue<Nodo>();
    List<Nodo> path = new List<Nodo>();

    public void OpenNode(Nodo n) => open.Enqueue(n);
    public void CloseNode(Nodo n) => closed.Enqueue(n);
    public Nodo GetNode() => open.Dequeue();

    public override List<Nodo> GetPath(Nodo root, Nodo goal)
    {
        OpenNode(root);

        while (open.Count > 0)
        {
            Nodo currentNode = GetNode();
            if (currentNode == goal)
            {
                path.Add(currentNode);
                while (currentNode.Father != null)
                {
                    path.Add(currentNode.Father);
                    currentNode.Father = currentNode.Father.Father;
                }
                path.Reverse();
                return path;
            }

            CloseNode(currentNode);

            for (int i = 0; i < currentNode.Adjacents.Count; i++)
            {
                Nodo n = currentNode.Adjacents[i];
                if (!n.Walkable || !closed.Contains(n))
                {
                    OpenNode(n);
                    n.Father = currentNode;
                }
            }
        }
        return default;
    }
}
