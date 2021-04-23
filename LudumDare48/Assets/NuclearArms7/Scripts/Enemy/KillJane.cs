using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy.Interface;

public class KillJane : MonoBehaviour, IKillable, IDamageable<float>
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
    public GameObject teleportEffect;
    public AudioClip attackSound;

    public RoomController roomController;
	
    private AudioSource audio;
    private Rigidbody2D enemyBody;
    private Vector3 velocity = Vector3.zero;
    private GameManager gameManager;
    private GameObject player;
    private CraigController craigController;

    private float currentHealth;
    private float attackDuration;
    private SpriteRenderer spriteRenderer;

    private bool isDead = false;
    private bool isAttacking = false;

    private Transform shootPosition;

    //Enemy controller stuff
    //public float maxDistance = 20f;
    //public float attackDistance = 40f;
    //public float speed = 10f;
    public float attackSpeed = 20f;
    public float dashSpeed;
    public float attackCooldown = 5f;
    public float dashTime = 1f;

    public GameObject projectile;

    private Transform playerTransform;
    private Vector3 moveDir = Vector3.zero;

    private float currentAttackCooldown;
    private float currentCooldown;

    private bool dash = false;
    private bool attack = false;
    private bool isDashing = false;
    private bool teleport = false;
    private Vector2 dashVelocity;

    private bool dashNext = false;
    private bool dashNext2 = false;
    private bool dashNext3 = false;
    private bool attackNext = true;

    [SerializeField] private float timeBetweenVoiceLines = 20f;

    [Range(0, 50)] [SerializeField] private int amountOfBulletsAtATime = 1;
    [Range(0f, 360f)][SerializeField] private float projectileSpread = 25f;
    public Transform shotOriginatingLocation;
    [Range(1f, 100f)] [SerializeField] private float projectileSize = 1f;
    [Range(0f, 50f)][SerializeField] private float projectileSpeed = 1f;
    [Range(0f, 1f)][SerializeField] private float projectileSpawnRate = 0.2f;

    private bool inFinalPhase = false;
    private int attackCount = 0;
    private float currentProjectileCooldown;
    private float currentTimeBetweenVoiceLines;

    public AudioClip[] voiceLines;
    public AudioSource voiceAudioSource;


    private void Awake() {
        enemyBody = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
        craigController = player.GetComponent<CraigController>();
        currentCooldown = 2f;
        currentProjectileCooldown = projectileSpawnRate;
        currentTimeBetweenVoiceLines = timeBetweenVoiceLines/2;
    }
    
    private void Start()
    {
        Instantiate(poofEffect, transform.position, Quaternion.identity);
        currentHealth = health;
        player = GameObject.FindGameObjectWithTag("Player");
        //this.controller = this.gameObject.GetComponent<IEnemy>();
        currentAttackCooldown = attackCooldown;
    }

    private void Update()
    {
        currentAttackCooldown -= Time.deltaTime;
        if (currentAttackCooldown <= 0f)
        {
            if(!inFinalPhase) {
                if(attackNext) {
                    attack = true;
                    dashNext = true;
                    attackNext = false;
                }
                else if(dashNext) {
                    dash = true;
                    dashNext = false;
                    attackNext = true;
                }
            }
            else {
                if(attackCount >=4) {
                    attackCount = 0;
                    teleport = true;
                } else {
                    attack = true;
                    attackCount++;
                }


                //Teleport after 3 attacks
            }
            currentAttackCooldown = attackCooldown;
         
        }
    }

    private void FixedUpdate() {
        Vector3 playerNormal = (player.transform.position - transform.position).normalized;
        
        if(isDashing) {
            //Vector3 targetVelocity = new Vector2(tarX * 10f, tarY * 10f);
            enemyBody.velocity = dashSpeed * transform.up; //Vector3.SmoothDamp(enemyBody.velocity, dashVelocity, ref velocity, movementSmoothing);
//            Debug.Log(enemyBody.velocity);
        } else {
            enemyBody.velocity = Vector2.zero;
        }

        if (dash) {
            
            animator.SetBool("Dashing", true);
            StartCoroutine("StopDashing");
            isDashing = true;
            dash = false;
            
            // if(dashNext3){
            //     ResetAttack();
            // }

        }
        if (attack) {
            //Do some special stuff
          
            Attack(playerNormal.x * attackSpeed, playerNormal.y * attackSpeed);
            ResetAttack();
        }

        if (teleport) {
            Instantiate(teleportEffect, gameObject.transform.position, Quaternion.identity);
            gameObject.transform.position = roomController.PickSpawnPointNotOnPoint(transform.position);

            teleport = false;
        }
        Rotate(playerNormal);
        PlayAudio();
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

        if(currentHealth <= health/2 && !inFinalPhase) {
            inFinalPhase = true;
            attackCooldown = 0.46f;
            projectileSpread *=1.8f;
            projectileSpeed *= 0.9f;
            amountOfBulletsAtATime = Mathf.RoundToInt(amountOfBulletsAtATime * 1.5f);
            animator.SetBool("FinalForm", true);
        }
    }
    
    public void Kill()
    {
        if(!isDead) {
            isDead = true;
            Instantiate(explosion, transform.position, Quaternion.identity);
            if(roomController) {
                roomController.KillBoss();
            }
            Destroy(gameObject);
        }
    }

    // public void Move(float tarX, float tarY)
    // {
    //     Vector3 targetVelocity = new Vector2(tarX * 10f, tarY * 10f);
    //     enemyBody.velocity = Vector3.SmoothDamp(enemyBody.velocity, targetVelocity, ref velocity, movementSmoothing);
    // }
    
    public void Rotate(Vector3 direction)
    {

        if (direction != Vector3.zero) 
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }
    }

    public void Attack(float tarX, float tarY)
    {
        animator.SetTrigger("Attack");
        audio.PlayOneShot(attackSound);
        RegularSpreadAttack();
    }

    private void RegularSpreadAttack()
    {
        Quaternion rotation = transform.rotation;
        if (amountOfBulletsAtATime % 2 == 0)
        {
            var bulletAngle = projectileSpread / (amountOfBulletsAtATime / 2);
            var currentAngle = -projectileSpread;
            for (int i = 1; i <= amountOfBulletsAtATime; i++)
            {
                var bullet = Instantiate(projectile, shotOriginatingLocation.position, rotation) as GameObject;
                //bullet.transform.localScale = new Vector3(projectileSize, projectileSize);
                bullet.transform.Rotate(0, 0, currentAngle);
                bullet.GetComponent<Rigidbody2D>().velocity = projectileSpeed * bullet.transform.up;
                CheckForBulletTime(bullet);
                currentAngle += bulletAngle;
            }
            currentProjectileCooldown = projectileSpawnRate;
        }
        else
        {
            var bulletAngleIncrements = projectileSpread / (amountOfBulletsAtATime - 1);
            var currentAngle = -(projectileSpread / 2);
            for (int i = 1; i <= amountOfBulletsAtATime; i++)
            {
                var bullet = Instantiate(projectile, shotOriginatingLocation.position, rotation) as GameObject;
                //bullet.transform.localScale = new Vector3(projectileSize, projectileSize);
                bullet.transform.Rotate(0, 0, currentAngle);
                bullet.GetComponent<Rigidbody2D>().velocity = projectileSpeed * bullet.transform.up;
                CheckForBulletTime(bullet);
                currentAngle += bulletAngleIncrements;
            }
            currentProjectileCooldown = projectileSpawnRate;
        }
    }

    private void CheckForBulletTime(GameObject bullet)
    {
        if (craigController.bulletTime)
        {
            bullet.GetComponent<Rigidbody2D>().velocity *= craigController.bulletTimeEffect;
        }
    }

    //NOT CURRENTLY USED
    public void Dash(float tarX, float tarY)
    {
        if(dashNext)
        animator.SetBool("Dashing", true);
        StartCoroutine("StopDashing");
        dashVelocity = new Vector2(tarX, tarY);
        isDashing = true;
        //audio.PlayOneShot(attackSound);
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


    private void ResetAttack()
    {
        attack = false;
        dash = false;
    }

    // public float GetAttackRange()
    // {
    //     return attackDistance;
    // }

    public IEnumerator StopDashing() {
        yield return new WaitForSeconds(dashTime);
        animator.SetBool("Dashing", false);
        isDashing = false;
    }

    public void PlayAudio()
    {
        currentTimeBetweenVoiceLines -= Time.deltaTime;
        Debug.Log(currentTimeBetweenVoiceLines);
        if (currentTimeBetweenVoiceLines <= 0f)
        {
            int lineToPlay = Random.Range(1, voiceLines.Length);
            voiceAudioSource.PlayOneShot(voiceLines[lineToPlay]);
            currentTimeBetweenVoiceLines = timeBetweenVoiceLines;
        }
        
    }
}
