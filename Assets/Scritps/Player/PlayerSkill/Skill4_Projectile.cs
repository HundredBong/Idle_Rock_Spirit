using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill4_Projectile : MonoBehaviour
{
    //��ų�� �� ���� �������� �����ǰ� ������ �����ϴ� �����̴ϱ�, 
    //������ ������ ���ǵ�, ���� �����
    //����
    //���̸� ���׿��� �Ȱ��� ������ �����ϵ� �̵� �ӵ��� ��ģ���� ������ �� ���� ����

    public float projectileDamage;
    public float projectileSpeed;

    //��ġ�� �� �����浹 ����
    private bool hasCollided = false;

    private void Update()
    {
        gameObject.transform.Translate(Vector3.down * projectileSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("ThundeProj. OnTriggerEnter2D ����");
        if (hasCollided == true) { return; }
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            hasCollided = true;
            Debug.Log("ThundeProj. OnTriggerEnter2D ���ǹ� ����");
            enemy.TakeDamage(projectileDamage);
            Destroy(gameObject);
        }
    }
}
