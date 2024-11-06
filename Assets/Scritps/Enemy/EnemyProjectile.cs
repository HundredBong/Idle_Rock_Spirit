using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    public float damage;

    //투사체를 발사하지만 사실상 근접공격용도로 쓰니 속도는 Private으로 설정
    public float projectileSpeed;

    private void Start()
    {
        //target = GameManager.Instance.player.transform;
        projectileSpeed = 1f;
        //혹시 타겟에게 명중하지 않았을 경우를 대비하여 1초뒤 자기 자신을 제거하는 코드 추가
        Destroy(gameObject, 1f);
    }

    void Update()
    {
        //타겟이 없다면 아래 코드를 실행하지 않음
        if (GameManager.Instance.player == null) return;

        //타겟 위치 - 내 위치 = 내가 가야할 방향
        Vector2 fireDir = GameManager.Instance.player.transform.position - gameObject.transform.position;
        Vector2 nomalizedFireDir = fireDir.normalized;


        //타겟 위치로 이동함
        transform.Translate(nomalizedFireDir * Time.deltaTime * projectileSpeed);
    }

    //타겟에 닿았을때 처리할 코드
    private void OnTriggerEnter2D(Collider2D other)
    {
        //플레이어와 접촉하면
        if (other.TryGetComponent(out Player player))
        {
            //플레이어의 TakeDamage메서드 실행하고
            player.TakeDamage(damage);
            
            //자기 자신을 제거함
            Destroy(gameObject);
        }
    }
}
