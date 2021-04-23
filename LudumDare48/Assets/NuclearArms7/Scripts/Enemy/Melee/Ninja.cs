using System.Collections;
using System.Collections.Generic;
using Enemy.Interface;
using UnityEngine;


public class Ninja: MonoBehaviour, IDamageable<float>, IKillable, IEnemy
{
    
    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;
    [SerializeField] private float health = 10f;
    [SerializeField] private float shootCooldown = 5f;
    [SerializeField] private float attackDamage = 5f;
    [SerializeField] private float maxRangeToLandAttack = 7f;
    public Animator animator;

    public GameObject explosion;
    public GameObject poofEffect;
    public GameObject hitEffect;
    public AudioClip attackSound;

    public RoomController roomController;
	
    private AudioSource audio;
    private Rigidbody2D enemyBody;
    private Vector3 velocity = Vector3.zero;
    private float currentCooldown;
    private GameManager gameManager;
    private GameObject player;
    private EnemyController enemyController;

    private float currentHealth;
    private float attackDuration;
    private SpriteRenderer spriteRenderer;

    private bool isDead = false;
    private bool isAttacking = false;

    private Transform shootPosition;
    public Transform explodeLocation;

    public GameObject[] debris;
    
    private void Awake() {
        enemyBody = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemyController = GetComponent<EnemyController>();
        currentCooldown = 2f;
    }
    
    private void Start()
    {
        Instantiate(poofEffect, transform.position, Quaternion.identity);
        currentHealth = health;
    }

    private void Update()
    {

    }

    public void Damage(float damageTaken)
    {
        currentHealth -= damageTaken;
        if (currentHealth <= 0f && !isDead)
            Kill();
        if (health > currentHealth) 
        {
            var healthPercentage = currentHealth/health;
            spriteRenderer.color = new Color(1f, healthPercentage, healthPercentage);
        }
    }
    
    public void Kill()
    {
        if(!isDead) {
            isDead = true;
            if(roomController) {
                roomController.DecrementAliveEnemyCount();
            }

            float debrisForce = 1000f;
            float debrisTorque = 500f;
            CraigController craig = player.GetComponent<CraigController>();
            if(craig.explodingEnemies) {
                Instantiate(explosion, explodeLocation.position, Quaternion.identity);
                //Damage other enemies here:
                debrisForce += 500f;
                debrisTorque += 500f;

                Collider2D[] hitColliders = Physics2D.OverlapCircleAll(explodeLocation.position, 8f);
                foreach(Collider2D collider in hitColliders) {
                    IDamageable<float> damageable = collider.GetComponent<IDamageable<float>>();
                    if(damageable != null) {
                        damageable.Damage(craig.explodingEnemyDamage);
                    }
                }
            } else {
                Instantiate(poofEffect, explodeLocation.position, Quaternion.identity);
            }
            foreach(GameObject debrisPiece in debris) {
                GameObject part = Instantiate(debrisPiece, explodeLocation.position, Quaternion.identity);
                Rigidbody2D rb = part.GetComponent<Rigidbody2D>();
                Vector3 velocity = new Vector2(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
                velocity.Normalize();
                rb.AddForce(velocity * debrisForce);
                rb.AddTorque(Random.Range(0f, debrisTorque));
            }
            Destroy(gameObject);
        }
    }

    public void Move(float tarX, float tarY)
    {
        Vector3 targetVelocity = new Vector2(tarX * 10f, tarY * 10f);
        enemyBody.velocity = Vector3.SmoothDamp(enemyBody.velocity, targetVelocity, ref velocity, movementSmoothing);
    }
    
    public void Rotate(Vector3 direction)
    {
        if (direction != Vector3.zero) 
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }
    }

    public void StopMove() {
        //Nothing
    }

    public void StopAttack() {
       //nothing
    }

    public void Attack(float attackSpeed)
    {
        animator.SetTrigger("Attack");
        audio.PlayOneShot(attackSound);
    }

    public void FinishPunch()
    {
        if(Vector2.Distance(player.transform.position, transform.position) <= maxRangeToLandAttack) {
            //TODO: Do damage to player
            var craigController = player.gameObject.GetComponent<CraigController>();
            craigController.Damage(attackDamage);
            Instantiate(hitEffect, new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - 0.1f), Quaternion.identity);
        }
    }
}
