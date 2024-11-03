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
                //Debug.Log($"가장 가까운 적 : {targetEnemy.name}");
                //Debug.Log($"가장 가까운 적 : {Mathf.Abs(targetDistance)}");
            }
            Debug.Log($"가장 가까운 적 : {Mathf.Abs(targetDistance)}");

            projectileSpeed = (1.21f) * targetDistance + (3.29f);
        }
        Projectile proj = Instantiate(projectile, transform.position, transform.rotation);

        proj.transform.position = gameObject.transform.position;
        proj.transform.rotation = gameObject.transform.rotation;

        proj.damage = GameManager.Instance.player.damage;

        //확률적으로 대미지를 대미지 + (대미지 * 크리티컬 배율) 로 설정
        if (GameManager.Instance.player.critlcalChance >= Random.Range(0f, 100f))
        {
            proj.damage = GameManager.Instance.player.damage +
                (GameManager.Instance.player.damage * (GameManager.Instance.player.criticalMultiplier / 100));
        }

        proj.projectileSpeed = this.projectileSpeed;

        //확률적으로 한번 더 메서드 실행
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
                //Debug.Log($"가장 가까운 적 : {targetEnemy.name}");
                //Debug.Log($"가장 가까운 적 : {Mathf.Abs(targetDistance)}");
            }
            Debug.Log($"가장 가까운 적 : {Mathf.Abs(targetDistance)}");

            projectileSpeed = (1.21f) * targetDistance + (3.29f);
        }
        Projectile proj = Instantiate(projectile, transform.position, transform.rotation);

        proj.transform.position = gameObject.transform.position;
        proj.transform.rotation = gameObject.transform.rotation;

        proj.damage = GameManager.Instance.player.damage;

        //확률적으로 대미지를 대미지 + (대미지 * 크리티컬 배율) 로 설정
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
        //게임매니저의 enemy 리스트에서 적을 탐색
        foreach (Enemy enemy in GameManager.Instance.enemies)
        {
            //enemmy에 접근했을때 null이면 예외가 발생하므로 null일시 루프를 건너뜀
            if (enemy == null) { continue; }

            //foreach문을 순회할 때 마다 enemy와 플레이어와의 거리를 측정
            float distance = Mathf.Abs(enemy.transform.position.x - GameManager.Instance.transform.position.x);

            //현재 enemy와의 거리가 지정한 거리보다 가까우면
            if (distance < targetDistance)
            {
                //타겟을 설정하고, distance를 초기화
                targetEnemy = enemy;
                targetDistance = distance;
                //Debug.Log($"가장 가까운 적 : {targetEnemy.name}");
                //Debug.Log($"가장 가까운 적 : {Mathf.Abs(targetDistance)}");
            }
        }
        return targetDistance;
    }
}
