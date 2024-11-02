using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2_Void : MonoBehaviour
{
    [SerializeField, Header("스킬에 사용할 프리팹")] private Skill2_Projectile skill2_Projectile;
    private float projectileDamage; //투사체의 대미지
    [SerializeField, Header("기본공격 대비 대미지 배율(1.2)")] private float damageMultiplier;
    [SerializeField, Header("투사체가 날아갈 속도")] private float projectileSpeed;
    [SerializeField, Header("투사체의 공격 간격")] private float attackInterval;
    [SerializeField, Header("투사체의 공격 횟수(10)")] private int attackCount;
    [SerializeField, Header("스킬 쿨타임(5)")] private float fireInterval;
    [SerializeField, Header("스킬 지속시간)"),
        Tooltip("투사체 속도에 맞게 적절히 조절")] private float projectileDuration;
    private float preFireTime; //쿨타임 계산용 마지막으로 발사한 시간 

    //EnemyUtil을 사용하기 위한 변수
    private Enemy targetEnemy;
    private Vector3 closestEnemyPosition;
    private float closestEnemyDistance;

    //배속기능 시간 조절용 변수
    private float originalAttackInterval;
    private float originalFireInterval;
    private float originalProjectileDuration;

    //ProjectileLauncher의 정보를 가져오기 위한 변수
    private Transform projectileLauncherPos;

    //기획서 : 천천히가 어느정도 천천히 이동하는건가요
    //이동하는 오브젝트가 플레이어 투사체, enemy, 스킬 1,2,3,4 가 있는데 누구를 기준으로 잡을까요
    //이동하며 닿으면 한 번 피해를 주나요 아니면 닿는 동안 계속 피해를 주나요
    //만약 10회의 피해를 못주면 오브젝트는 언제 없어져야하나요
    //닿는 동안 계속 피해를 주면 피해를 주는 텀은 어떻게 되나요


    private void Start()
    {
        originalAttackInterval = attackInterval;
        originalFireInterval = fireInterval;
        originalProjectileDuration = projectileDuration;

        preFireTime = fireInterval * (-1);

        //보이드는 투사체가 생성되는 곳에서 생성되야하니 위치를 ProjectileLauncher로 설정하기위해 Find메서드 이용
        projectileLauncherPos = GameObject.Find("ProjectileLauncher").GetComponent<Transform>();
        transform.position = projectileLauncherPos.position;
        
        if (closestEnemyDistance <= GameManager.Instance.player.attackRange) { Fire(); }
    }

    private void Update()
    {
        SetInterval();
        Fire();
    }

    private void Fire()
    {
        //쿨타임 재보고 안되면 리턴
        if (preFireTime + fireInterval > Time.time) { return; }
        //쿨타임 됐는데 사거리가 안되면 리턴
        closestEnemyDistance = EnemyUtility.GetTargetDistance(transform, out targetEnemy);
        
        //projectileLauncher에서 나가므로 플레이어 사거리가 아닌 플레이어 사거리 - 런처 사이의 x좌표만큼 뺀 후 사거리 계산

        if (GameManager.Instance.player.attackRange-Mathf.Abs
            (GameManager.Instance.player.transform.position.x - 
                gameObject.transform.position.x) <= closestEnemyDistance)
        { return; }

        projectileDamage = GameManager.Instance.player.damage * damageMultiplier;

        Skill2_Projectile proj = Instantiate(skill2_Projectile, transform.position, Quaternion.identity);

        proj.projectileDamage = this.projectileDamage;
        proj.projectileSpeed = this.projectileSpeed;
        proj.attackInterval = this.attackInterval;
        proj.attackInterval = this.attackInterval;
        proj.attackCount = this.attackCount;
        proj.projectileDuration = this.projectileDuration;
        preFireTime = Time.time;
    }

    private void SetInterval()
    {
        if (UIManager.Instance.is2xSpeed == true)
        {
            attackInterval = originalAttackInterval / 2;
            fireInterval = originalFireInterval / 2;
            originalProjectileDuration = projectileDuration / 2;
        }
        else
        {
            attackInterval = originalAttackInterval;
            fireInterval = originalFireInterval;
            originalProjectileDuration = projectileDuration;
        }
    }
}
