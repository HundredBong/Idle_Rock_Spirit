using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField, Header("게임 오버 Yes버튼")]private Button gmaeOverButtonYes;
    [SerializeField, Header("게임 오버 No버튼")] private Button gmaeOverButtonNo;

    private EnemySpawner spawner;

    //1. 플

    private void Awake()
    {
        //enemySpawner의 값을 변경해야 하므로 가져옴
        spawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
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
        //앱 종료
        Application.Quit();
    }
    
    public void OnclickYes()
    {
        Time.timeScale = 1f;
        //enemy의 강화만 줄여야 하니까 enemy Spawn의 값을 변경
        //리스트가 비워질 때 ++로 0이 됨
        spawner.increase = -1;

        //게임매니저를 통해서 맵에 존재하는 모든 enemy를 제거해야 하는데 이때 골드는 어떡하지 아,
        //않이면 그냥 싹 다 Death메서드 실행시키게 하고 패널티랍시고 플레이어 골드를 0으로 맞추는건
        //매우 그럴싸하니까 당장 실행하기
        GameManager.Instance.RetryGame();

        //플레이어의 체력을 다시 회복
        GameManager.Instance.player.health = GameManager.Instance.player.maxHealth;

        //창 닫아줌
        GameManager.Instance.player.gold = 0;

        //UI창 업데이트
        UIManager.Instance.PlayerHPRenewal();
        UIManager.Instance.PlayerMoneyRenewal();

        gameObject.SetActive(false);
    }
}
