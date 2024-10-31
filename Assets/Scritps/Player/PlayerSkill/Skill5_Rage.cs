using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill5_Rage : MonoBehaviour
{
    public float rageDamage; //레이지 배율
    private float playerRageDamage; //레이지했을때 플레이어 속도
    private float beforeRageDamage; //레이지 전 원본속도

    public float rageInterval; //레이지 쿨타임
    public float rageDuration; //레이지 지속시간

    //기획서 : 유일한 버프기인데 버프기가 지속시간 끝나고 쿨타임이 도나요
    //아니면 버프기 걸자마자 지속시간 상관없이 바로 쿨타임이 도나요

    private IEnumerator Start()
    {
        yield return null;

        playerRageDamage = GameManager.Instance.player.damage * rageDamage;
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

