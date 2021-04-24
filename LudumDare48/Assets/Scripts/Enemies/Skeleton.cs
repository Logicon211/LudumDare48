using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour, IDamageable<float>, IKillable
{

    public float health = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(float damage)
    {
        health -= damage;
        Debug.Log("This bitch took " + damage + " and now has " + health);
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
