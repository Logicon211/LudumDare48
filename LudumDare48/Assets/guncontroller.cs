using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Characters.FirstPerson;

public class guncontroller : MonoBehaviour { 

    private float reloadSpeed = 0.8f;
    private float reloadCooldown = 0f;
    private Animator gunAnimator;
    private CharacterController FPcontrollerScript;
    private AudioSource gunSound;

    // Start is called before the first frame update
    void Start()
    {
        gunAnimator = GetComponent<Animator>();
        FPcontrollerScript = GetComponentInParent<CharacterController>();
        gunSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (reloadCooldown <= 0f && CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            print(reloadCooldown);
            shoot();
        }
            gunAnimator.SetBool("isWalking", (FPcontrollerScript.velocity.magnitude > 0 && FPcontrollerScript.isGrounded));
    }

    void FixedUpdate()
    {
        if(reloadCooldown > 0f)
        {
            reloadCooldown = reloadCooldown - Time.fixedDeltaTime;
            
        }
    }
    
    void shoot()
    {
        reloadCooldown = reloadSpeed;
        gunAnimator.SetTrigger("Shoot");
        gunSound.Play();
        //todo, spawn bullet, start reload cooldown, set animation

    }

}
