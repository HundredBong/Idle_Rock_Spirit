using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{

    //기획서 : enemy 크리, 더블, 이속은 테스트하고 결정한다 쳐도 공속은 좀 있었으면 해요

    [SerializeField, Header("체력(1)")] internal float health;
    //[Header("최대 체력(1)")]
    internal float maxHealth;
    [SerializeField, Header("이동 속도")] internal float moveSpeed;
    [SerializeField, Header("공격 속도")] internal float attackInterval;
    //마지막으로 공격한 시간 
    private float preAttackTime;
    [SerializeField, Header("공격력")] internal float damage;
    [SerializeField, Header("도착 지점 X값")] internal float arrivePosX;
    [SerializeField, Header("공격에 사용할 프리팹")] private EnemyProjectile enemyProjectile;

    //목표로 이동할 타겟
    private Transform target;

    //체력바 이미지, 프로퍼티
    [Header("체력바에 사용할 이미지")] public Image healthBar;
    public float healthAmount { get { return health / maxHealth; } }

    //애니메이션 제어용
    private Animator anim;
    private PolygonCollider2D coll;

    //거리 측정용
    private float distance;

    //이속 증감용
    private float enhancedMoveSpeed;
    private float originalMoveSpeed;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<PolygonCollider2D>();
    }

    private IEnumerator Start()
    {

        enhancedMoveSpeed = moveSpeed * 2f;
        originalMoveSpeed = moveSpeed;

        //자기 자신을 리스트에 추가함
        GameManager.Instance.enemies.Add(this);

        //Debug.Log($"Enemy 참조용 : {GameManager.Instance.currentStage}");
        //health += GameManager.Instance.currentStage / 10;
        //maxHealth = health;
        //damage += GameManager.Instance.currentStage;

        //1프레임 유예를 둬서 초기화 에러 방지
        yield return null;
        target = GameManager.Instance.player.transform;

        if (GameManager.Instance.player != null)
            Debug.Log($"Player Name : {target.name} (Enemy.Start)");
        else
            Debug.Log("Player가 Null 상태임 (Enemy.Start)");


    }

    private void Update()
    {
        
        
        
        

        //Debug.Log($"강화 이속 : {enhancedMoveSpeed}");
        //Debug.Log($"원본 이속 : {originalMoveSpeed}");
        
        //플레이어 위치 - 내 위치 = 내가 이동해야 할 방향
        Vector2 targetPos = GameManager.Instance.player.transform.position;
        Vector2 moveDir = new Vector2(targetPos.x - transform.position.x, 0);


        //플레이어와 enemy의 x축의 거리를 측정함
        distance = GameManager.Instance.player.transform.position.x - transform.position.x;
        //Debug.Log($"거리 : {distance}");
        //if (Mathf.Abs(distance) >= GameManager.Instance.player.attackRange)
        if(GameManager.Instance.player.anim.GetBool("isMove") == true)
        {
            moveSpeed = enhancedMoveSpeed;
        }
        else
        {
            moveSpeed = originalMoveSpeed;
        }

        //distance가 도착지점보다 크다면 즉, 아직 도착하지 않았다면
        if (Mathf.Abs(distance) > arrivePosX)
        {
            //플레이어 쪽으로 움직이는 Move 메서드를 실행함
            //정규화되서 방향만 남은 벡터를 인자로 전달함
            Move(moveDir.normalized);
        }

        //도착지점에 도착했다면
        else
        {
            Attack();
        }
    }

    private void Move(Vector2 dir)
    {
        //이동중에는 공격 애니메이션 취소
        if (anim.GetBool("isAttack") == true)
        {
            anim.SetBool("isAttack", false);
        }

        Debug.Log("enemy가 이동중 (Enemy.Move)");
        //Updated에서 구한 방향벡터 * 이동속도 * 속도보간으로 해당 방향으로 이동
        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }

    private void Attack()
    {
        //사망 애니메이션 재생을 위해 체력관련 조건문 추가
        if (preAttackTime + attackInterval > Time.time && 0 <= health)
            { return; }
        
        preAttackTime = Time.time;

        //조건을 만족하면 공격 애니메이션 재생
        anim.SetBool("isAttack", true);

        //자기 자신의 위치에 프리팹을 생성함
        EnemyProjectile enemyProj = Instantiate(enemyProjectile, transform.position, Quaternion.identity);

        //투사체 대미지를 자신의 대미지로 설정
        enemyProj.damage = this.damage;
    }

    public void TakeDamage(float damage)
    {
        //체력을 인자로 들어온 damage만큼 감소시킴
        health = health - damage;

        //감소된 체력만큼 체력바 업데이트
        healthBar.fillAmount = healthAmount;

        //감소시켰을 때 체력이 0이하라면 Death메서드 실행
        if (health <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        moveSpeed = 0;
        enhancedMoveSpeed = 0;
        originalMoveSpeed = 0;
        anim.SetTrigger("Death");
        float animationLength = anim.GetCurrentAnimatorStateInfo(0).length;
        Debug.Log($"애니메이션 재생 {animationLength}");
        coll.enabled = false;
        GameManager.Instance.enemies.Remove(this);
        GameManager.Instance.player.gold += 10;
        UIManager.Instance.PlayerMoneyRenewal();
        UIManager.Instance.PlayerMoneyCheckInUpgreade();
        Destroy(gameObject, animationLength+0.05f);
    }
}
