using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    public float damage;

    //����ü�� �߻������� ��ǻ� �������ݿ뵵�� ���� �ӵ��� Private���� ����
    public float projectileSpeed;

    private void Start()
    {
        //target = GameManager.Instance.player.transform;
        projectileSpeed = 1f;
        //Ȥ�� Ÿ�ٿ��� �������� �ʾ��� ��츦 ����Ͽ� 1�ʵ� �ڱ� �ڽ��� �����ϴ� �ڵ� �߰�
        Destroy(gameObject, 1f);
    }

    void Update()
    {
        //Ÿ���� ���ٸ� �Ʒ� �ڵ带 �������� ����
        if (GameManager.Instance.player == null) return;

        //Ÿ�� ��ġ - �� ��ġ = ���� ������ ����
        Vector2 fireDir = GameManager.Instance.player.transform.position - gameObject.transform.position;
        Vector2 nomalizedFireDir = fireDir.normalized;


        //Ÿ�� ��ġ�� �̵���
        transform.Translate(nomalizedFireDir * Time.deltaTime * projectileSpeed);
    }

    //Ÿ�ٿ� ������� ó���� �ڵ�
    private void OnTriggerEnter2D(Collider2D other)
    {
        //�÷��̾�� �����ϸ�
        if (other.TryGetComponent(out Player player))
        {
            //�÷��̾��� TakeDamage�޼��� �����ϰ�
            player.TakeDamage(damage);
            
            //�ڱ� �ڽ��� ������
            Destroy(gameObject);
        }
    }
}
