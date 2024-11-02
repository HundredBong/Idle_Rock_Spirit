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

    [Header("플레이어 체력바")] public Image playerHealthImage;
    [Header("배속 조절용 버튼")] public Button gameSpeedButton;
    [Header("배속 조절용 텍스트")] public Text gameSpeedText;
    [Header("게임 종료 버튼")] public Button escapeButton;
    [Header("게임 종료 패널")] public GameObject escapePanel;

    [Header("업그레이드 비용")] public List<int> upgradePrice;
    [Header("업그레이드 레벨")] public List<int> upgradeLevel;

    [Header("업그레이드 버튼")] public List<Button> upgradeButtons;
    [Header("업그레이드 버튼 비활성화 이미지")] public List<Image> hideUpgradeButtonImages;
    [Header("업그레이드 텍스트")] public List<Text> upgradeTexts;
    [Header("업그레이드 비활성화 텍스트")] public List<Text> hideUpgradeTexts;

    [Header("업그레이드 아이콘 텍스트")] public List<Text> upgradeIconTexts;
    [Header("업그레이드 이름 텍스트")] public List<Text> upgradeNameTexts;
    [Header("플레이어 공격력 인디케이터")] public Text playerDamageIndicator;
    [Header("플레이어 소지금 인디케이터")] public Text playerGoldIndicator;

    //배속 관련 bool변수
    internal bool is2xSpeed;

    //기획서 : 플레이어 HP관련 인디케이터가 하나도 없는데 어디서 확인하나요
    //X2 버튼 누르면 버튼이 어떻게 변하나요, 회색으로 변하나요 텍스트는 X1로 바꿔야 하나요
    //게임 종료 버튼 누르는 동안 게임을 일시정지 시켜야 하나요
    //왜 처음 이미지랑 중간에 이미지랑 버튼 배열이 다른가요
    //공격속도가 레벨당 0.1 상승이면 오히려 공격속도가 느려지지 않나요
    //계산식을 1/interval로 수정
    //스킬 밑에 공격력 3 이라고 적혀있는 부분은 원본 공격력을 표시하나요 레이지때는 공격력을 증가시켜서 표시하나요

    private void Awake()
    {
        //UI매니저가 없다면
        if (instance == null)
        {
            //현재 오브젝트를 UI매니저로 설정
            instance = this;
        }
        //이미 UI매니저가 있다면
        else
        {
            //오브젝트를 즉시 파괴하고 Awake 메서드를 종료함
            DestroyImmediate(this);
            return;
        }
        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        is2xSpeed = false;
        escapePanel.SetActive(false);

        //0번, 1번은 시작 레벨이 1이므로 둘 다 1씩 더해줌
        OnClickUpgrade(1);
        OnClickUpgrade(0);


        Debug.Log($"플레이어 Start : {player.originalDamage}");

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

    //플레이어 체력바 갱신 메서드
    public void PlayerHPRenewal()
    {
        if (GameManager.Instance.player != null)
        {
            playerHealthImage.fillAmount =
            GameManager.Instance.player.health / GameManager.Instance.player.maxHealth;
        }
        else
        {
            Debug.Log("UI매니저의 플레이어가 Null상태임");
        }
    }

    //배속 조절 버튼 클릭 메서드
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

    //종료버튼 클릭 메서드
    public void OnClickEscape()
    {
        Time.timeScale = 0f;
        escapePanel.gameObject.SetActive(true);
    }

    //업데이트에서 돌리지말고 플레이어 골드량에 변동이 있을때만 호출하기
    public void PlayerMoneyCheck()
    {
        for (int i = 0; i < upgradePrice.Count; i++)
        {
            if (GameManager.Instance.player.gold <= upgradePrice[0])
            {
                //소지금이 가격보다 적다면
                //버튼 [0]를 비활성화하고 숨기기 이미지[0]을 활성화
                //아니면 버튼[0]활성화

                upgradeButtons[i].gameObject.SetActive(false);
                hideUpgradeButtonImages[i].gameObject.SetActive(true);
            }
            else
            {
                upgradeButtons[i].gameObject.SetActive(true);
                hideUpgradeButtonImages[i].gameObject.SetActive(false);
            }
        }

        //크리 확률이 100이상일시 버튼 비활성화
        if (100 <= upgradeLevel[3])
        {
            upgradeButtons[3].gameObject.SetActive(false);
            hideUpgradeButtonImages[3].gameObject.SetActive(true);
        }
    }

    //인자로 받은 값을 기반으로 업그레이드 하는 메서드
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

        Debug.Log($"비용 {i} : {upgradePrice[i]}");

        upgradeTexts[i].text = $"강화\nG {upgradePrice[i].ToString()}";
        hideUpgradeTexts[i].text = $"강화\nG {upgradePrice[i].ToString()}";
        upgradeIconTexts[i].text = $"       Lv.{upgradeLevel[i]}";

        switch (i)
        {
            case 0:
                upgradeNameTexts[i].text = $"공격력\n  {player.originalDamage}";
                break;
            case 1:
                upgradeNameTexts[i].text = $"체력\n  {upgradeLevel[i] * 5}";

                break;
            case 2:
                upgradeNameTexts[i].text = $"체력 회복\n  {upgradeLevel[i] * 0.6f}";

                break;
            case 3:
                upgradeNameTexts[i].text = $"치명타 확률\n  {upgradeLevel[i] * 1}";

                break;
            case 4:
                upgradeNameTexts[i].text = $"치명타 피해\n  {100 + upgradeLevel[i] * 1}";
                break;
            case 5:
                upgradeNameTexts[i].text = $"공격 속도\n  {1 / player.originalAttackInterval}";
                break;
            case 6:
                upgradeNameTexts[i].text = $"더블 샷\n  {upgradeLevel[i] * 1}";
                break;
        }
    }

    public void SetDamageIndicator()
    {
        playerDamageIndicator.text = $"공격력 {GameManager.Instance.player.damage}";
    }
}