using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill3_Projectile : MonoBehaviour
{
    internal float damage;
    internal float duration;

    internal Vector3 rendererStartPos;
    private CircleCollider2D coll;
    private Transform rendererTransform;

    [SerializeField, Header("���������� ��ƼŬ")] private ParticleSystem particlePrefabHit;

    private void Awake()
    {
        coll = GetComponent<CircleCollider2D>();
        //�浹üũ ���Ҳ��ϱ� ��Ȱ��ȭ
        coll.enabled = false;
        //������ ��ġ ������ ���� transform�� ������ ������
        rendererTransform = transform.Find("Renderer");
    }

    //������ ��ġ���� ���� �ð� �Ŀ� ���� �������� ������ ������� �ְ� �����
    private void Start()
    {

        StartCoroutine(Explosion());
        //StartCoroutine(DisableCoroutine());
        Destroy(gameObject, duration + 0.1f);
    }

    IEnumerator Explosion()
    {
        float startTime = Time.time;
        float endTime = startTime + duration;
        rendererTransform.localPosition = rendererStartPos;

        //�̻����� �����ϱ� �� �������� ��ġ�� �ִϸ��̼�ó�� �̵���Ű�� ����
        while (Time.time < endTime) //endTime�� �̻����� �����ϴ� �ð��� �������� ����
        {//��, �̻����� �������� �ִϸ��̼��� ���ӽð��� �ǹ���
            yield return null; //�����Ӵ� 1ȸ�� �ݺ�

            //�� ������Ʈ�� ������ ���� ����� �ð�
            //���� �ð� - ���۵� �ð����� ����ð��� ����
            //���� 12�ʰ�, startTime�� 11�ʶ�� 1�� ����߰���
            float currentTime = Time.time - startTime;
            float duration = this.duration;

            //�ִϸ��̼��� ���� ���¸� 0�� 1������ ������ ��ȯ��
            float t = currentTime / duration;
            //Lerp �޼��带 ����ؼ� �������� ���������� ���� ������ ���� ��ġ���
            //t���� ���� ��ġ�� ��ȭ��, �� �ִϸ��̼��� ����ɼ��� �������� ��ġ�� �̵���
            Vector2 curRendPos = Vector2.Lerp(rendererStartPos, Vector2.zero, t);

            //���� ��ġ�� �������� ���� ���������� ������.
            //�̸� ���ؼ� �������� �̵��ϴµ��� ȿ���� ��
            rendererTransform.localPosition = curRendPos;

        }
        //�̻����� ������ ������ �������� coll.radius ũ���� �� �ȿ� �ִ� ��� �ݶ��̴��� ����
        Collider2D[] contactedColls = Physics2D.OverlapCircleAll(transform.position, coll.radius);

        foreach (Collider2D contactedColl in contactedColls)
        {
            Debug.Log($"Contacted name :  {contactedColl.name}");
            Debug.Log($"Missiles radius : {coll.radius}");
            if (contactedColl.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(damage);
            }
        }


        ParticleSystem parHit = Instantiate(particlePrefabHit);
        parHit.transform.position = new Vector2(transform.position.x, transform.position.y - 0.5f);
        parHit.Play();


        Destroy(gameObject);
    }
}
