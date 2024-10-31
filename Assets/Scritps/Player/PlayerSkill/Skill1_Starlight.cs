using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1_Starlight : MonoBehaviour
{
    public GameObject starlightPrefab;
    public int starlightCount;

    public float initalSpeed;
    public float fireSpeed;
    public float innerInterval;
    public float fireInterval;
    private float preFireTime;


    private Enemy targetEnemy;
    private Vector3 closestEnemyPosition;
    //1. ����ü�� ������ -> �÷��̾ ��
    //2. ������ ����ü�� �÷��̾� �� ������ ��ǥ�� ������ (

    //prefab : 
    //3. ���� ����� ���� ��ǥ�� ã��
    //4. �� ��ǥ�� ����ü�� innerInterval �������� �߻���

    //Start���� Fire�ڷ�ƾ ����

    //��ȹ�� : ������ �Ӹ����� ����ü 10�� ��ȯ�� �׳� �� �ϰ� ��Ÿ���� �ǳ���
    //�̷��� �Ű�Ἥ ���ʹ� ȭ�� �ۿ��� �����ǰ� �����鼭 �̰� �� �׳� �� �ϰ� ��Ÿ������
    //10�� ��ȯ�ϰ� 10���� �ϰ������� ���Ϳ��� ���ư����� �ϳ��� 10�� ���ư�����
    //���� ����� ���Ͱ� �����ϸ� �� �� ���� ����ü�� ���� �����ϳ���
    //Ÿ�����̶�� ���� �Ⱥ��̴µ� �׷� ������ ��ҵ� �����ϳ���
    //�������� ������Ʈ�� ���� ��������ϳ���

    //����� ����ϰ� �ڷ�ƾ�� ������ ���������� ����
    //���� �� ����ϰ� ���ư��°� Projectile���� ���� �ѹ�, enemy�� ���ư�����

    private void Start()
    {

    }

    void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (preFireTime + fireInterval > Time.time) { return; }

        StartCoroutine(FireCoroutine());
        preFireTime = Time.time;
    }

    private IEnumerator FireCoroutine()
    {
        for (int i = 0; i < starlightCount; i++)
        {
            Debug.Log($"Starlight. {i}��° �ڷ�ƾ");
            Instantiate(starlightPrefab,transform.position, transform.rotation);
            yield return new WaitForSeconds(innerInterval);
        }
    }

}
