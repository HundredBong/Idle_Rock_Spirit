using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class SkillLearnPanel : MonoBehaviour
{
    [SerializeField, Header("스킬 이름 텍스트")] private Text nameText;
    [SerializeField, Header("스킬 설명 텍스트")] private Text explanationText;
    [SerializeField, Header("Yes버튼")] private Button buttonYes;
    [SerializeField, Header("No버튼")] private Button buttonNo;
    [SerializeField, Header("OK버튼")] private Button buttonOK;
    [SerializeField, Header("Yes버튼 숨기기용 이미지")] private Image hideImage;
    [SerializeField, Header("스킬 해금 비용")] private int[] skillPrice;
    private int skillIndex;

    private void Start()
    {
        //처음 시작시 패널을 비활성화
        gameObject.SetActive(false);
    }

    public void OnClickSkill(int i)
    {
        //1. 인자로 전달받은 값에 따라 패널을 출력하되 텍스트 내용을 다르게 함
        //2. 플레이어가 스킬을 배웠는지 검사하고 배우지 않았다면 숨기기용 이미지 미출력
        //3. 이미 배운 상태라면 숨기기 이미지도 같이 출력해서 Yes버튼 못누르게
        //4. Yes버튼을 누르면 전달받은 i에 맞는 스킬을 활성화시켜줌
        //5. No버튼을 누르면 창을 닫음

        //Yes버튼에서 사용할 수 있게 인덱스값을 저장, 매 호출시 바뀜
        skillIndex = i;
        Debug.Log("스킬 배우기 패널 버튼 클릭");
        //창을 띄우고 버튼 두개를 활성화
        gameObject.SetActive(true);

        buttonYes.gameObject.SetActive(true);
        buttonNo.gameObject.SetActive(true);

        buttonOK.gameObject.SetActive(false);
        hideImage.gameObject.SetActive(false);

        switch (i)
        {
            case 0:
                nameText.text = "스타라이트";
                if (GameManager.Instance.player.skillObjects[i].activeSelf == true)
                {
                    explanationText.text = $"사거리 내에 적이 있을 경우 돌정령의 머리 위로 투사체를 10개 소환하여 돌정령과 가장 가까운 몬스터에게 " +
                    $"날아가 공격하며 사라진다. (쿨타임 {GameManager.Instance.player.skillCooltime[i]}초)";
                    //만약 스킬을 배운 상태라면 기존 버튼 비활성화 및 Ok버튼 활성화
                    DisplayOKButton();
                }
                else
                {
                    explanationText.text = $"사거리 내에 적이 있을 경우 돌정령의 머리 위로 투사체를 10개 소환하여 돌정령과 가장 가까운 몬스터에게 " +
                         $"날아가 공격하며 사라진다. (쿨타임 {GameManager.Instance.player.skillCooltime[i]}초)\n\n해금하시겠습니까? (G{skillPrice[i]})";
                    //만약 스킬을 배우지 않았지만 소지금이 모자라면
                    if (GameManager.Instance.player.gold < skillPrice[i])
                    {
                        //버튼을 가리는 이미지를 활성화
                        hideImage.gameObject.SetActive(true);
                    }
                }
                break;
            case 1:
                nameText.text = "보이드";
                if (GameManager.Instance.player.skillObjects[i].activeSelf == true)
                {
                    explanationText.text = $"사거리 내에 적이 있을 경우 돌정령의 전방에 검은 구체를 앞으로 발사한다." +
                        $" 구체는 적에게 닿을시 정해진 횟수의 피해를 주고 사라진다. (쿨타임 {GameManager.Instance.player.skillCooltime[i]}초)";
                    //만약 스킬을 배운 상태라면 기존 버튼 비활성화 및 Ok버튼 활성화
                    DisplayOKButton();
                }
                else
                {
                    explanationText.text = $"사거리 내에 적이 있을 경우 돌정령의 전방에 검은 구체를 앞으로 발사한다." +
                        $" 구체는 적에게 닿을시 정해진 횟수의 피해를 주고 사라진다. (쿨타임 {GameManager.Instance.player.skillCooltime[i]}초)" +
                        $"\n\n해금하시겠습니까? (G{skillPrice[i]})";
                    //만약 스킬을 배우지 않았지만 소지금이 모자라면
                    if (GameManager.Instance.player.gold < skillPrice[i])
                    {
                        //버튼을 가리는 이미지를 활성화
                        hideImage.gameObject.SetActive(true);
                    }
                }
                break;
            case 2:
                nameText.text = "메테오";
                if (GameManager.Instance.player.skillObjects[i].activeSelf == true)
                {
                    explanationText.text = $"사거리 내에 적이 있을 경우 가장 가까운 적을 향해 메테오를 낙하시키고 범위 피해를 준다. (쿨타임 {GameManager.Instance.player.skillCooltime[i]}초)";
                    //만약 스킬을 배운 상태라면 기존 버튼 비활성화 및 Ok버튼 활성화
                    DisplayOKButton();
                }
                else
                {
                    explanationText.text = $"사거리 내에 적이 있을 경우 가장 가까운 적을 향해 메테오를 낙하시키고 범위 피해를 준다. (쿨타임 {GameManager.Instance.player.skillCooltime[i]}초)" +
                        $"\n\n해금하시겠습니까? (G{skillPrice[i]})";
                    //만약 스킬을 배우지 않았지만 소지금이 모자라면
                    if (GameManager.Instance.player.gold < skillPrice[i])
                    {
                        //버튼을 가리는 이미지를 활성화
                        hideImage.gameObject.SetActive(true);
                    }
                }
                break;
            case 3:
                nameText.text = "벼락";
                if (GameManager.Instance.player.skillObjects[i].activeSelf == true)
                {
                    Debug.LogWarning($"플레이어 상태 : {GameManager.Instance.player.skillObjects[i].activeSelf}");
                    explanationText.text = $"사거리와 관계 없이 돌정령과 가까운 적을 향해 벼락을 8번 낙하시킨다. (쿨타임 {GameManager.Instance.player.skillCooltime[i]}초)";
                    //만약 스킬을 배운 상태라면 기존 버튼 비활성화 및 Ok버튼 활성화
                    DisplayOKButton();
                }
                else
                {
                    Debug.LogWarning($"플레이어 상태 : {GameManager.Instance.player.skillObjects[i].activeSelf}");

                    explanationText.text = $"사거리에 관계 없이 돌정령과 가까운 적을 향해 벼락을 8번 낙하시킨다. (쿨타임 {GameManager.Instance.player.skillCooltime[i]}초)" +
                        $"\n\n해금하시겠습니까? (G{skillPrice[i]})";
                    //만약 스킬을 배우지 않았지만 소지금이 모자라면
                    if (GameManager.Instance.player.gold < skillPrice[i])
                    {
                        //버튼을 가리는 이미지를 활성화
                        hideImage.gameObject.SetActive(true);
                    }
                }
                break;
            case 4:
                nameText.text = "분노";
                if (GameManager.Instance.player.skillObjects[i].activeSelf == true)
                {
                    explanationText.text = $"돌정령의 공격력이 10초간 2배로 상승하게된다. (쿨타임 {GameManager.Instance.player.skillCooltime[i]}초)";
                    //만약 스킬을 배운 상태라면 기존 버튼 비활성화 및 Ok버튼 활성화
                    DisplayOKButton();
                }
                else
                {
                    explanationText.text = "돌정령의 공격력이 10초간 2배로 상승하게된다. " +
                        $"(쿨타임 {GameManager.Instance.player.skillCooltime[i]}초)\n\n해금하시겠습니까? (G{skillPrice[i]})";
                    //만약 스킬을 배우지 않았지만 소지금이 모자라면
                    if (GameManager.Instance.player.gold < skillPrice[i])
                    {
                        //버튼을 가리는 이미지를 활성화
                        hideImage.gameObject.SetActive(true);
                    }
                }
                break;
        }
    }

    public void OnClickYes()
    {
        GameManager.Instance.player.ActivateSkill(skillIndex);
        CloseTab();

        GameManager.Instance.player.gold -= skillPrice[skillIndex];

        UIManager.Instance.PlayerMoneyRenewal();
    }

    public void CloseTab()
    {
        gameObject.SetActive(false);
    }

    private void DisplayOKButton()
    {
        buttonYes.gameObject.SetActive(false);
        buttonNo.gameObject.SetActive(false);
        buttonOK.gameObject.SetActive(true);
    }

}
