using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill4_Projectile : MonoBehaviour
{
    //��ų�� �� ���� �������� �����ǰ� ������ �����ϴ� �����̴ϱ�, 
    //������ ������ ���ǵ�, ���� �����
    //����

    public float projectileDamage;
    public float projectileSpeed;

    [SerializeField, Header("���������� ��ƼŬ")] private ParticleSystem particlePrefabHit;

    //��ġ�� �� �����浹 ����
    private bool hasCollided = false;

    private void Start()
    {
        //����ü �ӵ��� ���ϰ� ���� ������ ���� ������ Ȥ�� �𸣴� 2�ʵ� ����
        Destroy(gameObject, 2f);
    }

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
            ParticleSystem parHit =Instantiate(particlePrefabHit,other.ClosestPoint(other.transform.position),Quaternion.identity);
            parHit.Play();
            Debug.Log("ThundeProj. OnTriggerEnter2D ���ǹ� ����");
            enemy.TakeDamage(projectileDamage);
            Destroy(gameObject);
        }
    }
}
