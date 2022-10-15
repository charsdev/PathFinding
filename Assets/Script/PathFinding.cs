using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PathFinding 
{
	public abstract List<Nodo> GetPath (Nodo root, Nodo goal);
}

