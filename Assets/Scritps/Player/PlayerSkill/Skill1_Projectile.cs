using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Skill1_Projectile : MonoBehaviour
{
    public float projectileDamage; //투사체 대미지
    internal float projectileSpeed; //투사체 이동속도
    internal float riseTime; //투사체가 올라가는 시간
    internal float duration; //투사체 지속 시간
    private Vector2 riseDir; //처음에 위로 올라갈 방향


    [SerializeField, Header("명중했을때 파티클")] private ParticleSystem particlePrefabHit;
    [SerializeField, Header("생성될때 파티클")] private ParticleSystem particlePrefabSpawn;

    //EnemyUtil을 사용하기 위한 변수
    private Enemy targetEnemy;
    private Vector3 closestEnemyPosition;

    //겹치는 적 동시충돌 방지
    private bool hasCollided = false;

    //Start시 올라가게 하는 용도로 선언한 변수
    private bool isRise;

    private void Start()
    {
        isRise = true;
        riseDir = new Vector2(Random.Range(-1.5f, 0.5f), Random.Range(0.3f, 1.5f));
        Destroy(gameObject, riseTime + duration);
        ParticleSystem spawnPar = Instantiate(particlePrefabSpawn, transform);
        spawnPar.Play();
    }

    private void Update()
    {
        //riseDir = new Vector2(Random.Range(-2f, 2f), Random.Range(0.3f, 2f));
        //업데이트에서 방향 잡으면 올라갈때 계속 방향이 바뀌므로 Start에서 한번만 실행
        if (isRise == true)
        {
            transform.Translate(riseDir * (projectileSpeed / 15) * Time.deltaTime);
            Invoke("SetRise", riseTime);
            //Update로 매 프레임 위로 상승하다가 Invoke 메서드로 인해 bool변수 변경으로 아래 코드 실행
        }
        else
        {
            //가장 가까운 적의 위치를 찾고, 그 적의 정보를 인자로 넘겨줌
            closestEnemyPosition = EnemyUtility.GetTargetPosition(transform, out targetEnemy);
            Move(closestEnemyPosition);
        }
    }

    private void Move(Vector2 dir)
    {
        Vector2 moveDir = new Vector2(dir.x - transform.position.x, dir.y - transform.position.y);
        Vector2 nomalizedMoveDir = moveDir.normalized;
        transform.Translate(nomalizedMoveDir * projectileSpeed * Time.deltaTime);
    }

    private void SetRise()
    {
        isRise = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasCollided == true) { return; }

        if (other.TryGetComponent(out Enemy enemy))
        {
            hasCollided = true;

            ParticleSystem par = Instantiate(particlePrefabHit, other.ClosestPoint(other.transform.position), Quaternion.identity);
            par.Play();
            //Destroy(par, 0.4f);
            enemy.TakeDamage(projectileDamage);
            Destroy(gameObject);
        }
    }

}

