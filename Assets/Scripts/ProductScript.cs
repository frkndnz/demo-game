using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductScript : MonoBehaviour
{
    public string _name;
    public Sprite icon;
    public SpriteRenderer renderer;
    public Vector2 cell_size;
    public bool canMove;
    public Rigidbody2D Rb;
    public Collider2D col;
    public List<GroundScript> groundList;
    public GroundScript mytestGround;

    public void GetFeatures()
    {
        GetComponent<SpriteRenderer>().sprite = icon;
        GetComponent<BoxCollider2D>().size = new Vector2(cell_size.x * 0.32f, cell_size.y * 0.32f);

    }

    public virtual void Activated()
    {

    }
    public virtual void DeActivated()
    {

    }
}
