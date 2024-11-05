using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //��ó���� ������ �����
    //����׿����� ����ȭ
    [SerializeField]internal float damage;
    //��ó���� ������ ����ü �ӵ�
    internal float projectileSpeed;
    //��ġ�� �� �����浹 ����
    private bool hasCollided = false;

    private Rigidbody2D rb;

    [SerializeField, Header("���������� ��ƼŬ")] private ParticleSystem particlePrefabHit;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        //������ �̵��� ���� ������ٵ� ����
        rb.AddForce(transform.up * projectileSpeed, ForceMode2D.Impulse);

        //2���� �ڱ� �ڽ��� ������, ����϶��� 2�ʸ� �����
        Destroy(gameObject,2f);
    }

    //private void OnCollisionEnter2D(Collision2D other)
    //{
    //    if (hasCollided == true) { return; }

    //    if (other.collider.TryGetComponent(out Enemy enemy))
    //    {
    //        hasCollided = true;
    //        //enemy�� TakeDamage �޼��� ������ ����
    //        enemy.TakeDamage(damage);
    //        Destroy(gameObject);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasCollided == true) { return; }

        if (other.TryGetComponent(out Enemy enemy))
        {
            hasCollided = true;

            ParticleSystem par = Instantiate(particlePrefabHit, other.ClosestPoint(other.transform.position), Quaternion.identity);
            par.Play();

            //enemy�� TakeDamage �޼��� ������ ����
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
