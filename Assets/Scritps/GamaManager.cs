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

    private void Start()
    {

        //Debug.Log(enemies.Count);
    }

    private void Update()
    {
        //if (enemies.Count == 0)
        //{
        //    currentStage++;
        //}
        //Debug.Log($"���� �������� : {currentStage}");
    }
}
