using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack<T>
{
    void Attack(T gameObject, T targetObject);

    float GetCooldown();
}