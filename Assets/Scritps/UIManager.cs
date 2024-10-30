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

    public void PlayerHPRenewal()
    {
        playerHealthImage.fillAmount =
            GameManager.Instance.player.health / GameManager.Instance.player.maxHealth;
    }
}
