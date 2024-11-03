using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLuncher : MonoBehaviour
{
    private float fireaAngle;
    public float projectileSpeed;
    [HideInInspector] public int area;
    public Projectile projectile;

    private Enemy targetEnemy = null;
    private float targetDistance = float.MaxValue;

    public void Start()
    {

        //yield return null;
        //Fire();
    }
    public void Fire()
    {
        targetEnemy = null;
        targetDistance = float.MaxValue;

        foreach (Enemy enemy in GameManager.Instance.enemies)
        {
            if (enemy == null) { continue; }
            float distance = Mathf.Abs(enemy.transform.position.x - GameManager.Instance.player.transform.position.x);
            if (distance < targetDistance)
            {
                targetEnemy = enemy;
                targetDistance = distance;
                //Debug.Log($"���� ����� �� : {targetEnemy.name}");
                //Debug.Log($"���� ����� �� : {Mathf.Abs(targetDistance)}");
            }
            Debug.Log($"���� ����� �� : {Mathf.Abs(targetDistance)}");

            projectileSpeed = (1.21f) * targetDistance + (3.29f);
        }
        Projectile proj = Instantiate(projectile, transform.position, transform.rotation);

        proj.transform.position = gameObject.transform.position;
        proj.transform.rotation = gameObject.transform.rotation;

        proj.damage = GameManager.Instance.player.damage;

        //Ȯ�������� ������� ����� + (����� * ũ��Ƽ�� ����) �� ����
        if (GameManager.Instance.player.critlcalChance >= Random.Range(0f, 100f))
        {
            proj.damage = GameManager.Instance.player.damage +
                (GameManager.Instance.player.damage * (GameManager.Instance.player.criticalMultiplier / 100));
        }

        proj.projectileSpeed = this.projectileSpeed;

        //Ȯ�������� �ѹ� �� �޼��� ����
        if (GameManager.Instance.player.doubleShot >= Random.Range(0f, 100f))
        {
            StartCoroutine(DoubleShot());
        }
    }

    private IEnumerator DoubleShot()
    {
        yield return new WaitForSeconds(0.2f);

        targetEnemy = null;
        targetDistance = float.MaxValue;

        foreach (Enemy enemy in GameManager.Instance.enemies)
        {
            if (enemy == null) { continue; }
            float distance = Mathf.Abs(enemy.transform.position.x - GameManager.Instance.player.transform.position.x);
            if (distance < targetDistance)
            {
                targetEnemy = enemy;
                targetDistance = distance;
                //Debug.Log($"���� ����� �� : {targetEnemy.name}");
                //Debug.Log($"���� ����� �� : {Mathf.Abs(targetDistance)}");
            }
            Debug.Log($"���� ����� �� : {Mathf.Abs(targetDistance)}");

            projectileSpeed = (1.21f) * targetDistance + (3.29f);
        }
        Projectile proj = Instantiate(projectile, transform.position, transform.rotation);

        proj.transform.position = gameObject.transform.position;
        proj.transform.rotation = gameObject.transform.rotation;

        proj.damage = GameManager.Instance.player.damage;

        //Ȯ�������� ������� ����� + (����� * ũ��Ƽ�� ����) �� ����
        if (GameManager.Instance.player.critlcalChance >= Random.Range(0f, 100f))
        {
            proj.damage = GameManager.Instance.player.damage +
                (GameManager.Instance.player.damage * GameManager.Instance.player.criticalMultiplier / 100);
        }

        proj.projectileSpeed = this.projectileSpeed;
    }


    private float SerchTarget()
    {
        targetEnemy = null;
        targetDistance = float.MaxValue;
        //���ӸŴ����� enemy ����Ʈ���� ���� Ž��
        foreach (Enemy enemy in GameManager.Instance.enemies)
        {
            //enemmy�� ���������� null�̸� ���ܰ� �߻��ϹǷ� null�Ͻ� ������ �ǳʶ�
            if (enemy == null) { continue; }

            //foreach���� ��ȸ�� �� ���� enemy�� �÷��̾���� �Ÿ��� ����
            float distance = Mathf.Abs(enemy.transform.position.x - GameManager.Instance.transform.position.x);

            //���� enemy���� �Ÿ��� ������ �Ÿ����� ������
            if (distance < targetDistance)
            {
                //Ÿ���� �����ϰ�, distance�� �ʱ�ȭ
                targetEnemy = enemy;
                targetDistance = distance;
                //Debug.Log($"���� ����� �� : {targetEnemy.name}");
                //Debug.Log($"���� ����� �� : {Mathf.Abs(targetDistance)}");
            }
        }
        return targetDistance;
    }
}
