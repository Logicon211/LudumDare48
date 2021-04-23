using System.Collections;
using System.Collections.Generic;
using Enemy.Interface;
using UnityEngine;

public class FossilFuelController : MonoBehaviour
{
    [SerializeField] private float maxDistance = 20f;
    [SerializeField] private float attackDistance = 40f;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float attackSpeed = 20f;
    [SerializeField] private float attackCooldown = 5f;
    [SerializeField] private GameObject gunHole;

    private GameObject player;
    private Transform playerTransform;
    private Transform gunHoleTransform;
    private bool move = false;
    private bool attack = false;
    private Vector3 moveDir = Vector3.zero;
    private FossilFuel controller;
    private float currentAttackCooldown;

    // Use this for initialization
    private void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        this.controller = this.gameObject.GetComponent<FossilFuel>();
        gunHoleTransform = gunHole.GetComponent<Transform>();
        currentAttackCooldown = attackCooldown;
    }
	
    // Update is called once per frame
    private void Update () {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist > maxDistance) {
            move = true;
        }
        if (dist < attackDistance)
        {
            attack = true;
        }

    }

    private void FixedUpdate() {
        Vector3 normal = (player.transform.position - transform.position).normalized;
        if (move) {
            moveDir = normal;	
        }
        if (attack) {
            controller.Attack(attackSpeed);
            ResetAttack();
        }
        controller.PlayAudio();
        controller.Move(moveDir.x * speed * Time.fixedDeltaTime, moveDir.y * speed * Time.fixedDeltaTime);
        Vector3 gunHoleNormal = (player.transform.position - gunHoleTransform.position).normalized;
        controller.Rotate(gunHoleNormal);
        moveDir = Vector3.zero;
        move = false;
    }

    private void ResetAttack()
    {
        attack = false;
    }

    public float GetAttackRange()
    {
        return attackDistance;
    }
}
