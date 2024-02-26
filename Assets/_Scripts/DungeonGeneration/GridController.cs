using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{

    //struct useful for passing group data together
    [System.Serializable]
    public struct Grid 
    {

        public int columns, rows;

        public float verticalOffset, horizontalOffset;

    }

    public Grid grid; // to be able to change the values of the grid from the inspector
    public GameObject gridTile; //for visualizing the grid
    public GameObject room;
    public List<Vector2> avaliablePoints = new List<Vector2>(); //storing all positions within our grid

    private PolygonCollider2D roomPolygonCollider;

    private void Awake()
    {
        roomPolygonCollider = room.GetComponent<PolygonCollider2D>();
        Debug.Log(roomPolygonCollider);
        float roomWidth = Mathf.Abs(roomPolygonCollider.points[0].x) + Mathf.Abs(roomPolygonCollider.points[3].x);
        
        float roomHeight = Mathf.Abs(roomPolygonCollider.points[0].y) + Mathf.Abs(roomPolygonCollider.points[1].y);
        Debug.Log("Room width is: " + roomWidth);
        Debug.Log("Room height is: " + roomHeight);

        grid.columns = Mathf.FloorToInt(roomWidth - 2f);
        grid.rows = Mathf.FloorToInt(roomHeight - 2f);

        //GenerateGrid();
    }

    private void GenerateGrid()
    {
        grid.verticalOffset += DungeonGenerator_three.instance.rooms[0].transform.localPosition.y;
        grid.horizontalOffset += DungeonGenerator_three.instance.rooms[0].transform.localPosition.x;

        Debug.Log("vertical Offset is: " + grid.verticalOffset);
        Debug.Log("horizontal Offset is: " + grid.horizontalOffset);

        for (int y = 0; y < grid.rows; y++)
        {
            for (int x = 0; x < grid.columns; x++)
            {

                GameObject go = Instantiate(gridTile, transform);
                go.GetComponent<Transform>().position = new Vector2(x - (grid.columns - grid.horizontalOffset), y-(grid.rows - grid.verticalOffset));   //setting position of each tile
                go.name = "X: " + x + " , Y: " + y;
                avaliablePoints.Add(go.transform.position);
            }
        }
    }

    public IEnumerator waitForDungeonGeneraton()
    {
        yield return new WaitForSeconds(1);
        GenerateGrid();
    }
}
