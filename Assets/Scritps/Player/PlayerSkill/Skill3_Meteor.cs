using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill3_Meteor : MonoBehaviour
{
    public Skill3_Projectile projtilePrefab;
    private Enemy targetEnemy;

    public float damage;
    public float projectileSpeed;
    public float projectileScale;
    public float shotInterval;

    //기획서 : 범위인지 단일인지 안적어놓았는데 단일이면 Thunder랑 컨셉이 겹치지 않나요
    //추적이 완전히 타게팅인가요 아니면 몬스터가 있는 위치에 떨군다는 뜻인가요
    //카메라 화면 상단이라 했는데 그럼 메테오가 오른쪽에서 떨어질 수도 있나요
    //카메라 화면 상단이면 카메라가 보는 곳에서 생성되나요 그래도 메테오인데
    //메테오는 언제 없어져야 하나요 

    private void Start()
    {
        StartCoroutine(FireCoroutine());
    }
    private void Update()
    {
        Vector3 closestEnemyPosition = EnemyUtility.GetTargetPosition(transform, out targetEnemy);
        if(GameManager.Instance.enemies != null)
            gameObject.transform.position = closestEnemyPosition;
    }
    private IEnumerator FireCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(shotInterval);
            Fire();
        }
    }

    private void Fire()
    {
        //Vector2 randomPos = Random.insideUnitCircle * MaxDist;

        Skill3_Projectile proj = Instantiate(projtilePrefab);

        proj.damage = this.damage;
        //거리 = 속도 x 시간
        //시간 = 거리 / 속도
        //속도 = 거리 / 시간
        proj.duration = 1 / projectileSpeed; //시간 = 속도 / 거리
        proj.transform.localScale = proj.transform.localScale * projectileScale;
        //부모 오브젝트 기준으로 랜덤한 위치에서 생성
        proj.transform.localPosition = new Vector2(transform.position.x - 0.5f, transform.position.y);
        proj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(10, 50));
    }
}