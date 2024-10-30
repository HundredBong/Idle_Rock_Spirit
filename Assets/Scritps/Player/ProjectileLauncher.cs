using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLuncher : MonoBehaviour
{
    private float fireaAngle;
    public float projectileSpeed;
    [HideInInspector] public int area;
    public Projectile projectile;

    public void Start()
    {
         //yield return null;
        //Fire();
    }
    public void Fire()
    {
        Debug.Log("런처 Fire 메서드 확인용 로그");

        if (area == 0)
        {

        }
        else if (area == 1)
        {

        }
        else
        {

        }

        Projectile proj = Instantiate(projectile, transform.position, transform.rotation);

        proj.transform.position = gameObject.transform.position;
        proj.transform.rotation = gameObject.transform.rotation;

        proj.damage = GameManager.Instance.player.damage;
        proj.projectileSpeed = this.projectileSpeed;
    }
}
