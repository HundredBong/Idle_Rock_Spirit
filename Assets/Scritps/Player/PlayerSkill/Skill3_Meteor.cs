using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class Skill3_Meteor : MonoBehaviour
{
    [SerializeField, Header("스킬에 사용할 프리팹")] private Skill3_Projectile projtilePrefab;

    private float projectileDamage;//투사체 대미지
    [SerializeField, Header("기본공격 대비 대미지 배율(12)")] private float damageMultiplier;
    [SerializeField, Header("투사체가 날아갈 속도")] private float projectileSpeed;
    [SerializeField, Header("스킬 쿨타임(3)")] private float fireInterval;
    [SerializeField, Header("투사체 생성 좌표")] private Vector3 rendererStartPos;

    //public float projectileScale;
    private float preFireTime;


    //EnemyUtil을 사용하기 위한 변수
    private Enemy targetEnemy;
    private Vector3 closestEnemyPosition;
    private float closestEnemyDistance;


    //기획서 : 범위인지 단일인지 안적어놓았는데 단일이면 Thunder랑 컨셉이 겹치지 않나요
    //추적이 완전히 타게팅인가요 아니면 몬스터가 있는 위치에 떨군다는 뜻인가요
    //카메라 화면 상단이라 했는데 그럼 메테오가 오른쪽에서 떨어질 수도 있나요
    //카메라 화면 상단이면 카메라가 보는 곳에서 생성되나요 그래도 메테오인데
    //메테오는 언제 없어져야 하나요 

    private void Start()
    {
        fireInterval = GameManager.Instance.player.skillCooltime[2];
        //StartCoroutine(FireCoroutine());
    }
    private void Update()
    {
        closestEnemyPosition = EnemyUtility.GetTargetPosition(transform, out targetEnemy);

        if (GameManager.Instance.enemies != null)
            gameObject.transform.position = closestEnemyPosition;

        Fire();

    }

    private IEnumerator FireCoroutine()
    {
        while (true)
        {
            Fire();
            yield return new WaitForSeconds(fireInterval);
        }
    }

    private void Fire()
    {
        //쿨타임 재보고 안되면 리턴
        if (preFireTime + fireInterval > Time.time) { return; }

        float dis = Vector3.Distance(transform.position, GameManager.Instance.player.transform.position);

        //쿨타임 됐는데 사거리가 안되면 리턴
        if (dis >= GameManager.Instance.player.attackRange)
        {
            return;
        }

        projectileDamage = GameManager.Instance.player.damage * damageMultiplier;

        Skill3_Projectile proj = Instantiate(projtilePrefab);

        proj.damage = this.projectileDamage;
        //거리 = 속도 x 시간
        //시간 = 거리 / 속도
        //속도 = 거리 / 시간
        proj.duration = 1 / projectileSpeed; //시간 = 속도 / 거리
        proj.rendererStartPos = this.rendererStartPos;
        //proj.transform.localScale = proj.transform.localScale * projectileScale;
        //부모 오브젝트 기준으로 랜덤한 위치에서 생성
        proj.transform.localPosition = new Vector2(transform.position.x - 0.5f, transform.position.y);
        proj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(10, 50));

        SkillCooltimeManager.Instance.UseSkill(2);

        preFireTime = Time.time;
    }
}