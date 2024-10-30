using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //��ó���� ������ �����
    internal float damage;
    //��ó���� ������ ����ü �ӵ�
    internal float projectileSpeed;
    //��ġ�� �� �����浹 ����
    private bool hasCollided = false;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        //������ �̵��� ���� ������ٵ� ����
        rb.AddForce(transform.up * projectileSpeed, ForceMode2D.Impulse);

        //2���� �ڱ� �ڽ��� ������
        Destroy(gameObject,2f);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (hasCollided == true) { return; }

        if (other.collider.TryGetComponent(out Enemy enemy))
        {
            hasCollided = true;
            //enemy�� TakeDamage �޼��� ������ ����
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasCollided == true) { return; }

        if (other.TryGetComponent(out Enemy enemy))
        {
            //enemy�� TakeDamage �޼��� ������ ����
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
