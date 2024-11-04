using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    internal Player player;
    internal List<Enemy> enemies = new List<Enemy>();

    //스테이지 상승에 따른 enemy능력치 증가량
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
        //enemies 리스트의 원본을 수정하면 오류가 발생할 가능성이 있으므로 복사본을 이용
        List<Enemy> removeEnemies = new List<Enemy>(enemies);

        foreach (Enemy enemy in removeEnemies)
        {
            enemy.Death();
        }

        player.gold = 0;
    }
}
