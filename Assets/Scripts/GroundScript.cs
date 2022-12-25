using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour
{
    public bool hasBuilding;
    public bool hasSoldier;
    public bool hasSpawnPoint;
    public Vector2 pos;
    public bool walkable;
    public float gCost;
    public float hCost;
    public List<GroundScript> neighboursList;
    public GroundScript parent;
    public float fCost {
        get
        {
            return gCost + hCost;
        }
    }
    public void FindNeighbours()
    {
        GroundScript curGround;
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;
                tempRay = transform.position;
                tempRay.x += x * 0.32f;
                tempRay.y += y * 0.32f;

                hit = Physics2D.Raycast(tempRay, Vector2.zero, 10f, PlayerController.scr.layer);
                if (hit.collider != null)
                {
                    if (hit.collider.tag == "ground")
                    {
                        curGround = hit.transform.GetComponent<GroundScript>();
                        neighboursList.Add(curGround);
                    }
                }
            }
        }
    }


    RaycastHit2D hit;
    public Vector2 tempRay;

    public GameObject highLight;
    public GameObject spawnPoint;
    private SpriteRenderer mySprite;
    public SpriteRenderer s;
    public List<ColorSettings> ColorsList;
    [System.Serializable]
    public class ColorSettings
    {
        public HighlightType colorType;
        string _name;
        public Color color;
    }
    public enum HighlightType {none,type1,type2,type3,type4 }
    private void Awake()
    {
        mySprite =highLight.GetComponent<SpriteRenderer>();
        s= GetComponent<SpriteRenderer>();
    }
    public void SetHighlight(HighlightType type)
    {
        if (hasSpawnPoint)
            return;
        highLight.SetActive(true);
        mySprite.sortingOrder = 1;
        switch (type)
        {
            case HighlightType.none:
                highLight.SetActive(false);
                spawnPoint.SetActive(false);
                break;
            case HighlightType.type1:
                mySprite.color = ColorsList[0].color;
                break;
            case HighlightType.type2:
                mySprite.color = ColorsList[1].color;
                mySprite.sortingOrder = 5;
                
                break;
            case HighlightType.type3:
                mySprite.color = ColorsList[2].color;
                
                break;
            case HighlightType.type4:
                //mySprite.color = ColorsList[3].color;
                highLight.SetActive(false);
                spawnPoint.SetActive(true);
               
                break;

        }

    }
   

    
    
}
