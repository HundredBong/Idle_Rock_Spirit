using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField, Header("ü��")] public float health;
    [Header("�ִ� ü��")] private float maxHealth;
    [SerializeField, Header("�̵� �ӵ�")] private float moveSpeed;
    [SerializeField, Header("���ݷ�")] private float damage;
    [SerializeField, Header("���� ���� X��")] private float arrivePosX;

    //��ǥ�� �̵��� Ÿ��
    private Transform target;

    //���ݿ� �ʿ��� ����ü ������
    public EnemyProjectile projectilePrefab;

    private IEnumerator Start()
    {
        //�ڱ� �ڽ��� ����Ʈ�� �߰���
        //TODO: �÷��̾ foreach������ ����Ʈ�� ��ȸ�ϸ� ����� �� Ž��
        GameManager.Instance.enemies.Add(this);

        //�ִ� ü���� ���� ü������ ����
        maxHealth = health;

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
        Debug.Log($"Distance : {distance}");

        //distance�� ������������ ũ�ٸ� ��, ���� �������� �ʾҴٸ�
        if (Mathf.Abs(distance) > arrivePosX)
        {
            //�÷��̾� ������ �����̴� Move �޼��带 ������
            Move(moveDir.normalized);
        }

        //���������� �����ߴٸ�
        else
        {
            //�÷��̾ �����ϴ� Attack �޼��带 ������
            Attack();
        }


        //����ȭ�Ǽ� ���⸸ ���� ���͸� ���ڷ� ������
        
    }

    private void FixedUpdate()
    {


    }

    private void Move(Vector2 dir)
    {
        Debug.Log("enemy�� �̵��� (Enemy.Move)");
        //Updated���� ���� ���⺤�� * �̵��ӵ� * �ӵ��������� �ش� �������� �̵�
        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }

    private void Attack()
    {
        //�ڱ� �ڽ��� ��ġ�� �������� ������
        EnemyProjectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        //����ü ������� �ڽ��� ������� ����
        projectile.damage = this.damage;
    }

    public void TakeDamage(float damage)
    {
        //ü���� ���ڷ� ���� damage��ŭ ���ҽ�Ŵ
        health = health - damage;

        //���ҽ����� �� ü���� 0���϶�� Death�޼��� ����
        if (health <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
