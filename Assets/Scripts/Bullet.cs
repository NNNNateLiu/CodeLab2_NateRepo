using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject targetEnemy;
    public float bulletSpeed = 2f;
    private float currentEnemyDistanceToPlayer;
    private bool hasTarget;

    void Start()
    {
        targetEnemy = CircleSpawn.instance.enemyInstances[0];
        currentEnemyDistanceToPlayer = CircleSpawn.instance.enemyInstances[0].GetComponent<EnemyController>().distanceToPlayer;
        FindTarget();
    }

    // Update is called once per frame
    void Update()
    {
        CheckHasTarget();
        transform.position = Vector3.MoveTowards(transform.position, targetEnemy.transform.position,
            bulletSpeed * Time.deltaTime);
        Vector3 directionToTarget = (targetEnemy.transform.position - transform.position).normalized;
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, directionToTarget);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, bulletSpeed);
    }

    void CheckHasTarget()
    {
        if (targetEnemy == null)
        {
            FindTarget();
        }
    }

    void FindTarget()
    {
        foreach (var enemy in CircleSpawn.instance.enemyInstances)
        {
            if (enemy.GetComponent<EnemyController>().distanceToPlayer <= currentEnemyDistanceToPlayer)
            {
                targetEnemy = enemy;
                currentEnemyDistanceToPlayer = enemy.GetComponent<EnemyController>().distanceToPlayer;
            }
        }
        hasTarget = true;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }
}
