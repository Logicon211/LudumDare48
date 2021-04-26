using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMovement : MonoBehaviour
{

    public string targetTag = "Player";
    public float speed = 10;

    bool stopped = false;

    public AudioSource movementAudio;
    public NavMeshAgent agent;
    public GameObject target;
    public Animator animationController;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animationController = GetComponentInChildren<Animator>();
        agent.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            GameObject gameObject = GameObject.FindGameObjectWithTag(targetTag);
            if (gameObject != null) target = gameObject;
        }
        // Updating target position
        if (target != null && !stopped)
        {
            Transform goal = target.transform;
            agent.destination = goal.position;
        }
        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        if (animationController)
        {
            if (IsMoving())
            {
                if (movementAudio != null)
                {
                    if (!movementAudio.isPlaying) movementAudio.Play(0);
                }
                animationController.SetBool("moving", true);
            }
            else
            {
                movementAudio.Stop();
                animationController.SetBool("moving", false);
            }
        }
    }

    public void Stop()
    {
        agent.speed = 0;
        agent.destination = gameObject.transform.position;
        stopped = true;

    }

    public bool IsMoving()
    {
        return agent.velocity.magnitude >= 1f;
    }

}
