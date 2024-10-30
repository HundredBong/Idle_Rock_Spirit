using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField, Header("ü��")] public float health;
    [SerializeField, Header("�ִ� ü��")] public int maxHealth;
    [SerializeField, Header("ü�� ���")] public float healthRegen;
    [SerializeField, Header("���ݷ�")] public int damage;
    [SerializeField, Header("����ü �ӵ�")] private float projectileSpeed;
    [SerializeField, Header("ġ��Ÿ Ȯ��")] public int critlcalChance;
    [SerializeField, Header("ġ��Ÿ ����")] public int criticalMultiplier;
    [SerializeField, Header("���� �ӵ�"),Tooltip("�⺻�� 1.0 = / 1.0s 1�ʴ� 1ȸ")] public int attackInterval;
    [SerializeField, Header("���� ��")] public int doubleShot;
    [SerializeField, Header("������")] public int gold;
    [Header("���ݿ� ����� ������")] public Projectile projectilePreafab;

    //ü�� ��� ��Ÿ��
    private float regenInterval;

    //���������� ü���� ����� �ð�
    private float preRegenTime;

    //����ü�� �߻��� ���� ����
    [HideInInspector] public int fireArea;
    ProjectileLuncher launcher;
    private Coroutine attackCoroutine;

    private List<Skill> skill;

    public void Start()
    {
        //�ٸ� ��ü�� ������ �� �ֵ��� ���ӸŴ����� �÷��̾ ������Ʈ�� ����
        GameManager.Instance.player = this;

        attackCoroutine = StartCoroutine(AttackCoroutine());

        //ü�� ��� ���� ���� �ʱ�ȭ
        preRegenTime = 0;
        regenInterval = 5;
    }

    public void Update()
    {
        


        HealthRegeneration();
    }

    public void TakeDamage(float damage)
    {
        //���ڷ� ���� damage��ŭ ü���� ����Ŵ
        health = health - damage;
        
        //���ҵ����� ü���� 0 ���϶��
        if (health <= 0)
        {
            //Death �޼��带 ������
            Death();
        }
    }

    //ü���� 0���ϰ� ������ ���� �� �޼���
    public void Death()
    {
        //������Ʈ�� ��Ȱ��ȭ
        Destroy(gameObject);

        //���� �ڷ�ƾ ����
        StopCoroutine(attackCoroutine);
    }

    private void HealthRegeneration()
    {
        //5�ʸ��� ü���� ����� ����

        //�÷��̾��� ���������� �����ϱ����� ü���� 0���϶�� �Ʒ� �ڵ带 �������� ����
        if (health <= 0)
            return;

        //������� + ���������� ����� �ð��� ���� �ð����� ũ�ٸ� �Ʒ� �ڵ带 ������������
        if (regenInterval + preRegenTime > Time.time)
             return; 

        //�׷��� �ʴٸ� ü�� ����� ������
        health = health + healthRegen;

        //���������� ȸ���� �ð��� �ʱ�ȭ����
        //�ʱ�ȭ ���ϸ� �������� ȸ���ؼ� power overwhelming
        preRegenTime = Time.time;
    }

    private IEnumerator AttackCoroutine()
    {
        //TODO : foreach������ ����Ʈ�� Ž���ϰ� enemy���� �Ÿ��� ���� �� �����϶��� ����ǰ� �ϴ� ���� �ۼ��ϱ�
        //TODO : Projectile���� ���� �������� �߻�Ǵ� ���� �ۼ��ϱ�

        //�÷��̾� ������Ʈ�� �ؾ��� �� : ����ü �߻� ���� ����

        //���� ����� ���� Ž���ϱ� ���� ���� �ʱ�ȭ

        //1. �÷��̾�� ���� ����� ���� Ž���� -> �÷��̾ �� ��
        //2. ���� ����� ���� �ִ� �������� ����ü�� ���ư� ������ ������ -> ��ó�� �� ��
        //2-2. ��ó�� �ؾ��ϴ� ���� : ���⼭ ���� ������ �÷��̾ ���ư�
        //3. ����ü�� �ӵ��� �����ϰ� ����ü�� �߻��� -> ��ó�� �� ��


        while (true)
        {
            SerchTarget();

            //Enemy���� �߻��� �������� ������
            Projectile projectile = Instantiate(projectilePreafab);

            //������ �������� ������� �ӵ��� ����
            projectile.damage = this.damage;
            projectile.projectileSpeed = this.projectileSpeed;

            //���� ��Ÿ�Ӹ�ŭ ����� ��� ����
            yield return new WaitForSeconds(attackInterval); 
        }
    }

    private void SerchTarget()
    {
        if (GameManager.Instance.enemies.Count == 0)
        {
            Debug.Log("enemies�� ������� (Player.SerchTarget)");
            return;
        }

        Enemy targetEnemy = null;
        float targetDistance = float.MaxValue;

        //���ӸŴ����� enemy ����Ʈ���� ���� Ž��
        foreach (Enemy enemy in GameManager.Instance.enemies)
        {
            //foreach���� ��ȸ�� �� ���� enemy�� �÷��̾���� �Ÿ��� ����
            float distance = Vector3.Distance(enemy.transform.position, transform.position);

            //���� enemy���� �Ÿ��� ������ �Ÿ����� ������
            if (distance < targetDistance)
            {
                //Ÿ���� �����ϰ�, distance�� �ʱ�ȭ
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
