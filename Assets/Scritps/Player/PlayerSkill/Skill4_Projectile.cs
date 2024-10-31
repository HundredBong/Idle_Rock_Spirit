using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill4_Projectile : MonoBehaviour
{
    //스킬이 적 위에 프리팹이 생성되고 빠르게 낙하하는 형식이니까, 
    //빠르게 낙하할 스피드, 입힐 대미지
    //낙하
    //않이면 메테오랑 똑같은 로직을 적용하되 이동 속도만 미친듯이 빠르게 는 제가 싫음

    public float projectileDamage;
    public float projectileSpeed;

    //겹치는 적 동시충돌 방지
    private bool hasCollided = false;

    private void Update()
    {
        gameObject.transform.Translate(Vector3.down * projectileSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("ThundeProj. OnTriggerEnter2D 진입");
        if (hasCollided == true) { return; }
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            hasCollided = true;
            Debug.Log("ThundeProj. OnTriggerEnter2D 조건문 만족");
            enemy.TakeDamage(projectileDamage);
            Destroy(gameObject);
        }
    }
}
