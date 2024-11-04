using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2_Projectile : MonoBehaviour
{
    internal float projectileDamage; //투사체 대미지
    internal float projectileSpeed; //투사체 이동속도
    internal float attackInterval; //투사체 공격 간격
    private float preAttackTime; //공격 간격 계산용 마지막으로 공격한 시간 
    internal int attackCount; //투사체 공격 횟수
    internal float projectileDuration; //투사체 지속 시간

    //오버랩용 캡슐 콜라이더
    private CircleCollider2D coll;

    [SerializeField, Header("명중했을때 파티클")] private ParticleSystem particlePrefabHit;
    [SerializeField, Header("생성될때 파티클")] private ParticleSystem particlePrefabSpawn;

    private void Awake()
    {
        coll = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        Destroy(gameObject, projectileDuration);
        ParticleSystem spawnPar = Instantiate(particlePrefabSpawn, transform);
        spawnPar.Play();
    }

    private void Update()
    {
        transform.Translate(Vector3.right * projectileSpeed * Time.deltaTime);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (preAttackTime + attackInterval > Time.time) { return; }

        Collider2D[] contactedEnemies = Physics2D.OverlapCircleAll(transform.position, coll.radius);
        foreach (Collider2D ContactedEnemy in contactedEnemies)
        {
            if (ContactedEnemy.TryGetComponent(out Enemy enemy))
            {
                ParticleSystem hitPar = Instantiate(particlePrefabHit, other.ClosestPoint(other.transform.position),
                    Quaternion.identity);

                hitPar.Play();

                enemy.TakeDamage(projectileDamage);
                attackCount--;
                if (attackCount <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
        preAttackTime = Time.time;
    }
}
