using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("생성할 enemy 프리팹")] public Enemy enemyPrefab;
    [SerializeField, Header("생성될 적의 수")] public int enemyCount;
    [SerializeField, Header("생성될 X좌표 범위")] private Vector2 randomPosX;
    [SerializeField, Header("생성될 Y좌표 범위")] private Vector2 randomPosY;

    [SerializeField, Header("체력(1)")] internal float health;
    internal float maxHealth;
    [SerializeField, Header("이동 속도(0.5)")] internal float moveSpeed;
    [SerializeField, Header("공격 속도(1)")] internal float attackInterval;
    [SerializeField, Header("공격력(1)")] internal float damage;
    [SerializeField, Header("도착 지점 X값(1.5)")] internal float arrivePosX;

    private Coroutine spawnCoroutine;
    internal float increase;

    private void Start()
    {
        increase = -1;
    }

    private void Update()
    {
        if (GameManager.Instance.enemies.Count == 0)
        {
            increase++;
            Spawn();
        }
        Debug.Log($"increase : {increase}");
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
            Enemy enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            //enemy.health = this.health + (increase / 10);
            enemy.health = this.health + (increase * 10);

            enemy.maxHealth = enemy.health;
            enemy.moveSpeed = this.moveSpeed;
            enemy.attackInterval = this.attackInterval;
            enemy.damage = this.damage + increase;
            enemy.arrivePosX = this.arrivePosX;
        }
    }
}
