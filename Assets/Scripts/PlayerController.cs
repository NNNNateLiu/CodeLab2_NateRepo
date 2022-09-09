using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float experienceNeededToLevelUp = 101;
    public float currentExperience = 1;
    public int level = 1;
    
    public static PlayerController instance;
    
    private Vector2 moveDirection;

    private Rigidbody2D rb;

    public float speed;

    public Transform topLeftCorner;
    public Transform bottomRightCorner;

    public GameObject killZone;

    private void Awake()
    {
        instance = this;
        InvokeRepeating("NormalAttack",1,1);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        processInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void processInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY);
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
    }

    void NormalAttack()
    {
        GameObject go = Instantiate(Resources.Load("Prefabs/Bullet"),gameObject.transform.position,Quaternion.identity) as GameObject;
    }

    public void GainExperience()
    {
        if (currentExperience + 10 >= experienceNeededToLevelUp)
        {
            LevelUp();
        }
        else
        {
            currentExperience += 10;
        }
    }

    public void LevelUp()
    {
        //TODO:Choose Rewards
        Time.timeScale = 0;
        experienceNeededToLevelUp += 50;
        currentExperience = 0;
        level += 1;
        RewardManager.instance.LevelUp();
    }
}
