using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeExplodingEnemies : PowerUp
{

    private CraigController cc;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        cc = (GameObject.FindGameObjectWithTag("Player")).GetComponent<CraigController>();
        SetHealthCost(0.4f);
    }
    
    public override void PowerUpEffect()
    {
           //Turn on explosions
        //play some unique sound effect?
        cc.explodingEnemies = true;
    }

}