using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductButton : MonoBehaviour
{
    public BuildCard build;
    public Image myImage;
    public Text productText;
    public GameManager.BuildType buildType;
    public BarrackScript.SoldierType soldierType;
#if UNITY_EDITOR
    private void OnValidate()
    {
        myImage = GetComponent<Image>();
        myImage.sprite = build.icon;
        buildType = build.buildType;
        soldierType = build.soldierType;
        productText.text = build._name.ToString();
    }
#endif
   public void GetFeatures()
    {
        myImage = GetComponent<Image>();
        myImage.sprite = build.icon;
        buildType = build.buildType;
        soldierType = build.soldierType;
        productText.text = build._name.ToString();
    }
    public void SendInfo() // BUILD SEND INFO'S AT PLAYER
    {
        switch (buildType)
        {
            case GameManager.BuildType.Barrack:
                PlayerController.scr.SetValues(GameManager.scr.barrackList[GameManager.scr.barrack_index]);
                GameManager.scr.barrack_index++;
                break;
            case GameManager.BuildType.PowerPlant:
                PlayerController.scr.SetValues(GameManager.scr.powerplantList[GameManager.scr.powerplant_index]);
                GameManager.scr.powerplant_index++;
                break;
            case GameManager.BuildType.Soldier:
                break;
        }
        InfoScript.scr.SetInfo(buildType);

    }
    public void SpawnProduct() // CREATE PRODUCTS
    {
        switch (buildType)
        {
            case GameManager.BuildType.Barrack:
                GameManager.scr.curBarrack.SoldierSpawn(soldierType);
                break;
            case GameManager.BuildType.Soldier:
                break;
            case GameManager.BuildType.PowerPlant:
                break;
        }
    }
}
