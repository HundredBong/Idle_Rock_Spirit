using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    internal Player player;
    internal List<Enemy> enemies = new List<Enemy>();

    //�������� ��¿� ���� enemy�ɷ�ġ ������
    //internal int enemyDamaageIncrease;
    //internal float enemyHealthIncrease;
    //internal int currentStage;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(this);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    
    public void RetryGame()
    {
        //enemies ����Ʈ�� ������ �����ϸ� ������ �߻��� ���ɼ��� �����Ƿ� ���纻�� �̿�
        List<Enemy> removeEnemies = new List<Enemy>(enemies);

        foreach (Enemy enemy in removeEnemies)
        {
            enemy.Death();
        }

        player.gold = 0;
    }
}
