using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator_three : MonoBehaviour
{
    /// <summary>
    /// Cell class used to hold info for each cell in the dungeon
    /// </summary>
    public class Cell
    {
        //could implement these two variables using onehot encoding and simplify it to a singular var
        public int visited = 0;
        public bool[] status = new bool[4]; //used for door/wall opening
    }

    public Vector2 size; // the size of the grid
    private int startPos = 45; //start position of dungeon, always keeping grid at 10*10 
    public GameObject room;
    public Vector2 offset; //distance between each room
    public float level;       //level player is on
    private int numRooms;    //number of rooms allowed
    private int minRooms = 7;    //number of rooms allowed
    private int roomCount = 0; // used to count number of rooms visited

    List<Cell> board;
    List<int> deadEnds = new List<int>(); //rooms that have no neighbors added
    Queue<int> q = new Queue<int>();     //queue to keep track of each cell for bfs 

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
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {

                Cell currentCell = board[Mathf.FloorToInt(i + j * 10)];
                if (currentCell.visited == 1)
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
        numRooms = 12;

        board = new List<Cell>();

        //initializing our board
        for (int i = 0; i < 100; i++)
        {
            board.Add(new Cell());

        }

        //keeps track which position were at currently at
        int currentCell = startPos;
        bool tmp = false;
        visit(startPos);

        while (q.Count > 0)
        {

            currentCell = q.Dequeue();
            Debug.Log("working " + currentCell);
            var x = currentCell % 10;
            var created = false;
            if (x > 1)
            {
                tmp = visit(currentCell - 1);
                created = created | tmp; //maybe try amking visit a var and preforming the if statement for doors after visit is returned
                if (tmp)
                {
                    editDoors(currentCell - 1, currentCell, board);
                }
            
            }
            if (x < 9)
            {
                tmp = visit(currentCell + 1);
                created = created | tmp;
                if (tmp)
                {
                    editDoors(currentCell + 1, currentCell, board);
                }
            }
            if (currentCell > 20)
            {
                tmp = visit(currentCell - 10);
                created = created | tmp;
                if (tmp)
                {
                    editDoors(currentCell - 10, currentCell, board);
                }
            }
            if (currentCell < 70)
            {
                tmp = visit(currentCell + 10);
                created = created | tmp;
                if (tmp)
                {
                    editDoors(currentCell + 10, currentCell, board);
                    tmp = false;
                }
            }

            if (!created)
            {
                deadEnds.Add(currentCell);
            }
        }
        while (roomCount < minRooms)
        {
            MazeGenerator();
            Debug.Log("Regenerate called");
        }
        GenerateDungeon();

    }

    int ncount(int cell)
    {
        int total = 0;
        if (cell >= 10) // add up neighbor
        {
            total += board[Mathf.FloorToInt(cell -10)].visited;
        }
        if (cell < 90) // add down neighbor
        {
            total += board[Mathf.FloorToInt(cell + 10)].visited;
        }
        if ((cell + 1) % 10 < 9) // add right neighbor
        {
            total += board[Mathf.FloorToInt(cell + 1)].visited;
        }
        if ((cell - 1) % 10 > 1) // add left neighbor
        {
            total += board[Mathf.FloorToInt(cell - 1)].visited;
        }
        return total;
    }

    bool visit(int cell)
    {
        if (board[cell].visited == 1) //already visited cell
        {
            return false;
        }
        if (Random.Range(0, 2) == 1 && cell != startPos)   //50% random chance to return False
        {
            return false;
        }

        if (roomCount >= numRooms) //exceeding number of allowed rooms
        {
            return false;
        }
        var neighbors = ncount(cell);

        if (neighbors > 1) // if cell has more than 1 neighbor that has been visited
        {
            return false;
        }


        q.Enqueue(cell);
        board[cell].visited = 1;
        roomCount += 1;
        return true;
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
