using System;
using System.Collections.Generic;

public class DeepFirts : PathFinding
{
    Stack<Nodo> closed = new Stack<Nodo>();
    Stack<Nodo> open = new Stack<Nodo>();
    List<Nodo> path = new List<Nodo>();

    public void OpenNode(Nodo n) => open.Push(n);
    public void CloseNode(Nodo n) => closed.Push(n);
    public Nodo Node => open.Pop();

    public override List<Nodo> GetPath(Nodo root, Nodo goal)
    {
        OpenNode(root);

        while (open.Count > 0)
        {
            Nodo currentNode = Node;

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
                if (!n.Walkable || closed.Contains(n))
                {
                    OpenNode(n);
                    n.Father = currentNode;
                }
            }
        }
        return default;
    }
}

