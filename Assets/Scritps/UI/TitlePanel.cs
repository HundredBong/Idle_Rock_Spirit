using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitlePanel : MonoBehaviour
{
    [Header("타이틀 Start버튼")] public Button titleStart;
    [Header("타이틀 Quit버튼")] public Button titleQuit;
    
    public void Start()
    {
        ActivatePanel();
    }

    public void OnClickStart()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        //Invoke("DelayStart", 0.5f);
        //UIManager.Instance.gameOverPanel.gameObject.SetActive(false);
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

    public void ActivatePanel()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    //private void DelayStart()
    //{
    //    gameObject.SetActive(false);
    //}
}
