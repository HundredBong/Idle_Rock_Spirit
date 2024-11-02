using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance { get { return instance; } }

    internal Player player;

    [Header("�÷��̾� ü�¹�")] public Image playerHealthImage;
    [Header("��� ������ ��ư")] public Button gameSpeedButton;
    [Header("��� ������ �ؽ�Ʈ")] public Text gameSpeedText;
    [Header("���� ���� ��ư")] public Button escapeButton;
    [Header("���� ���� �г�")] public GameObject escapePanel;

    [Header("���׷��̵� ���")] public List<int> upgradePrice;
    [Header("���׷��̵� ����")] public List<int> upgradeLevel;

    [Header("���׷��̵� ��ư")] public List<Button> upgradeButtons;
    [Header("���׷��̵� ��ư ��Ȱ��ȭ �̹���")] public List<Image> hideUpgradeButtonImages;
    [Header("���׷��̵� �ؽ�Ʈ")] public List<Text> upgradeTexts;
    [Header("���׷��̵� ��Ȱ��ȭ �ؽ�Ʈ")] public List<Text> hideUpgradeTexts;

    [Header("���׷��̵� ������ �ؽ�Ʈ")] public List<Text> upgradeIconTexts;
    [Header("���׷��̵� �̸� �ؽ�Ʈ")] public List<Text> upgradeNameTexts;
    [Header("�÷��̾� ���ݷ� �ε�������")] public Text playerDamageIndicator;
    [Header("�÷��̾� ������ �ε�������")] public Text playerGoldIndicator;

    //��� ���� bool����
    internal bool is2xSpeed;

    //��ȹ�� : �÷��̾� HP���� �ε������Ͱ� �ϳ��� ���µ� ��� Ȯ���ϳ���
    //X2 ��ư ������ ��ư�� ��� ���ϳ���, ȸ������ ���ϳ��� �ؽ�Ʈ�� X1�� �ٲ�� �ϳ���
    //���� ���� ��ư ������ ���� ������ �Ͻ����� ���Ѿ� �ϳ���
    //�� ó�� �̹����� �߰��� �̹����� ��ư �迭�� �ٸ�����
    //���ݼӵ��� ������ 0.1 ����̸� ������ ���ݼӵ��� �������� �ʳ���
    //������ 1/interval�� ����
    //��ų �ؿ� ���ݷ� 3 �̶�� �����ִ� �κ��� ���� ���ݷ��� ǥ���ϳ��� ���������� ���ݷ��� �������Ѽ� ǥ���ϳ���

    private void Awake()
    {
        //UI�Ŵ����� ���ٸ�
        if (instance == null)
        {
            //���� ������Ʈ�� UI�Ŵ����� ����
            instance = this;
        }
        //�̹� UI�Ŵ����� �ִٸ�
        else
        {
            //������Ʈ�� ��� �ı��ϰ� Awake �޼��带 ������
            DestroyImmediate(this);
            return;
        }
        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        is2xSpeed = false;
        escapePanel.SetActive(false);

        //0��, 1���� ���� ������ 1�̹Ƿ� �� �� 1�� ������
        OnClickUpgrade(1);
        OnClickUpgrade(0);


        Debug.Log($"�÷��̾� Start : {player.originalDamage}");

        for (int i = 2; i < upgradeLevel.Count; i++)
        {
            upgradeLevel[i] = 0;
        }

        for (int i = 2; i < upgradePrice.Count; i++)
        {
            SetPrice(i);
        }

        PlayerMoneyCheck();
    }

    private void Update()
    {


    }

    //�÷��̾� ü�¹� ���� �޼���
    public void PlayerHPRenewal()
    {
        if (GameManager.Instance.player != null)
        {
            playerHealthImage.fillAmount =
            GameManager.Instance.player.health / GameManager.Instance.player.maxHealth;
        }
        else
        {
            Debug.Log("UI�Ŵ����� �÷��̾ Null������");
        }
    }

    //��� ���� ��ư Ŭ�� �޼���
    public void OnClickButton2X()
    {
        if (is2xSpeed == false)
        {
            Time.timeScale = 2f;
            is2xSpeed = true;
            gameSpeedText.text = "X2";
        }
        else
        {
            Time.timeScale = 1f;
            is2xSpeed = false;
            gameSpeedText.text = "X1";
        }
    }

    //�����ư Ŭ�� �޼���
    public void OnClickEscape()
    {
        Time.timeScale = 0f;
        escapePanel.gameObject.SetActive(true);
    }

    //������Ʈ���� ���������� �÷��̾� ��差�� ������ �������� ȣ���ϱ�
    public void PlayerMoneyCheck()
    {
        for (int i = 0; i < upgradePrice.Count; i++)
        {
            if (GameManager.Instance.player.gold <= upgradePrice[0])
            {
                //�������� ���ݺ��� ���ٸ�
                //��ư [0]�� ��Ȱ��ȭ�ϰ� ����� �̹���[0]�� Ȱ��ȭ
                //�ƴϸ� ��ư[0]Ȱ��ȭ

                upgradeButtons[i].gameObject.SetActive(false);
                hideUpgradeButtonImages[i].gameObject.SetActive(true);
            }
            else
            {
                upgradeButtons[i].gameObject.SetActive(true);
                hideUpgradeButtonImages[i].gameObject.SetActive(false);
            }
        }

        //ũ�� Ȯ���� 100�̻��Ͻ� ��ư ��Ȱ��ȭ
        if (100 <= upgradeLevel[3])
        {
            upgradeButtons[3].gameObject.SetActive(false);
            hideUpgradeButtonImages[3].gameObject.SetActive(true);
        }
    }

    //���ڷ� ���� ���� ������� ���׷��̵� �ϴ� �޼���
    public void OnClickUpgrade(int num)
    {
        upgradeLevel[num]++;
        GameManager.Instance.player.gold -= upgradePrice[num];
        SetPlayerStatus(num);
        SetPrice(num);
        SetDamageIndicator();
        PlayerMoneyCheck();
    }

    public void SetPlayerStatus(int i)
    {
        switch (i)
        {
            case 0:
                GameManager.Instance.player.damage += 1;
                GameManager.Instance.player.originalDamage += 1;
                break;
            case 1:
                GameManager.Instance.player.health += 5;
                GameManager.Instance.player.maxHealth += 5;
                break;
            case 2:
                GameManager.Instance.player.healthRegen += 0.6f;
                break;
            case 3:
                GameManager.Instance.player.critlcalChance += 1;
                break;
            case 4:
                GameManager.Instance.player.criticalMultiplier += 1;
                break;
            case 5:
                GameManager.Instance.player.attackInterval -= 0.1f;
                break;
            case 6:
                GameManager.Instance.player.doubleShot += 1;
                break;
        }
    }

    private void SetPrice(int i)
    {
        upgradePrice[i] = (upgradeLevel[i] + 1) * 10;

        Debug.Log($"��� {i} : {upgradePrice[i]}");

        upgradeTexts[i].text = $"��ȭ\nG {upgradePrice[i].ToString()}";
        hideUpgradeTexts[i].text = $"��ȭ\nG {upgradePrice[i].ToString()}";
        upgradeIconTexts[i].text = $"       Lv.{upgradeLevel[i]}";

        switch (i)
        {
            case 0:
                upgradeNameTexts[i].text = $"���ݷ�\n  {player.originalDamage}";
                break;
            case 1:
                upgradeNameTexts[i].text = $"ü��\n  {upgradeLevel[i] * 5}";

                break;
            case 2:
                upgradeNameTexts[i].text = $"ü�� ȸ��\n  {upgradeLevel[i] * 0.6f}";

                break;
            case 3:
                upgradeNameTexts[i].text = $"ġ��Ÿ Ȯ��\n  {upgradeLevel[i] * 1}";

                break;
            case 4:
                upgradeNameTexts[i].text = $"ġ��Ÿ ����\n  {100 + upgradeLevel[i] * 1}";
                break;
            case 5:
                upgradeNameTexts[i].text = $"���� �ӵ�\n  {1 / player.originalAttackInterval}";
                break;
            case 6:
                upgradeNameTexts[i].text = $"���� ��\n  {upgradeLevel[i] * 1}";
                break;
        }
    }

    public void SetDamageIndicator()
    {
        playerDamageIndicator.text = $"���ݷ� {GameManager.Instance.player.damage}";
    }
}