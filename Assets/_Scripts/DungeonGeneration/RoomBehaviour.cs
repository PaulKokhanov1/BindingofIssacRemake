using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{

    public GameObject[] walls; // 0 - Up, 1 - Down, 2 - Right, 3 - Left
    public GameObject[] doors;
    public GameObject[] doorsBoss;
    public GameObject[] doorsTreasure;

    //this function is used to setup rooms to have opened doors or conceled walls
    //the status bool array is used to tell which doors are open and which are closed, if true @ index i => door is opened 

    public void UpdateRoom(bool[] status, bool[] statusBoss = null, bool[] statusTreasure = null)
    {
        if (statusBoss == null)
        {
            statusBoss = new bool[] { false, false, false, false };
        }
        if (statusTreasure == null)
        {
            statusTreasure = new bool[] { false, false, false, false };
        }

        for (int i = 0; i < status.Length; i++)
        {
            if (statusTreasure[i] || statusBoss[i])
            {
                doors[i].SetActive(false);
            }
            else
            {
                doors[i].SetActive(status[i]);

            }

            doorsTreasure[i].SetActive(statusTreasure[i]);
            doorsBoss[i].SetActive(statusBoss[i]);  
            walls[i].SetActive(!(status[i] | statusBoss[i] | statusTreasure[i]));
        }
    }
}
