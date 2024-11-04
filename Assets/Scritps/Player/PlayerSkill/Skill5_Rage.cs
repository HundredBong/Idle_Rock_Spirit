using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill5_Rage : MonoBehaviour
{
    [SerializeField, Header("상승시킬 대미지 배율 (2)")] private float damageMultiplier;
    [SerializeField, Header("스킬 쿨타임(20)"), Tooltip("지속시간이 끝난 뒤 쿨타임이 도니까 실제 쿨타임이랑 다름")]
    private float rageInterval;
    [SerializeField, Header("스킬 지속시간(10)")] private float rageDuration; //레이지 지속시간
    private float playerRageDamage; //레이지 대미지
    private float originalPlayerDamage; //원본 대미지

    //저는 배속이 싫어요
    private float originalRageDuration;
    private float originalRageInterval;

    //기획서 : 유일한 버프기인데 버프기가 지속시간 끝나고 쿨타임이 도나요
    //아니면 버프기 걸자마자 지속시간 상관없이 바로 쿨타임이 도나요
    [SerializeField, Header("생성될때 파티클")] private ParticleSystem particlePrefabSpawn;
    //업그레이드 하자마자 바로 적용되도록 공격력 설정을 Update로 변경 및 bool 변수 선언
    private bool isRage;

    private void Start()
    {
        isRage = false;

        rageInterval = GameManager.Instance.player.skillCooltime[4];

        originalRageDuration = rageDuration;
        originalRageInterval = rageInterval;
        originalPlayerDamage = GameManager.Instance.player.damage;


        StartCoroutine(BoostCoroutine(rageDuration));
    }

    private void Update()
    {
        //SetInterval();

        Debug.Log($"플레이어 레이지 대미지 : {playerRageDamage}");
        Debug.Log($"오리지날 플레이어 대미지 : {originalPlayerDamage}");
        Debug.Log($"게임매니저 플레이어 대미지 : {GameManager.Instance.player.damage}");

        originalPlayerDamage = GameManager.Instance.player.originalDamage;

        if (isRage == true)
        {
            playerRageDamage = originalPlayerDamage * damageMultiplier;
            GameManager.Instance.player.damage = playerRageDamage;
            Debug.Log($"레이지 중일때 대미지: {playerRageDamage}");
            Debug.Log($"레이지 아닐때 대미지: {originalPlayerDamage}");

        }
        else 
        {
            GameManager.Instance.player.damage = originalPlayerDamage;
            Debug.Log($"기본 대미지: {originalPlayerDamage}");
        }
    }

    private IEnumerator BoostCoroutine(float duration)
    {
        ParticleSystem parSpawn = Instantiate(particlePrefabSpawn);
        parSpawn.transform.position = gameObject.transform.position;
        parSpawn.Play();
        parSpawn.gameObject.SetActive(false);

        while (true)
        {
            isRage = true; //레이지 활성화로 공격력 증가시킴
            parSpawn.gameObject.SetActive(true); //파티클 활성화
            UIManager.Instance.SetDamageIndicator(); //하단 대미지 인디케이터 갱신
            SkillCooltimeManager.Instance.UseSkill(4); //스킬 쿨타임 갱신

            yield return new WaitForSeconds(duration); //지속시간동안 대기

            isRage = false; //레이지 비활성화로 공격력 복구
            parSpawn.gameObject.SetActive(false); //파티클 비활성화, Stop하니 Destroy되서 참조 불가
            UIManager.Instance.SetDamageIndicator(); //대미지 인디케이터 갱신

            yield return new WaitForSeconds(rageInterval-duration); //쿨타임 - 지속시간동안 대기후 다시 실행
        }
    }
    private void SetInterval()
    {
        if (UIManager.Instance.is2xSpeed == true)
        {
            rageInterval = originalRageInterval / 2;
            rageDuration = originalRageDuration / 2;
        }
        else
        {
            rageInterval = originalRageInterval;
            rageDuration = originalRageDuration;
        }
    }
}

