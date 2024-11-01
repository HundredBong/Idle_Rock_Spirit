using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance { get { return instance; } }

    internal Player player;

    [Header("�÷��̾� ü�¹�")]public Image playerHealthImage;
    [Header("��� ������ ��ư")] public Button gameSpeedButton;
    [Header("��� ������ �ؽ�Ʈ")] public Text gameSpeedText;
    [Header("���� ���� ��ư")] public Button escapeButton;
    [Header("���� ���� �г�")] public GameObject escapePanel;
    [Header("��ų ��Ÿ�� �̹���")] public List<Image> intervalImages;
    [Header("���׷��̵� ��ư")] public List<Button> upgradeButtons;
    //��� ���� bool����
    internal bool is2xSpeed;

    //��ȹ�� : �÷��̾� HP���� �ε������Ͱ� �ϳ��� ���µ� ��� Ȯ���ϳ���
    //X2 ��ư ������ ��ư�� ��� ���ϳ���, ȸ������ ���ϳ��� �ؽ�Ʈ�� X1�� �ٲ�� �ϳ���
    //���� ���� ��ư ������ ���� ������ �Ͻ����� ���Ѿ� �ϳ���
    //�� ó�� �̹����� �߰��� �̹����� ��ư �迭�� �ٸ�����


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
        //player = GameManager.Instance.player;
        //Debug.Log($"UI�Ŵ��� �÷��̾� �ν� : {player.name}");
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
    
}
