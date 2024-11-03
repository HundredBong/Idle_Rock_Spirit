using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //기획서 : 더듬이도 히트박스로 넣었던데 무슨 의미가 있나요 너무 정교한건 좋지 않다고 생각해요
    //공격 범위가 카메라 화면의 3분의 2라고 하셨는데 지금 해상도 2:3 비율에서는 그렇다 치고, 
    //해상도가 달라지면 공격 범위도 달라지나요
    //유니티 좌표같은 정보가 없는 상황에서 1돌정령미터는 정말 적절한 비유라고 생각해요
    //돌정령 기본공격 쿨타임, 체력, 공격력 등 기본 스탯은 어떻게 설정하면 될까요
    //앗 있었네 ㅎㅎ ㅈㅅ ㅋㅋ
    //하지만 더블샷 발동했을때 텀은 없죠?
    //더블샷에 크리 적용되는지 얘기는 없죠?
    //"기본값 1.0 = 1 즉, 1.0s로 1초당 1회" 가독성좀 제발
    //더블샷은 두개를 쏘나요, 두번을 쏘나요 
    //두개를 쏘면 각도는 씨잇펄 내가 어떻게 계산해
    //두번을 쏘면 텀은 어떻게 되나요

    [SerializeField, Header("체력(5)")] internal float health;
    [SerializeField, Header("최대 체력(5)")] internal int maxHealth;
    [SerializeField, Header("체력 재생(0)")] internal float healthRegen;
    [SerializeField, Header("체력 재생 쿨타임(5)")] internal float regenInterval;
    [SerializeField, Header("공격력(1)")] internal float damage;
    [SerializeField, Header("치명타 확률(0)")] internal int critlcalChance;
    [SerializeField, Header("치명타 피해(100)")] internal float criticalMultiplier;
    [SerializeField, Header("공격 속도(1)"), Tooltip("기본값 1.0 = / 1.0s 1초당 1회")] internal float attackInterval;
    [SerializeField, Header("더블 샷 확률(0)")] internal int doubleShot;
    [SerializeField, Header("소지금")] internal int gold;
    [SerializeField, Header("플레이어 공격 반경")] internal float attackRange;
    [SerializeField, Header("스킬 쿨타임(7,5,3,5,20)")] internal float[] skillCooltime;
    //마지막으로 체력을 재생한 시간
    private float preRegenTime;

    private ProjectileLuncher launcher;

    public List<Skill> skills;
    internal List<GameObject> skillObjects;
    //공격할수 있는지 판단하기위한 bool변수
    private bool attackAble;

    //배속 기능 시간 조절용 변수
    internal float originalAttackInterval;
    internal float originalRegenInterval;

    private Enemy targetEnemy = null;
    private float targetDistance = float.MaxValue;

    //공격력 증가용 원본 공격력
    internal float originalDamage;

    private void Awake()
    {
        //다른 객체가 참조할 수 있도록 게임매니저의 플레이어를 오브젝트로 설정
        //if (GameManager.Instance.player != null)
            GameManager.Instance.player = this;
        //else
        //    Debug.Log("게임매니저의 플레이어가 Null상태임");


        //if (UIManager.Instance.player != null)
            UIManager.Instance.player = this;
        //else
        //    Debug.Log("UI매니저의 플레이어가 Null상태임");

        skillCooltime = new float[] { 7, 5, 3, 5, 20 };

    }

    private void Start()
    {
        //원본 쿨타임 저장용
        originalAttackInterval = attackInterval;
        originalRegenInterval = regenInterval;

        originalDamage = damage;


        //런처를 참조할 수 있도록 런처 초기화
        launcher = GameObject.Find("ProjectileLauncher").GetComponent<ProjectileLuncher>();

        StartCoroutine(FireCoroutine());
        Debug.Log($"UI매니저 : {UIManager.Instance.name}");

        skillObjects = new List<GameObject>();

        foreach (Skill skill in skills)
        {
            GameObject skillobj = Instantiate(skill.skillPrefab);
            skillobj.transform.position = transform.position;
            skillobj.transform.SetParent(transform);
            skillobj.SetActive(false);

            skillObjects.Add(skillobj);
        }
    }

    private void Update()
    {
        HealthRegeneration();
        //UI매니저에서 누를때마다 호출할려다가 도저히 안되서 그만
        //SetInterval();

        //if (GameManager.Instance.player == null)
        //    Debug.Log("게임매니저 null");
        //if (UIManager.Instance.player == null)
        //    Debug.Log("UI매니저 null");

        //ActivateSkill(0);
    }

    public void TakeDamage(float damage)
    {
        //인자로 들어온 damage만큼 체력을 감소킴
        health = health - damage;

        UIManager.Instance.PlayerHPRenewal();

        //감소됐을때 체력이 0 이하라면
        if (health <= 0)
        {
            //Death 메서드를 실행함
            Death();
        }
    }

    //체력이 0이하가 됐을때 실행 될 메서드
    private void Death()
    {
        //오브젝트를 비활성화
        //를 하면 게임매니저에서 이거 참조 못하니까 큰일남
        //고로 애니메이션만 재생하는걸로
        //Destroy(gameObject);
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

        if (maxHealth <= health)
            health = maxHealth;

        UIManager.Instance.PlayerHPRenewal();
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

            if (targetDistance <= attackRange)
                attackAble = true;
            else
                attackAble = false;
            Debug.Log($"AttackAble {targetDistance}");
            //Debug.Log($"코루틴 실행됨 {GameManager.Instance.enemies.Count}");

            Debug.Log($"AttackAble : {attackAble}");

            //enemies 리스트에 적이 없다면 아래 코드를 실행하지 않음
            if (GameManager.Instance.enemies.Count != 0)
            {
                if (targetDistance <= attackRange && attackAble == true)
                    launcher.Fire();
            }

            // 1초를 기준으로 1 / 1.1 일시 약 0.9초마다 발사하게됨 
            yield return new WaitForSeconds(1 / attackInterval);
        }

    }

    private void SetInterval()
    {
        if (UIManager.Instance.is2xSpeed == true)
        {
            attackInterval = originalAttackInterval / 2;
            regenInterval = originalRegenInterval / 2;
        }
        else
        {
            attackInterval = originalAttackInterval;
            regenInterval = originalRegenInterval;
        }
    }

    public void ActivateSkill(int i)
    {
        //스킬이 활성화되지 않은 상태라면 스킬을 활성화시켜줌
        //Debug.Log("왜 호출됨 시발");
        if (skillObjects[i].activeSelf == false)
            skillObjects[i].SetActive(true);
    }
}
