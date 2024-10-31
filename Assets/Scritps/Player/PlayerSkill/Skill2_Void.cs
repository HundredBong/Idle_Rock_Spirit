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
    //기획서 : 천천히가 어느정도 천천히 이동하는건가요
    //이동하는 오브젝트가 플레이어 투사체, enemy, 스킬 1,2,3,4 가 있는데 누구를 기준으로 잡을까요
    //이동하며 닿으면 한 번 피해를 주나요 아니면 닿는 동안 계속 피해를 주나요
    //한 번 피해를 주면 얘는 재수없으면 10회 타격 못해서 없어지는 조건이 없어지는데 언제 없어져야 하나요
    //닿는 동안 계속 피해를 주면 피해를 주는 텀은 어떻게 되나요
    
    
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
