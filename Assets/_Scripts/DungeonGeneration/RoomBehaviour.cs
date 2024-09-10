using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{

    public GameObject[] walls; // 0 - Up, 1 - Down, 2 - Right, 3 - Left
    public GameObject[] doors;
    public GameObject[] doorsBoss;
    public GameObject[] doorsTreasure;
    public GameObject[] doorColliders;
    public GameObject trapDoor;
    public GameObject bossRock;
    public GameObject itemSpawner;

    public int Xpos, Ypos;

    private float delay = 10f;
    private float timer = 0f;

    //Subscribing to delegates 
    private void OnEnable()
    {
        PlayerUnitBase.OnPlayerPosChange += OnPlayerEnterRoom;
    }

    private void OnDisable()
    {
        PlayerUnitBase.OnPlayerPosChange -= OnPlayerEnterRoom;
    }

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

    /// <summary>
    /// Used to open the trapdoor in the boss level after the boss is defeated
    /// </summary>
    public void openEnding()
    {
        if(trapDoor != null)
        {
            trapDoor.SetActive(true);
        }
        if (bossRock != null)
        {
            bossRock.SetActive(true);
        }        
        if (itemSpawner != null)
        {
            itemSpawner.SetActive(true);
        }

    }

    /// <summary>
    /// Handles checking if the players position is in the specific room this script is added to
    /// </summary>

    public void OnPlayerEnterRoom(int playerPosX, int playerPosY)
    {
        //Debug.Log("OnplaerEnterRoom called");
        //Debug.Log("Xpos, YPos: " + Xpos+ " " + Ypos + " Player Pos: " + playerPosX + " " + playerPosY);
        if (Mathf.Abs(playerPosX) == Xpos && Mathf.Abs(playerPosY) == Ypos)
        {
            //player is in this instance of the rooms
            Debug.Log("Player is in Room: " + Xpos + ", " + Ypos);


            UpdateCurrentRooms();

            EnemyController[] enemies = GetComponentsInChildren<EnemyController>();
            BossController[] bosses = GetComponentsInChildren<BossController>();
            if (enemies.Length > 0 || bosses.Length >0)
            {
                StartCoroutine(CountdownToCloseRooms());

                //If player is in boss room activate UI health bar of boss
                foreach (BossController boss in bosses)
                {
                    boss.healthBar.gameObject.SetActive(true);
                }
            }
            
        }
    }

    /// <summary>
    /// Sets the appropriate state of each enemy and/or boss if player is in the room or not
    /// </summary>
    public void UpdateCurrentRooms()
    {
        EnemyController[] enemies = GetComponentsInChildren<EnemyController>();
        BossController[] bosses = GetComponentsInChildren<BossController>();
        Debug.Log(bosses.Length);
        Debug.Log(enemies.Length);

        if (enemies != null || bosses != null)
        {
            foreach(EnemyController enemy in enemies)
            {
                
                enemy.notInRoom = false;
                enemy.currState = EnemyState.Wander;
                //Debug.Log("enemy current state: " + enemy.currState);

                enemy.Wander();
            }
            foreach (BossController boss in bosses)
            {
                
                boss.notInRoom = false;
                boss.currState = BossState.Idle;
            }
        } 
    }

    public void closeCurrentRoomDoors()
    {
        for (int i = 0; i < doors.Length; i++)
        { 
            if(doors[i].activeSelf || doorsBoss[i].activeSelf || doorsTreasure[i].activeSelf)
            {
                doorColliders[i].SetActive(true);
                Debug.Log("Set door collider active");
            }
        }
    }

    /// <summary>
    /// Once all enemies defeated in specific room, then open doors and allow player to move to next room
    /// </summary>
    public void checkEnemiesInRoom()
    {
        EnemyController[] enemies = GetComponentsInChildren<EnemyController>();
        BossController[] bosses = GetComponentsInChildren<BossController>();

        Debug.Log(enemies.Length);
        if (enemies.Length == 1 || bosses.Length == 1 && enemies.Length == 0)
        {
            Debug.Log("Open doors called");
            openAllDoors();
        }
    }

    private void openAllDoors()
    {
        for (int i = 0; i < doorColliders.Length; i++)
        {
            doorColliders[i].SetActive(false);
        }
    }

    /// <summary>
    /// Need to use countdown to close doors, otherwise player would get stuck as they try to enter a room
    /// </summary>
    /// <returns></returns>
    public IEnumerator CountdownToCloseRooms()
    {
        yield return new WaitForSeconds(0.5f);

        closeCurrentRoomDoors();

    }
}
