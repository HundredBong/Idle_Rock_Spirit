using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField, Header("게임 오버 Yes버튼")] private Button gmaeOverButtonYes;
    [SerializeField, Header("게임 오버 No버튼")] private Button gmaeOverButtonNo;
    [SerializeField, Header("Continue 파티클")] private ParticleSystem particlePrefab;

    private EnemySpawner spawner;
    //private TitlePanel titlePanell;
    //1. 플

    private float playerMaxHp;

    private void Awake()
    {
        //enemySpawner의 값을 변경해야 하므로 가져옴
        spawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        //titlePanell = GameObject.Find("StartTitlePanel").GetComponent<TitlePanel>();
    }

    private void Start()
    {
        //게임 시작시 오브젝트를 숨겨서 보이지 않게 함
        gameObject.SetActive(false);
    }

    public void ActivatePanel()
    {
        //플레이어의 Death메서드에서 호출됨
        //Time.timeScale = 0.1f;
        gameObject.SetActive(true);
    }

    public void OnClickNo()
    {
        ////몬스터 제거
        //GameManager.Instance.RetryGame();
        //gameObject.SetActive(false);

        ////타이틀 화면 활성화 및 각종 업그레이드, 스킬 초기화
        //Time.timeScale = 0f;

        ////하드코딩
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
        //임시로 플레이어의 체력을 저장
        playerMaxHp = GameManager.Instance.player.maxHealth;

        if (UIManager.Instance.is2xSpeed == true)
            Time.timeScale = 2f;
        else
            Time.timeScale = 1f;
        //enemy의 강화만 줄여야 하니까 enemy Spawn의 값을 변경
        //리스트가 비워질 때 ++로 0이 됨
        spawner.increase = -1;

        //게임매니저를 통해서 맵에 존재하는 모든 enemy를 제거해야 하는데 이때 골드는 어떡하지 아,
        //않이면 그냥 싹 다 Death메서드 실행시키게 하고 패널티랍시고 플레이어 골드를 0으로 맞추는건
        //매우 그럴싸하니까 당장 실행하기
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

    //enemy는 날아가는데 enemy의 투사체가 안날아가서 나오자마자 게임종료패널 재호출됨
    //일시적으로 무적으로 만들기
    //이게 다 쓸데없이 enemy의 공격을 투명한 투사체로 해서 그렇읍니다.
    public void DelayHealthRegen()
    {
        //임시로 저장된 값을 불러옴
        GameManager.Instance.player.health = playerMaxHp;
        GameManager.Instance.player.maxHealth = GameManager.Instance.player.health;

        //UI창 업데이트
        UIManager.Instance.PlayerHPRenewal();
        UIManager.Instance.PlayerMoneyRenewal();
    }
}
