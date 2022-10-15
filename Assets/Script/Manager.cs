using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Manager : MonoBehaviour
{
    private Nodo _root;
    private Nodo _goal;
    public PathFinding Pathfinding;
    private List<Nodo> _path = new List<Nodo>();
    private RaycastHit raycastHit = new RaycastHit();
    public Text ObjectText;

    public enum Type
    {
        BreadthFirts,
        DeepFirts,
        Dijktra,
        Astar,
        nothing
    }

    public Type FindBy;


    public void ChangeTo(Type type)
    {
        FindBy = type;
    }

    public void ChangeToBreadthFirts()
    {
        FindBy = Type.BreadthFirts;
    }
    public void ChangeToDeepFirts()
    {
        FindBy = Type.DeepFirts;
    }
    public void ChangeToDijktra()
    {
        FindBy = Type.Dijktra;
    }
    public void ChangeToAstar()
    {
        FindBy = Type.Astar;
    }

    public void Update()
    {
        SetRootAndGoal();
    }

    public void SetRootAndGoal()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit))
        {
            var CurrentNode = GetNode();
            ObjectText.text = GetNode().Weight.ToString();
            Vector3 newPos = raycastHit.transform.position;
            newPos.z = -1;
            ObjectText.transform.position = newPos;

            if (Input.GetMouseButtonDown(0))
            {
                _root?.SetNodeColor(Color.white);
                _root = CurrentNode;
                _root.SetNodeColor(Color.blue);
            }

            if (Input.GetMouseButtonDown(1))
            {
                _goal?.SetNodeColor(Color.white);
                _goal = CurrentNode;
                _goal.SetNodeColor(Color.red);
            }

            if (Input.GetMouseButtonDown(2))
            {
                Nodo n = CurrentNode;

                if (_goal || _root)
                {
                    n.Walkable = false;
                }

                n.SetNodeColor(Color.black);
            }
        }
    }

    private Nodo GetNode()
    {
        return raycastHit.collider.gameObject.GetComponent<Nodo>();
    }

    public void Clear()
    {
        if (_path.Count > 0)
        {
            _path.Clear();
        }

        for (int x = 0; x < Grid.sharedInstance.horCells; x++)
        {
            for (int y = 0; y < Grid.sharedInstance.verCells; y++)
            {
                var node = Grid.sharedInstance.cellsArray[x, y];
                node.Father = null;
                node.Walkable = true;

                if (_root != node || _goal != node)
                {
                    node.SetNodeColor(Color.white);
                }

            }
        }
    }

    public void Play()
    {
        Pathfinding = FindBy switch
        {
            Type.BreadthFirts => new BreadthFirts(),
            Type.DeepFirts => new DeepFirts(),
            Type.Dijktra => new Dijktra(),
            Type.Astar => new AStart(),
            _ => null,
        };

        if(_root != null && _goal != null)
        {
            _path = Pathfinding.GetPath(_root, _goal);
        }

        for (int i = 0; i < _path.Count; i++)
        {
            Nodo n = _path[i];
            if (n != _root && n != _goal && n == _path[i].Walkable)
            {
                _path[i].SetNodeColor(Color.green);
            }
        }
    }
}

