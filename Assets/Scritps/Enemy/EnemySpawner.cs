using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("������ enemy ������")]public GameObject enemyPrefab;
    [SerializeField,Header("������ ���� ��")]public int enemyCount;
    [SerializeField, Header("������ X��ǥ ����")] private Vector2 randomPosX;
    [SerializeField, Header("������ Y��ǥ ����")] private Vector2 randomPosY;

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
            //�Լ��� ȣ��� �� ���� �ٸ� ���� ��ǥ�� ����
            float spawnPosX = Random.Range(randomPosX.x, randomPosX.y);
            float spawnPosY = Random.Range(randomPosY.x, randomPosY.y);
            Vector2 spawnPos = new Vector2(transform.position.x + spawnPosX,
                transform.position.y + spawnPosY);

            //������ ���� ��ǥ�� enemy ����
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        }
    }
}
