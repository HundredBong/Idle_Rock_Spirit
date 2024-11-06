using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Skill1_Projectile : MonoBehaviour
{
    public float projectileDamage; //����ü �����
    internal float projectileSpeed; //����ü �̵��ӵ�
    internal float riseTime; //����ü�� �ö󰡴� �ð�
    internal float duration; //����ü ���� �ð�
    private Vector2 riseDir; //ó���� ���� �ö� ����


    [SerializeField, Header("���������� ��ƼŬ")] private ParticleSystem particlePrefabHit;
    [SerializeField, Header("�����ɶ� ��ƼŬ")] private ParticleSystem particlePrefabSpawn;

    //EnemyUtil�� ����ϱ� ���� ����
    private Enemy targetEnemy;
    private Vector3 closestEnemyPosition;

    //��ġ�� �� �����浹 ����
    private bool hasCollided = false;

    //Start�� �ö󰡰� �ϴ� �뵵�� ������ ����
    private bool isRise;

    private void Start()
    {
        isRise = true;
        riseDir = new Vector2(Random.Range(-1.5f, 0.5f), Random.Range(0.3f, 1.5f));
        Destroy(gameObject, riseTime + duration);
        ParticleSystem spawnPar = Instantiate(particlePrefabSpawn, transform);
        spawnPar.Play();
    }

    private void Update()
    {
        //riseDir = new Vector2(Random.Range(-2f, 2f), Random.Range(0.3f, 2f));
        //������Ʈ���� ���� ������ �ö󰥶� ��� ������ �ٲ�Ƿ� Start���� �ѹ��� ����
        if (isRise == true)
        {
            transform.Translate(riseDir * (projectileSpeed / 15) * Time.deltaTime);
            Invoke("SetRise", riseTime);
            //Update�� �� ������ ���� ����ϴٰ� Invoke �޼���� ���� bool���� �������� �Ʒ� �ڵ� ����
        }
        else
        {
            //���� ����� ���� ��ġ�� ã��, �� ���� ������ ���ڷ� �Ѱ���
            closestEnemyPosition = EnemyUtility.GetTargetPosition(transform, out targetEnemy);
            Move(closestEnemyPosition);
        }
    }

    private void Move(Vector2 dir)
    {
        Vector2 moveDir = new Vector2(dir.x - transform.position.x, dir.y - transform.position.y);
        Vector2 nomalizedMoveDir = moveDir.normalized;
        transform.Translate(nomalizedMoveDir * projectileSpeed * Time.deltaTime);
    }

    private void SetRise()
    {
        isRise = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasCollided == true) { return; }

        if (other.TryGetComponent(out Enemy enemy))
        {
            hasCollided = true;

            ParticleSystem par = Instantiate(particlePrefabHit, other.ClosestPoint(other.transform.position), Quaternion.identity);
            par.Play();
            //Destroy(par, 0.4f);
            enemy.TakeDamage(projectileDamage);
            Destroy(gameObject);
        }
    }

}

