using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CircleSpawn : MonoBehaviour
{
    public static CircleSpawn instance;
    
    public Sprite[] sprites;

    public List<GameObject> enemyInstances;

    public int enemyPerWave;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemyGroup",0,10);
        //SpawnEnemyGroup(30);
    }

    void SpawnSingle(Vector2 spawnPosition, int colorPicked)
    {
        GameObject go = Instantiate(Resources.Load("Prefabs/Enemy")) as GameObject;
        go.transform.position = spawnPosition;
        enemyInstances.Add(go);
        go.GetComponent<SpriteRenderer>().sprite = sprites[colorPicked];
    }

    private Vector2 GetRandomSpawnPosition()
    {
        int spawnSideIndex = Random.Range(0, 4);
        Vector2 spawnPositionWithoutVariation = new Vector2();
        switch (spawnSideIndex)
        {
            case 0:
                spawnPositionWithoutVariation = new Vector2(PlayerController.instance.bottomRightCorner.position.x,
                    Random.Range(-PlayerController.instance.bottomRightCorner.position.y,
                        PlayerController.instance.bottomRightCorner.position.y));
                break;
            case 1:
                spawnPositionWithoutVariation = new Vector2(-PlayerController.instance.bottomRightCorner.position.x,
                    Random.Range(-PlayerController.instance.bottomRightCorner.position.y,
                        PlayerController.instance.bottomRightCorner.position.y));
                break;
            case 2:
                spawnPositionWithoutVariation = new Vector2(Random.Range(-PlayerController.instance.bottomRightCorner.position.x,
                        PlayerController.instance.bottomRightCorner.position.x),
                    PlayerController.instance.bottomRightCorner.position.y);
                break;
            case 3:
                spawnPositionWithoutVariation = new Vector2(Random.Range(-PlayerController.instance.bottomRightCorner.position.x,
                        PlayerController.instance.bottomRightCorner.position.x),
                    -PlayerController.instance.bottomRightCorner.position.y);
                break;
        }
        spawnPositionWithoutVariation.x += Random.Range(-1, 1);
        spawnPositionWithoutVariation.y += Random.Range(1, 1);
        Vector2 spawnPositionWithVariation = spawnPositionWithoutVariation;
        
        return spawnPositionWithVariation;
    }

    private void SpawnEnemyGroup()
    {
        int colorForThisWave = GetComponent<ColorPicker>().SetSprite();
        for (int i = 0; i <= enemyPerWave; i++)
        {
            SpawnSingle(GetRandomSpawnPosition(),colorForThisWave);
        }

        enemyPerWave += 10;
        enemyPerWave += Random.Range(-5, 5);
    }
}
