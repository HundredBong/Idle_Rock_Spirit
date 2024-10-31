using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill3_Meteor : MonoBehaviour
{
    public Skill3_Projectile projtilePrefab;
    private Enemy targetEnemy;

    public float damage;
    public float projectileSpeed;
    public float projectileScale;
    public float shotInterval;

    //��ȹ�� : �������� �������� ��������Ҵµ� �����̸� Thunder�� ������ ��ġ�� �ʳ���
    //������ ������ Ÿ�����ΰ��� �ƴϸ� ���Ͱ� �ִ� ��ġ�� �����ٴ� ���ΰ���
    //ī�޶� ȭ�� ����̶� �ߴµ� �׷� ���׿��� �����ʿ��� ������ ���� �ֳ���
    //ī�޶� ȭ�� ����̸� ī�޶� ���� ������ �����ǳ��� �׷��� ���׿��ε�
    //���׿��� ���� �������� �ϳ��� 

    private void Start()
    {
        StartCoroutine(FireCoroutine());
    }
    private void Update()
    {
        Vector3 closestEnemyPosition = EnemyUtility.GetTargetPosition(transform, out targetEnemy);
        if(GameManager.Instance.enemies != null)
            gameObject.transform.position = closestEnemyPosition;
    }
    private IEnumerator FireCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(shotInterval);
            Fire();
        }
    }

    private void Fire()
    {
        //Vector2 randomPos = Random.insideUnitCircle * MaxDist;

        Skill3_Projectile proj = Instantiate(projtilePrefab);

        proj.damage = this.damage;
        //�Ÿ� = �ӵ� x �ð�
        //�ð� = �Ÿ� / �ӵ�
        //�ӵ� = �Ÿ� / �ð�
        proj.duration = 1 / projectileSpeed; //�ð� = �ӵ� / �Ÿ�
        proj.transform.localScale = proj.transform.localScale * projectileScale;
        //�θ� ������Ʈ �������� ������ ��ġ���� ����
        proj.transform.localPosition = new Vector2(transform.position.x - 0.5f, transform.position.y);
        proj.transform.rotation = Quaternion.Euler(0, 0, Random.Range(10, 50));
    }
}