using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1_Starlight : MonoBehaviour
{
    [SerializeField, Header("��ų�� ����� ������")] private Skill1_Projectile starlightPrefab;
    [SerializeField, Header("������ ����ü ����(10)")] private int starlightCount;
    private float projectileDamage; //����ü�� �����
    [SerializeField,Header("�⺻���� ��� ����� ����(1.5)")] private float damageMultiplier;
    [SerializeField, Header("����ü�� ���ư� �ӵ�")] private float projectileSpeed;
    [SerializeField, Header("����ü�� ���ư��� ����")] private float innerInterval;
    [SerializeField, Header("����ü�� �ö󰡴� �ð�"),
        Tooltip("��� ������ �غ���; �׸�")] private float riseTime;
    [SerializeField, Header("��ų ��Ÿ��(7)")] private float fireInterval;
    private float preFireTime; //��Ÿ�� ���� ���������� �߻��� �ð�

    //EnemyUtil�� ����ϱ� ���� ����
    private Enemy targetEnemy;
    private Vector3 closestEnemyPosition;
    private float closestEnemyDistance;

    //��ȹ�� : ������ �Ӹ����� ����ü 10�� ��ȯ�� �׳� �� �ϰ� ��Ÿ���� �ǳ���
    //�̷��� �Ű�Ἥ ���ʹ� ȭ�� �ۿ��� �����ǰ� �����鼭 �̰� �� �׳� �� �ϰ� ��Ÿ������
    //10�� ��ȯ�ϰ� 10���� �ϰ������� ���Ϳ��� ���ư����� �ϳ��� 10�� ���ư�����
    //���� ���ڸ� ��Ÿ���� ��� ����ü�� �߻�� �ĸ� �������� �ϳ���, ù ����ü�� ���ö��� �������� �ϳ���
    //���� ����� ���Ͱ� �����ϸ� �� �� ���� ����ü�� ���� �����ϳ���
    //Ÿ�����̶�� ���� �Ⱥ��̴µ� �׷� ������ ��ҵ� �����ϳ���
    //�������� ������Ʈ�� ���� ��������ϳ���

    //Start���� Fire�ڷ�ƾ ���� <- ��Ÿ�� ���������� Update�� ������ �޼��� ���ο��� ���� �˻�
    //����� ����ϰ� �ڷ�ƾ�� ������ ���������� ����
    //������ ����ϰ� ���ư��°� Projectile���� ���� �ѹ� �ö��� Invoke�޼���� enemy�� ���ư�������

    private void Start()
    {
        closestEnemyDistance = EnemyUtility.GetTargetDistance(transform, out targetEnemy);

        //��ų�� ����� �� ��Ÿ� �ȿ� ���� �ִٸ� �ٷ� �ѹ� ����
        if (closestEnemyDistance <= GameManager.Instance.player.attackRange) { Fire(); }
    }

    void Update()
    {
        Debug.Log($"Starlight {fireInterval} : {Time.time}");
        Fire();
    }

    private void Fire()
    {
        if (preFireTime + fireInterval > Time.time) { return; }

        closestEnemyDistance = EnemyUtility.GetTargetDistance(transform, out targetEnemy);
        Debug.Log($"Starlight �غ�� {closestEnemyDistance}");
        if (GameManager.Instance.player.attackRange <= closestEnemyDistance) { return; }
        
        StartCoroutine(FireCoroutine());
        preFireTime = Time.time;
    }

    private IEnumerator FireCoroutine()
    {
        for (int i = 0; i < starlightCount; i++)
        {
            Debug.Log($"Starlight. {i}��° �ڷ�ƾ");
            projectileDamage = GameManager.Instance.player.damage * damageMultiplier;
            Skill1_Projectile proj = Instantiate(starlightPrefab,transform.position, transform.rotation);
            proj.projectileDamage = this.projectileDamage;
            proj.projectileSpeed = this.projectileSpeed;
            proj.riseTime = this.riseTime;
            yield return new WaitForSeconds(innerInterval);
        }
    }
}
