using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField, Header("ü��")] public float health;
    [SerializeField, Header("�ִ� ü��")] public int maxHealth;
    [SerializeField, Header("ü�� ���")] public float healthRegen;
    [SerializeField, Header("���ݷ�")] public int damage;
    [SerializeField, Header("ġ��Ÿ Ȯ��")] public int critlcalChance;
    [SerializeField, Header("ġ��Ÿ ����")] public int criticalMultiplier;
    [SerializeField, Header("���� �ӵ�"), Tooltip("�⺻�� 1.0 = / 1.0s 1�ʴ� 1ȸ")] public float attackInterval;
    [SerializeField, Header("���� ��")] public int doubleShot;
    [SerializeField, Header("������")] public int gold;

    //ü�� ��� ��Ÿ��
    private float regenInterval;

    //���������� ü���� ����� �ð�
    private float preRegenTime;

    //����ü�� �߻��� ���� ����
    [HideInInspector] public int fireArea;

    ProjectileLuncher launcher;

    private List<Skill> skill;

    public void Start()
    {
        //�ٸ� ��ü�� ������ �� �ֵ��� ���ӸŴ����� �÷��̾ ������Ʈ�� ����
        //��ó�� ������ �� �ֵ��� ��ó �ʱ�ȭ
        GameManager.Instance.player = this;

        launcher = GameObject.Find("ProjectileLauncher").GetComponent<ProjectileLuncher>();

        //ü�� ��� ���� ���� �ʱ�ȭ
        preRegenTime = 0;
        regenInterval = 5;
        StartCoroutine(SerchTarget());
    }

    public void Update()
    {
        HealthRegeneration();
    }

    public void TakeDamage(float damage)
    {
        //���ڷ� ���� damage��ŭ ü���� ����Ŵ
        health = health - damage;

        //���ҵ����� ü���� 0 ���϶��
        if (health <= 0)
        {
            //Death �޼��带 ������
            Death();
        }
    }

    //ü���� 0���ϰ� ������ ���� �� �޼���
    public void Death()
    {
        //������Ʈ�� ��Ȱ��ȭ
        Destroy(gameObject);
    }

    private void HealthRegeneration()
    {
        //5�ʸ��� ü���� ����� ����

        //�÷��̾��� ���������� �����ϱ����� ü���� 0���϶�� �Ʒ� �ڵ带 �������� ����
        if (health <= 0)
            return;

        //������� + ���������� ����� �ð��� ���� �ð����� ũ�ٸ� �Ʒ� �ڵ带 ������������
        if (regenInterval + preRegenTime > Time.time)
            return;

        //�׷��� �ʴٸ� ü�� ����� ������
        health = health + healthRegen;

        //���������� ȸ���� �ð��� �ʱ�ȭ����
        //�ʱ�ȭ ���ϸ� �������� ȸ���ؼ� power overwhelming
        preRegenTime = Time.time;
    }

    //private IEnumerator AttackCoroutine()
    //{
    //    //TODO : foreach������ ����Ʈ�� Ž���ϰ� enemy���� �Ÿ��� ���� �� �����϶��� ����ǰ� �ϴ� ���� �ۼ��ϱ�
    //    //TODO : Projectile���� ���� �������� �߻�Ǵ� ���� �ۼ��ϱ�

    //    //�÷��̾� ������Ʈ�� �ؾ��� �� : ����ü �߻� ���� ����

    //    //���� ����� ���� Ž���ϱ� ���� ���� �ʱ�ȭ

    //    //�÷��̾�� ���� ����� ���� Ž����
    //���� �Ÿ��� ���� ���� ��������, ��ó�� ������ �������� �߻��Ҽ��ְ� ����ü �ӵ��� ������ ����
    //
    //    //��ó�� �ؾ��ϴ� ���� : ���⼭ ���� ������ �÷��̾ ���ư�
    //    //��ó�� ������ ����ü�� �ӵ��� ������ ����ü�� �߻���

    //}

    private IEnumerator SerchTarget()
    {
        while (true)
        {


            //���ӸŴ����� enemy ����Ʈ���� ���� Ž��
            foreach (Enemy enemy in GameManager.Instance.enemies)
            {
                Enemy targetEnemy = null;
                float targetDistance = float.MaxValue;
                //enemmy�� ���������� null�̸� ���ܰ� �߻��ϹǷ� null�Ͻ� ������ �ǳʶ�
                if (enemy == null) { continue; }

                //foreach���� ��ȸ�� �� ���� enemy�� �÷��̾���� �Ÿ��� ����
                float distance = Vector3.Distance(enemy.transform.position, transform.position);

                //���� enemy���� �Ÿ��� ������ �Ÿ����� ������
                if (distance < targetDistance)
                {
                    //Ÿ���� �����ϰ�, distance�� �ʱ�ȭ
                    targetEnemy = enemy;
                    targetDistance = distance;
                    Debug.Log($"���� ����� �� : {targetEnemy.name}");
                    Debug.Log($"���� ����� �� : {Mathf.Abs(targetDistance)}");
                }


                //�Ÿ��� ���� ���� ������ ��ó�� �̸� �����Ͽ� �߻簢 ����
                if (distance < 1) { launcher.area = 0; }
                else if (distance < 0) { launcher.area = 1; }
                else { launcher.area = 2; }

            }
            launcher.Fire();
            yield return new WaitForSeconds(attackInterval);
        }
    }
}
