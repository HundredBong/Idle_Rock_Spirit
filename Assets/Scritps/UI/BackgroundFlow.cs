using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFlow : MonoBehaviour
{
    [SerializeField, Header("��� �̵� �ӵ�")] private float flowSpeed;

    private void Update()
    {
        if (GameManager.Instance.player.anim.GetBool("isMove") == true)
        {
            transform.Translate(Vector2.left * flowSpeed * Time.deltaTime);
        }
        if (gameObject.transform.position.x < -19.9f)
        {
            if (GameManager.Instance.player.health >= 0)
            {
                Debug.Log("��� �̵���");
                transform.position = new Vector2(-2.2f, 2.1f);
            }
        }
    }
}
