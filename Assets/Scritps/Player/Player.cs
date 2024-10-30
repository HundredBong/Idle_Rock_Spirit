using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField, Header("체력")] public float health;
    [SerializeField, Header("최대 체력")] public int maxHealth;
    [SerializeField, Header("체력 재생")] public float healthRegen;
    [SerializeField, Header("공격력")] public int damage;
    [SerializeField, Header("투사체 속도")] private float projectileSpeed;
    [SerializeField, Header("치명타 확률")] public int critlcalChance;
    [SerializeField, Header("치명타 피해")] public int criticalMultiplier;
    [SerializeField, Header("공격 속도"),Tooltip("기본값 1.0 = / 1.0s 1초당 1회")] public int attackInterval;
    [SerializeField, Header("더블 샷")] public int doubleShot;
    [SerializeField, Header("소지금")] public int gold;
    [Header("공격에 사용할 프리팹")] public Projectile projectilePreafab;

    //체력 재생 쿨타임
    private float regenInterval;

    //마지막으로 체력을 재생한 시간
    private float preRegenTime;

    //투사체를 발사할 영역 설정
    [HideInInspector] public int fireArea;
    ProjectileLuncher launcher;
    private Coroutine attackCoroutine;

    private List<Skill> skill;

    public void Start()
    {
        //다른 객체가 참조할 수 있도록 게임매니저의 플레이어를 오브젝트로 설정
        GameManager.Instance.player = this;

        attackCoroutine = StartCoroutine(AttackCoroutine());

        //체력 재생 관련 변수 초기화
        preRegenTime = 0;
        regenInterval = 5;
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

        //공격 코루틴 중지
        StopCoroutine(attackCoroutine);
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

    private IEnumerator AttackCoroutine()
    {
        //TODO : foreach문으로 리스트를 탐색하고 enemy와의 거리가 일정 값 이하일때만 실행되게 하는 로직 작성하기
        //TODO : Projectile에서 몬스터 방향으로 발사되는 공식 작성하기

        //플레이어 오브젝트가 해야할 일 : 투사체 발사 각도 조절

        //가장 가까운 적을 탐색하기 위한 변수 초기화

        //1. 플레이어와 가장 가까운 적을 탐색함 -> 플레이어가 할 일
        //2. 가장 가까운 적이 있는 영역으로 투사체가 날아갈 각도를 조절함 -> 런처가 할 일
        //2-2. 런처가 해야하는 이유 : 여기서 각도 돌리면 플레이어가 돌아감
        //3. 투사체의 속도를 설정하고 투사체를 발사함 -> 런처가 할 일


        while (true)
        {
            SerchTarget();

            //Enemy에게 발사할 프리팹을 생성함
            Projectile projectile = Instantiate(projectilePreafab);

            //생성된 프리팹의 대미지와 속도를 설정
            projectile.damage = this.damage;
            projectile.projectileSpeed = this.projectileSpeed;

            //공격 쿨타임만큼 대기후 계속 실행
            yield return new WaitForSeconds(attackInterval); 
        }
    }

    private void SerchTarget()
    {
        if (GameManager.Instance.enemies.Count == 0)
        {
            Debug.Log("enemies가 비어있음 (Player.SerchTarget)");
            return;
        }

        Enemy targetEnemy = null;
        float targetDistance = float.MaxValue;

        //게임매니저의 enemy 리스트에서 적을 탐색
        foreach (Enemy enemy in GameManager.Instance.enemies)
        {
            //foreach문을 순회할 때 마다 enemy와 플레이어와의 거리를 측정
            float distance = Vector3.Distance(enemy.transform.position, transform.position);

            //현재 enemy와의 거리가 지정한 거리보다 가까우면
            if (distance < targetDistance)
            {
                //타겟을 설정하고, distance를 초기화
                targetEnemy = enemy;
                targetDistance = distance;
            }
        }
        SetAngle();
    }

    private void SetAngle()
    {

        launcher.FireCoroutine();
    }
}
