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

    private IEnumerator Start()
    {
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
        //�÷��̾� ��ġ - �� ��ġ = ���� �̵��ؾ� �� ����
        Vector2 targetPos = GameManager.Instance.player.transform.position;
        Vector2 moveDir = new Vector2(targetPos.x - transform.position.x, 0);

        //�÷��̾�� enemy�� x���� �Ÿ��� ������
        float distance =
            GameManager.Instance.player.transform.position.x - transform.position.x;

        // Debug.Log($"Distance : {distance}");

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
            //�÷��̾ �����ϴ� Attack �޼��带 ������
            Attack();
        }
    }

    private void Move(Vector2 dir)
    {
        Debug.Log("enemy�� �̵��� (Enemy.Move)");
        //Updated���� ���� ���⺤�� * �̵��ӵ� * �ӵ��������� �ش� �������� �̵�
        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }

    private void Attack()
    {

        if (preAttackTime + attackInterval > Time.time)
            return;

        //�ڱ� �ڽ��� ��ġ�� �������� ������
        EnemyProjectile enemyProj = Instantiate(enemyProjectile, transform.position, Quaternion.identity);

        //����ü ������� �ڽ��� ������� ����
        enemyProj.damage = this.damage;

        preAttackTime = Time.time;
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
        GameManager.Instance.enemies.Remove(this);
        GameManager.Instance.player.gold += 10;
        UIManager.Instance.PlayerMoneyRenewal();
        Destroy(gameObject);
    }
}
