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
    private float originalPlayerDamage; //���� �����

    //���� ����� �Ⱦ��
    private float originalRageDuration;
    private float originalRageInterval;

    //��ȹ�� : ������ �������ε� �����Ⱑ ���ӽð� ������ ��Ÿ���� ������
    //�ƴϸ� ������ ���ڸ��� ���ӽð� ������� �ٷ� ��Ÿ���� ������
    [SerializeField, Header("�����ɶ� ��ƼŬ")] private ParticleSystem particlePrefabSpawn;
    //���׷��̵� ���ڸ��� �ٷ� ����ǵ��� ���ݷ� ������ Update�� ���� �� bool ���� ����
    private bool isRage;

    private void Start()
    {
        isRage = false;

        rageInterval = GameManager.Instance.player.skillCooltime[4];

        //originalRageDuration = rageDuration;
        //originalRageInterval = rageInterval;
        originalPlayerDamage = GameManager.Instance.player.damage;


        StartCoroutine(BoostCoroutine(rageDuration));
    }

    private void Update()
    {
        //SetInterval();

        //Debug.Log($"�÷��̾� ������ ����� : {playerRageDamage}");
        //Debug.Log($"�������� �÷��̾� ����� : {originalPlayerDamage}");
        //Debug.Log($"���ӸŴ��� �÷��̾� ����� : {GameManager.Instance.player.damage}");

        originalPlayerDamage = GameManager.Instance.player.originalDamage;

        if (isRage == true)
        {
            playerRageDamage = originalPlayerDamage * damageMultiplier;
            GameManager.Instance.player.damage = playerRageDamage;
            Debug.Log($"�������� Ȱ��ȭ �� : {playerRageDamage}");
            //Debug.Log($"������ �ƴҶ� �����: {originalPlayerDamage}");

        }
        else 
        {
            GameManager.Instance.player.damage = originalPlayerDamage;
            Debug.Log($"�������� ��Ȱ��ȭ �� : {originalPlayerDamage}");
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
            if (GameManager.Instance.player.health >= 0)
            {
                isRage = true; //������ Ȱ��ȭ�� ���ݷ� ������Ŵ
                parSpawn.gameObject.SetActive(true); //��ƼŬ Ȱ��ȭ

                SkillCooltimeManager.Instance.UseSkill(4); //��ų ��Ÿ�� ����

                //����� ������� �ٷ� �ݿ����� ���ϰ� ���� ������� �ݿ��ϴ�
                //������ �־ 1������ ������ �� �� ������� ���Ž�Ŵ
                yield return null;
                UIManager.Instance.SetDamageIndicator(); //������Ų ������� �ϴ� ����� �ε������� ����
                Debug.Log("(������)�ε������� ���ŵ� (ture)");

                yield return new WaitForSeconds(duration); //���ӽð����� ���

                isRage = false; //������ ��Ȱ��ȭ�� ���ݷ� ����
                parSpawn.gameObject.SetActive(false); //��ƼŬ ��Ȱ��ȭ, Stop�ϴ� Destroy�Ǽ� ���� �Ұ�
                
                yield return null; 
                UIManager.Instance.SetDamageIndicator(); //���ҽ�Ų ������� �ϴ� �����  �ε������� ����
                Debug.Log("�ε������� ���ŵ� (false)");
            }

            yield return new WaitForSeconds(rageInterval-duration); //��Ÿ�� - ���ӽð����� ����� �ٽ� ����
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

