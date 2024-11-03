using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCooltimeManager : MonoBehaviour
{
    private static SkillCooltimeManager instance;
    public static SkillCooltimeManager Instance { get { return instance; } }

    //[Header("스킬 버튼")]
    //[Header("스킬 이름 텍스트")]
    //[Header("스킬 버튼 이미지")] public List<Image> skillImgae;
    [Header("스킬 버튼 숨기기용 이미지")] public List<Image> skillHideImage;
    [Header("스킬 쿨타임 텍스트")] public List<Text> skillTimeText;

    //스킬 쿨타임 : 플레이어의 쿨타임을 참조하여 초기화
    private float[] skillCoolTime;
    //쿨다운 코루틴이 실행될 때 쿨타임으로 초기화
    private float[] currentSkillCoolTime;
    //코루틴 중지용
    private List<Coroutine> displayCooldown;

    private void Awake()
    {
        //스킬매니저가 없다면
        if (instance == null)
        {
            //현재 오브젝트를 스킬매니저로 설정
            instance = this;
        }
        //이미 스킬매니저가 있다면
        else
        {
            //오브젝트를 즉시 파괴하고 Awake 메서드를 종료함
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
        if (displayCooldown[i] != null) //코루틴이 이미 실행중이라면 
        {
            StopCoroutine(displayCooldown[i]); // 기존 실행 중인 코루틴 중지
        }
        //근데 test를 여기서 초기화했는데 위에꺼 왜 실행됨????????
        //처음 실행은 null이니까 건너뛰고 바로 아래코드 실행
        //그 후 초기화된 test를 참조해서 다음 Test메서드 실행때 StopCoroutine(test)에 전달?
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
                skillTimeText[i].text = ""; // 쿨타임이 끝났을 때 텍스트 지우기
                skillHideImage[i].fillAmount = 0; //굳이 텍스트랑 이미지 비활성화할 필요 x
            }
        }
    }
}
