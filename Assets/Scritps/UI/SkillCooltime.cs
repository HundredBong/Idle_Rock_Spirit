using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCooltime : MonoBehaviour
{
    private static SkillCooltime instance;
    public static SkillCooltime Instance { get { return instance; } }

    //[Header("��ų ��ư")]
    //[Header("��ų �̸� �ؽ�Ʈ")]
    [Header("��ų ��ư �̹���")] public List<Image> skillImgae;
    [Header("��ų ��ư ������ �̹���")] public List<Image> skillHideImage;
    [Header("��ų ��Ÿ�� �ؽ�Ʈ")] public List<Text> skillTimeText;

    [Header("��ų ��Ÿ��")] public List<float> skillCoolTime;
    [Header("���� ��ų ��Ÿ��")] public List<float> currentSkillCoolTime;

    private Coroutine test;
    private void Start()
    {
        //StartCoroutine(SkillCheck(0));
    }

    public void Test()
    {
        if (test != null) //�ڷ�ƾ�� �̹� �������̶�� 
        {
            StopCoroutine(test); // ���� ���� ���� �ڷ�ƾ ����
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
                skillTimeText[i].text = ""; // ��Ÿ���� ������ �� �ؽ�Ʈ �����
                skillHideImage[i].fillAmount = 0; //���� �ؽ�Ʈ�� �̹��� ��Ȱ��ȭ�� �ʿ� x
            }
        }
    }
}
