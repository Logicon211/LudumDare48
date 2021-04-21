using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUpgrade : PowerUp
{

    private CraigController cc;
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        cc = (GameObject.FindGameObjectWithTag("Player")).GetComponent<CraigController>();
        SetHealthCost(0.35f);
    }

    public override void PowerUpEffect()
    {
        cc.upgradeDamage();        
        //play some unique sound effect?
    }
}
