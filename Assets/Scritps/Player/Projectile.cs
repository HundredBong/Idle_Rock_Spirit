using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using UnityEngine;

//투사체에 Rigidbody2D를 적용하여 중력의 영향을 받게해서 포물선 구현
//is Trigger 체크후 물리의 영향을받는 트리거로 설정

//지금은 투사체가 원형이라서 회전값의 의미가 없지만,
//추후 다른 모양의 투사체가 추가될 수 있으므로 투사체의 회전값이 변경되는 로직도 작성

public class Projectile : MonoBehaviour
{
    //투사체가 입힐 대미지, 날아가는 속도
    //Player의 AttackCoroutine에서 값이 결정됨
    internal float damage;
    internal float projectileSpeed;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.velocity = transform.position * projectileSpeed;
    }

    private void Update()
    {

    }

    //투사체가 다른 객체에게 닿았을 때 
    private void OnTriggerEnter2D(Collider2D other)
    {
        //닿은 객체가 Enemy 컴포넌트를 가지고있는지 검사하고 가져옴
        if (other.TryGetComponent(out Enemy enemy))
        {
            //enemy가 대미지를 입는 메서드를 실행함
            enemy.TakeDamage(damage);
        }
    }

}
