using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public int Rows = 4;
    public int Columns = 4;
    public GameObject Wall;
    public GameObject Floor;

    private MazeCell[,] grid;
    private int currentRow = 0;
    private int currentColumn = 0;

    // Start is called before the first frame update
    void Start()
    {
        CreateGrid();
        HuntAndKill();
    }

    void CreateGrid()
    {
        float size = Wall.transform.localScale.x;
        grid = new MazeCell[Rows, Columns];

        for (int i = 0; i < Columns; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                GameObject floor = Instantiate(Floor, new Vector3(j * size, 0, -i * size), Quaternion.identity);
                floor.name = "Floor_" + i + "_" + j;

                GameObject upWall = Instantiate(Wall, new Vector3(j * size, 1.75f, -i * size + 1.25f), Quaternion.identity);
                upWall.name = "UpWall_" + i + "_" + j;

                GameObject downWall = Instantiate(Wall, new Vector3(j * size, 1.75f, -i * size - 1.25f), Quaternion.identity);
                downWall.name = "DownWall_" + i + "_" + j;

                GameObject leftWall = Instantiate(Wall, new Vector3(j * size - 1.25f, 1.75f, -i * size), Quaternion.Euler(0, 90, 0));
                leftWall.name = "LeftWall_" + i + "_" + j;

                GameObject rightWall = Instantiate(Wall, new Vector3(j * size + 1.25f, 1.75f, -i * size), Quaternion.Euler(0, 90, 0));
                rightWall.name = "RighttWall_" + i + "_" + j;

                grid[i, j] = new MazeCell();
                grid[i, j].UpWall = upWall;
                grid[i, j].DownWall = downWall;
                grid[i, j].LeftWall = leftWall;
                grid[i, j].RightWall = rightWall;

                floor.transform.parent = transform;
                upWall.transform.parent = transform;
                downWall.transform.parent = transform;
                leftWall.transform.parent = transform;
                rightWall.transform.parent = transform;

            }
        }
    }

    bool UnvisitedNeighbours()
    {
        //check up
        if (IsUnvisited(currentRow - 1, currentColumn))
        {
            return true;
        }

        //check down
        if (IsUnvisited(currentRow + 1, currentColumn))
        {
            return true;
        }

        //check left
        if (IsUnvisited(currentRow, currentColumn + 1))
        {
            return true;
        }

        //check right
        if (IsUnvisited(currentRow, currentColumn - 1))
        {
            return true;
        }
        return false;
    }

    bool IsUnvisited(int row, int column)
    {
        if (row >= 0 && row < Rows && column >= 0 && column < Columns && grid[row, column].Visited)
        {
            return true;
        }
        return false;
    }
    void HuntAndKill()
    {
        grid[currentRow, currentColumn].Visited = true;

        while (UnvisitedNeighbours())
        {
            int direction = Random.Range(0, 4);

            //check up
            if (direction == 0)
            {
                Debug.Log("Check Up");
                if (IsUnvisited(currentRow - 1, currentColumn))
                {
                    if (grid[currentRow, currentColumn].UpWall)
                    {
                        Destroy(grid[currentRow, currentColumn].UpWall);
                    }

                    currentRow--;
                    grid[currentRow, currentColumn].Visited = true;

                    if (grid[currentRow, currentColumn].DownWall)
                    {
                        Destroy(grid[currentRow, currentColumn].DownWall);
                    }
                }
            }

            //check down
            else if (direction == 1)
            {
                Debug.Log("Check Down");
                if (IsUnvisited(currentRow + 1, currentColumn))
                {
                    if (grid[currentRow, currentColumn].DownWall)
                    {
                        Destroy(grid[currentRow, currentColumn].DownWall);
                    }

                    currentRow++;
                    grid[currentRow, currentColumn].Visited = true;

                    if (grid[currentRow, currentColumn].UpWall)
                    {
                        Destroy(grid[currentRow, currentColumn].UpWall);
                    }
                }
            }

            //check left
            else if (direction == 2)
            {
                Debug.Log("Check Left");
                if (IsUnvisited(currentRow, currentColumn - 1))
                {
                    if (grid[currentRow, currentColumn].LeftWall)
                    {
                        Destroy(grid[currentRow, currentColumn].LeftWall);
                    }

                    currentColumn--;
                    grid[currentRow, currentColumn].Visited = true;

                    if (grid[currentRow, currentColumn].RightWall)
                    {
                        Destroy(grid[currentRow, currentColumn].RightWall);
                    }
                }
            }

            //chcek right
            else if (direction == 3)
            {
                Debug.Log("Check Right");
                if (IsUnvisited(currentRow, currentColumn + 1))
                {
                    if (grid[currentRow, currentColumn].RightWall)
                    {
                        Destroy(grid[currentRow, currentColumn].RightWall);
                    }

                    currentColumn++;
                    grid[currentRow, currentColumn].Visited = true;

                    if (grid[currentRow, currentColumn].LeftWall)
                    {
                        Destroy(grid[currentRow, currentColumn].LeftWall);
                    }
                }
            }
        }
    }
}
