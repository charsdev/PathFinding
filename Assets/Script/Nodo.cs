using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodo : MonoBehaviour
{
	public Nodo Father;
    public int Col;
	public int Row;
	public Vector3 pos;
	public bool Walkable = true;
	public Renderer Render;
	public int Weight;
	public int Height;

	public void Start()
	{
		Render = GetComponent<Renderer> ();
		Weight = Random.Range (1, 10);
		Render.material.color = Color.white;
	}

	public void SetNodeColor(Color color)
    {
		Render.material.color = color;
	}

    public List<Nodo> Adjacents { get; set; }

}
