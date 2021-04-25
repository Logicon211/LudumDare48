using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBotController : MonoBehaviour, IDamageable<float>, IKillable
{
    public float health = 100f;
    public float attackRange = 50f;

    public string targetTag = "Player";
    public string attackScriptName;

    float currentHealth;
    float currentAttackCooldown = 0f;
    float attackCooldown = 3f;

    Transform targetTransform;
    Animator animator;
    IAttack<Animator, GameObject> attack;

    public bool testKill = false;
    public bool testDamage = false;

    void Start()
    {
        currentHealth = health;
        attack = GetComponent<IAttack<Animator, GameObject>>();
        animator = GetComponentInChildren<Animator>();
        GameObject possibleTarget = GameObject.FindGameObjectWithTag(targetTag);
        targetTransform = possibleTarget.GetComponent<Transform>();
    }

    void Update()
    {
        // Cooldown check
        if(currentAttackCooldown <= 0)
        {
            // Range check
            if(Vector3.Distance(transform.position, targetTransform.position) >= attackRange)
            {
                PerformAttack();
            }
        }
    }

    void FixedUpdate()
    {
        if (currentAttackCooldown > 0f) currentAttackCooldown -= Time.deltaTime;
    }

    void PerformAttack()
    {
        // attack.Attack(animator, target);

        currentAttackCooldown = attackCooldown;
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0f) Kill();
    }

    public void Kill()
    {
        animator.SetTrigger("Death");
        NavMovement nav = GetComponent<NavMovement>();
        Rigidbody rb = GetComponent<Rigidbody>();
        if (nav != null) nav.Stop();
        if (rb != null) rb.velocity = Vector3.zero;
    }
}
