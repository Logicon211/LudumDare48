using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPowerUp : PowerUp
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        SetHealthCost(0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public override void PowerUpEffect()
    {
     //   throw new System.NotImplementedException();
    }

}
