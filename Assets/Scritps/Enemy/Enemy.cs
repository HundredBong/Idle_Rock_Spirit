using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{

    //��ȹ�� : enemy ũ��, ����, �̼��� �׽�Ʈ�ϰ� �����Ѵ� �ĵ� ������ �� �־����� �ؿ�

    [SerializeField, Header("ü��(1)")] internal float health;
    //[Header("�ִ� ü��(1)")]
    internal float maxHealth;
    [SerializeField, Header("�̵� �ӵ�")] internal float moveSpeed;
    [SerializeField, Header("���� �ӵ�")] internal float attackInterval;
    //���������� ������ �ð� 
    private float preAttackTime;
    [SerializeField, Header("���ݷ�")] internal float damage;
    [SerializeField, Header("���� ���� X��")] internal float arrivePosX;
    [SerializeField, Header("���ݿ� ����� ������")] private EnemyProjectile enemyProjectile;

    //��ǥ�� �̵��� Ÿ��
    private Transform target;

    //ü�¹� �̹���, ������Ƽ
    [Header("ü�¹ٿ� ����� �̹���")] public Image healthBar;
    public float healthAmount { get { return health / maxHealth; } }

    //�ִϸ��̼� �����
    private Animator anim;
    private PolygonCollider2D coll;

    //�Ÿ� ������
    private float distance;

    //�̼� ������
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

        //�ڱ� �ڽ��� ����Ʈ�� �߰���
        GameManager.Instance.enemies.Add(this);

        //Debug.Log($"Enemy ������ : {GameManager.Instance.currentStage}");
        //health += GameManager.Instance.currentStage / 10;
        //maxHealth = health;
        //damage += GameManager.Instance.currentStage;

        //1������ ������ �ּ� �ʱ�ȭ ���� ����
        yield return null;
        target = GameManager.Instance.player.transform;

        if (GameManager.Instance.player != null)
            Debug.Log($"Player Name : {target.name} (Enemy.Start)");
        else
            Debug.Log("Player�� Null ������ (Enemy.Start)");


    }

    private void Update()
    {
        
        
        
        

        //Debug.Log($"��ȭ �̼� : {enhancedMoveSpeed}");
        //Debug.Log($"���� �̼� : {originalMoveSpeed}");
        
        //�÷��̾� ��ġ - �� ��ġ = ���� �̵��ؾ� �� ����
        Vector2 targetPos = GameManager.Instance.player.transform.position;
        Vector2 moveDir = new Vector2(targetPos.x - transform.position.x, 0);


        //�÷��̾�� enemy�� x���� �Ÿ��� ������
        distance = GameManager.Instance.player.transform.position.x - transform.position.x;
        //Debug.Log($"�Ÿ� : {distance}");
        //if (Mathf.Abs(distance) >= GameManager.Instance.player.attackRange)
        if(GameManager.Instance.player.anim.GetBool("isMove") == true)
        {
            moveSpeed = enhancedMoveSpeed;
        }
        else
        {
            moveSpeed = originalMoveSpeed;
        }

        //distance�� ������������ ũ�ٸ� ��, ���� �������� �ʾҴٸ�
        if (Mathf.Abs(distance) > arrivePosX)
        {
            //�÷��̾� ������ �����̴� Move �޼��带 ������
            //����ȭ�Ǽ� ���⸸ ���� ���͸� ���ڷ� ������
            Move(moveDir.normalized);
        }

        //���������� �����ߴٸ�
        else
        {
            Attack();
        }
    }

    private void Move(Vector2 dir)
    {
        //�̵��߿��� ���� �ִϸ��̼� ���
        if (anim.GetBool("isAttack") == true)
        {
            anim.SetBool("isAttack", false);
        }

        Debug.Log("enemy�� �̵��� (Enemy.Move)");
        //Updated���� ���� ���⺤�� * �̵��ӵ� * �ӵ��������� �ش� �������� �̵�
        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }

    private void Attack()
    {
        //��� �ִϸ��̼� ����� ���� ü�°��� ���ǹ� �߰�
        if (preAttackTime + attackInterval > Time.time && 0 <= health)
            { return; }
        
        preAttackTime = Time.time;

        //������ �����ϸ� ���� �ִϸ��̼� ���
        anim.SetBool("isAttack", true);

        //�ڱ� �ڽ��� ��ġ�� �������� ������
        EnemyProjectile enemyProj = Instantiate(enemyProjectile, transform.position, Quaternion.identity);

        //����ü ������� �ڽ��� ������� ����
        enemyProj.damage = this.damage;
    }

    public void TakeDamage(float damage)
    {
        //ü���� ���ڷ� ���� damage��ŭ ���ҽ�Ŵ
        health = health - damage;

        //���ҵ� ü�¸�ŭ ü�¹� ������Ʈ
        healthBar.fillAmount = healthAmount;

        //���ҽ����� �� ü���� 0���϶�� Death�޼��� ����
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
        Debug.Log($"�ִϸ��̼� ��� {animationLength}");
        coll.enabled = false;
        GameManager.Instance.enemies.Remove(this);
        GameManager.Instance.player.gold += 10;
        UIManager.Instance.PlayerMoneyRenewal();
        UIManager.Instance.PlayerMoneyCheckInUpgreade();
        Destroy(gameObject, animationLength+0.05f);
    }
}
