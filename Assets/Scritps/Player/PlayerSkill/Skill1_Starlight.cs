using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1_Starlight : MonoBehaviour
{
    public GameObject starlightPrefab;
    public int starlightCount;

    public float initalSpeed;
    public float fireSpeed;
    public float innerInterval;
    public float fireInterval;
    private float preFireTime;


    private Enemy targetEnemy;
    private Vector3 closestEnemyPosition;
    //1. 투사체를 생성함 -> 플레이어가 함
    //2. 생성된 투사체가 플레이어 위 랜덤한 좌표로 떠오름 (

    //prefab : 
    //3. 가장 가까운 적의 좌표를 찾음
    //4. 그 좌표로 투사체를 innerInterval 간격으로 발사함

    //Start에서 Fire코루틴 실행

    //기획서 : 돌정령 머리위로 투사체 10개 소환은 그냥 뿅 하고 나타나면 되나요
    //이런거 신경써서 몬스터는 화면 밖에서 스폰되게 했으면서 이건 왜 그냥 뿅 하고 나타나나요
    //10개 소환하고 10개가 일괄적으로 몬스터에게 날아가나요 하나씩 10번 날아가나요
    //가장 가까운 몬스터가 뒈짓하면 갈 길 잃은 투사체는 어디로 가야하나요
    //타겟팅이라는 말이 안보이는데 그럼 빗나갈 요소도 존재하나요
    //빗나가면 오브젝트는 언제 사라져야하나요

    //썬더랑 비슷하게 코루틴을 돌려서 순차적으로 생성
    //생성 만 담당하고 날아가는건 Projectile에서 위로 한번, enemy로 날아가도록

    private void Start()
    {

    }

    void Update()
    {
        Fire();
    }

    private void Fire()
    {
        if (preFireTime + fireInterval > Time.time) { return; }

        StartCoroutine(FireCoroutine());
        preFireTime = Time.time;
    }

    private IEnumerator FireCoroutine()
    {
        for (int i = 0; i < starlightCount; i++)
        {
            Debug.Log($"Starlight. {i}번째 코루틴");
            Instantiate(starlightPrefab,transform.position, transform.rotation);
            yield return new WaitForSeconds(innerInterval);
        }
    }

}
