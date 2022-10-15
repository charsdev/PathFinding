using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStart : PathFinding
{
	private List<Nodo> _closed = new List<Nodo>();
	private List<Nodo> _open = new List<Nodo>();
	private List<Nodo> _path = new List<Nodo>();

    public void OpenNode(Nodo n) => _open.Add(n);
    public void CloseNode(Nodo n) => _closed.Add(n);
    private Nodo GetFirstNode() => _open[0];

	public int GetDistance(Nodo a, Nodo b)
	{
		int distX = Mathf.Abs(a.Row - b.Row);
		int distY = Mathf.Abs(a.Col - b.Col);
        return (distX > distY) ? (Grid.sharedInstance.horCells * distY) + (Grid.sharedInstance.verCells * (distX - distY)) : (Grid.sharedInstance.horCells * distX) + (Grid.sharedInstance.verCells * (distY - distX));
    }

	public override List<Nodo> GetPath (Nodo root, Nodo goal)
	{
		OpenNode(root);
		while (_open.Count > 0)
		{
			Nodo currentNode = GetFirstNode();
			for (int i = 1; i < _open.Count; i++)
			{
                if ((_open[i].Weight < currentNode.Weight || _open[i].Weight == currentNode.Weight)
					&& _open[i].Height < currentNode.Height)
                {
                    currentNode = _open[i];
                }
            }
			_open.Remove (currentNode);
			CloseNode (currentNode);

			if (currentNode == goal)
			{
				_path.Add (currentNode);
				while(currentNode.Father != null)
				{
					_path.Add (currentNode.Father);
					currentNode.Father = currentNode.Father.Father;
				}
				_path.Reverse();
                return _path;
			}

			for (int i = 0; i < currentNode.Adjacents.Count; i++)
			{
				Nodo n = currentNode.Adjacents [i];
				if (!n.Walkable || _closed.Contains (n))
				{
					continue;
				}
				int newCostToNeighbour = n.Weight + GetDistance(currentNode, n);
				if (newCostToNeighbour < n.Weight || !_open.Contains(n))
				{
					n.Father = currentNode;
					n.Weight = newCostToNeighbour;
					n.Height = GetDistance(n, goal);

					if (!_open.Contains(n))
					{
						OpenNode(n);
					}
				}
			}
		}
		return default;
	}
}

