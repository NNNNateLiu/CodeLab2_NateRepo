using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public float distanceToPlayer;
    
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position,PlayerController.instance.transform.position,speed * Time.deltaTime);

        distanceToPlayer =
            Vector3.Distance(gameObject.transform.position, PlayerController.instance.transform.position);
    }



    private void OnDestroy()
    {
        GameObject go = Instantiate(Resources.Load("Prefabs/ExperienceBall"),transform.position,Quaternion.identity) as GameObject;
        CircleSpawn.instance.enemyInstances.Remove(gameObject);
    }
}
