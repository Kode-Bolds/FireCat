using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [Header("NodeStuff")]
    public GameObject NodePrefab;
    public int NumberOfNodes = 10;
    public float perimiterWith = 5;

    [Header("BuildingFireSpread")]
    public float TickToSpread;
    public int MinFlamingNodesToSpread;

    private List<FireNode> _nodes = new List<FireNode>();
    private List<Building> _neighbors = new List<Building>();
    private float _timeSinceSpread;

    Renderer buildingRenderer;
    float health = 100;

    // Use this for initialization
    void Awake()
    {
        buildingRenderer = GetComponentInChildren<Renderer>();
        float anglef = (Mathf.PI * 2) / NumberOfNodes;
        for (int i = 0; i < NumberOfNodes; i++)
        {
            var newNode = Instantiate(NodePrefab, transform);
            float x = Mathf.Sin(anglef * i) * perimiterWith;
            float z = Mathf.Cos(anglef * i) * perimiterWith;
            if(Mathf.Abs(z) > Mathf.Abs(x))
            {
                z = perimiterWith * Mathf.Sign(z);
            }
            else
            {
                x = perimiterWith * Mathf.Sign(x);
            }
            newNode.transform.localPosition += (new Vector3(x, 0, z));
            var fireNode = newNode.GetComponent<FireNode>();
            if (fireNode == null)
            {
                print("Node Prefab didn't have a fire node script HELP!");
            }
            if(i > 0)
            {
                fireNode.AddNeighbor(_nodes[i - 1]);
            }
            _nodes.Add(fireNode);
        }
        if(NumberOfNodes > 1)
        {
            _nodes[0].AddNeighbor(_nodes[NumberOfNodes - 1]);
        }
        //_nodes[0].SetOnFire();
    }

    // Update is called once per frame
    void Update()
    {
        if(_neighbors.Count < 1)
        {
            return;
        }
        int onFireCount = 0;
        bool onFire = false;
        foreach (var node in _nodes)
        {
            onFireCount += node.OnFire ? 1 : 0;
            if(onFireCount > MinFlamingNodesToSpread)
            {
                onFire = true;
            }
        }
        if(onFire)
        {
            _timeSinceSpread += Time.deltaTime;
            if(_timeSinceSpread > TickToSpread)
            {
                int index = Random.Range(0, _neighbors.Count);
                _neighbors[index].AddFire();
                _timeSinceSpread = 0;
            }

            health -= Time.deltaTime;

            buildingRenderer.material.SetFloat("_BurnTime", 1 - (health / 100f));
        }

        
    }

    public void AddNeighbor(Building neighbor)
    {
        _neighbors.Add(neighbor);
        neighbor.AddNeighborOnce(this);
    }
    private void AddNeighborOnce(Building neighbor)
    {
        _neighbors.Add(neighbor);
    }
    public void AddFire()
    {
        int index = Random.Range(0, _nodes.Count);
        _nodes[index].SetOnFire();
    }
}
