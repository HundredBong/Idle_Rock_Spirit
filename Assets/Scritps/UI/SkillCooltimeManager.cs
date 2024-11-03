using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCooltimeManager : MonoBehaviour
{
    private static SkillCooltimeManager instance;
    public static SkillCooltimeManager Instance { get { return instance; } }

    //[Header("��ų ��ư")]
    //[Header("��ų �̸� �ؽ�Ʈ")]
    //[Header("��ų ��ư �̹���")] public List<Image> skillImgae;
    [Header("��ų ��ư ������ �̹���")] public List<Image> skillHideImage;
    [Header("��ų ��Ÿ�� �ؽ�Ʈ")] public List<Text> skillTimeText;

    //��ų ��Ÿ�� : �÷��̾��� ��Ÿ���� �����Ͽ� �ʱ�ȭ
    private float[] skillCoolTime;
    //��ٿ� �ڷ�ƾ�� ����� �� ��Ÿ������ �ʱ�ȭ
    private float[] currentSkillCoolTime;
    //�ڷ�ƾ ������
    private List<Coroutine> displayCooldown;

    private void Awake()
    {
        //��ų�Ŵ����� ���ٸ�
        if (instance == null)
        {
            //���� ������Ʈ�� ��ų�Ŵ����� ����
            instance = this;
        }
        //�̹� ��ų�Ŵ����� �ִٸ�
        else
        {
            //������Ʈ�� ��� �ı��ϰ� Awake �޼��带 ������
            DestroyImmediate(this);
            return;
        }
        DontDestroyOnLoad(gameObject);


    }

    private IEnumerator Start()
    {
        yield return null;

        displayCooldown = new List<Coroutine>(new Coroutine[skillHideImage.Count]);

        skillCoolTime = new float[GameManager.Instance.player.skillCooltime.Length];
        currentSkillCoolTime = new float[GameManager.Instance.player.skillCooltime.Length];

        for (int i = 0; i < GameManager.Instance.player.skillCooltime.Length; i++)
        {
            skillCoolTime[i] = GameManager.Instance.player.skillCooltime[i];
        }
    }

    public void UseSkill(int i)
    {
        if (displayCooldown[i] != null) //�ڷ�ƾ�� �̹� �������̶�� 
        {
            StopCoroutine(displayCooldown[i]); // ���� ���� ���� �ڷ�ƾ ����
        }
        //�ٵ� test�� ���⼭ �ʱ�ȭ�ߴµ� ������ �� �����????????
        //ó�� ������ null�̴ϱ� �ǳʶٰ� �ٷ� �Ʒ��ڵ� ����
        //�� �� �ʱ�ȭ�� test�� �����ؼ� ���� Test�޼��� ���ට StopCoroutine(test)�� ����?
        displayCooldown[i] = StartCoroutine(DisplayCooldownCoroutine(i));
       
    }

    public IEnumerator DisplayCooldownCoroutine(int i)
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
