using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField, Header("체력")] public float health;
    [Header("최대 체력")] private float maxHealth;
    [SerializeField, Header("이동 속도")] private float moveSpeed;
    [SerializeField, Header("공격력")] private float damage;
    [SerializeField, Header("도착 지점 X값")] private float arrivePosX;

    //목표로 이동할 타겟
    private Transform target;

    //공격에 필요한 투사체 프리팹
    public EnemyProjectile projectilePrefab;

    private IEnumerator Start()
    {
        //자기 자신을 리스트에 추가함
        //TODO: 플레이어가 foreach문으로 리스트를 순회하며 가까운 적 탐색
        GameManager.Instance.enemies.Add(this);

        //최대 체력을 현재 체력으로 설정
        maxHealth = health;

        //1프레임 유예를 둬서 초기화 에러 방지
        yield return null;
        target = GameManager.Instance.player.transform;

        if (GameManager.Instance.player != null)
            Debug.Log($"Player Name : {target.name} (Enemy.Start)");
        else
            Debug.Log("Player가 Null 상태임 (Enemy.Start)");
    }

    private void Update()
    {  
        //플레이어 위치 - 내 위치 = 내가 이동해야 할 방향
        Vector2 targetPos = GameManager.Instance.player.transform.position;
        Vector2 moveDir = new Vector2(targetPos.x - transform.position.x, 0);

        //플레이어와 enemy의 x축의 거리를 측정함
        float distance =
            GameManager.Instance.player.transform.position.x - transform.position.x;
        Debug.Log($"Distance : {distance}");

        //distance가 도착지점보다 크다면 즉, 아직 도착하지 않았다면
        if (Mathf.Abs(distance) > arrivePosX)
        {
            //플레이어 쪽으로 움직이는 Move 메서드를 실행함
            Move(moveDir.normalized);
        }

        //도착지점에 도착했다면
        else
        {
            //플레이어를 공격하는 Attack 메서드를 실행함
            Attack();
        }


        //정규화되서 방향만 남은 벡터를 인자로 전달함
        
    }

    private void FixedUpdate()
    {


    }

    private void Move(Vector2 dir)
    {
        Debug.Log("enemy가 이동중 (Enemy.Move)");
        //Updated에서 구한 방향벡터 * 이동속도 * 속도보간으로 해당 방향으로 이동
        transform.Translate(dir * moveSpeed * Time.deltaTime);
    }

    private void Attack()
    {
        //자기 자신의 위치에 프리팹을 생성함
        EnemyProjectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        //투사체 대미지를 자신의 대미지로 설정
        projectile.damage = this.damage;
    }

    public void TakeDamage(float damage)
    {
        //체력을 인자로 들어온 damage만큼 감소시킴
        health = health - damage;

        //감소시켰을 때 체력이 0이하라면 Death메서드 실행
        if (health <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
