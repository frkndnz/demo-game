using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Product Card", menuName = "ProductCard")]
public class ProductCard : ScriptableObject
{

    public Sprite icon;
    public string _name;
    public GameManager.ProductType productType;
    public string description;

}
