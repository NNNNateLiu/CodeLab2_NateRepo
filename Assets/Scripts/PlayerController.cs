using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

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
    public float bulletSpeedModifier = 0;

    public int currentHealth;
    public int maxHealth;
    public float attackRate = 1;

    private void Awake()
    {
        instance = this;
        InvokeRepeating("NormalAttack",1,attackRate);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        processInput();
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
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
        go.GetComponent<Bullet>().bulletSpeed += bulletSpeedModifier;
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
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            Destroy(col.gameObject);
            currentHealth -= 10;
            if (currentHealth <= 0)
            {
                Debug.Log("gg");
                UIManager.instance.GGPanel.SetActive(true);
                CancelInvoke();
            }
        }
    }
}
