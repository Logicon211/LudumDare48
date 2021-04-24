using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase: MonoBehaviour, IDamageable<float>, IKillable
{
    public float health = 100f;
    public float speed = 1f;
    public float attackRange = 5f;

    public string targetTag = "Player";
    public GameObject target;

    Transform targetTransform;

    float currentHealth;

    public bool testKill = false;
    public bool testDamage = false;

    void Start()
    {
        currentHealth = health;
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
    }

    void Attack()
    {

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

    void Move()
    {

    }
}
