using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    /// <summary>
    /// Cell class used to hold info for each cell in the dungeon
    /// </summary>
    public class Cell
    {
        //could implement these two variables using onehot encoding and simplify it to a singular var
        public bool visited = false;
        public bool[] status = new bool[4]; //used for door/wall opening
        public bool[] statusBoss = new bool[4]; //used for door/wall opening
    }

    public Vector2 size; // the size of the grid
    public int startPos = 0; //start position of dungeon
    public GameObject room;
    public Vector2 offset; //distance between each room

    List<Cell> board;

    // Start is called before the first frame update
    void Start()
    {
        MazeGenerator();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void GenerateDungeon()
    {

        //go through all rows and columns of the board
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {

                Cell currentCell = board[Mathf.FloorToInt(i + j * size.x)];
                if (currentCell.visited)
                {
                    var newRoom = Instantiate(room, new Vector3(i * offset.x, -j * offset.y, 0), Quaternion.identity, transform).GetComponent<RoomBehaviour>();
                    newRoom.UpdateRoom(currentCell.status);

                    newRoom.name += " " + i + "-" + j;
                }


            }
        }
    }

    void MazeGenerator()
    {
        board = new List<Cell>();

        //initializing our board
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                board.Add(new Cell());

            }
        }

        //keeps track which position were at currently at
        int currentCell = startPos;

        //keeping track of the path we have made, used for the backtracking portion of the algo
        Stack<int> path = new Stack<int>();

        int k = 0;

        //adjust case to minimize or enlarge dungeon
        while(k < 1000)
        {
            Debug.Log(currentCell);
            k++;

            board[currentCell].visited = true;

            if(currentCell == board.Count - 1)
            {
                break;
            }


            //check cell's neighbors
            List<int> neighbors = CheckNeighbors(currentCell);

            //no avaliable neighbors
            if (neighbors.Count == 0)
            {
                if (path.Count == 0)
                {
                    break;
                }
                else
                {
                    currentCell = path.Pop(); // backtrack to previous cell
                }
            }
            else
            {
                path.Push(currentCell);

                int newCell = neighbors[Random.Range(0, neighbors.Count)]; //get one of the neightbors randomly

                //cell is going either left or right
                if (newCell > currentCell)
                {
                    //cell is going right
                    if (newCell - 1 == currentCell)
                    {
                        board[currentCell].status[2] = true; // since we moved right need to have currentcell RIGHT door open
                        currentCell = newCell;
                        board[currentCell].status[3] = true; //need to have the newCell LEFT door opened to create passage
                    }
                    else
                    {
                        board[currentCell].status[1] = true; // since we moved right need to have currentcell RIGHT door open
                        currentCell = newCell;
                        board[currentCell].status[0] = true; //need to have the newCell LEFT door opened to create passage
                    }
                }
                else
                {
                    //cell is going left
                    if (newCell + 1 == currentCell)
                    {
                        board[currentCell].status[3] = true; // since we moved right need to have currentcell RIGHT door open
                        currentCell = newCell;
                        board[currentCell].status[2] = true; //need to have the newCell LEFT door opened to create passage
                    }
                    else
                    {
                        board[currentCell].status[0] = true; // since we moved right need to have currentcell RIGHT door open
                        currentCell = newCell;
                        board[currentCell].status[1] = true; //need to have the newCell LEFT door opened to create passage
                    }
                }
            }

        }

        GenerateDungeon();


    }

    //parameter "cell" is the current cell we are checking
    List<int> CheckNeighbors(int cell)
    {
        List<int> neighbors = new List<int>();

        //check up neighbor, if it is within the size and if it has a cell there already
        if (cell - size.x >= 0 && !board[Mathf.FloorToInt(cell-size.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell-size.x));
        }

        //check down neighbor
        if (cell + size.x < board.Count && !board[Mathf.FloorToInt(cell + size.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + size.x));
        }


        //check right neighbor

        if ((cell+1) % size.x != 0 && !board[Mathf.FloorToInt(cell + 1 )].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + 1));
        }


        //check left neighbor
        if (cell % size.x != 0 && !board[Mathf.FloorToInt(cell - 1)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell - 1));
        }


        return neighbors;
    }
}
