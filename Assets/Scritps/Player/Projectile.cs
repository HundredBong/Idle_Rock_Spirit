using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using UnityEngine;

//����ü�� Rigidbody2D�� �����Ͽ� �߷��� ������ �ް��ؼ� ������ ����
//is Trigger üũ�� ������ �������޴� Ʈ���ŷ� ����

//������ ����ü�� �����̶� ȸ������ �ǹ̰� ������,
//���� �ٸ� ����� ����ü�� �߰��� �� �����Ƿ� ����ü�� ȸ������ ����Ǵ� ������ �ۼ�

public class Projectile : MonoBehaviour
{
    //����ü�� ���� �����, ���ư��� �ӵ�
    //Player�� AttackCoroutine���� ���� ������
    internal float damage;
    internal float projectileSpeed;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.velocity = transform.position * projectileSpeed;
    }

    private void Update()
    {

    }

    //����ü�� �ٸ� ��ü���� ����� �� 
    private void OnTriggerEnter2D(Collider2D other)
    {
        //���� ��ü�� Enemy ������Ʈ�� �������ִ��� �˻��ϰ� ������
        if (other.TryGetComponent(out Enemy enemy))
        {
            //enemy�� ������� �Դ� �޼��带 ������
            enemy.TakeDamage(damage);
        }
    }

}
