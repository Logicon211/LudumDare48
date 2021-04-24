using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootable : MonoBehaviour
{

    public int currentHealth = 3;

    public void Damage()

    {

        //subtract damage amount when Damage function is called

        currentHealth -= 1;

        //Check if health has fallen below zero

        if (currentHealth <= 0)

        {

            //if health has fallen below zero, deactivate it 

            gameObject.SetActive(false);

        }

    }
}
