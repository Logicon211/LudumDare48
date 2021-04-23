using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeFireRate : PowerUp
{
    private CraigController cc;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        cc = (GameObject.FindGameObjectWithTag("Player")).GetComponent<CraigController>();
        SetHealthCost(0.25f);
    }
    
    public override void PowerUpEffect()
    {
        cc.upgradeFireRate();
        //play some unique sound effect?
    }

}