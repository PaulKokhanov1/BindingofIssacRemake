using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    //creates a singleton for all other scripts to be able to access it
    public static CameraController instance;

    private PlayerUnitBase player;
    public float moveSpeedWhenRoomChange;
    public Vector3 currentCameraPosition;

    private void Awake()
    {
        
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerUnitBase>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }



    void UpdatePosition()
    {
        if (DungeonGenerator_three.instance.checkifPlayerInInstantiatedRoom(player.posX, player.posY))
        {
            Vector3 targetPos = new Vector3(player.posX * DungeonGenerator_three.instance.offset.x, player.posY * DungeonGenerator_three.instance.offset.y, -10);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeedWhenRoomChange);
            currentCameraPosition = transform.position;
        }

    }

}
