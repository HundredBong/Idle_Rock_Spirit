using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCooltime : MonoBehaviour
{
    private static SkillCooltime instance;
    public static SkillCooltime Instance { get { return instance; } }

    //[Header("스킬 버튼")]
    //[Header("스킬 이름 텍스트")]
    [Header("스킬 버튼 이미지")] public List<Image> skillImgae;
    [Header("스킬 버튼 숨기기용 이미지")] public List<Image> skillHideImage;
    [Header("스킬 쿨타임 텍스트")] public List<Text> skillTimeText;

    [Header("스킬 쿨타임")] public List<float> skillCoolTime;
    [Header("현재 스킬 쿨타임")] public List<float> currentSkillCoolTime;

    private Coroutine test;
    private void Start()
    {
        //StartCoroutine(SkillCheck(0));
    }

    public void Test()
    {
        if (test != null) //코루틴이 이미 실행중이라면 
        {
            StopCoroutine(test); // 기존 실행 중인 코루틴 중지
        }
        test = StartCoroutine(SkillCheck(0));
    }

    public IEnumerator SkillCheck(int i)
    {
        currentSkillCoolTime[i] = skillCoolTime[i];

        while (currentSkillCoolTime[i] <= skillCoolTime[i])
        {
            yield return null;

            if (currentSkillCoolTime[i] > 0)
            {
                currentSkillCoolTime[i] -= Time.deltaTime;

                if (currentSkillCoolTime[i] < 0)
                {
                    currentSkillCoolTime[i] = 0f;
                }

                skillTimeText[i].text = $"{Mathf.Ceil(currentSkillCoolTime[i]).ToString()}s";
                float time = currentSkillCoolTime[i] / skillCoolTime[i];
                skillHideImage[i].fillAmount = time;
            }

            else
            {
                skillTimeText[i].text = ""; // 쿨타임이 끝났을 때 텍스트 지우기
                skillHideImage[i].fillAmount = 0; //굳이 텍스트랑 이미지 비활성화할 필요 x
            }
        }
    }
}
