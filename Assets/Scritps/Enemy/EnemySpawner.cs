using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("������ enemy ������")] public Enemy enemyPrefab;
    [SerializeField, Header("������ ���� ��")] public int enemyCount;
    [SerializeField, Header("������ X��ǥ ����")] private Vector2 randomPosX;
    [SerializeField, Header("������ Y��ǥ ����")] private Vector2 randomPosY;

    [SerializeField, Header("ü��(1)")] internal float health;
    internal float maxHealth;
    [SerializeField, Header("�̵� �ӵ�(0.5)")] internal float moveSpeed;
    [SerializeField, Header("���� �ӵ�(1)")] internal float attackInterval;
    [SerializeField, Header("���ݷ�(1)")] internal float damage;
    [SerializeField, Header("���� ���� X��(1.5)")] internal float arrivePosX;

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
            //�Լ��� ȣ��� �� ���� �ٸ� ���� ��ǥ�� ����
            float spawnPosX = Random.Range(randomPosX.x, randomPosX.y);
            float spawnPosY = Random.Range(randomPosY.x, randomPosY.y);
            Vector2 spawnPos = new Vector2(transform.position.x + spawnPosX,
                transform.position.y + spawnPosY);

            //������ ���� ��ǥ�� enemy ����
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
