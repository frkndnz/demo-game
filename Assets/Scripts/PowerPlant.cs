using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPlant : BuildScript
{

    public void Start()
    {
        GetFeatures();
    }


    public override void Activated()
    {
        base.Activated();
        InfoScript.scr.SetInfo(GameManager.BuildType.PowerPlant);
    }
    public override void DeActivated()
    {
        base.DeActivated();
    }
}
