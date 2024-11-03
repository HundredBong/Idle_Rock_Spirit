using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //��ȹ�� : �����̵� ��Ʈ�ڽ��� �־����� ���� �ǹ̰� �ֳ��� �ʹ� �����Ѱ� ���� �ʴٰ� �����ؿ�
    //���� ������ ī�޶� ȭ���� 3���� 2��� �ϼ̴µ� ���� �ػ� 2:3 ���������� �׷��� ġ��, 
    //�ػ󵵰� �޶����� ���� ������ �޶�������
    //����Ƽ ��ǥ���� ������ ���� ��Ȳ���� 1�����ɹ��ʹ� ���� ������ ������� �����ؿ�
    //������ �⺻���� ��Ÿ��, ü��, ���ݷ� �� �⺻ ������ ��� �����ϸ� �ɱ��
    //�� �־��� ���� ���� ����
    //������ ���� �ߵ������� ���� ����?
    //������ ũ�� ����Ǵ��� ���� ����?
    //"�⺻�� 1.0 = 1 ��, 1.0s�� 1�ʴ� 1ȸ" �������� ����
    //������ �ΰ��� ���, �ι��� ��� 
    //�ΰ��� ��� ������ ������ ���� ��� �����
    //�ι��� ��� ���� ��� �ǳ���

    [SerializeField, Header("ü��(5)")] internal float health;
    [SerializeField, Header("�ִ� ü��(5)")] internal int maxHealth;
    [SerializeField, Header("ü�� ���(0)")] internal float healthRegen;
    [SerializeField, Header("ü�� ��� ��Ÿ��(5)")] internal float regenInterval;
    [SerializeField, Header("���ݷ�(1)")] internal float damage;
    [SerializeField, Header("ġ��Ÿ Ȯ��(0)")] internal int critlcalChance;
    [SerializeField, Header("ġ��Ÿ ����(100)")] internal float criticalMultiplier;
    [SerializeField, Header("���� �ӵ�(1)"), Tooltip("�⺻�� 1.0 = / 1.0s 1�ʴ� 1ȸ")] internal float attackInterval;
    [SerializeField, Header("���� �� Ȯ��(0)")] internal int doubleShot;
    [SerializeField, Header("������")] internal int gold;
    [SerializeField, Header("�÷��̾� ���� �ݰ�")] internal float attackRange;
    [SerializeField, Header("��ų ��Ÿ��(7,5,3,5,20)")] internal float[] skillCooltime;
    //���������� ü���� ����� �ð�
    private float preRegenTime;

    private ProjectileLuncher launcher;

    public List<Skill> skills;
    internal List<GameObject> skillObjects;
    //�����Ҽ� �ִ��� �Ǵ��ϱ����� bool����
    private bool attackAble;

    //��� ��� �ð� ������ ����
    internal float originalAttackInterval;
    internal float originalRegenInterval;

    private Enemy targetEnemy = null;
    private float targetDistance = float.MaxValue;

    //���ݷ� ������ ���� ���ݷ�
    internal float originalDamage;

    private void Awake()
    {
        //�ٸ� ��ü�� ������ �� �ֵ��� ���ӸŴ����� �÷��̾ ������Ʈ�� ����
        //if (GameManager.Instance.player != null)
            GameManager.Instance.player = this;
        //else
        //    Debug.Log("���ӸŴ����� �÷��̾ Null������");


        //if (UIManager.Instance.player != null)
            UIManager.Instance.player = this;
        //else
        //    Debug.Log("UI�Ŵ����� �÷��̾ Null������");

        skillCooltime = new float[] { 7, 5, 3, 5, 20 };

    }

    private void Start()
    {
        //���� ��Ÿ�� �����
        originalAttackInterval = attackInterval;
        originalRegenInterval = regenInterval;

        originalDamage = damage;


        //��ó�� ������ �� �ֵ��� ��ó �ʱ�ȭ
        launcher = GameObject.Find("ProjectileLauncher").GetComponent<ProjectileLuncher>();

        StartCoroutine(FireCoroutine());
        Debug.Log($"UI�Ŵ��� : {UIManager.Instance.name}");

        skillObjects = new List<GameObject>();

        foreach (Skill skill in skills)
        {
            GameObject skillobj = Instantiate(skill.skillPrefab);
            skillobj.transform.position = transform.position;
            skillobj.transform.SetParent(transform);
            skillobj.SetActive(false);

            skillObjects.Add(skillobj);
        }
    }

    private void Update()
    {
        HealthRegeneration();
        //UI�Ŵ������� ���������� ȣ���ҷ��ٰ� ������ �ȵǼ� �׸�
        //SetInterval();

        //if (GameManager.Instance.player == null)
        //    Debug.Log("���ӸŴ��� null");
        //if (UIManager.Instance.player == null)
        //    Debug.Log("UI�Ŵ��� null");

        //ActivateSkill(0);
    }

    public void TakeDamage(float damage)
    {
        //���ڷ� ���� damage��ŭ ü���� ����Ŵ
        health = health - damage;

        UIManager.Instance.PlayerHPRenewal();

        //���ҵ����� ü���� 0 ���϶��
        if (health <= 0)
        {
            //Death �޼��带 ������
            Death();
        }
    }

    //ü���� 0���ϰ� ������ ���� �� �޼���
    private void Death()
    {
        //������Ʈ�� ��Ȱ��ȭ
        //�� �ϸ� ���ӸŴ������� �̰� ���� ���ϴϱ� ū�ϳ�
        //��� �ִϸ��̼Ǹ� ����ϴ°ɷ�
        //Destroy(gameObject);
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

        if (maxHealth <= health)
            health = maxHealth;

        UIManager.Instance.PlayerHPRenewal();
        //���������� ȸ���� �ð��� �ʱ�ȭ����
        //�ʱ�ȭ ���ϸ� �������� ȸ���ؼ� power overwhelming
        preRegenTime = Time.time;
    }

    private float SerchTarget()
    {
        targetEnemy = null;
        targetDistance = float.MaxValue;

        //���ӸŴ����� enemy ����Ʈ���� ���� Ž��
        foreach (Enemy enemy in GameManager.Instance.enemies)
        {
            //enemmy�� ���������� null�̸� ���ܰ� �߻��ϹǷ� null�Ͻ� ������ �ǳʶ�
            if (enemy == null) { continue; }

            //foreach���� ��ȸ�� �� ���� enemy�� �÷��̾���� �Ÿ��� ����
            float distance = Mathf.Abs(enemy.transform.position.x - transform.position.x);

            //���� enemy���� �Ÿ��� ������ �Ÿ����� ������
            if (distance < targetDistance)
            {
                //Ÿ���� �����ϰ�, distance�� �ʱ�ȭ
                targetEnemy = enemy;
                targetDistance = distance;
                //Debug.Log($"���� ����� �� : {targetEnemy.name}");
                Debug.Log($"���� ����� �� : {Mathf.Abs(targetDistance)}");
            }
        }

        return targetDistance;
    }

    private IEnumerator FireCoroutine()
    {

        while (true)
        {
            SerchTarget();

            if (targetDistance <= attackRange)
                attackAble = true;
            else
                attackAble = false;
            Debug.Log($"AttackAble {targetDistance}");
            //Debug.Log($"�ڷ�ƾ ����� {GameManager.Instance.enemies.Count}");

            Debug.Log($"AttackAble : {attackAble}");

            //enemies ����Ʈ�� ���� ���ٸ� �Ʒ� �ڵ带 �������� ����
            if (GameManager.Instance.enemies.Count != 0)
            {
                if (targetDistance <= attackRange && attackAble == true)
                    launcher.Fire();
            }

            // 1�ʸ� �������� 1 / 1.1 �Ͻ� �� 0.9�ʸ��� �߻��ϰԵ� 
            yield return new WaitForSeconds(1 / attackInterval);
        }

    }

    private void SetInterval()
    {
        if (UIManager.Instance.is2xSpeed == true)
        {
            attackInterval = originalAttackInterval / 2;
            regenInterval = originalRegenInterval / 2;
        }
        else
        {
            attackInterval = originalAttackInterval;
            regenInterval = originalRegenInterval;
        }
    }

    public void ActivateSkill(int i)
    {
        //��ų�� Ȱ��ȭ���� ���� ���¶�� ��ų�� Ȱ��ȭ������
        //Debug.Log("�� ȣ��� �ù�");
        if (skillObjects[i].activeSelf == false)
            skillObjects[i].SetActive(true);
    }
}
