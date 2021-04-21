using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy.Interface;

public class EnemyBulletController : MonoBehaviour
{

	public GameObject hitEffect;
	private float damage = 10f;
    private bool knockback;
    


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setValues(float damageIn, bool knockbackIn)
    {
        damage = damageIn;
        knockback = knockbackIn;
    }




	/// <summary>
	/// Sent when another object enters a trigger collider attached to this
	/// object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerEnter2D(Collider2D other)
	{   
        if (other.gameObject.tag == "Player") {
            CraigController craig = other.gameObject.GetComponent<CraigController>();
            craig.Damage(damage);
        }
		Instantiate(hitEffect, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
