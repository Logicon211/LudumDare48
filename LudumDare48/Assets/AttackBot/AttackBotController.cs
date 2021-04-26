using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBotController : MonoBehaviour, IDamageable<float>, IKillable
{
    private float health = 3f;
    private float attackRange = 50f;

    private string targetTag = "Player";

    float currentHealth;
    float currentAttackCooldown = 0f;
    float attackCooldown = 2f;

    Transform targetTransform;
    Animator animator;
    IAttack<Animator, GameObject> attack;

    public bool testKill = false;
    public bool testDamage = false;
    public GameObject enemyAttackSphere;
    public Transform rightGunArm;
    public Transform leftGunArm;
    public LayerMask layerMask;
    public AudioSource attackAudio;
    private Rigidbody rigidbody;

    void Start()
    {
        currentHealth = health;
        attack = GetComponent<IAttack<Animator, GameObject>>();
        animator = GetComponentInChildren<Animator>();
        GameObject possibleTarget = GameObject.FindGameObjectWithTag(targetTag);
        targetTransform = possibleTarget.GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position;
        Vector3 rayTarget = transform.forward * 50;
        // Cooldown check
        if (currentHealth > 0) {
            if (currentAttackCooldown <= 0)
            {
                print("cooldown check passed");
                // Range check
                if (Vector3.Distance(transform.position, targetTransform.position) <= attackRange)
                {
                    print("distance check passed");
                    //RaycastHit hit;
                    //Vector3 rayOrigin = transform.position;
                    //Vector3 rayTarget = rayOrigin + new Vector3(50f, 0, 0);
                    if (Physics.Raycast(rayOrigin, rayTarget, out hit, 50f, layerMask))
                    {
                        Debug.DrawRay(rayOrigin, hit.point, Color.grey, 10f);
                        print("Raycast check passed");
                        if (hit.transform.tag == "Player")
                        {
                            PerformAttack();
                        }
                    }
                }
            }
        }
        Debug.DrawRay(rayOrigin, rayTarget, Color.cyan);
    }

    void FixedUpdate()
    {
        if (currentAttackCooldown > 0f) currentAttackCooldown -= Time.deltaTime;

        print("attack cooldown: " + currentAttackCooldown);
    }

    void PerformAttack()
    {
        attackAudio.Play();
        animator.SetTrigger("Attack");
        GameObject enemyattacksphere1 = Instantiate(enemyAttackSphere, leftGunArm.position, gameObject.transform.rotation) as GameObject;
        enemyattacksphere1.GetComponent<Rigidbody>().velocity = enemyattacksphere1.transform.forward * 10f;

        GameObject enemyattacksphere2 = Instantiate(enemyAttackSphere, rightGunArm.position, gameObject.transform.rotation) as GameObject;
        enemyattacksphere2.GetComponent<Rigidbody>().velocity = enemyattacksphere2.transform.forward * 10f;

        currentAttackCooldown = attackCooldown;
    }

    public void Damage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0f)
        {
            rigidbody.isKinematic = true;
            Kill();
        }
        else
        {
            rigidbody.isKinematic = false;
            StartCoroutine(AfterShot());
        }
        
    }

    public void Kill()
    {
        animator.SetTrigger("Death");
        NavMovement nav = GetComponent<NavMovement>();
        Rigidbody rb = GetComponent<Rigidbody>();
        if (nav != null) nav.Stop();
        if (rb != null) rb.velocity = Vector3.zero;
    }

    private IEnumerator AfterShot()
    {
        yield return new WaitForSeconds(0.25f);
        rigidbody.isKinematic = true;
    }
}
