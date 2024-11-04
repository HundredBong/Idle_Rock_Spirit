using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2_Projectile : MonoBehaviour
{
    internal float projectileDamage; //����ü �����
    internal float projectileSpeed; //����ü �̵��ӵ�
    internal float attackInterval; //����ü ���� ����
    private float preAttackTime; //���� ���� ���� ���������� ������ �ð� 
    internal int attackCount; //����ü ���� Ƚ��
    internal float projectileDuration; //����ü ���� �ð�

    //�������� ĸ�� �ݶ��̴�
    private CircleCollider2D coll;

    [SerializeField, Header("���������� ��ƼŬ")] private ParticleSystem particlePrefabHit;
    [SerializeField, Header("�����ɶ� ��ƼŬ")] private ParticleSystem particlePrefabSpawn;

    private void Awake()
    {
        coll = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        Destroy(gameObject, projectileDuration);
        ParticleSystem spawnPar = Instantiate(particlePrefabSpawn, transform);
        spawnPar.Play();
    }

    private void Update()
    {
        transform.Translate(Vector3.right * projectileSpeed * Time.deltaTime);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (preAttackTime + attackInterval > Time.time) { return; }

        Collider2D[] contactedEnemies = Physics2D.OverlapCircleAll(transform.position, coll.radius);
        foreach (Collider2D ContactedEnemy in contactedEnemies)
        {
            if (ContactedEnemy.TryGetComponent(out Enemy enemy))
            {
                ParticleSystem hitPar = Instantiate(particlePrefabHit, other.ClosestPoint(other.transform.position),
                    Quaternion.identity);

                hitPar.Play();

                enemy.TakeDamage(projectileDamage);
                attackCount--;
                if (attackCount <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
        preAttackTime = Time.time;
    }
}
