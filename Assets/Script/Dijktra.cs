using System;
using UnityEngine;
using System.Collections.Generic;

public class Dijktra : PathFinding
{
    List<Nodo> closed = new List<Nodo>();
    List<Nodo> open = new List<Nodo>();
    List<Nodo> path = new List<Nodo>();

    public void OpenNode(Nodo n) => open.Add(n);
    public void CloseNode(Nodo n) => closed.Add(n);

    public Nodo GetNode()
    {
        return open[0];
    }

    public override List<Nodo> GetPath(Nodo root, Nodo goal)
    {
        OpenNode(root);
        while (open.Count > 0)
        {
            Nodo currentNode = GetNode();
            for (int i = 1; i < open.Count; i++)
            {
                if (open[i].Weight < currentNode.Weight || open[i].Weight == currentNode.Weight)
                {
                    currentNode = open[i];
                }
            }

            open.Remove(currentNode);
            CloseNode(currentNode);

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
            for (int i = 0; i < currentNode.Adjacents.Count; i++)
            {
                Nodo n = currentNode.Adjacents[i];
                if (!n.Walkable || closed.Contains(n))
                {
                    continue;
                }
                int newCostToNeighbour = n.Weight + currentNode.Weight;
                if (newCostToNeighbour < n.Weight || !open.Contains(n))
                {
                    n.Father = currentNode;
                    n.Weight = newCostToNeighbour;
                    if (!open.Contains(n))
                    {
                        OpenNode(n);
                    }
                }
            }
        }
        return default;
    }
}


