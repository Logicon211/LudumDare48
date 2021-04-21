using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeHeatCooldownDelay : PowerUp
{

    private CraigController cc;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        cc = (GameObject.FindGameObjectWithTag("Player")).GetComponent<CraigController>();
        SetHealthCost(0.3f);
    }

    public override void PowerUpEffect()
    {
        cc.upgradeHeatCooldownDelay();
            //play some unique sound effect?
    }
}
