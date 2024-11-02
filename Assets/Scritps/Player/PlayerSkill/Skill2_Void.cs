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

    //��ӱ�� �ð� ������ ����
    private float originalAttackInterval;
    private float originalFireInterval;
    private float originalProjectileDuration;

    //ProjectileLauncher�� ������ �������� ���� ����
    private Transform projectileLauncherPos;

    //��ȹ�� : õõ���� ������� õõ�� �̵��ϴ°ǰ���
    //�̵��ϴ� ������Ʈ�� �÷��̾� ����ü, enemy, ��ų 1,2,3,4 �� �ִµ� ������ �������� �������
    //�̵��ϸ� ������ �� �� ���ظ� �ֳ��� �ƴϸ� ��� ���� ��� ���ظ� �ֳ���
    //���� 10ȸ�� ���ظ� ���ָ� ������Ʈ�� ���� ���������ϳ���
    //��� ���� ��� ���ظ� �ָ� ���ظ� �ִ� ���� ��� �ǳ���


    private void Start()
    {
        originalAttackInterval = attackInterval;
        originalFireInterval = fireInterval;
        originalProjectileDuration = projectileDuration;

        preFireTime = fireInterval * (-1);

        //���̵�� ����ü�� �����Ǵ� ������ �����Ǿ��ϴ� ��ġ�� ProjectileLauncher�� �����ϱ����� Find�޼��� �̿�
        projectileLauncherPos = GameObject.Find("ProjectileLauncher").GetComponent<Transform>();
        transform.position = projectileLauncherPos.position;
        
        if (closestEnemyDistance <= GameManager.Instance.player.attackRange) { Fire(); }
    }

    private void Update()
    {
        SetInterval();
        Fire();
    }

    private void Fire()
    {
        //��Ÿ�� �纸�� �ȵǸ� ����
        if (preFireTime + fireInterval > Time.time) { return; }
        //��Ÿ�� �ƴµ� ��Ÿ��� �ȵǸ� ����
        closestEnemyDistance = EnemyUtility.GetTargetDistance(transform, out targetEnemy);
        
        //projectileLauncher���� �����Ƿ� �÷��̾� ��Ÿ��� �ƴ� �÷��̾� ��Ÿ� - ��ó ������ x��ǥ��ŭ �� �� ��Ÿ� ���

        if (GameManager.Instance.player.attackRange-Mathf.Abs
            (GameManager.Instance.player.transform.position.x - 
                gameObject.transform.position.x) <= closestEnemyDistance)
        { return; }

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

    private void SetInterval()
    {
        if (UIManager.Instance.is2xSpeed == true)
        {
            attackInterval = originalAttackInterval / 2;
            fireInterval = originalFireInterval / 2;
            originalProjectileDuration = projectileDuration / 2;
        }
        else
        {
            attackInterval = originalAttackInterval;
            fireInterval = originalFireInterval;
            originalProjectileDuration = projectileDuration;
        }
    }
}
