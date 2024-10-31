using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField, Header("체력")] public float health;
    [SerializeField, Header("최대 체력")] public int maxHealth;
    [SerializeField, Header("체력 재생")] public float healthRegen;
    [SerializeField, Header("공격력")] public int damage;
    [SerializeField, Header("치명타 확률")] public int critlcalChance;
    [SerializeField, Header("치명타 피해")] public int criticalMultiplier;
    [SerializeField, Header("공격 속도"), Tooltip("기본값 1.0 = / 1.0s 1초당 1회")] public float attackInterval;
    [SerializeField, Header("더블 샷")] public int doubleShot;
    [SerializeField, Header("소지금")] public int gold;

    //체력 재생 쿨타임
    private float regenInterval;

    //마지막으로 체력을 재생한 시간
    private float preRegenTime;

    ProjectileLuncher launcher;

    private List<Skill> skill;

    //공격할수 있는지 판단하기위한 bool변수
    internal bool attackAble;

    private Enemy targetEnemy = null;
    private float targetDistance = float.MaxValue;
    public void Start()
    {
        //다른 객체가 참조할 수 있도록 게임매니저의 플레이어를 오브젝트로 설정
        //런처를 참조할 수 있도록 런처 초기화
        GameManager.Instance.player = this;

        launcher = GameObject.Find("ProjectileLauncher").GetComponent<ProjectileLuncher>();

        //체력 재생 관련 변수 초기화
        preRegenTime = 0;
        regenInterval = 5;
        StartCoroutine(FireCoroutine());

    }

    public void Update()
    {
        HealthRegeneration();
    }

    public void TakeDamage(float damage)
    {
        //인자로 들어온 damage만큼 체력을 감소킴
        health = health - damage;

        //감소됐을때 체력이 0 이하라면
        if (health <= 0)
        {
            //Death 메서드를 실행함
            Death();
        }
    }

    //체력이 0이하가 됐을때 실행 될 메서드
    public void Death()
    {
        //오브젝트를 비활성화
        Destroy(gameObject);
    }

    private void HealthRegeneration()
    {
        //5초마다 체력을 재생할 로직

        //플레이어의 예토전생을 방지하기위해 체력이 0이하라면 아래 코드를 실행하지 않음
        if (health <= 0)
            return;

        //재생간격 + 마지막으로 재생한 시간이 현재 시간보다 크다면 아래 코드를 실행하지않음
        if (regenInterval + preRegenTime > Time.time)
            return;

        //그렇지 않다면 체력 재생을 실행함
        health = health + healthRegen;

        //마지막으로 회복한 시간을 초기화해줌
        //초기화 안하면 매프레임 회복해서 power overwhelming
        preRegenTime = Time.time;
    }

    private float SerchTarget()
    {
        targetEnemy = null;
        targetDistance = float.MaxValue;

        //게임매니저의 enemy 리스트에서 적을 탐색
        foreach (Enemy enemy in GameManager.Instance.enemies)
        {
            //enemmy에 접근했을때 null이면 예외가 발생하므로 null일시 루프를 건너뜀
            if (enemy == null) { continue; }

            //foreach문을 순회할 때 마다 enemy와 플레이어와의 거리를 측정
            float distance = Mathf.Abs(enemy.transform.position.x - transform.position.x);

            //현재 enemy와의 거리가 지정한 거리보다 가까우면
            if (distance < targetDistance)
            {
                //타겟을 설정하고, distance를 초기화
                targetEnemy = enemy;
                targetDistance = distance;
                //Debug.Log($"가장 가까운 적 : {targetEnemy.name}");
                Debug.Log($"가장 가까운 적 : {Mathf.Abs(targetDistance)}");
            }
        }


        return targetDistance;
    }

    private IEnumerator FireCoroutine()
    {

        while (true)
        {
            SerchTarget();

            if (targetDistance <= 4.8f)
                attackAble = true;
            else
                attackAble = false;
            Debug.Log($"AttackAble {targetDistance}");
            //Debug.Log($"코루틴 실행됨 {GameManager.Instance.enemies.Count}");

            Debug.Log($"AttackAble : {attackAble}");

            //enemies 리스트에 적이 없다면 아래 코드를 실행하지 않음
            if (GameManager.Instance.enemies.Count != 0)
            {
                if (targetDistance <= 4.8f && attackAble == true)
                    launcher.Fire();
            }
            //공격 쿨타임동안 대기후 코드 재실행
            yield return new WaitForSeconds(attackInterval);
        }

    }
}
