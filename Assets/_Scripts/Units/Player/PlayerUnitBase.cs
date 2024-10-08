using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


//used for logic regarding Player Specific elements, like movement & shooting
public class PlayerUnitBase : MonoBehaviour
{

    public float moveSpeed;
    [HideInInspector] public float moveX, moveY;
    float shootHor, shootVert;
    float bulletAngle = 45f;
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    [SerializeField] public Animator animator;

   
    public float acceleration = 8;
    public float decceleration = 24;
    public float velPwr = 0.87f;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float fireDelay;
    public float dashSpeed;
    public float dashLength = .5f, dashCooldown = 1f;
    public bool isDead = false;



    public delegate void OnPlayerPosChangeDelegate(int XVal, int YVal);
    public static event OnPlayerPosChangeDelegate OnPlayerPosChange;

    [HideInInspector] public int posX
    {
        get { return _posX; }
        set
        {
            if (_posX == value) return;
            _posX = value;
            if (OnPlayerPosChange != null)
            {
                OnPlayerPosChange(_posX, _posY);
                Debug.Log("Player position Changed: " + "posX: " + _posX + " posY: "+ _posY);
            }
                
        }
    }
    [HideInInspector] public int posY
    {
        get { return _posY; }
        set
        {
            if (_posY == value) return;
            _posY = value;
            if (OnPlayerPosChange != null)
            {
                OnPlayerPosChange(_posX, _posY);
                //Debug.Log("Player position Changed: " + "posX: " + _posX + " posY: " + _posY);


            }
        }
    }

    private Vector2 movement;
    private float lastFire;
    private int _posX = 0;
    private int _posY = 0;
    private float dashCounter;
    private float dashCoolCounter;
    private float activeMoveSpeed;


    private void Start()
    {
    }

    //good for processing inputs
    void Update()
    {
        if (!isDead)
        {
            ProcessInputs();
            playerPosition();
        }


        if (GameManager.Health <= 0)
        {
            animator.SetTrigger("Death");
            isDead = true;
        }
    }

    //best to be used for physics calculations
    private void FixedUpdate()
    {
        if (!isDead)
        {
            Move();
        }

    }
    
    public void deathSequence()
    {
        LevelChanger.Instance.FadeToLevel(0);
    }

    void ProcessInputs()
    {
        if (dashCounter <= 0)
        {
            activeMoveSpeed = moveSpeed;
        }
        
        moveSpeed = GameManager.MoveSpeed;
        fireDelay = GameManager.FireRate;

        dashSpeed = moveSpeed + 20;
        


        //strictly gets 0 or 1
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        shootHor = Input.GetAxisRaw("ShootHorizontal");
        shootVert = Input.GetAxisRaw("ShootVertical");

        if((shootHor != 0 || shootVert != 0) && Time.time > lastFire + fireDelay)
        {
            shoot();
            lastFire = Time.time;
        }

        //normalized so that regardless of direction player moves at same speed
        moveDirection = new Vector2(moveX, moveY).normalized;

        //to make sure character stays facing direction they were last moving towards
        if (moveDirection.x != 0 || moveDirection.y != 0)
        {
            animator.SetFloat("X", moveDirection.x);
            animator.SetFloat("Y", moveDirection.y);

            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        dashMovement();
        
    }

    void Move()
    {
        //rb.velocity = new Vector2(moveDirection.x * moveSpeed * Time.fixedDeltaTime, moveDirection.y * moveSpeed * Time.fixedDeltaTime);

        //firstly find desired speed to attain
        Vector2 targetSpeed = new Vector2( moveDirection.x * activeMoveSpeed, moveDirection.y * activeMoveSpeed);
        //find how far we are from the desired speed
        Vector2 speedDif = new Vector2(targetSpeed.x - rb.velocity.x, targetSpeed.y - rb.velocity.y);
        //calculate whether we need to accelerate or decelerate depending on applied input, i.e if we are pushing right or left arrow key and similarly for up and down
        float accelRateX = (Math.Abs(targetSpeed.x) > 0.01f) ? acceleration: decceleration;
        float accelRateY = (Math.Abs(targetSpeed.y) > 0.01f) ? acceleration : decceleration;
        //finally apply acceleration to the speed difference and then multiply by the appropriate sign to preserve direction
        movement.x = Mathf.Pow(Mathf.Abs(speedDif.x) * accelRateX, velPwr) * Mathf.Sign(speedDif.x);
        movement.y = Mathf.Pow(Mathf.Abs(speedDif.y) * accelRateY, velPwr) * Mathf.Sign(speedDif.y);

        //no need to add time.deltatime since its automatically added with forcemode
        rb.AddForce(movement * Vector2.one);

    }

    void shoot()
    {
        if (!GameManager.DoubleShot)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
            bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
                (shootHor < 0) ? Mathf.Floor(shootHor) * bulletSpeed : Mathf.Ceil(shootHor) * bulletSpeed,
                (shootVert < 0) ? Mathf.Floor(shootVert) * bulletSpeed : Mathf.Ceil(shootVert) * bulletSpeed,
                0
            );
        }
        else
        {
            GameObject bulletOne = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
            GameObject bulletTwo = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
            float bulletAngleInRadian = bulletAngle * Mathf.Deg2Rad;

            bulletOne.AddComponent<Rigidbody2D>().gravityScale = 0;
            bulletTwo.AddComponent<Rigidbody2D>().gravityScale = 0;
            if (shootHor < 0)
            {
                bulletOne.GetComponent<Rigidbody2D>().velocity = new Vector3( Mathf.Floor(shootHor) * bulletSpeed, bulletAngleInRadian * bulletSpeed, 0 );
                bulletTwo.GetComponent<Rigidbody2D>().velocity = new Vector3( Mathf.Floor(shootHor) * bulletSpeed, -bulletAngleInRadian * bulletSpeed, 0 );
            }
            else if (shootHor > 0)
            {
                bulletOne.GetComponent<Rigidbody2D>().velocity = new Vector3( Mathf.Ceil(shootHor) * bulletSpeed, bulletAngleInRadian * bulletSpeed, 0 );
                bulletTwo.GetComponent<Rigidbody2D>().velocity = new Vector3( Mathf.Ceil(shootHor) * bulletSpeed, -bulletAngleInRadian * bulletSpeed, 0 );
            }            
            else if (shootVert < 0)
            {
                bulletOne.GetComponent<Rigidbody2D>().velocity = new Vector3( bulletAngleInRadian * bulletSpeed, Mathf.Floor(shootVert) * bulletSpeed, 0 );
                bulletTwo.GetComponent<Rigidbody2D>().velocity = new Vector3( -bulletAngleInRadian * bulletSpeed, Mathf.Floor(shootVert) * bulletSpeed, 0 );
            }            
            else if (shootVert > 0)
            {
                bulletOne.GetComponent<Rigidbody2D>().velocity = new Vector3(bulletAngleInRadian * bulletSpeed, Mathf.Ceil(shootVert) * bulletSpeed, 0);
                bulletTwo.GetComponent<Rigidbody2D>().velocity = new Vector3(-bulletAngleInRadian * bulletSpeed, Mathf.Ceil(shootVert) * bulletSpeed, 0);
            }
        }
    }

    void dashMovement()
    {
        if (GameManager.DashAbility)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (dashCoolCounter <= 0 && dashCounter <= 0)
                {
                    Debug.Log("Dash Called");
                    activeMoveSpeed = dashSpeed;
                    dashCounter = dashLength;
                }
            }

            if (dashCounter > 0)
            {
                dashCounter -= Time.deltaTime;
                if (dashCounter <= 0)
                {
                    activeMoveSpeed = moveSpeed;
                    dashCoolCounter = dashCooldown;
                }
            }

            if (dashCoolCounter > 0)
            {
                dashCoolCounter -= Time.deltaTime;
            }
        }
    }

    //convert player transform position to integers along grid for the map
    void playerPosition()
    {
        
        posX = Mathf.FloorToInt((transform.position.x + DungeonGenerator_three.instance.offset.x/2) / DungeonGenerator_three.instance.offset.x);
        posY = Mathf.FloorToInt((transform.position.y + DungeonGenerator_three.instance.offset.y/2) / DungeonGenerator_three.instance.offset.y);
/*        Debug.Log("Player's XPos: " + posX);
        Debug.Log("Player's YPos: " + posY);*/

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "TrapDoor")
        {
            AudioManager.instance.newGame = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
