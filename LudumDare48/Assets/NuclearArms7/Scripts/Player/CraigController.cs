using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraigController : MonoBehaviour
{

    private Rigidbody2D PlayerRigidBody;
    private Vector3 velocity = Vector3.zero;
    private float horizontalMove;
    private float verticalMove;

    public float maxHealth = 200f;
    public float health = 200f;
    private float energy = 0f;
    private float bulletVelocity = 50f;

    public GameObject bullet;
    public AudioClip hurtSound;
    public AudioClip shotgunSound;

    public GameObject gameControllerObject;
    private GameManager gameManager;

    private bool LeftClickDown;
    private bool LeftClick;
    private bool RightClick;
    private bool RightClickRelease;
    private bool RightClickDown;
    private float punchCharge;
    Vector2 direction;
    private bool SpacebarDown;
    private bool Dodging;
    private Vector2 DodgeDirection;
    private Vector2 NewPos;
    private float dodgeCooldown;
    Animator animator;
    private bool punchCharging;
    private Vector2 punchChargeDirection;
    private bool punching;
    private float punchCooldown;
    private Vector2 punchDirecion;

    private bool usingShotgun = true;

    private AudioSource AS;
    private GameObject particles;

    private Transform shootPosition;
    public AudioSource GunSizzlan;

    private int BASE_ANIMATION_LAYER = 0;
    private int SHOTGUN_ANIMATION_LAYER = 1;
    private int LAZER_ANIMATION_LAYER = 2;


    private float heat = 0;
    private int heatMax = 100;
    private float cooldown = 0;
    private float heatcooldowndelaytimer;
    private float heatCooldownAccelBase = 0.2f;
    private float heatCooldownAccel = 0f;

    //Modular upgrade
    private int spread = 1;
    private float fireRate = 0.2f;
    private float damage = 3f;
    private float heatCost = 5f;
    private float playerspeed = 12f;
    private float accuracy = 8f;
    

    //Bullet speed
    //One time upgrades:
    private bool knockback = false;
    private bool heatCooldownDelay = false;
    public bool bulletTime = false;
    private bool healthRegenUp = false;
    public bool explodingEnemies = false;

    public float bulletTimeEffect = 0.6f;
    public float explodingEnemyDamage = 10f;

    //Joke upgrades
    //Gun sound
    //Jeans

    HealthBar healthbar;
    CooldownBar cooldownBar;
    GameObject radiationEffect;

    public float transitionSpeed = 2f;
    private float preTransitionSpeed = 10f;

    public float healthRegenPerTransition = 0.5f;
    private float regenAmountRemaining = 0f;
    private bool isRegenerating = false;

    private bool isCoolingDown = false;


    // Use this for initialization
    void Start()
    {
        if (gameControllerObject == null)
        {
            gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        }
        gameManager = gameControllerObject.GetComponent<GameManager>();
        PlayerRigidBody = this.GetComponent<Rigidbody2D>();
        Dodging = false;
        NewPos = transform.position;
        animator = GetComponent<Animator>();
        AS = GetComponent<AudioSource>();
        shootPosition = transform.Find("ShootPosition");
        particles = transform.Find("ParticleEffect").gameObject;
        animator.SetLayerWeight(BASE_ANIMATION_LAYER, 0f);
        animator.SetLayerWeight(SHOTGUN_ANIMATION_LAYER, 100f);
        animator.SetLayerWeight(LAZER_ANIMATION_LAYER, 0f);

        healthbar = GameObject.FindGameObjectWithTag("Health Bar").GetComponent<HealthBar>();
        cooldownBar = GameObject.FindGameObjectWithTag("Cooldown Bar").GetComponent<CooldownBar>();
        radiationEffect = GameObject.FindGameObjectWithTag("RadiationEffect");
        if(radiationEffect) {
            radiationEffect.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

        //Get Movement unputs
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
        //Get Action inputs
        LeftClickDown = Input.GetButton("Fire1");
        LeftClick = Input.GetButton("Fire1");
        RightClick = Input.GetButton("Fire2");
        RightClickRelease = Input.GetButtonUp("Fire2");
        RightClickDown = Input.GetButtonDown("Fire2");
        SpacebarDown = Input.GetButtonDown("Jump");

        //Player rotation to mouse          
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        direction = Camera.main.ScreenToWorldPoint(mousePos) - transform.position;
        direction.Normalize();

        PlayerRigidBody.mass = 1;


        if (direction != Vector2.zero)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }
        else
        {
            PlayerRigidBody.freezeRotation = false;
        }

        // if(SpacebarDown == true && !Dodging){
        //     Dodging = true;
        //     dodgeCooldown = 0.25f;
        // 	DodgeDirection = NewPos;
        // 	DodgeDirection.Normalize ();
        // }


        //PlaYer movement
        //if (!Dashing && !Dodging)
        //{
        NewPos = Vector3.Normalize(new Vector2(horizontalMove * playerspeed, verticalMove * playerspeed));
        PlayerRigidBody.velocity = playerspeed * NewPos;
        if (horizontalMove != 0f || verticalMove != 0f)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        //}

        // if(Dodging){
        //     PlayerRigidBody.velocity = 35 * DodgeDirection;
        // 	dodgeCooldown -= Time.fixedDeltaTime;
        //     if(dodgeCooldown <= 0){
        //         Dodging=false;
        //     }

        // }


        if (LeftClickDown && !gameManager.IsPaused())
        {
            ShootShotgun();
            
        }
        
        if(!gameManager.IsPaused())
        {
            if (heatcooldowndelaytimer > 0)
            {
                heatcooldowndelaytimer = heatcooldowndelaytimer - Time.deltaTime;

            }
            else if (heat > 0)
            {
                heatCooldownAccel = heatCooldownAccel + (0.4f * Time.deltaTime);
                if (heat - heatCooldownAccel < 0)
                {
                    heat = 0;
                }
                else
                {
                    heat = heat - heatCooldownAccel;
                }
            }
        }

        cooldown = cooldown -Time.deltaTime;
        float heatPercentage = heat / heatMax;
        cooldownBar.SetCooldown(heatPercentage);
        GunSizzlan.volume = (0.5f * (heatPercentage));

        if(heat >= 100) {
            isCoolingDown = true;
        }

        if(heat <= 0) {
            isCoolingDown = false;
        }

        if(isRegenerating && regenAmountRemaining > 0f) {
            float regenAmount = Time.deltaTime * 20f;
            regenAmountRemaining -= regenAmount;
            if(regenAmountRemaining <= 0f) {
                regenAmountRemaining = 0f;
                isRegenerating = false;
            }
            health += regenAmount;
            if(health > maxHealth) {
                health = maxHealth;
            }
            healthbar.SetHealth(health/maxHealth);
        }

    }


    private void FixedUpdate()
    {

    }

    public void Damage(float damageTaken)
    {
        health -= damageTaken;
        if(damageTaken > 0f) {
            AS.PlayOneShot(hurtSound);
        }
        healthbar.SetHealth(health/maxHealth);
    }

    public float GetHealth()
    {
        return health;
    }



    public void PickupHealth()
    {
        health += 15f;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }




    public void ShootShotgun()
    {
        if (cooldown <= 0 && !isCoolingDown)
        {
            //Rotate shoot position so its pointing towards the mouse
            Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 gunHoleNormal = (mousePoint - shootPosition.position).normalized;
            shootPosition.rotation = Quaternion.LookRotation(Vector3.forward, gunHoleNormal);
            shootPosition.rotation *= Quaternion.Euler(Vector3.forward * 90);


            //Spread = 1;
            //fireRate = 1.5f;
            //damage = 3f;
            //heatCooldownAccel = 1;
            //heatCost = 5f;
            //craigSpeed = 5f;
            //bulletVolume = 50f;

            for (int i = 0; i<spread; i++) {
            GameObject projectileLaunched = Instantiate(bullet, shootPosition.position, shootPosition.rotation) as GameObject;
            projectileLaunched.GetComponent<BulletController>().setValues(damage, knockback);
            projectileLaunched.transform.Rotate(0, 0, Random.Range(-accuracy, accuracy));
            projectileLaunched.GetComponent<Rigidbody2D>().velocity = projectileLaunched.transform.right * (bulletVelocity);

        }
            animator.SetTrigger("ShootGun");
            AS.PlayOneShot(shotgunSound);
            heat += heatCost;
            heat = Mathf.Min(heat, heatMax);
            //Debug.Log(heat);

            

            cooldown = fireRate + (0.35f * (heat/heatMax));
            if (!heatCooldownDelay)
            {
                heatcooldowndelaytimer = 1f;
            }
            else
            {
                heatcooldowndelaytimer = 0.5f;
            }
            heatCooldownAccel = heatCooldownAccelBase;


        }//End of cooldown
    }


    //Spread = 1;
    //fireRate = 1.5f;
    //damage = 3f;
    //heatCooldownAccel = 1;
    //heatCost = 5f;
    //craigSpeed = 5f;
    //bulletVolume = 50f;

        //Modular upgrades
    public void upgradeSpread()
    {
        spread++;
        damage *= 0.8f;
    }
    public void upgradeFireRate()
    {
        fireRate = fireRate - 0.05f;
    }
    public void upgradeDamage()
    {
        damage++;
    }
    public void upgradeHeatCost()
    {
        heatCost = heatCost - 1f;
    }
    public void upgradeCraigSpeed()
    {
        playerspeed += 3f;
    }
    public void upgradeBulletVolume()
    {
        if (AS.volume + 0.3f < 1f)
        {
            AS.volume = AS.volume + 0.3f;
        }
        else
        {
            AS.volume = 1f;
        }
    }
    public void upgradeAccuracy()
    {
        if (accuracy - 3f >= 0f)
        {
            accuracy = accuracy - 3f;
        }
        else
        {
            accuracy = 0f;
        }
    }

    public void upgradeMaxHealth()
    {
        maxHealth = maxHealth + 50;
    }

    public void startTransition() {
        if(radiationEffect) {
            radiationEffect.SetActive(true);
        }
        preTransitionSpeed = playerspeed;
        playerspeed = transitionSpeed;

        isRegenerating = true;
        regenAmountRemaining = maxHealth * healthRegenPerTransition;

        if(healthRegenUp) {
            regenAmountRemaining += 40f;
        }
    }

    public void endTransition() {
        if(radiationEffect) {
            radiationEffect.SetActive(false);
        }
        playerspeed = preTransitionSpeed;
    }


    /**
     * One time upgrades
    private bool knockback = false;
    private bool heatCooldownDelay = false;
    private bool bulletTime = false;
    private bool healthRegenUp = false;
    private bool explodingEnemies = false;
    */
    public void upgradeKnockback()
    {
        knockback = true;
    }
    public void upgradeHeatCooldownDelay()
    {
        heatCooldownDelay = true;
    }
    public void upgradeBulletTime()
    {
        bulletTime = true;
    }
    public void upgradeHealthRegenUp()
    {
        healthRegenUp = true;
    }
    public void UpgradeExplodingEnemies()
    {
        explodingEnemies = true;
    }
    
}
