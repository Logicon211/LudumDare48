using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableItemBase : MonoBehaviour, IDamageable<float>, IKillable
{

    public float health;
    float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(float damageTaken)
    {
        currentHealth -= damageTaken;
        if (currentHealth <= 0f) Kill();
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
