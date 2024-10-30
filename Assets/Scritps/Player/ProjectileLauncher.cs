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
        foreach (Enemy enemy in GameManager.Instance.enemies)
        {
            if (enemy == null) { continue; }
            float distance = Vector3.Distance(enemy.transform.position, transform.position);
            if (distance < targetDistance)
            {
                targetEnemy = enemy;
                targetDistance = distance;
                //Debug.Log($"���� ����� �� : {targetEnemy.name}");
                //Debug.Log($"���� ����� �� : {Mathf.Abs(targetDistance)}");
            }
            Debug.Log($"���� ����� �� : {Mathf.Abs(targetDistance)}");

            projectileSpeed = (1.21f) * distance + (3.29f);
        }
        Projectile proj = Instantiate(projectile, transform.position, transform.rotation);

        proj.transform.position = gameObject.transform.position;
        proj.transform.rotation = gameObject.transform.rotation;

        proj.damage = GameManager.Instance.player.damage;
        proj.projectileSpeed = this.projectileSpeed;
    }
}
