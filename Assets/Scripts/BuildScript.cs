using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;


public class BuildScript : MonoBehaviour
{
    public BuildCard myCard;
    public string _name;
    public Sprite icon;
    public Vector2 cell_size;
    public bool canProduce;
    public Rigidbody2D Rb;
    public Collider2D col;
    public List<GroundScript> groundList;
    
    

    public void GetFeatures()
    {
        GetComponent<SpriteRenderer>().sprite = myCard.icon;
        cell_size = myCard.cell_size;
        GetComponent<BoxCollider2D>().size = new Vector2(myCard.cell_size.x * 0.32f, myCard.cell_size.y * 0.32f);

    }
    public void GetProductCards()
    {

    }
    public virtual void Activated()
    {

    }
    public virtual void DeActivated()
    {

    }
    
    
}
