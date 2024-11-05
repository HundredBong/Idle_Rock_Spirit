using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFlow : MonoBehaviour
{
    [SerializeField, Header("배경 이동 속도")] private float flowSpeed;

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
                Debug.Log("배경 이동용");
                transform.position = new Vector2(-2.2f, 2.1f);
            }
        }
    }
}
