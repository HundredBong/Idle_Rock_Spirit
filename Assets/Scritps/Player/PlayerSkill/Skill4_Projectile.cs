using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill4_Projectile : MonoBehaviour
{
    //스킬이 적 위에 프리팹이 생성되고 빠르게 낙하하는 형식이니까, 
    //빠르게 낙하할 스피드, 입힐 대미지
    //낙하

    public float projectileDamage;
    public float projectileSpeed;

    [SerializeField, Header("명중했을때 파티클")] private ParticleSystem particlePrefabHit;

    //겹치는 적 동시충돌 방지
    private bool hasCollided = false;

    private void Start()
    {
        //투사체 속도가 심하게 빨라서 빗나갈 일은 없지만 혹시 모르니 2초뒤 삭제
        Destroy(gameObject, 2f);
    }

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
            ParticleSystem parHit =Instantiate(particlePrefabHit,other.ClosestPoint(other.transform.position),Quaternion.identity);
            parHit.Play();
            Debug.Log("ThundeProj. OnTriggerEnter2D 조건문 만족");
            enemy.TakeDamage(projectileDamage);
            Destroy(gameObject);
        }
    }
}
