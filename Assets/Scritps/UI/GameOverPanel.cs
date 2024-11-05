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

    public void ActivatePanel()
    {
        //�÷��̾��� Death�޼��忡�� ȣ���
        //Time.timeScale = 0.1f;
        gameObject.SetActive(true);
    }

    public void OnClickNo()
    {
        //�� ����
        Application.Quit();
    }
    
    public void OnclickYes()
    {
        Time.timeScale = 1f;
        //enemy�� ��ȭ�� �ٿ��� �ϴϱ� enemy Spawn�� ���� ����
        //����Ʈ�� ����� �� ++�� 0�� ��
        spawner.increase = -1;

        //���ӸŴ����� ���ؼ� �ʿ� �����ϴ� ��� enemy�� �����ؾ� �ϴµ� �̶� ���� ����� ��,
        //���̸� �׳� �� �� Death�޼��� �����Ű�� �ϰ� �г�Ƽ���ð� �÷��̾� ��带 0���� ���ߴ°�
        //�ſ� �׷����ϴϱ� ���� �����ϱ�
        GameManager.Instance.RetryGame();

        //�÷��̾��� ü���� �ٽ� ȸ��
        GameManager.Instance.player.health = GameManager.Instance.player.maxHealth;

        //â �ݾ���
        GameManager.Instance.player.gold = 0;

        //UIâ ������Ʈ
        UIManager.Instance.PlayerHPRenewal();
        UIManager.Instance.PlayerMoneyRenewal();

        gameObject.SetActive(false);
    }
}
