using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class SkillLearnPanel : MonoBehaviour
{
    [SerializeField, Header("��ų �̸� �ؽ�Ʈ")] private Text nameText;
    [SerializeField, Header("��ų ���� �ؽ�Ʈ")] private Text explanationText;
    [SerializeField, Header("Yes��ư")] private Button buttonYes;
    [SerializeField, Header("No��ư")] private Button buttonNo;
    [SerializeField, Header("OK��ư")] private Button buttonOK;
    [SerializeField, Header("Yes��ư ������ �̹���")] private Image hideImage;
    [SerializeField, Header("��ų �ر� ���")] private int[] skillPrice;
    private int skillIndex;

    private void Start()
    {
        //ó�� ���۽� �г��� ��Ȱ��ȭ
        gameObject.SetActive(false);
    }

    public void OnClickSkill(int i)
    {
        //1. ���ڷ� ���޹��� ���� ���� �г��� ����ϵ� �ؽ�Ʈ ������ �ٸ��� ��
        //2. �÷��̾ ��ų�� ������� �˻��ϰ� ����� �ʾҴٸ� ������ �̹��� �����
        //3. �̹� ��� ���¶�� ����� �̹����� ���� ����ؼ� Yes��ư ��������
        //4. Yes��ư�� ������ ���޹��� i�� �´� ��ų�� Ȱ��ȭ������
        //5. No��ư�� ������ â�� ����

        //Yes��ư���� ����� �� �ְ� �ε������� ����, �� ȣ��� �ٲ�
        skillIndex = i;
        Debug.Log("��ų ���� �г� ��ư Ŭ��");
        //â�� ���� ��ư �ΰ��� Ȱ��ȭ
        gameObject.SetActive(true);

        buttonYes.gameObject.SetActive(true);
        buttonNo.gameObject.SetActive(true);

        buttonOK.gameObject.SetActive(false);
        hideImage.gameObject.SetActive(false);

        switch (i)
        {
            case 0:
                nameText.text = "��Ÿ����Ʈ";
                if (GameManager.Instance.player.skillObjects[i].activeSelf == true)
                {
                    explanationText.text = $"��Ÿ� ���� ���� ���� ��� �������� �Ӹ� ���� ����ü�� 10�� ��ȯ�Ͽ� �����ɰ� ���� ����� ���Ϳ��� " +
                    $"���ư� �����ϸ� �������. (��Ÿ�� {GameManager.Instance.player.skillCooltime[i]}��)";
                    //���� ��ų�� ��� ���¶�� ���� ��ư ��Ȱ��ȭ �� Ok��ư Ȱ��ȭ
                    DisplayOKButton();
                }
                else
                {
                    explanationText.text = $"��Ÿ� ���� ���� ���� ��� �������� �Ӹ� ���� ����ü�� 10�� ��ȯ�Ͽ� �����ɰ� ���� ����� ���Ϳ��� " +
                         $"���ư� �����ϸ� �������. (��Ÿ�� {GameManager.Instance.player.skillCooltime[i]}��)\n\n�ر��Ͻðڽ��ϱ�? (G{skillPrice[i]})";
                    //���� ��ų�� ����� �ʾ����� �������� ���ڶ��
                    if (GameManager.Instance.player.gold < skillPrice[i])
                    {
                        //��ư�� ������ �̹����� Ȱ��ȭ
                        hideImage.gameObject.SetActive(true);
                    }
                }
                break;
            case 1:
                nameText.text = "���̵�";
                if (GameManager.Instance.player.skillObjects[i].activeSelf == true)
                {
                    explanationText.text = $"��Ÿ� ���� ���� ���� ��� �������� ���濡 ���� ��ü�� ������ �߻��Ѵ�." +
                        $" ��ü�� ������ ������ ������ Ƚ���� ���ظ� �ְ� �������. (��Ÿ�� {GameManager.Instance.player.skillCooltime[i]}��)";
                    //���� ��ų�� ��� ���¶�� ���� ��ư ��Ȱ��ȭ �� Ok��ư Ȱ��ȭ
                    DisplayOKButton();
                }
                else
                {
                    explanationText.text = $"��Ÿ� ���� ���� ���� ��� �������� ���濡 ���� ��ü�� ������ �߻��Ѵ�." +
                        $" ��ü�� ������ ������ ������ Ƚ���� ���ظ� �ְ� �������. (��Ÿ�� {GameManager.Instance.player.skillCooltime[i]}��)" +
                        $"\n\n�ر��Ͻðڽ��ϱ�? (G{skillPrice[i]})";
                    //���� ��ų�� ����� �ʾ����� �������� ���ڶ��
                    if (GameManager.Instance.player.gold < skillPrice[i])
                    {
                        //��ư�� ������ �̹����� Ȱ��ȭ
                        hideImage.gameObject.SetActive(true);
                    }
                }
                break;
            case 2:
                nameText.text = "���׿�";
                if (GameManager.Instance.player.skillObjects[i].activeSelf == true)
                {
                    explanationText.text = $"��Ÿ� ���� ���� ���� ��� ���� ����� ���� ���� ���׿��� ���Ͻ�Ű�� ���� ���ظ� �ش�. (��Ÿ�� {GameManager.Instance.player.skillCooltime[i]}��)";
                    //���� ��ų�� ��� ���¶�� ���� ��ư ��Ȱ��ȭ �� Ok��ư Ȱ��ȭ
                    DisplayOKButton();
                }
                else
                {
                    explanationText.text = $"��Ÿ� ���� ���� ���� ��� ���� ����� ���� ���� ���׿��� ���Ͻ�Ű�� ���� ���ظ� �ش�. (��Ÿ�� {GameManager.Instance.player.skillCooltime[i]}��)" +
                        $"\n\n�ر��Ͻðڽ��ϱ�? (G{skillPrice[i]})";
                    //���� ��ų�� ����� �ʾ����� �������� ���ڶ��
                    if (GameManager.Instance.player.gold < skillPrice[i])
                    {
                        //��ư�� ������ �̹����� Ȱ��ȭ
                        hideImage.gameObject.SetActive(true);
                    }
                }
                break;
            case 3:
                nameText.text = "����";
                if (GameManager.Instance.player.skillObjects[i].activeSelf == true)
                {
                    Debug.LogWarning($"�÷��̾� ���� : {GameManager.Instance.player.skillObjects[i].activeSelf}");
                    explanationText.text = $"��Ÿ��� ���� ���� �����ɰ� ����� ���� ���� ������ 8�� ���Ͻ�Ų��. (��Ÿ�� {GameManager.Instance.player.skillCooltime[i]}��)";
                    //���� ��ų�� ��� ���¶�� ���� ��ư ��Ȱ��ȭ �� Ok��ư Ȱ��ȭ
                    DisplayOKButton();
                }
                else
                {
                    Debug.LogWarning($"�÷��̾� ���� : {GameManager.Instance.player.skillObjects[i].activeSelf}");

                    explanationText.text = $"��Ÿ��� ���� ���� �����ɰ� ����� ���� ���� ������ 8�� ���Ͻ�Ų��. (��Ÿ�� {GameManager.Instance.player.skillCooltime[i]}��)" +
                        $"\n\n�ر��Ͻðڽ��ϱ�? (G{skillPrice[i]})";
                    //���� ��ų�� ����� �ʾ����� �������� ���ڶ��
                    if (GameManager.Instance.player.gold < skillPrice[i])
                    {
                        //��ư�� ������ �̹����� Ȱ��ȭ
                        hideImage.gameObject.SetActive(true);
                    }
                }
                break;
            case 4:
                nameText.text = "�г�";
                if (GameManager.Instance.player.skillObjects[i].activeSelf == true)
                {
                    explanationText.text = $"�������� ���ݷ��� 10�ʰ� 2��� ����ϰԵȴ�. (��Ÿ�� {GameManager.Instance.player.skillCooltime[i]}��)";
                    //���� ��ų�� ��� ���¶�� ���� ��ư ��Ȱ��ȭ �� Ok��ư Ȱ��ȭ
                    DisplayOKButton();
                }
                else
                {
                    explanationText.text = "�������� ���ݷ��� 10�ʰ� 2��� ����ϰԵȴ�. " +
                        $"(��Ÿ�� {GameManager.Instance.player.skillCooltime[i]}��)\n\n�ر��Ͻðڽ��ϱ�? (G{skillPrice[i]})";
                    //���� ��ų�� ����� �ʾ����� �������� ���ڶ��
                    if (GameManager.Instance.player.gold < skillPrice[i])
                    {
                        //��ư�� ������ �̹����� Ȱ��ȭ
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
