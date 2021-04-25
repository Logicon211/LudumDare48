using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BearAttack : MonoBehaviour, IAttack<Animator, GameObject>
{

    public float cooldown = 5f;
    public float attackRange = 5f;
    public float damage = 5f;
    public float damageDelay = 1f; // Delay from animation start to damage being dealt in case player moves away

    float delayCooldown = 0f;
    bool attacking = false;

    GameObject target;


    void FixedUpdate()
    {
        if (attacking)
        {
            if (delayCooldown > 0f)
            {
                delayCooldown -= Time.deltaTime;
            }
            if (delayCooldown <= 0F)
            {
                DealDamage();
            }
        }
    }

    public void Attack(Animator animator, GameObject target)
    {
        this.target = target;
        if (animator != null)
        {
            animator.SetTrigger("attacking");
        }
        attacking = true;
        delayCooldown = damageDelay;
    }

    void DealDamage()
    {
        if(Vector3.Distance(gameObject.transform.position, target.transform.position) <= attackRange + 1)
        {
            IDamageable<float> targetDamageable = target.GetComponent<IDamageable<float>>();
            if (targetDamageable != null) targetDamageable.Damage(damage);
            attacking = false;
        }
    }

    public float GetCooldown()
    {
        return cooldown;
    }

    public float GetAttackRange()
    {
        return attackRange;
    }

}
