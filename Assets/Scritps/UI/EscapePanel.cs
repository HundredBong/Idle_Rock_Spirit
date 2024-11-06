using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EscapePanel : MonoBehaviour
{
    public Button yes;
    public Button no; 

    public void OnClickYes()
    {
        Application.Quit();
        
    }

    public void OnClickNo()
    {
        if (UIManager.Instance.is2xSpeed == true)
            Time.timeScale = 2f;
        else
            Time.timeScale = 1f;

        gameObject.SetActive(false);
    }
}
