using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup_Spawner : MonoBehaviour
{

    public List<GameObject> PowerupPrefabs;
    public List<int> listIndex = new List<int>();
    private Hashtable NumberSpawnable = new Hashtable();

    private List<GameObject> SpawnerList = new List<GameObject>();
 
 
        

// Start is called before the first frame update
void Start()
    {
        NumberSpawnable.Add(0, 1);//Bullet Time
        NumberSpawnable.Add(1, 1);//Cooldown delay
        NumberSpawnable.Add(2, 99);//Damage
        NumberSpawnable.Add(3, 1);//Explosion
        NumberSpawnable.Add(4, 3);//FireRate
        NumberSpawnable.Add(5, 1);//HealthRegen
        NumberSpawnable.Add(6, 3);//HeatCost
        NumberSpawnable.Add(7, 1);//Knockback
        NumberSpawnable.Add(8, 3);//Mystery Box
        NumberSpawnable.Add(9, 99);//Speed
        NumberSpawnable.Add(10, 99);//Spread
        NumberSpawnable.Add(11, 4);//HealthMax
        NumberSpawnable.Add(12, 2);//Accuracy
        NumberSpawnable.Add(13, 2);//Volume



        foreach (GameObject fooObj in GameObject.FindGameObjectsWithTag("PowerUpSpawner"))
        {
            SpawnerList.Add(fooObj);
        }


        foreach (GameObject fooObj in SpawnerList)
        {
            SpawnPowerUp(fooObj);
        }


    }

    


    public GameObject SpawnPowerUp(GameObject objectIn)
    {
        int powerup = (int) Mathf.Floor(Random.Range(0, listIndex.Count));
        int powerupIn = listIndex[powerup];
         
        

        GameObject instantiatedPowerup = Instantiate(PowerupPrefabs[powerupIn], objectIn.transform.position, Quaternion.identity);
        //instantiatedPowerup.transform.localScale = new Vector3(1f, 1f, 1f);

        NumberSpawnable[powerupIn] = (int) NumberSpawnable[powerupIn] - 1;
                    
        if((int)NumberSpawnable[powerupIn] == 0)
        {
            listIndex.Remove(powerupIn);
        }
        return instantiatedPowerup;
    }

        
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
