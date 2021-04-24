using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMovement : MonoBehaviour
{

    public string targetTag = "Player";

    public NavMeshAgent agent;
    public GameObject target;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            GameObject gameObject = GameObject.FindGameObjectWithTag(targetTag);
            if (gameObject != null) target = gameObject;
        }
        if (target != null)
        {
            Transform goal = target.transform;
            agent.destination = goal.position;
        }
    }
}
