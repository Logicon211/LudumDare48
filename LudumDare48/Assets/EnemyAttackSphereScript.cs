using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackSphereScript : MonoBehaviour, IDamageable<float>
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DespawnBullet());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponentInParent<Player>();
        if (player != null)
        {
            print("Bullet ran into player");
            player.Damage(2);
        }
        else
        {
            print("Bullet ran into object");
        }
        print(other.gameObject);
        Destroy(gameObject);
    }

    public void Damage(float i)
    {
        Destroy(gameObject);
    }

    //If we don't collide with anything, let's despawn after 10 seconds to avoid laggin the game.
    private IEnumerator DespawnBullet()
    {
        yield return new WaitForSeconds(60f); ;
        print("despawning bullet due to time");
        Destroy(gameObject);   
    }
}
