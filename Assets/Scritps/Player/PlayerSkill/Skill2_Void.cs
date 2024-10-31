using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2_Void : MonoBehaviour
{
    [SerializeField, Header("��ų�� ����� ������")] private Skill2_Projectile skill2_Projectile;
    private float projectileDamage; //����ü�� �����
    [SerializeField, Header("�⺻���� ��� ����� ����(1.2)")] private float damageMultiplier;
    [SerializeField, Header("����ü�� ���ư� �ӵ�")] private float projectileSpeed;
    [SerializeField, Header("����ü�� ���� ����")] private float attackInterval;
    [SerializeField, Header("����ü�� ���� Ƚ��(10)")] private int attackCount;
    [SerializeField, Header("��ų ��Ÿ��(5)")] private float fireInterval;
    [SerializeField, Header("��ų ���ӽð�)"),
        Tooltip("����ü �ӵ��� �°� ������ ����")] private float projectileDuration;
    private float preFireTime; //��Ÿ�� ���� ���������� �߻��� �ð� 

    //EnemyUtil�� ����ϱ� ���� ����
    private Enemy targetEnemy;
    private Vector3 closestEnemyPosition;
    private float closestEnemyDistance;

    //��ȹ�� : õõ���� ������� õõ�� �̵��ϴ°ǰ���
    //�̵��ϴ� ������Ʈ�� �÷��̾� ����ü, enemy, ��ų 1,2,3,4 �� �ִµ� ������ �������� �������
    //�̵��ϸ� ������ �� �� ���ظ� �ֳ��� �ƴϸ� ��� ���� ��� ���ظ� �ֳ���
    //���� 10ȸ�� ���ظ� ���ָ� ������Ʈ�� ���� ���������ϳ���
    //��� ���� ��� ���ظ� �ָ� ���ظ� �ִ� ���� ��� �ǳ���

    private void Start()
    {
        if (closestEnemyDistance <= GameManager.Instance.player.attackRange) { Fire(); }
    }

    private void Update()
    {
        Fire();
    }

    private void Fire()
    {
        //��Ÿ�� �纸�� �ȵǸ� ����
        if (preFireTime + fireInterval > Time.time) { return; }
        //��Ÿ�� �ƴµ� ��Ÿ��� �ȵǸ� ����
        closestEnemyDistance = EnemyUtility.GetTargetDistance(transform, out targetEnemy);
        if (GameManager.Instance.player.attackRange <= closestEnemyDistance) { return; }

        projectileDamage = GameManager.Instance.player.damage * damageMultiplier;

        Skill2_Projectile proj = Instantiate(skill2_Projectile, transform.position, Quaternion.identity);

        proj.projectileDamage = this.projectileDamage;
        proj.projectileSpeed = this.projectileSpeed;
        proj.attackInterval = this.attackInterval;
        proj.attackInterval = this.attackInterval;
        proj.attackCount = this.attackCount;
        proj.projectileDuration = this.projectileDuration;
        preFireTime = Time.time;
    }
}
