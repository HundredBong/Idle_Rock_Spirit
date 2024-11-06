using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;
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
        //밸런스용 주석 처리
        fireInterval = GameManager.Instance.player.skillCooltime[2];
    }
    private void Update()
    {
        closestEnemyPosition = EnemyUtility.GetTargetPosition(transform, out targetEnemy);
        closestEnemyDistance = EnemyUtility.GetTargetDistance(transform, out targetEnemy);
        //메테오 스포너는 항상 가장 가까운 적을 찾고 그 좌표에 위치함
        gameObject.transform.position = closestEnemyPosition;

        //가장 가까운 적의 거리가 플레이어의 사거리보다 크면
        if (GameManager.Instance.player.attackRange <= closestEnemyDistance)
        {
            //오브젝트의 위치를 공격 사거리 끝에 위치시킴
            //메테오가 0,0에 생성되던 문제 해결용 
            transform.position = new Vector2(GameManager.Instance.player.transform.position.x +
                GameManager.Instance.player.attackRange+0.1f, GameManager.Instance.player.transform.position.y);
        }
        

        ////혹시나 없으면 Player의 공격 사거리 끝거리에 위치시킴
        ////메테오가 0,0에 생성되던 문제 해결용 
        //if (GameManager.Instance.enemies == null)
        //{
        //    gameObject.transform.position =
        //        new Vector2(GameManager.Instance.player.transform.position.x + GameManager.Instance.player.attackRange,
        //        GameManager.Instance.player.transform.position.y);
        //}

        Fire();

        if (transform.position == Vector3.zero)
        {
            Debug.LogError("메테오 스포너의 위치가 zero임");
        }

    }

    //private IEnumerator FireCoroutine()
    //{
    //    while (true)
    //    {
    //        Fire();
    //        yield return new WaitForSeconds(fireInterval);
    //    }
    //}

    private void Fire()
    {
        //쿨타임 재보고 안되면 리턴
        if (preFireTime + fireInterval > Time.time) { return; }

        //플레이어의 체력이 0이하면 리턴
        if (GameManager.Instance.player.health <= 0) { return; }

        float dis = Vector3.Distance(transform.position, GameManager.Instance.player.transform.position);

        //쿨타임 됐는데 사거리가 안되면 리턴
        if (dis >= GameManager.Instance.player.attackRange)
        {
            return;
        }

        //생성할 투사체의 대미지를 플레이어 대미지 * 배율로 설정
        projectileDamage = GameManager.Instance.player.damage * damageMultiplier;

        Skill3_Projectile proj = Instantiate(projtilePrefab, new Vector2(transform.position.x - 0.5f, transform.position.y),
            Quaternion.Euler(0, 0, Random.Range(10, 50)));

        proj.damage = this.projectileDamage;
        //거리 = 속도 x 시간
        //시간 = 거리 / 속도
        //속도 = 거리 / 시간
        proj.duration = 1 / projectileSpeed; //시간 = 속도 / 거리  
        proj.rendererStartPos = this.rendererStartPos; //메테오 스폰 위치를 설정

        //투사체가 생성될 위치를 몬스터의 x축 좌표 - 0.5로 예상 이동 경로에 생성
        //proj.transform.position = new Vector2(transform.position.x - 0.5f, transform.position.y);

        //z좌표를 랜덤으로 하여 떨어지는 각도를 다양하게 조절
        //proj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(10, 50));

        //쿨타임 타이머 활성화
        SkillCooltimeManager.Instance.UseSkill(2);

        //마지막으로 공격한 시간 초기화
        preFireTime = Time.time;
    }
}