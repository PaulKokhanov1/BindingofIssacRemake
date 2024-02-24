using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
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
        public bool[] statusBoss = new bool[4]; //used for door/wall opening for Boss room
        public bool[] statusTreasure = new bool[4]; //used for door/wall opening fro Treasure room
    }
    //creates a singleton for all other scripts to be able to access it
    public static DungeonGenerator_three instance;
    public Vector2 size; // the size of the grid
    private int startPos = 45; //start position of dungeon, always keeping grid at 10*10 
    public GameObject room;
    public GameObject roomBoss;
    public GameObject roomTreasure;
    public GameObject player;
    public Vector2 offset; //distance between each room
    public float level;       //level player is on
    private int numRooms;    //number of rooms allowed
    private int minRooms = 7;    //number of rooms allowed
    private int roomCount = 0; // used to count number of rooms visited

    List<Cell> board;
    List<int> n = new List<int> { 1, -1, 10, -10 }; //list of possible neighbors
    List<int> deadEnds = new List<int>(); //rooms that have no neighbors added
    Queue<int> q = new Queue<int>();     //queue to keep track of each cell for bfs 

    // Start is called before the first frame update
    void Start()
    {
        MazeGenerator(false);
    }

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool checkifPlayerInInstantiatedRoom(int posX, int posY)
    {
        if (board[Mathf.FloorToInt(posX + Mathf.Abs(posY) * 10)].visited == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }



    void GenerateDungeon()
    {
        //didnt use stack since still need to work with deadends list later, for getting random element

        int idx = deadEnds.Count - 1;
        int bossroom = deadEnds[idx];
        deadEnds.RemoveAt(idx);

        Debug.Log(bossroom);
        foreach (int i in n)
        {
            if (board[Mathf.FloorToInt(bossroom + i)].visited == 1)
            {
                editDoorsBoss(bossroom + i, bossroom, board);
                break;
            }
        }

        var random = new System.Random();
        idx = random.Next(deadEnds.Count);
        int treasureroom = deadEnds[idx];
        deadEnds.RemoveAt(idx);

        foreach (int i in n)
        {
            if (board[Mathf.FloorToInt(treasureroom + i)].visited == 1)
            {
                editDoorsTreasure(treasureroom + i, treasureroom, board);
                break;
            }
        }
        Debug.Log(treasureroom);


        //go through all rows and columns of the board
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {

                Cell currentCell = board[Mathf.FloorToInt(i + j * 10)];
                if (currentCell.visited == 1)
                {
                    if (bossroom == Mathf.FloorToInt(i + j * 10))
                    {
                        var newRoom = Instantiate(roomBoss, new Vector3(i * offset.x, -j * offset.y, 0), Quaternion.identity, transform).GetComponent<RoomBehaviour>();
                        newRoom.UpdateRoom(currentCell.status);
                        newRoom.name += " " + i + "-" + j;

                    }                    
                    else if (treasureroom == Mathf.FloorToInt(i + j * 10))
                    {
                        var newRoom = Instantiate(roomTreasure, new Vector3(i * offset.x, -j * offset.y, 0), Quaternion.identity, transform).GetComponent<RoomBehaviour>();
                        newRoom.UpdateRoom(currentCell.status, currentCell.statusBoss, currentCell.statusTreasure);
                        newRoom.name += " " + i + "-" + j;

                    }
                    else
                    {
                        var newRoom = Instantiate(room, new Vector3(i * offset.x, -j * offset.y, 0), Quaternion.identity, transform).GetComponent<RoomBehaviour>();
                        newRoom.UpdateRoom(currentCell.status, currentCell.statusBoss, currentCell.statusTreasure);
                        newRoom.name += " " + i + "-" + j;
                    }
                       
                }
            }
        }
    }

    void MazeGenerator(bool regenerate = false)
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
            MazeGenerator(true);

            Debug.Log("Regenerate called");
        }
        if (!regenerate)
        {
            GenerateDungeon();
        }
        

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
        if (UnityEngine.Random.Range(0, 2) == 1 && cell != startPos)   //50% random chance to return False
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
            int rand = UnityEngine.Random.Range(i, inputList.Count);
            inputList[i] = inputList[rand];
            inputList[rand] = temp;
        }
    }
/*    void popRandomEndRoom()
    {
        var index = Mathf.floor(Math.random() * endrooms.length);
        var i = endrooms[index];
        endrooms.splice(index, 1);
        return i;
    }*/

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


    void editDoorsBoss(int newCell, int currentCell, List<Cell> board)
    {
        //cell is going either down or right
        if (newCell > currentCell)
        {
            //cell is going right
            if (newCell - 1 == currentCell)
            {
                board[currentCell].status[2] = true; // since we moved right need to have currentcell RIGHT door open
                board[newCell].statusBoss[3] = true; //need to have the newCell LEFT door opened to create passage
            }
            else
            {
                board[currentCell].status[1] = true; // since we moved right need to have currentcell RIGHT door open
                board[newCell].statusBoss[0] = true; //need to have the newCell LEFT door opened to create passage
            }
        }
        else
        {
            //cell is going left
            if (newCell + 1 == currentCell)
            {
                board[currentCell].status[3] = true; // since we moved right need to have currentcell RIGHT door open
                board[newCell].statusBoss[2] = true; //need to have the newCell LEFT door opened to create passage
            }
            else
            {
                board[currentCell].status[0] = true; // since we moved right need to have currentcell RIGHT door open
                board[newCell].statusBoss[1] = true; //need to have the newCell LEFT door opened to create passage
            }
        }
    }    
    
    
    void editDoorsTreasure(int newCell, int currentCell, List<Cell> board)
    {
        //cell is going either down or right
        if (newCell > currentCell)
        {
            //cell is going right
            if (newCell - 1 == currentCell)
            {
                board[currentCell].statusTreasure[2] = true; // since we moved right need to have currentcell RIGHT door open
                board[newCell].statusTreasure[3] = true; //need to have the newCell LEFT door opened to create passage
            }
            else
            {
                board[currentCell].statusTreasure[1] = true; // since we moved right need to have currentcell RIGHT door open
                board[newCell].statusTreasure[0] = true; //need to have the newCell LEFT door opened to create passage
            }
        }
        else
        {
            //cell is going left
            if (newCell + 1 == currentCell)
            {
                board[currentCell].statusTreasure[3] = true; // since we moved right need to have currentcell RIGHT door open
                board[newCell].statusTreasure[2] = true; //need to have the newCell LEFT door opened to create passage
            }
            else
            {
                board[currentCell].statusTreasure[0] = true; // since we moved right need to have currentcell RIGHT door open
                board[newCell].statusTreasure[1] = true; //need to have the newCell LEFT door opened to create passage
            }
        }
    }
}
