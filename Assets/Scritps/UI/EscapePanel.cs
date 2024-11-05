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
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}
