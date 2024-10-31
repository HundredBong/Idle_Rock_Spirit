using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill5_Rage : MonoBehaviour
{
    [SerializeField, Header("상승시킬 대미지 배율 (2)")] private float damageMultiplier; 
    [SerializeField, Header("스킬 쿨타임(20)"),Tooltip("지속시간이 끝난 뒤 쿨타임이 도니까 실제 쿨타임이랑 다름")]
    private float rageInterval;
    [SerializeField, Header("스킬 지속시간(10)")] private float rageDuration; //레이지 지속시간

    private float playerRageDamage; //레이지했을때 플레이어 속도
    private float beforeRageDamage; //레이지 전 원본속도

    //기획서 : 유일한 버프기인데 버프기가 지속시간 끝나고 쿨타임이 도나요
    //아니면 버프기 걸자마자 지속시간 상관없이 바로 쿨타임이 도나요

    private IEnumerator Start()
    {
        yield return null;

        playerRageDamage = GameManager.Instance.player.damage * damageMultiplier;
        beforeRageDamage = GameManager.Instance.player.damage;

        StartCoroutine(BoostCoroutine(rageDuration));
    }

    private void Update()
    {
    }

    private IEnumerator BoostCoroutine(float duration)
    {
        while (true)
        {
            GameManager.Instance.player.damage = playerRageDamage;
            yield return new WaitForSeconds(duration);
            GameManager.Instance.player.damage = beforeRageDamage;
            yield return new WaitForSeconds(rageInterval);

        }
    }
}

