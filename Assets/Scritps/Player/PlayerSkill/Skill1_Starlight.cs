using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1_Starlight : MonoBehaviour
{
    [SerializeField, Header("스킬에 사용할 프리팹")] private Skill1_Projectile starlightPrefab;
    [SerializeField, Header("생성할 투사체 개수(10)")] private int starlightCount;
    private float projectileDamage; //투사체의 대미지
    [SerializeField, Header("기본공격 대비 대미지 배율(1.5)")] private float damageMultiplier;
    [SerializeField, Header("투사체가 날아갈 속도")] private float projectileSpeed;
    [SerializeField, Header("투사체가 날아가는 간격")] private float innerInterval;
    [SerializeField, Header("투사체가 올라가는 시간"),
        Tooltip("기능 구현을 해보고싶어서 그만")]
    private float riseTime;
    //[SerializeField, Header("스킬 쿨타임(7)")]
    private float fireInterval;
    [SerializeField, Header("스킬 지속시간(2)"),Tooltip("지속시간동안 존재하다가 사라짐")] private float duration;
    private float preFireTime; //쿨타임 계산용 마지막으로 발사한 시간

    //배속때 시간 조절용 변수
    private float originalInnerInterval;
    private float originalRiseTime;
    private float originalFireInterval;

    //EnemyUtil을 사용하기 위한 변수
    private Enemy targetEnemy;
    private Vector3 closestEnemyPosition;
    private float closestEnemyDistance;
    private float originalDuration;

    //UI 쿨타임용 변수
    public float currentInterval { get { return fireInterval / originalInnerInterval; } }

    //기획서 : 돌정령 머리위로 투사체 10개 소환은 그냥 뿅 하고 나타나면 되나요
    //이런거 신경써서 몬스터는 화면 밖에서 스폰되게 했으면서 이건 왜 그냥 뿅 하고 나타나나요
    //10개 소환하고 10개가 일괄적으로 몬스터에게 날아가나요 하나씩 10번 날아가나요
    //만약 후자면 쿨타임은 모든 투사체가 발사된 후를 기준으로 하나요, 첫 투사체가 나올때를 기준으로 하나요
    //가장 가까운 몬스터가 뒈짓하면 갈 길 잃은 투사체는 어디로 가야하나요
    //타겟팅이라는 말이 안보이는데 그럼 빗나갈 요소도 존재하나요
    //빗나가면 오브젝트는 언제 사라져야하나요

    //Start에서 Fire코루틴 실행 <- 쿨타임 문제때문에 Update로 실행후 메서드 내부에서 조건 검사
    //썬더랑 비슷하게 코루틴을 돌려서 순차적으로 생성
    //생성만 담당하고 날아가는건 Projectile에서 위로 한번 올라간후 Invoke메서드로 enemy로 날아가도록함

    private void Start()
    {

        fireInterval = GameManager.Instance.player.skillCooltime[0];

        originalInnerInterval = innerInterval;
        originalRiseTime = riseTime;
        originalFireInterval = fireInterval;
        originalDuration = duration;

        closestEnemyDistance = EnemyUtility.GetTargetDistance(transform, out targetEnemy);

        preFireTime = fireInterval * (-1);

        //스킬을 배웠을 때 사거리 안에 적이 있다면 바로 한번 실행
        if (closestEnemyDistance <= GameManager.Instance.player.attackRange) { Fire(); }
    }

    void Update()
    {
        //SetInterval();
        Debug.Log($"Starlight {fireInterval} : {Time.time}");
        Fire();
        //UIManager.Instance.intervalImages[0].fillAmount = currentInterval;
    }

    private void Fire()
    {
        if (preFireTime + fireInterval > Time.time) { return; }

        closestEnemyDistance = EnemyUtility.GetTargetDistance(transform, out targetEnemy);
        Debug.Log($"Starlight 준비됨 {closestEnemyDistance}");
        if (GameManager.Instance.player.attackRange <= closestEnemyDistance) { return; }

        StartCoroutine(FireCoroutine());

        SkillCooltimeManager.Instance.UseSkill(0);

        preFireTime = Time.time;
    }

    private IEnumerator FireCoroutine()
    {
        for (int i = 0; i < starlightCount; i++)
        {
            Debug.Log($"Starlight. {i}번째 코루틴");
            projectileDamage = GameManager.Instance.player.damage * damageMultiplier;
            Skill1_Projectile proj = Instantiate(starlightPrefab, transform.position, transform.rotation);
            proj.projectileDamage = this.projectileDamage;
            proj.projectileSpeed = this.projectileSpeed;
            proj.riseTime = this.riseTime;
            proj.duration = this.duration;
            yield return new WaitForSeconds(innerInterval);
        }
    }

    private void SetInterval()
    {
        if (UIManager.Instance.is2xSpeed == true)
        {
            fireInterval = originalFireInterval / 2;
            riseTime = originalRiseTime / 2;
            innerInterval = originalInnerInterval / 2;
            duration = originalDuration / 2;
        }
        else
        {
            fireInterval = originalFireInterval;
            riseTime = originalRiseTime;
            innerInterval = originalInnerInterval;
            duration = originalDuration; 
        }
    }
}
