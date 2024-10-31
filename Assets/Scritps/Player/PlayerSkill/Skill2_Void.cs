using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2_Void : MonoBehaviour
{
    public float damage;
    public float projectileSpeed;
    public float attackInterval;
    private float preAttackTime;

    public float fireInterval;

    public Skill2_Projectile skill2_Projectile;
    //��ȹ�� : õõ���� ������� õõ�� �̵��ϴ°ǰ���
    //�̵��ϴ� ������Ʈ�� �÷��̾� ����ü, enemy, ��ų 1,2,3,4 �� �ִµ� ������ �������� �������
    //�̵��ϸ� ������ �� �� ���ظ� �ֳ��� �ƴϸ� ��� ���� ��� ���ظ� �ֳ���
    //�� �� ���ظ� �ָ� ��� ��������� 10ȸ Ÿ�� ���ؼ� �������� ������ �������µ� ���� �������� �ϳ���
    //��� ���� ��� ���ظ� �ָ� ���ظ� �ִ� ���� ��� �ǳ���
    
    
    private void Start()
    {
        StartCoroutine(FireCoroutine());
    }

    public IEnumerator FireCoroutine()
    {
        while (true)
        {
            Skill2_Projectile proj = Instantiate(skill2_Projectile, transform.position, Quaternion.identity);
            proj.damage = this.damage;
            proj.projectileSpeed = this.projectileSpeed;
            proj.attackInterval = this.attackInterval;

            yield return new WaitForSeconds(fireInterval);
        }
    }
}
