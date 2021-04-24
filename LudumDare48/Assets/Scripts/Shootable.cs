using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootable : MonoBehaviour
{

    public int currentHealth = 3;
    public void Damage()
    {
        gameObject.GetComponent<IDamageable<float>>().Damage(1);
    }
}
