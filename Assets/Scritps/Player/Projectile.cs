using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //런처에서 설정할 대미지
    //디버그용으로 직렬화
    [SerializeField]internal float damage;
    //런처에서 설정할 투사체 속도
    internal float projectileSpeed;
    //겹치는 적 동시충돌 방지
    private bool hasCollided = false;

    private Rigidbody2D rb;

    [SerializeField, Header("명중했을때 파티클")] private ParticleSystem particlePrefabHit;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        //포물선 이동을 위해 리지드바디 적용
        rb.AddForce(transform.up * projectileSpeed, ForceMode2D.Impulse);

        //2초후 자기 자신을 삭제함, 배속일때도 2초면 충분함
        Destroy(gameObject,2f);
    }

    //private void OnCollisionEnter2D(Collision2D other)
    //{
    //    if (hasCollided == true) { return; }

    //    if (other.collider.TryGetComponent(out Enemy enemy))
    //    {
    //        hasCollided = true;
    //        //enemy의 TakeDamage 메서드 실행후 삭제
    //        enemy.TakeDamage(damage);
    //        Destroy(gameObject);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasCollided == true) { return; }

        if (other.TryGetComponent(out Enemy enemy))
        {
            hasCollided = true;

            ParticleSystem par = Instantiate(particlePrefabHit, other.ClosestPoint(other.transform.position), Quaternion.identity);
            par.Play();

            //enemy의 TakeDamage 메서드 실행후 삭제
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
