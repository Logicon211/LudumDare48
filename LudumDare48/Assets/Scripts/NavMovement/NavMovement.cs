using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMovement : MonoBehaviour
{

    public string targetTag = "Player";
    public float speed = 10;

    bool stopped = false;

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
                animationController.SetBool("moving", true);
                animationController.SetFloat("walkMoveSpeed", speed / 2);
            }
            else animationController.SetBool("moving", false);
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
        return agent.velocity != new Vector3(0, 0, 0);
    }

}
