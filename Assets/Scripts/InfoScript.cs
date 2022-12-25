using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoScript : MonoBehaviour
{
    public static InfoScript scr;
    public Image icon;
    public Text description;

   

    
    public ProductButton build;
    public List<ProductButton> productList;
    public List<BuildCard> producecardList; // get builds
    private void Awake()
    {
        scr = this;
    }
    
    BarrackScript tempBarrack;
    PowerPlant tempPowerPlant;
    int productCost;
    BuildScript temp;
    public void SetInfo(GameManager.BuildType type)
    {
       // ResetCards();
        switch (type)
        {
            case GameManager.BuildType.Barrack:
                tempBarrack = GameManager.scr.barrackList[GameManager.scr.barrack_index].gameObject.GetComponent<BarrackScript>();
                temp = GameManager.scr.barrackList[GameManager.scr.barrack_index];
                build.build = tempBarrack.myCard;
                productCost = tempBarrack.MySoldierTypes.Count;
                for (int i = 0; i < tempBarrack.MySoldierTypes.Count; i++)
                {
                    producecardList.Add(tempBarrack.MySoldierTypes[i].soldierCard);
                }
                break;
            case GameManager.BuildType.PowerPlant:
                temp = GameManager.scr.powerplantList[GameManager.scr.powerplant_index];
                tempPowerPlant = GameManager.scr.powerplantList[GameManager.scr.powerplant_index].gameObject.GetComponent<PowerPlant>();
                build.build = tempPowerPlant.myCard;
                break;
           
        }
        build.GetFeatures();
        description.text = build.build.description;
        for (int i = 0; i < productList.Count; i++)
        {
            productList[i].transform.parent.gameObject.SetActive(false);
        }
        if (temp.canProduce)
        {
            for (int i = 0; i < productCost; i++)
            {
                productList[i].build = producecardList[i];
                productList[i].GetFeatures();
                productList[i].transform.parent.gameObject.SetActive(true);

            }

        }
        UIManager.scr.InformationButton(true);
        

    }
    public void ResetCards()
    {
        producecardList.Clear();
    }
}
