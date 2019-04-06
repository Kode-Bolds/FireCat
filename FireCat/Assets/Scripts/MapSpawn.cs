using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawn : MonoBehaviour
{
    [Header("Building")]
    public GameObject BuildingPrefab;

    [Header("Grid Dimensions")]
    public int X;
    public int Z;
    public float spacing;


    private List<List<Building>> _buildings = new List<List<Building>>();
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < X; i++)
        {
            _buildings.Add(new List<Building>());
            for (int j = 0; j < Z; j++)
            {
                var building = Instantiate(BuildingPrefab, transform);
                building.transform.localPosition = new Vector3(i * spacing, 0, j * spacing);
                Building b = building.GetComponent<Building>();
                if (b == null)
                {
                    print("Tried to instatiate a buidling without a buidling script, HELP!");
                }
                _buildings[i].Add(b);
            }
        }
        if (X > 1 && Z > 1)
        {
            for (int i = 0; i < X ; i++)
            {
                for (int j = 0; j < Z; j++)
                {
                    int one = i + 1;
                    int two = j + 1;
                    if(one < _buildings.Count)
                    {
                        _buildings[i][j].AddNeighbor(_buildings[one][j]);
                    }
                    if (two < _buildings[i].Count)
                    {
                        _buildings[i][j].AddNeighbor(_buildings[i][two]);
                    }

                }
            }
        }
        _buildings[0][0].AddFire();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
