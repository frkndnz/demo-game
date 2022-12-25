using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProducePanel : MonoBehaviour
{
    public static ProducePanel scr;
    private void Awake()
    {
        scr = this;
    }
    public List<ProductButton> productList;
    public List<BuildCard> cardList;
    public void ProductsActive(int productCost)
    {
        for (int i = 0; i < productList.Count; i++)
        {
            productList[i].transform.parent.gameObject.SetActive(false);
        }

        for (int i = 0; i < productCost; i++)
        {
            productList[i].build = cardList[i];
            productList[i].GetFeatures();
            productList[i].transform.parent.gameObject.SetActive(true);

        }
        UIManager.scr.ProducePanel(true);
    }
    public void ResetCards()
    {
        cardList.Clear();
    }
}
