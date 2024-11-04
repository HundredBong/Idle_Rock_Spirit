using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill4_Thunder : MonoBehaviour
{
    [SerializeField, Header("스킬에 사용할 프리팹")] private Skill4_Projectile projtilePrefab;
    [SerializeField, Header("생성할 투사체 개수(8)")] private int thunderCount;
    private float projectileDamage; //투사체의 대미지
    [SerializeField, Header("기본공격 대비 대미지 배율(1)")] private float damageMultiplier;
    [SerializeField, Header("투사체가 떨어지는 속도")] private float projectileSpeed;
    [SerializeField, Header("투사체가 떨어지는 간격")] private float innerInterval;
    [SerializeField, Header("스킬 쿨타임(5)"), Tooltip("번개가 다 떨어지고 나서 쿨타임이 돌게 설정해 놓은" +
        "상태이므로 실제 쿨타임은 쿨타임 + (투사체 간격 * 투사체 개수)")]
    private float fireInterval;
    private float preFireTime; //쿨타임 계산용 마지막으로 발사한 시간

    //EnemyUtil을 사용하기 위한 변수
    private Enemy targetEnemy;
    private Vector3 closestEnemyPosition;

    //시간 관련 노가다네 ㄹㅇ루
    private float originalInnerInterval;
    private float originalFireInterval;


    //스프라이트 렌더러로 사기칠 예정
    //리스트에 있는 enemy중 가장 가까운 enemy를 찾아서 
    //데가리 위에 콜라이더는 작지만 렌더러는 크고 길쭉한걸로 소환하기

    //기획서 : 8번 떨어진다 했는데 떨어질때 딜레이는 얼마나 줘야 하나요
    //딜레이를 줘야하면 벼락이 다 떨어져야 쿨타임이 도나요, 떨어지는 시점부터 쿨타임이 도나요
    //"벼락은 8번 떨어지면 순차적으로 타격함(오타아님)" 이라는게 8개가 순차적으로 가장 가까운 enemy에게 떨어진다는 뜻인가요
    //아니면 가까운 순서대로 8마리까지 맞추는건가요
    //전자라면 가장 가까운 몬스터가 죽으면 그 위치에 계속 벼락을 꽂나요 아니면 다음 적을 타겟팅하나요
    //후자라면 enemy가 4마리 있으면 4개만 꽂으면 되나요 
    //예시 이미지는 사선으로 치던데 꽂히는 각도가 어떻게 되나요
    //벼락 오브젝트는 언제 없어져야 하나요


    //여기서 할 일 : 번개를 가장 가까운 enemy에게 소환함
    //for문 안에서 소환할 번개 개수만큼 SerchEnemy 돌리고 가까운 enemy 위에 소환
    //소환된 번개 projectile은 Start메서드에서 빠르게 아래로 내려감

    private IEnumerator Start()
    {
        yield return null;

        originalInnerInterval = innerInterval;
        originalFireInterval = fireInterval;

        preFireTime = fireInterval * (-1);
        //projectileDamage = GameManager.Instance.player.damage * damageMultiplier;
        fireInterval = GameManager.Instance.player.skillCooltime[3];

        //거리와 관계 없는 스킬이므로 스킬을 배웠을 때 바로 한번 실행
        //Fire();
    }

    void Update()
    {
        //가장 가까운 적을 찾음
        if (GameManager.Instance.enemies != null)
            closestEnemyPosition = EnemyUtility.GetTargetPosition(transform, out targetEnemy);

        //SetInterval();
        Fire();
    }

    private void Fire()
    {
        if (preFireTime + fireInterval > Time.time) { return; }

        Debug.Log($"Thunder.Fire메서드 실행됨");

        StartCoroutine(FireCoroutine());

        //SkillCooltimeManager.Instance.UseSkill(3);

        preFireTime = Time.time;

    }

    private IEnumerator FireCoroutine()
    {
        SkillCooltimeManager.Instance.UseSkill(3);

        for (int i = 0; i < thunderCount; i++)
        {
            projectileDamage = GameManager.Instance.player.damage * damageMultiplier;

            Debug.Log($"Thunder.Coroutine {i}번째 루프");

            closestEnemyPosition = EnemyUtility.GetTargetPosition(transform, out targetEnemy);
            Skill4_Projectile proj = Instantiate(projtilePrefab,
                new Vector3(closestEnemyPosition.x, closestEnemyPosition.y + 4, 0), Quaternion.identity);

            proj.projectileDamage = this.projectileDamage;
            proj.projectileSpeed = this.projectileSpeed;
            yield return new WaitForSeconds(innerInterval);
        }
        //yield return new WaitForSeconds(thunderInterval + (thunderCount * innerInterval));
    }
    private void SetInterval()
    {
        if (UIManager.Instance.is2xSpeed == true)
        {
            innerInterval = originalInnerInterval / 2;
            fireInterval = originalFireInterval / 2;
        }
        else
        {
            innerInterval = originalInnerInterval;
            fireInterval = originalFireInterval;
        }
    }
}
