using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill4_Thunder : MonoBehaviour
{
    [SerializeField, Header("��ų�� ����� ������")] private Skill4_Projectile projtilePrefab;
    [SerializeField, Header("������ ����ü ����(8)")] private int thunderCount;
    private float projectileDamage; //����ü�� �����
    [SerializeField, Header("�⺻���� ��� ����� ����(1)")] private float damageMultiplier;
    [SerializeField, Header("����ü�� �������� �ӵ�")] private float projectileSpeed;
    [SerializeField, Header("����ü�� �������� ����")] private float innerInterval;
    [SerializeField, Header("��ų ��Ÿ��(5)"),Tooltip("������ �� �������� ���� ��Ÿ���� ���� ������ ����" +
        "�����̹Ƿ� ���� ��Ÿ���� ��Ÿ�� + (����ü ���� * ����ü ����)")] private float fireInterval;
    private float preFireTime; //��Ÿ�� ���� ���������� �߻��� �ð�

    //EnemyUtil�� ����ϱ� ���� ����
    private Enemy targetEnemy;
    private Vector3 closestEnemyPosition;


    //��������Ʈ �������� ���ĥ ����
    //����Ʈ�� �ִ� enemy�� ���� ����� enemy�� ã�Ƽ� 
    //������ ���� �ݶ��̴��� ������ �������� ũ�� �����Ѱɷ� ��ȯ�ϱ�

    //��ȹ�� : 8�� �������� �ߴµ� �������� �����̴� �󸶳� ��� �ϳ���
    //�����̸� ����ϸ� ������ �� �������� ��Ÿ���� ������, �������� �������� ��Ÿ���� ������
    //"������ 8�� �������� ���������� Ÿ����(��Ÿ�ƴ�)" �̶�°� 8���� ���������� ���� ����� enemy���� �������ٴ� ���ΰ���
    //�ƴϸ� ����� ������� 8�������� ���ߴ°ǰ���
    //���ڶ�� ���� ����� ���Ͱ� ������ �� ��ġ�� ��� ������ �ȳ��� �ƴϸ� ���� ���� Ÿ�����ϳ���
    //���ڶ�� enemy�� 4���� ������ 4���� ������ �ǳ��� 
    //���� �̹����� �缱���� ġ���� ������ ������ ��� �ǳ���
    //���� ������Ʈ�� ���� �������� �ϳ���


    //���⼭ �� �� : ������ ���� ����� enemy���� ��ȯ��
    //for�� �ȿ��� ��ȯ�� ���� ������ŭ SerchEnemy ������ ����� enemy ���� ��ȯ
    //��ȯ�� ���� projectile�� Start�޼��忡�� ������ �Ʒ��� ������

    void Start()
    {
        //projectileDamage = GameManager.Instance.player.damage * damageMultiplier;

        //�Ÿ��� ���� ���� ��ų�̹Ƿ� ��ų�� ����� �� �ٷ� �ѹ� ����
        Fire();
    }

    void Update()
    {
        //���� ����� ���� ã��
        if (GameManager.Instance.enemies != null)
            closestEnemyPosition = EnemyUtility.GetTargetPosition(transform, out targetEnemy);

        Fire();

    }

    private void Fire()
    {
        if (preFireTime + (fireInterval + (thunderCount * innerInterval)) > Time.time) { return; }

        Debug.Log($"Thunder.Fire�޼��� �����");

        StartCoroutine(FireCoroutine());
        preFireTime = Time.time;

    }

    private IEnumerator FireCoroutine()
    {
        for (int i = 0; i < thunderCount; i++)
        {
            projectileDamage = GameManager.Instance.player.damage * damageMultiplier;

            Debug.Log($"Thunder.Coroutine {i}��° ����");

            closestEnemyPosition = EnemyUtility.GetTargetPosition(transform, out targetEnemy);
            Skill4_Projectile proj = Instantiate(projtilePrefab,
                new Vector3(closestEnemyPosition.x, closestEnemyPosition.y + 4, 0), Quaternion.identity);

            proj.projectileDamage = this.projectileDamage;
            proj.projectileSpeed = this.projectileSpeed;
            yield return new WaitForSeconds(innerInterval);
        }
        //yield return new WaitForSeconds(thunderInterval + (thunderCount * innerInterval));

    }
}
