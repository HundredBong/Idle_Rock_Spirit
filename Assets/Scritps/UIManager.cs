using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance { get { return instance; } }

    internal Player player;

    [Header("플레이어 체력바")]public Image playerHealthImage;
    [Header("배속 조절용 버튼")] public Button gameSpeedButton;
    [Header("배속 조절용 텍스트")] public Text gameSpeedText;
    [Header("게임 종료 버튼")] public Button escapeButton;
    [Header("게임 종료 패널")] public GameObject escapePanel;
    [Header("스킬 쿨타임 이미지")] public List<Image> intervalImages;
    [Header("업그레이드 버튼")] public List<Button> upgradeButtons;
    //배속 관련 bool변수
    internal bool is2xSpeed;

    //기획서 : 플레이어 HP관련 인디케이터가 하나도 없는데 어디서 확인하나요
    //X2 버튼 누르면 버튼이 어떻게 변하나요, 회색으로 변하나요 텍스트는 X1로 바꿔야 하나요
    //게임 종료 버튼 누르는 동안 게임을 일시정지 시켜야 하나요
    //왜 처음 이미지랑 중간에 이미지랑 버튼 배열이 다른가요


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
        //player = GameManager.Instance.player;
        //Debug.Log($"UI매니저 플레이어 인식 : {player.name}");
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
    
}
