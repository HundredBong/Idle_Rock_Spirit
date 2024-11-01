using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill5_Rage : MonoBehaviour
{
    [SerializeField, Header("��½�ų ����� ���� (2)")] private float damageMultiplier;
    [SerializeField, Header("��ų ��Ÿ��(20)"), Tooltip("���ӽð��� ���� �� ��Ÿ���� ���ϱ� ���� ��Ÿ���̶� �ٸ�")]
    private float rageInterval;
    [SerializeField, Header("��ų ���ӽð�(10)")] private float rageDuration; //������ ���ӽð�

    private float playerRageDamage; //������ �����
    private float beforeRageDamage; //���� �����

    //���� ����� �Ⱦ��
    private float originalRageDuration;
    private float originalRageInterval;


    //��ȹ�� : ������ �������ε� �����Ⱑ ���ӽð� ������ ��Ÿ���� ������
    //�ƴϸ� ������ ���ڸ��� ���ӽð� ������� �ٷ� ��Ÿ���� ������

    private IEnumerator Start()
    {

        originalRageDuration = rageDuration;
        originalRageInterval = rageInterval;

        yield return null;

        playerRageDamage = GameManager.Instance.player.damage * damageMultiplier;
        beforeRageDamage = GameManager.Instance.player.damage;

        StartCoroutine(BoostCoroutine(rageDuration));
    }

    private void Update()
    {
        SetInterval();
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

