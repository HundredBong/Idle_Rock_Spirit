using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("생성할 enemy 프리팹")]public GameObject enemyPrefab;
    [SerializeField,Header("생성될 적의 수")]public int enemyCount;
    [SerializeField, Header("생성될 X좌표 범위")] private Vector2 randomPosX;
    [SerializeField, Header("생성될 Y좌표 범위")] private Vector2 randomPosY;

    private Coroutine spawnCoroutine;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (GameManager.Instance.enemies.Count == 0)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            //함수가 호출될 때 마다 다른 랜덤 좌표를 선택
            float spawnPosX = Random.Range(randomPosX.x, randomPosX.y);
            float spawnPosY = Random.Range(randomPosY.x, randomPosY.y);
            Vector2 spawnPos = new Vector2(transform.position.x + spawnPosX,
                transform.position.y + spawnPosY);

            //생성된 랜덤 좌표에 enemy 생성
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        }
    }
}
