using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField, Header("���� ���� Yes��ư")]private Button gmaeOverButtonYes;
    [SerializeField, Header("���� ���� No��ư")] private Button gmaeOverButtonNo;

    private EnemySpawner spawner;

    //1. ��

    private void Awake()
    {
        //enemySpawner�� ���� �����ؾ� �ϹǷ� ������
        spawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
    }

    private void Start()
    {
        //���� ���۽� ������Ʈ�� ���ܼ� ������ �ʰ� ��
        gameObject.SetActive(false);
    }

    public void OnClickYes()
    {
        //�� ����
        Application.Quit();
    }
    
    public void OnclickNo()
    {
        //enemy�� ��ȭ�� �ٿ��� �ϴϱ� enemy Spawn�� ���� ����
        spawner.increase = 0;

        //���ӸŴ����� ���ؼ� �ʿ� �����ϴ� ��� enemy�� �����ؾ� �ϴµ� �̶� ���� ����� ��,
        //���̸� �׳� �� �� Death�޼��� �����Ű�� �ϰ� �г�Ƽ���ð� �÷��̾� ��带 0���� ���ߴ°�
        //�ſ� �׷����ϴϱ� ���� �����ϱ�
        GameManager.Instance.RetryGame();

        //�÷��̾��� ü���� �ٽ� ȸ��
        GameManager.Instance.player.health = GameManager.Instance.player.maxHealth;

        //â �ݾ���
        GameManager.Instance.player.gold = 0;

        gameObject.SetActive(false);
    }
}
