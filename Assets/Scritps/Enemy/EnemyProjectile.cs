using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    public float damage;

    //����ü�� �߻������� ��ǻ� �������ݿ뵵�� ���� �ӵ��� Private���� ����
    private float projectileSpeed;

    public Transform target;

    private void Start()
    {
        projectileSpeed = 10f;
        //Ȥ�� Ÿ�ٿ��� �������� �ʾ��� ��츦 ����Ͽ� 1�ʵ� �ڱ� �ڽ��� �����ϴ� �ڵ� �߰�
        Destroy(gameObject, 1f);
    }

    void Update()
    {
        //Ÿ���� ���ٸ� �Ʒ� �ڵ带 �������� ����
        if (target == null) return;

        //Ÿ�� ��ġ - �� ��ġ = ���� ������ ����
        Vector2 fireDir = target.transform.position - gameObject.transform.position;
        Vector2 nomalizedFireDir = fireDir.normalized;


        //Ÿ�� ��ġ�� �̵���
        transform.Translate(nomalizedFireDir * Time.deltaTime * projectileSpeed);
    }

    //Ÿ�ٿ� ������� ó���� �ڵ�
    private void OnTriggerEnter2D(Collider2D other)
    {
        //�÷��̾�� �����ϸ�
        if (other.CompareTag("Player"))
        {
            //�÷��̾��� TakeDamage�޼��� �����ϰ�
            other.GetComponent<Player>().TakeDamage(damage);
            
            //�ڱ� �ڽ��� ������
            Destroy(gameObject);
        }
    }
}
