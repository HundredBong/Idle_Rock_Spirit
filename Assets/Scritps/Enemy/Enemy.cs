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

    private IEnumerator Start()
    {
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
        //플레이어 위치 - 내 위치 = 내가 이동해야 할 방향
        Vector2 targetPos = GameManager.Instance.player.transform.position;
        Vector2 moveDir = new Vector2(targetPos.x - transform.position.x, 0);

        //플레이어와 enemy의 x축의 거리를 측정함
        float distance =
            GameManager.Instance.player.transform.position.x - transform.position.x;

        // Debug.Log($"Distance : {distance}");

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
            //플레이어를 공격하는 Attack 메서드를 실행함
            Attack();
        }
    }

    private void Move(Vector2 dir)
    {
        Debug.Log("enemy가 이동중 (Enemy.Move)");
        //Updated에서 구한 방향벡터 * 이동속도 * 속도보간으로 해당 방향으로 이동
        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }

    private void Attack()
    {

        if (preAttackTime + attackInterval > Time.time)
            return;

        //자기 자신의 위치에 프리팹을 생성함
        EnemyProjectile enemyProj = Instantiate(enemyProjectile, transform.position, Quaternion.identity);

        //투사체 대미지를 자신의 대미지로 설정
        enemyProj.damage = this.damage;

        preAttackTime = Time.time;
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
        GameManager.Instance.enemies.Remove(this);
        GameManager.Instance.player.gold += 10;
        UIManager.Instance.PlayerMoneyRenewal();
        Destroy(gameObject);
    }
}
