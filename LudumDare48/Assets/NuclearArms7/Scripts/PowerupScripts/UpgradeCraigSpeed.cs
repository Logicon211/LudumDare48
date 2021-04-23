using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCraigSpeed : PowerUp
{

    private CraigController cc;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        cc = (GameObject.FindGameObjectWithTag("Player")).GetComponent<CraigController>();
        SetHealthCost(0.2f);
    }
    
    public override void PowerUpEffect()
    {
        cc.upgradeCraigSpeed();
        //play some unique sound effect?
    }

}