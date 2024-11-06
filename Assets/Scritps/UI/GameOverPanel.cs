using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField, Header("���� ���� Yes��ư")] private Button gmaeOverButtonYes;
    [SerializeField, Header("���� ���� No��ư")] private Button gmaeOverButtonNo;
    [SerializeField, Header("Continue ��ƼŬ")] private ParticleSystem particlePrefab;

    private EnemySpawner spawner;
    //private TitlePanel titlePanell;
    //1. ��

    private float playerMaxHp;

    private void Awake()
    {
        //enemySpawner�� ���� �����ؾ� �ϹǷ� ������
        spawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        //titlePanell = GameObject.Find("StartTitlePanel").GetComponent<TitlePanel>();
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
        ////���� ����
        //GameManager.Instance.RetryGame();
        //gameObject.SetActive(false);

        ////Ÿ��Ʋ ȭ�� Ȱ��ȭ �� ���� ���׷��̵�, ��ų �ʱ�ȭ
        //Time.timeScale = 0f;

        ////�ϵ��ڵ�
        //GameManager.Instance.player.damage = 1;
        //GameManager.Instance.player.health = 5;
        //GameManager.Instance.player.maxHealth = GameManager.Instance.player.health;

        //GameManager.Instance.player.healthRegen = 0;
        //GameManager.Instance.player.critlcalChance = 0;
        //GameManager.Instance.player.criticalMultiplier = 0;
        //GameManager.Instance.player.attackInterval = 1;
        //GameManager.Instance.player.doubleShot = 0;

        //for (int i = 0; i < UIManager.Instance.upgradeLevel.Count; i++)
        //{
        //UIManager.Instance.upgradeLevel[i] = 0;
        //UIManager.Instance.SetPrice(i);
        //}

        //UIManager.Instance.upgradeLevel[0] = 1;
        //UIManager.Instance.upgradeLevel[2] = 1;

        //UIManager.Instance.SetPrice(0);
        //UIManager.Instance.SetPrice(1);

        //spawner.increase = -1;

        //GameManager.Instance.player.gold = 0;

        //UIManager.Instance.PlayerHPRenewal();
        //UIManager.Instance.PlayerMoneyRenewal();

        //titlePanell.gameObject.SetActive(true);
        Application.Quit();
    }

    public void OnclickYes()
    {
        //�ӽ÷� �÷��̾��� ü���� ����
        playerMaxHp = GameManager.Instance.player.maxHealth;

        if (UIManager.Instance.is2xSpeed == true)
            Time.timeScale = 2f;
        else
            Time.timeScale = 1f;
        //enemy�� ��ȭ�� �ٿ��� �ϴϱ� enemy Spawn�� ���� ����
        //����Ʈ�� ����� �� ++�� 0�� ��
        spawner.increase = -1;

        //���ӸŴ����� ���ؼ� �ʿ� �����ϴ� ��� enemy�� �����ؾ� �ϴµ� �̶� ���� ����� ��,
        //���̸� �׳� �� �� Death�޼��� �����Ű�� �ϰ� �г�Ƽ���ð� �÷��̾� ��带 0���� ���ߴ°�
        //�ſ� �׷����ϴϱ� ���� �����ϱ�
        GameManager.Instance.RetryGame();

        GameManager.Instance.player.health = 9999;
        GameManager.Instance.player.maxHealth = 9999;

        GameManager.Instance.player.gold = 0;
        UIManager.Instance.PlayerMoneyCheckInUpgreade();

        ParticleSystem parResurrection = Instantiate(particlePrefab, GameManager.Instance.player.transform.position, Quaternion.identity);
        parResurrection.Play();
        GameManager.Instance.player.anim.SetBool("isDeath", false);

        Invoke("DelayHealthRegen", 1.5f);
        
        gameObject.SetActive(false);
    }

    //enemy�� ���ư��µ� enemy�� ����ü�� �ȳ��ư��� �����ڸ��� ���������г� ��ȣ���
    //�Ͻ������� �������� �����
    //�̰� �� �������� enemy�� ������ ������ ����ü�� �ؼ� �׷����ϴ�.
    public void DelayHealthRegen()
    {
        //�ӽ÷� ����� ���� �ҷ���
        GameManager.Instance.player.health = playerMaxHp;
        GameManager.Instance.player.maxHealth = GameManager.Instance.player.health;

        //UIâ ������Ʈ
        UIManager.Instance.PlayerHPRenewal();
        UIManager.Instance.PlayerMoneyRenewal();
    }
}
