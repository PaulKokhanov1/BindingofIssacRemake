using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator_two : MonoBehaviour
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
    private int startPos = 35; //start position of dungeon, always keeping grid at 9*8 
    public GameObject room;
    public Vector2 offset; //distance between each room
    public float level;       //level player is on
    private int numRooms;    //number of rooms allowed
    private int count = 0; // used to count number of rooms visited
    
    
    List<int> rng = new List<int> { 0,0,0,0};
    List<Cell> board;
    List<int> deadEnds = new List<int>(); //rooms that have no neighbors added

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
        //generate number of rooms in level
        //numRooms = Mathf.FloorToInt(Random.Range(0, 2) + 5 + level*2.6f);
        numRooms = 15;

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
        int currentCell;

        //queue to keep track of each cell for bfs 
        Queue<int> q = new Queue<int>();
        q.Enqueue(startPos);

        while (q.Count != 0)
        {

            currentCell = q.Dequeue();
            Debug.Log("working " + currentCell);

            board[currentCell].visited = true;
            //if the maze generator reaches the final position of our board then stop the generation
            if (currentCell == board.Count - 1)
            {
                break;
            }

            //check cell's neighbors
            List<int> neighbors = CheckNeighbors(currentCell);
            Debug.Log(string.Join(", ", neighbors));
            Debug.Log(string.Join(", ", q));

            //no avaliable neighbors
            if (neighbors.Count == 0)
            {
                deadEnds.Add(currentCell);
            }
            else 
            {

                Shuffle(neighbors);
                int newCell;
                if (currentCell == 35)
                {
                    foreach (var i in neighbors)
                    {
                        q.Enqueue(i);
                        newCell = i;
                        editDoors(newCell, currentCell, board);
                    }
                }
                else
                {
                    q.Enqueue(neighbors[0]);
                    newCell = neighbors[0];
                    //int newCell = neighbors[Random.Range(0, neighbors.Count)]; //get one of the neightbors randomly

                    editDoors(newCell, currentCell, board);
                }
              
            }


        }

        GenerateDungeon();


    }

    //parameter "cell" is the current cell we are checking
    List<int> CheckNeighbors(int cell)
    {
        List<int> neighbors = new List<int>();
        int tmp;

        // Generate and add 4 random integers to the list
        for (int i = 0; i < 4; i++)
        {
            tmp = Random.Range(0, 2); // Generate a random integer
            rng[i] = tmp;
        }
        Debug.Log(string.Join(", ", rng));


        if (count < numRooms)
        {
            //check up neighbor, if it is within the size and if it has a cell there already
            if (cell - size.x >= 0 && !board[Mathf.FloorToInt(cell - size.x)].visited && rng[0] == 1)
            {

                neighbors.Add(Mathf.FloorToInt(cell - size.x));
                count += 1;
            }
            //check down neighbor
            if (cell + size.x < board.Count && !board[Mathf.FloorToInt(cell + size.x)].visited && rng[1] == 1)
            {
                neighbors.Add(Mathf.FloorToInt(cell + size.x));
                count += 1;
            }
            //check right neighbor

            if ((cell + 1) % size.x != 0 && !board[Mathf.FloorToInt(cell + 1)].visited && rng[2] == 1)
            {
                neighbors.Add(Mathf.FloorToInt(cell + 1));
                count += 1;
            }
            //check left neighbor
            if (cell % size.x != 0 && !board[Mathf.FloorToInt(cell - 1)].visited && rng[3] == 1)
            {
                neighbors.Add(Mathf.FloorToInt(cell - 1));
                count += 1;
            }
        }


        return neighbors;
    }

    /// <summary>
    /// Move this to a utils class later
    /// </summary>
    void Shuffle<T>(List<T> inputList)
    {
        for (int i = 0; i < inputList.Count - 1; i++)
        {
            T temp = inputList[i];
            int rand = Random.Range(i, inputList.Count);
            inputList[i] = inputList[rand];
            inputList[rand] = temp;
        }
    }

    void editDoors(int newCell, int currentCell, List<Cell> board)
    {
        //cell is going either down or right
        if (newCell > currentCell)
        {
            //cell is going right
            if (newCell - 1 == currentCell)
            {
                board[currentCell].status[2] = true; // since we moved right need to have currentcell RIGHT door open
                board[newCell].status[3] = true; //need to have the newCell LEFT door opened to create passage
            }
            else
            {
                board[currentCell].status[1] = true; // since we moved right need to have currentcell RIGHT door open
                board[newCell].status[0] = true; //need to have the newCell LEFT door opened to create passage
            }
        }
        else
        {
            //cell is going left
            if (newCell + 1 == currentCell)
            {
                board[currentCell].status[3] = true; // since we moved right need to have currentcell RIGHT door open
                board[newCell].status[2] = true; //need to have the newCell LEFT door opened to create passage
            }
            else
            {
                board[currentCell].status[0] = true; // since we moved right need to have currentcell RIGHT door open
                board[newCell].status[1] = true; //need to have the newCell LEFT door opened to create passage
            }
        }
    }
}
