using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill4_Thunder : MonoBehaviour
{
    private Enemy targetEnemy;
    public int thunderCount;
    public Skill4_Projectile projtilePrefab;

    public float projectileDamage;
    public float projectileSpeed;
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
    private Vector3 closestEnemyPosition;
    private float distance;

    public float thunderInterval;
    private float preThunderTime;
    public float innerInterval;



    void Start()
    {
    }

    void Update()
    {
        //���� ����� ���� ã��
        if (GameManager.Instance.enemies != null)
            closestEnemyPosition = EnemyUtility.SearchTargetPosition(transform, out targetEnemy);

        Fire();
        Debug.Log($"Thunder.preThunderTime : {preThunderTime}");
    }

    private void Fire()
    {
        if (preThunderTime + (thunderInterval + (thunderCount * innerInterval)) > Time.time) { return; }

        Debug.Log($"Thunder.Fire�޼��� �����");

        StartCoroutine(FireCoroutine());
        preThunderTime = Time.time;

    }

    private IEnumerator FireCoroutine()
    {
        for (int i = 0; i < thunderCount; i++)
        {
            Debug.Log($"Thunder.Coroutine {i}��° ����");

            closestEnemyPosition = EnemyUtility.SearchTargetPosition(transform, out targetEnemy);
            Skill4_Projectile proj = Instantiate(projtilePrefab,
                new Vector3(closestEnemyPosition.x, closestEnemyPosition.y + 4, 0), Quaternion.identity);

            proj.projectileDamage = this.projectileDamage;
            proj.projectileSpeed = this.projectileSpeed;
            yield return new WaitForSeconds(innerInterval);
        }
        //yield return new WaitForSeconds(thunderInterval + (thunderCount * innerInterval));

    }
}
