using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerBase: MonoBehaviour, IDamageable<float>, IKillable
{
    public float health = 100f;
    public float speed = 1f;
    public float attackRange = 5f;

    public string targetTag = "Player";
    public string attackScriptName;
    public GameObject target;

    Transform targetTransform;
    float currentHealth;
    float currentAttackCooldown = 0f;

    IAttack<GameObject> attack;

    public bool testKill = false;
    public bool testDamage = false;

    void Start()
    {
        currentHealth = health;
        attack = GetComponent<IAttack<GameObject>>();
        if (attack != null) currentAttackCooldown = attack.GetCooldown();
    }

    void Update()
    {
        // Search for player if target is null
        if (target == null) {
            GameObject possibleTarget = GameObject.FindGameObjectWithTag(targetTag);
            if (possibleTarget != null)
            {
                target = possibleTarget;
                targetTransform = target.GetComponent<Transform>();
            }
        }
        PerformAttack();

        // update values
        if (currentAttackCooldown > 0f) currentAttackCooldown -= Time.deltaTime;

    }

    void PerformAttack()
    {
        if (Vector3.Distance(transform.position, targetTransform.position) >= attackRange) return;
        if (attack != null && currentAttackCooldown <= 0f)
        {
            attack.Attack(gameObject, target);
            currentAttackCooldown = attack.GetCooldown();
        }
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0f) Kill();
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
