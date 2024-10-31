using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Skill1_Projectile : MonoBehaviour
{
    public float damage = 10;//데미지
    public float moveSpeed = 5;//이동속도
    public float duration = 3;//지속시간
    private Vector2 riseDir;

    private Enemy targetEnemy;
    private Vector3 closestEnemyPosition;

    private bool isRise;
    void Start()
    {
        isRise = true;
        //riseDir = new Vector2(Random.Range(-1f, 1f), Random.Range(0.3f, 1f));
    }

    void Update()
    {
        riseDir = new Vector2(Random.Range(-2f, 2f), Random.Range(0.3f, 2f));

        if (isRise == true)
        {
            transform.Translate(riseDir * (moveSpeed / 5) * Time.deltaTime);
            Invoke("SetRise", 1f);
        }
        else
        {
            closestEnemyPosition = EnemyUtility.SearchTargetPosition(transform, out targetEnemy);
            Move(closestEnemyPosition);
        }

    }

    public void Move(Vector2 dir)
    {
        Vector2 moveDir = new Vector2(dir.x-transform.position.x, dir.y - transform.position.y);

        transform.Translate(moveDir * moveSpeed * Time.deltaTime);
    }

    private void SetRise()
    {
        isRise = false;
    }
}

