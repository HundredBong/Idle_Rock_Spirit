using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class Skill3_Meteor : MonoBehaviour
{
    [SerializeField, Header("��ų�� ����� ������")] private Skill3_Projectile projtilePrefab;

    private float projectileDamage;//����ü �����
    [SerializeField, Header("�⺻���� ��� ����� ����(12)")] private float damageMultiplier;
    [SerializeField, Header("����ü�� ���ư� �ӵ�")] private float projectileSpeed;
    [SerializeField, Header("��ų ��Ÿ��(3)")] private float fireInterval;
    [SerializeField, Header("����ü ���� ��ǥ")] private Vector3 rendererStartPos;

    //public float projectileScale;
    private float preFireTime;


    //EnemyUtil�� ����ϱ� ���� ����
    private Enemy targetEnemy;
    private Vector3 closestEnemyPosition;
    private float closestEnemyDistance;


    //��ȹ�� : �������� �������� ��������Ҵµ� �����̸� Thunder�� ������ ��ġ�� �ʳ���
    //������ ������ Ÿ�����ΰ��� �ƴϸ� ���Ͱ� �ִ� ��ġ�� �����ٴ� ���ΰ���
    //ī�޶� ȭ�� ����̶� �ߴµ� �׷� ���׿��� �����ʿ��� ������ ���� �ֳ���
    //ī�޶� ȭ�� ����̸� ī�޶� ���� ������ �����ǳ��� �׷��� ���׿��ε�
    //���׿��� ���� �������� �ϳ��� 

    private void Start()
    {
        fireInterval = GameManager.Instance.player.skillCooltime[2];
        //StartCoroutine(FireCoroutine());
    }
    private void Update()
    {
        closestEnemyPosition = EnemyUtility.GetTargetPosition(transform, out targetEnemy);

        if (GameManager.Instance.enemies != null)
            gameObject.transform.position = closestEnemyPosition;

        Fire();

    }

    private IEnumerator FireCoroutine()
    {
        while (true)
        {
            Fire();
            yield return new WaitForSeconds(fireInterval);
        }
    }

    private void Fire()
    {
        //��Ÿ�� �纸�� �ȵǸ� ����
        if (preFireTime + fireInterval > Time.time) { return; }

        float dis = Vector3.Distance(transform.position, GameManager.Instance.player.transform.position);

        //��Ÿ�� �ƴµ� ��Ÿ��� �ȵǸ� ����
        if (dis >= GameManager.Instance.player.attackRange)
        {
            return;
        }

        projectileDamage = GameManager.Instance.player.damage * damageMultiplier;

        Skill3_Projectile proj = Instantiate(projtilePrefab);

        proj.damage = this.projectileDamage;
        //�Ÿ� = �ӵ� x �ð�
        //�ð� = �Ÿ� / �ӵ�
        //�ӵ� = �Ÿ� / �ð�
        proj.duration = 1 / projectileSpeed; //�ð� = �ӵ� / �Ÿ�
        proj.rendererStartPos = this.rendererStartPos;
        //proj.transform.localScale = proj.transform.localScale * projectileScale;
        //�θ� ������Ʈ �������� ������ ��ġ���� ����
        proj.transform.localPosition = new Vector2(transform.position.x - 0.5f, transform.position.y);
        proj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(10, 50));

        SkillCooltimeManager.Instance.UseSkill(2);

        preFireTime = Time.time;
    }
}