using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAttack : MonoBehaviour, IAttack<GameObject>
{

    public float cooldown = 5f;
    public void Attack(GameObject position, GameObject target)
    {
        Debug.Log("Testing Attack");
    }

    public float GetCooldown()
    {
        return cooldown;
    }

}
