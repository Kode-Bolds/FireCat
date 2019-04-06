using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralMaze : MonoBehaviour
{
    public GameObject BuildingPrefab;
    public static int columns = 9;
    public static int rows = 9;
    private static System.Random rnd = new System.Random();
    public static Vector2 start;

    //enum used to store each tiles contents
    public enum tile
    {
        ROAD,
        BUILDING
    }

    //enum used to select directions for carving
    private enum directions
    {
        NORTH,
        SOUTH,
        EAST,
        WEST
    }

    private static tile[,] grid = new tile[columns, rows];
    private static bool[,] visited = new bool[columns, rows];

    // Use this for initialization
    void Start()
    {
        tile[,] tiles = mazeGenerator();
        GameObject[,] go = new GameObject[columns, rows];
        //instantiate buildings
        for (int i = 0; i < tiles.GetLength(0); ++i)
        {
            for (int j = 0; j < tiles.GetLength(1); ++j)
            {
                if (tiles[i,j] == tile.BUILDING)
                {
                    //create a building
                    go[i,j] = Instantiate(BuildingPrefab, new Vector3(i*20,0,j*20), Quaternion.identity);
                        
                }
            }
        }

        for (int i = 0; i < columns; ++i)
        {
            for (int j = 0; j < rows; ++j)
            {
                if (tiles[i, j] == tile.BUILDING)
                {
                    Building b = go[i,j].GetComponent<Building>();
                    int nextIndex = i + 1;
                    if (nextIndex > 0 && nextIndex < columns)
                    {
                        if (tiles[i + 1, j] == tile.BUILDING)
                        {
                            Building b2 = go[i + 1, j].GetComponent<Building>();
                            b.AddNeighbor(b2);
                        }
                    }
                    nextIndex = j + 1;
                    if (nextIndex > 0 && nextIndex < rows)
                    {
                        if (tiles[i, j + 1] == tile.BUILDING)
                        {
                            Building b2 = go[i, j + 1].GetComponent<Building>();
                            b.AddNeighbor(b2);
                        }
                    }

                    nextIndex = i - 1;
                    if (nextIndex > 0 && nextIndex < columns)
                    {
                        if (tiles[i - 1, j] == tile.BUILDING)
                        {
                            Building b2 = go[i - 1, j].GetComponent<Building>();
                            b.AddNeighbor(b2);
                        }
                    }

                    nextIndex = j - 1;
                    if (nextIndex > 0 && nextIndex < rows)
                    {
                        if (tiles[i, j - 1] == tile.BUILDING)
                        {
                            Building b2 = go[i, j - 1].GetComponent<Building>();
                            b.AddNeighbor(b2);
                        }
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }




    //generate a maze
    public static tile[,] mazeGenerator()
    {
        //reset arrays
        InitialiseGrid();
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                visited[x, y] = false;
            }

        }

        //set all edges to visited
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                if ((x == 0) || (x == columns - 1) || (y == 0) || (y == rows - 1))
                {
                    visited[x, y] = true;
                }
            }
        }

        start = getRandomPos();

        // if youve been to this square already (its a boundary) then find a new square
        while (visited[(int)start.x, (int)start.y] != false)
        {
            start = getRandomPos();
        }

        Vector2 currentCell = start;
        visited[(int)start.x, (int)start.y] = true;
        grid[(int)currentCell.x, (int)currentCell.y] = tile.ROAD;
        carveWall(currentCell);
        cleanUpMaze();
        loopDeadEnds();

        return grid;
    }

    /// <summary>
    /// Gets a random number and ensures it is odd
    /// </summary>
    /// <returns> a random position in the maze as a vector2 </returns>
    private static Vector2 getRandomPos()
    {
        Vector2 position;
        position.x = rnd.Next(1, columns - 2);
        position.y = rnd.Next(1, rows - 2);

        if (position.x % 2 == 0)
        {
            position.x += 1;
        }
        if (position.y % 2 == 0)
        {
            position.y += 1;
        }

        return position;
    }

    /// <summary>
    /// Sets the whole grid to walls
    /// </summary>
    private static void InitialiseGrid()
    {
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                grid[x, y] = tile.BUILDING;
            }
        }

    }

    /// <summary>
    /// Find spaces in the maze and fills them in
    /// </summary>
    private static void cleanUpMaze()
    {
        for (int x = 1; x < columns - 1; x++)
        {
            for (int y = 1; y < rows - 1; y++)
            {
                if ((grid[x + 1, y] == tile.BUILDING)
                    && (grid[x + 1, y + 1] == tile.BUILDING)
                    && (grid[x, y + 1] == tile.BUILDING)
                    && (grid[x - 1, y + 1] == tile.BUILDING)
                    && (grid[x - 1, y] == tile.BUILDING)
                    && (grid[x - 1, y - 1] == tile.BUILDING)
                    && (grid[x, y - 1] == tile.BUILDING)
                    && (grid[x + 1, y - 1] == tile.BUILDING))
                {
                    carveWall(new Vector2(x, y));
                }
            }
        }
    }

    /// <summary>
    /// Carve recursively
    /// </summary>
    /// <param name="currentCell"></param>
    private static void carveWall(Vector2 currentCell)
    {

        //list of available directions
        List<directions> neighbours = new List<directions>() { directions.NORTH, directions.EAST, directions.SOUTH, directions.WEST };

        int n = neighbours.Count;

        //randomise the list order
        while (n > 1)
        {
            n--;
            int k = rnd.Next(n + 1);
            directions temp = neighbours[k];
            neighbours[k] = neighbours[n];
            neighbours[n] = temp;
        }

        Vector2 nextCell = currentCell;

        //go through each direction
        while (neighbours.Count > 0)
        {
            directions nextDir = neighbours[0];
            neighbours.RemoveAt(0);
            nextCell = currentCell;

            switch (nextDir)
            {
                case (directions.NORTH):
                    {
                        nextCell.y += 2;
                        break;
                    }
                case (directions.EAST):
                    {
                        nextCell.x += 2;
                        break;
                    }
                case (directions.SOUTH):
                    {
                        nextCell.y -= 2;
                        break;
                    }
                case (directions.WEST):
                    {
                        nextCell.x -= 2;
                        break;
                    }
                default:
                    {
                        return;
                    }
            }

            //if the next cell is a valid one
            if ((nextCell.x > 0) && (nextCell.x < columns - 1) && (nextCell.y > 0) && (nextCell.y < rows - 1))
            {
                //if the next cell is unvisited and isnt a room
                if (visited[(int)nextCell.x, (int)nextCell.y] == false)
                {
                    //carve and recurse
                    grid[(int)nextCell.x, (int)nextCell.y] = tile.ROAD;
                    visited[(int)nextCell.x, (int)nextCell.y] = true;
                    Vector2 wallToBeBroken = currentCell + ((nextCell - currentCell) / 2);
                    grid[(int)wallToBeBroken.x, (int)wallToBeBroken.y] = tile.ROAD;
                    visited[(int)wallToBeBroken.x, (int)wallToBeBroken.y] = true;
                    carveWall(nextCell);
                }
            }
        }
    }

    /// <summary>
    /// Carves dead ends to make the maze have loops
    /// </summary>
    private static void loopDeadEnds()
    {
        //list of available directions
        List<directions> neighbours = new List<directions>();

        //go through each cell in the maze, and inspect it if it is a corridor
        for (int x = 0; x < columns - 1; x++)
        {
            for (int y = 0; y < rows - 1; y++)
            {
                if (grid[x, y] == tile.ROAD)
                {
                    //Clear list of neighbours andfill it with neighbours that are walls
                    neighbours.Clear();
                    int wallNeighbours = 0;
                    {
                        if (grid[x + 1, y] == tile.BUILDING)
                        {
                            wallNeighbours += 1;
                            neighbours.Add(directions.EAST);
                        }
                        if (grid[x, y + 1] == tile.BUILDING)
                        {
                            wallNeighbours += 1;
                            neighbours.Add(directions.NORTH);
                        }
                        if (grid[x - 1, y] == tile.BUILDING)
                        {
                            wallNeighbours += 1;
                            neighbours.Add(directions.WEST);
                        }
                        if (grid[x, y - 1] == tile.BUILDING)
                        {
                            wallNeighbours += 1;
                            neighbours.Add(directions.SOUTH);
                        }

                        //if there are 3 walls as neighbours then this is a dead end
                        if (wallNeighbours == 3)
                        {

                            int n = neighbours.Count;

                            //randomise the neighbour list order
                            while (n > 1)
                            {
                                n--;
                                int k = rnd.Next(n + 1);
                                directions temp = neighbours[k];
                                neighbours[k] = neighbours[n];
                                neighbours[n] = temp;
                            }

                            Vector2 currentCell = new Vector2(x, y);
                            Vector2 nextCell = currentCell;

                            //while nothing has been carved yet - loop
                            bool carved = false;
                            while (!carved)
                            {
                                //take the first direction of the list and then remove it
                                directions nextDir = neighbours[0];
                                neighbours.RemoveAt(0);
                                nextCell = currentCell;

                                //find the cell in that direction
                                switch (nextDir)
                                {
                                    case (directions.NORTH):
                                        {
                                            nextCell.y += 2;
                                            break;
                                        }
                                    case (directions.EAST):
                                        {
                                            nextCell.x += 2;
                                            break;
                                        }
                                    case (directions.SOUTH):
                                        {
                                            nextCell.y -= 2;
                                            break;
                                        }
                                    case (directions.WEST):
                                        {
                                            nextCell.x -= 2;
                                            break;
                                        }
                                    default:
                                        {
                                            return;
                                        }
                                }
                                //if the cell is a valid cell in the grid and is a corridor
                                if ((nextCell.x > 0) && (nextCell.x < columns - 1) && (nextCell.y > 0) && (nextCell.y < rows - 1))
                                {
                                    if (grid[(int)nextCell.x, (int)nextCell.y] == tile.ROAD)
                                    {
                                        //break the cell through to the corridor to remove the dead end
                                        Vector2 wallToBeBroken = currentCell + ((nextCell - currentCell) / 2);
                                        grid[(int)wallToBeBroken.x, (int)wallToBeBroken.y] = tile.ROAD;
                                        carved = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}


