using UnityEngine;
using System.Collections.Generic;

public class Grid: MonoBehaviour
{
	public GameObject cube;
	public int horCells = 10;
	public int verCells = 10;
	public Vector3 startPos = new Vector3(0f, 0f, 0f);
	public float spacingX = 1f;
	public float spacingY = 1f;
	public Nodo[,] cellsArray;
	public static Grid sharedInstance;

	private void Awake()
	{
		sharedInstance = this;
	}

	private void Start()
	{
		MakeGrid(horCells, verCells);
	}

	private void MakeGrid(int hor, int vert)
	{
		cellsArray = new Nodo[hor, vert];
		GameObject clone;
		Vector3 clonePos;
		var grid = new GameObject("Grid");

		for(int x = 0; x < hor; x++)
		{
			for(int y = 0; y < vert; y++)
			{
				clonePos = new Vector3(startPos.x + (x * -spacingX), startPos.y + (y * -spacingY), startPos.z);
				clone = Instantiate(cube, clonePos, Quaternion.identity);
				clone.name = $"{y} x {x}";
				clone.tag = "Node";
				var node = clone.AddComponent<Nodo>();
				node.Col = y;
				node.Row = x;
				node.pos = clone.transform.position;
				clone.transform.SetParent (grid.transform);
				cellsArray[x,y] = clone.GetComponent<Nodo>();
			}
		}

		//Agrego adyacentes
		for (int x = 0; x < hor; x++)
		{
			for (int y = 0; y < vert; y++)
			{
				var node = cellsArray[x, y];
				node.Adjacents = GetAdjacents(node);
			}
		}
		grid.transform.position = new Vector3(13f, 8f, 0f);
	}

	public List<Nodo> GetAdjacents(Nodo current)
	{
		List<Nodo> neighbours = new List<Nodo>();

		for (int ix = current.Row - 1; ix <= current.Row + 1; ix++)
		{
			for (int iy = current.Col - 1; iy <= current.Col + 1; iy++)
			{
				if (ix >= 0 && ix < cellsArray.GetLength (0) && iy >= 0 && iy < cellsArray.GetLength (1))
                {
					neighbours.Add(cellsArray[ix, iy]);
				}
			}
		}

		for (int ix = 0; ix < neighbours.Count; ix++)
		{
			if (neighbours[ix] == current)
			{
				neighbours.Remove(current);
				break;
			}
		}

		return neighbours;
	}
}