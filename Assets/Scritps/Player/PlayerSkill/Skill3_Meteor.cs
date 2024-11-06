using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;
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
        //�뷱���� �ּ� ó��
        fireInterval = GameManager.Instance.player.skillCooltime[2];
    }
    private void Update()
    {
        closestEnemyPosition = EnemyUtility.GetTargetPosition(transform, out targetEnemy);
        closestEnemyDistance = EnemyUtility.GetTargetDistance(transform, out targetEnemy);
        //���׿� �����ʴ� �׻� ���� ����� ���� ã�� �� ��ǥ�� ��ġ��
        gameObject.transform.position = closestEnemyPosition;

        //���� ����� ���� �Ÿ��� �÷��̾��� ��Ÿ����� ũ��
        if (GameManager.Instance.player.attackRange <= closestEnemyDistance)
        {
            //������Ʈ�� ��ġ�� ���� ��Ÿ� ���� ��ġ��Ŵ
            //���׿��� 0,0�� �����Ǵ� ���� �ذ�� 
            transform.position = new Vector2(GameManager.Instance.player.transform.position.x +
                GameManager.Instance.player.attackRange+0.1f, GameManager.Instance.player.transform.position.y);
        }
        

        ////Ȥ�ó� ������ Player�� ���� ��Ÿ� ���Ÿ��� ��ġ��Ŵ
        ////���׿��� 0,0�� �����Ǵ� ���� �ذ�� 
        //if (GameManager.Instance.enemies == null)
        //{
        //    gameObject.transform.position =
        //        new Vector2(GameManager.Instance.player.transform.position.x + GameManager.Instance.player.attackRange,
        //        GameManager.Instance.player.transform.position.y);
        //}

        Fire();

        if (transform.position == Vector3.zero)
        {
            Debug.LogError("���׿� �������� ��ġ�� zero��");
        }

    }

    //private IEnumerator FireCoroutine()
    //{
    //    while (true)
    //    {
    //        Fire();
    //        yield return new WaitForSeconds(fireInterval);
    //    }
    //}

    private void Fire()
    {
        //��Ÿ�� �纸�� �ȵǸ� ����
        if (preFireTime + fireInterval > Time.time) { return; }

        //�÷��̾��� ü���� 0���ϸ� ����
        if (GameManager.Instance.player.health <= 0) { return; }

        float dis = Vector3.Distance(transform.position, GameManager.Instance.player.transform.position);

        //��Ÿ�� �ƴµ� ��Ÿ��� �ȵǸ� ����
        if (dis >= GameManager.Instance.player.attackRange)
        {
            return;
        }

        //������ ����ü�� ������� �÷��̾� ����� * ������ ����
        projectileDamage = GameManager.Instance.player.damage * damageMultiplier;

        Skill3_Projectile proj = Instantiate(projtilePrefab, new Vector2(transform.position.x - 0.5f, transform.position.y),
            Quaternion.Euler(0, 0, Random.Range(10, 50)));

        proj.damage = this.projectileDamage;
        //�Ÿ� = �ӵ� x �ð�
        //�ð� = �Ÿ� / �ӵ�
        //�ӵ� = �Ÿ� / �ð�
        proj.duration = 1 / projectileSpeed; //�ð� = �ӵ� / �Ÿ�  
        proj.rendererStartPos = this.rendererStartPos; //���׿� ���� ��ġ�� ����

        //����ü�� ������ ��ġ�� ������ x�� ��ǥ - 0.5�� ���� �̵� ��ο� ����
        //proj.transform.position = new Vector2(transform.position.x - 0.5f, transform.position.y);

        //z��ǥ�� �������� �Ͽ� �������� ������ �پ��ϰ� ����
        //proj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(10, 50));

        //��Ÿ�� Ÿ�̸� Ȱ��ȭ
        SkillCooltimeManager.Instance.UseSkill(2);

        //���������� ������ �ð� �ʱ�ȭ
        preFireTime = Time.time;
    }
}