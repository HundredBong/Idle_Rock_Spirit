using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance;

    public Image playerHealthImage;

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

    public void PlayerHPRenewal()
    {
        playerHealthImage.fillAmount =
            GameManager.Instance.player.health / GameManager.Instance.player.maxHealth;
    }
}
