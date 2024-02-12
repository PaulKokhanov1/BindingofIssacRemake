using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


//used for logic regarding Player Specific elements, like movement
public class PlayerUnitBase : UnitBase
{

    public float moveSpeed;
    public float moveX, moveY;
    public Rigidbody2D rb;
    private Vector2 moveDirection;

    public float acceleration = 8;
    public float decceleration = 24;
    public float velPwr = 0.87f;
    private Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //good for processing inputs
    void Update()
    {
        ProcessInputs();
    }

    //best to be used for physics calculations
    private void FixedUpdate()
    {
        Move();
    }

    void ProcessInputs()
    {
        //strictly gets 0 or 1
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        //normalized so that regardless of direction player moves at same speed
        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        //rb.velocity = new Vector2(moveDirection.x * moveSpeed * Time.fixedDeltaTime, moveDirection.y * moveSpeed * Time.fixedDeltaTime);

        //firstly find desired speed to attain
        Vector2 targetSpeed = new Vector2( moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
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
}
