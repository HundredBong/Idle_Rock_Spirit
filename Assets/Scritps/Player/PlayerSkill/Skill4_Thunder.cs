using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill4_Thunder : MonoBehaviour
{
    private Enemy targetEnemy;
    public int thunderCount;
    public Skill4_Projectile projtilePrefab;

    public float projectileDamage;
    public float projectileSpeed;
    //스프라이트 렌더러로 사기칠 예정
    //리스트에 있는 enemy중 가장 가까운 enemy를 찾아서 
    //데가리 위에 콜라이더는 작지만 렌더러는 크고 길쭉한걸로 소환하기

    //기획서 : 8번 떨어진다 했는데 떨어질때 딜레이는 얼마나 줘야 하나요
    //딜레이를 줘야하면 벼락이 다 떨어져야 쿨타임이 도나요, 떨어지는 시점부터 쿨타임이 도나요
    //"벼락은 8번 떨어지면 순차적으로 타격함(오타아님)" 이라는게 8개가 순차적으로 가장 가까운 enemy에게 떨어진다는 뜻인가요
    //아니면 가까운 순서대로 8마리까지 맞추는건가요
    //전자라면 가장 가까운 몬스터가 죽으면 그 위치에 계속 벼락을 꽂나요 아니면 다음 적을 타겟팅하나요
    //후자라면 enemy가 4마리 있으면 4개만 꽂으면 되나요 
    //예시 이미지는 사선으로 치던데 꽂히는 각도가 어떻게 되나요
    //벼락 오브젝트는 언제 없어져야 하나요


    //여기서 할 일 : 번개를 가장 가까운 enemy에게 소환함
    //for문 안에서 소환할 번개 개수만큼 SerchEnemy 돌리고 가까운 enemy 위에 소환
    //소환된 번개 projectile은 Start메서드에서 빠르게 아래로 내려감
    private Vector3 closestEnemyPosition;
    private float distance;

    public float thunderInterval;
    private float preThunderTime;
    public float innerInterval;



    void Start()
    {
    }

    void Update()
    {
        //가장 가까운 적을 찾음
        if (GameManager.Instance.enemies != null)
            closestEnemyPosition = EnemyUtility.SearchTargetPosition(transform, out targetEnemy);

        Fire();
        Debug.Log($"Thunder.preThunderTime : {preThunderTime}");
    }

    private void Fire()
    {
        if (preThunderTime + (thunderInterval + (thunderCount * innerInterval)) > Time.time) { return; }

        Debug.Log($"Thunder.Fire메서드 실행됨");

        StartCoroutine(FireCoroutine());
        preThunderTime = Time.time;

    }

    private IEnumerator FireCoroutine()
    {
        for (int i = 0; i < thunderCount; i++)
        {
            Debug.Log($"Thunder.Coroutine {i}번째 루프");

            closestEnemyPosition = EnemyUtility.SearchTargetPosition(transform, out targetEnemy);
            Skill4_Projectile proj = Instantiate(projtilePrefab,
                new Vector3(closestEnemyPosition.x, closestEnemyPosition.y + 4, 0), Quaternion.identity);

            proj.projectileDamage = this.projectileDamage;
            proj.projectileSpeed = this.projectileSpeed;
            yield return new WaitForSeconds(innerInterval);
        }
        //yield return new WaitForSeconds(thunderInterval + (thunderCount * innerInterval));

    }
}
