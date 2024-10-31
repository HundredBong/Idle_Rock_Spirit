using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2_Projectile : MonoBehaviour
{
    public float damage;
    public float projectileSpeed;
    public float attackInterval;
    private float preAttackTime;

    private CircleCollider2D coll;
    void Start()
    {
        coll = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        transform.Translate(Vector3.right*projectileSpeed*Time.deltaTime);
    }

    private void OnTriggerStay2D(Collider2D other)
    {

        //if (preAttackTime + attackInterval > Time.time) return;
        Debug.Log($"���̵�1 {this.damage}");

        if (other.TryGetComponent(out Enemy enemy))
        {
            Debug.Log($"���̵�21 {this.damage}");

            enemy.TakeDamage(damage);
            preAttackTime = Time.time;
            Debug.Log($"���̵�22 {this.damage}");
        }
    }
}
