using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill3_Projectile : MonoBehaviour
{
    internal float damage;
    internal float duration;

    internal Vector3 rendererStartPos;
    private CircleCollider2D coll;
    private Transform rendererTransform;

    [SerializeField, Header("명중했을때 파티클")] private ParticleSystem particlePrefabHit;

    private void Awake()
    {
        coll = GetComponent<CircleCollider2D>();
        //충돌체크 안할꺼니까 비활성화
        coll.enabled = false;
        //렌더러 위치 수정을 위해 transform의 정보를 가져옴
        rendererTransform = transform.Find("Renderer");
    }

    //생성된 위치에서 일정 시간 후에 일정 범위내의 적에게 대미지를 주고 사라짐
    private void Start()
    {

        StartCoroutine(Explosion());
        //StartCoroutine(DisableCoroutine());
        Destroy(gameObject, duration + 0.1f);
    }

    IEnumerator Explosion()
    {
        float startTime = Time.time;
        float endTime = startTime + duration;
        rendererTransform.localPosition = rendererStartPos;

        //미사일이 폭발하기 전 랜더러의 위치를 애니메이션처럼 이동시키는 로직
        while (Time.time < endTime) //endTime은 미사일이 폭발하는 시간을 기준으로 계산됨
        {//즉, 미사일이 떨어지는 애니메이션의 지속시간을 의미함
            yield return null; //프레임당 1회씩 반복

            //이 오브젝트가 생성된 이후 경과한 시간
            //현재 시간 - 시작된 시간으로 경과시간을 구함
            //현재 12초고, startTime이 11초라면 1초 경과했겠죠
            float currentTime = Time.time - startTime;
            float duration = this.duration;

            //애니메이션의 진행 상태를 0과 1사이의 값으로 반환함
            float t = currentTime / duration;
            //Lerp 메서드를 사용해서 시작점과 원점까지의 선형 보간을 통해 위치계산
            //t값에 따라 위치가 변화함, 즉 애니메이션이 진행될수록 렌더러의 위치가 이동됨
            Vector2 curRendPos = Vector2.Lerp(rendererStartPos, Vector2.zero, t);

            //계산된 위치를 렌더러의 로컬 포지션으로 설정함.
            //이를 통해서 렌더러가 이동하는듯한 효과를 줌
            rendererTransform.localPosition = curRendPos;

        }
        //미사일이 폭발한 지점을 기준으로 coll.radius 크기의 원 안에 있는 모든 콜라이더를 감지
        Collider2D[] contactedColls = Physics2D.OverlapCircleAll(transform.position, coll.radius);

        foreach (Collider2D contactedColl in contactedColls)
        {
            Debug.Log($"Contacted name :  {contactedColl.name}");
            Debug.Log($"Missiles radius : {coll.radius}");
            if (contactedColl.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(damage);
            }
        }


        ParticleSystem parHit = Instantiate(particlePrefabHit);
        parHit.transform.position = new Vector2(transform.position.x, transform.position.y - 0.5f);
        parHit.Play();


        Destroy(gameObject);
    }
}
