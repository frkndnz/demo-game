using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public static PlayerController scr;
    public bool menuProductSelected;
    public bool gameProductSelected;
    public bool soldierSelected;
    public bool isSelected;
    public BuildScript build;
    public SoldierScript soldier;
    private void Awake()
    {
        scr = this;
    }
    
    
    RaycastHit2D hitMouse;
    RaycastHit2D hit;
    public LayerMask layer2;
    public LayerMask walkLayer;
    GameObject selectedObj;
    private void Update()
    {
        if (menuProductSelected) // IF BUILD IS SELECTED FROM THE PRODUCTION MENU
        {
            EmptyAreaFinder();
            PlaceBuild();

        }
        if (Input.GetMouseButtonDown(0)) // LEFT CLICK
        {
            hitMouse = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1f, layer2);
            if (hitMouse.collider != null) // ONGAMEBOARD
            {
                if (menuProductSelected)
                {
                    isSelected = true;
                    return;

                }
                else
                {
                    selectedObj = hitMouse.transform.gameObject;
                    if (selectedObj.GetComponent<BuildScript>()) // IF BUILD
                    {
                        if (build != null)
                        {
                            if (build != selectedObj.GetComponent<BuildScript>())
                            {
                                build.DeActivated();
                                build = selectedObj.GetComponent<BuildScript>();
                                build.Activated();
                            }
                        }
                        else
                        {
                            build = selectedObj.GetComponent<BuildScript>();
                            build.Activated();
                        }
                        if (selectedObj.tag == "soldier")
                        {
                            soldierSelected = true;
                            GameProductCancel();
                            if (soldier != null)
                            {
                                if(soldier!= selectedObj.GetComponent<SoldierScript>())
                                {
                                    soldier.DeActivated();
                                    soldier = selectedObj.GetComponent<SoldierScript>();
                                    soldier.Activated();
                                }
                            }
                            else
                            {

                                soldier = selectedObj.GetComponent<SoldierScript>();
                                soldier.Activated();
                            }
                        }
                        else
                        {
                            gameProductSelected = true;
                            SoldierCancel();
                        }
                    }
                    else if (selectedObj.GetComponent<GroundScript>()) //IF GROUND
                    {
                        isSelected = true;
                        if (soldierSelected)
                            SoldierCancel();
                        
                        
                    }
                }

            }
            else
            {
                MenuProductCancel();
                GameProductCancel();
                SoldierCancel();
            }



        }
        if (Input.GetMouseButtonDown(1)) // RIGHT CLICK
        {
            if (soldierSelected)
            {
                hitMouse = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1f, walkLayer);
                if (hitMouse.collider != null)
                {
                    if(hitMouse.collider.gameObject.tag=="ground" /* && hitMouse.transform.GetComponent<GroundScript>().walkable */)
                    {
                        soldier.GetPath(hitMouse.transform.GetComponent<GroundScript>());
                    }
                }
            }
            if (gameProductSelected)
            {
                GameProductCancel();
            }
            if (menuProductSelected)
            {
                MenuProductCancel();
            }
        }


        if (Input.GetMouseButtonUp(0))
            isSelected = false;


    }

   

    
    Vector2 mainRay;
    public Vector2[,] subrays;
    float pixel_size = 0.32f;
    Vector2 cell_size;
    public Vector2 tempRay;
    public LayerMask layer;
    public GameObject test;
    public List<GroundScript> grounds;
    private bool fieldFull;
    public BuildScript placeable;

    public void SetValues(BuildScript _placeable)
    {
        placeable = _placeable;
        cell_size = placeable.cell_size;
        menuProductSelected = true;
        GameProductCancel();
        SoldierCancel();

    }
    Vector2 lastPos, curPos;
    private void EmptyAreaFinder()// IF BUILD SELECTED EMPTY AREA CONTROL 
    {
        curPos = Input.mousePosition;
        if (lastPos == curPos)
            return;
        bool onGameBoard;
        GroundScript curGround;
        for (int i = 0; i < grounds.Count; i++)
        {
            grounds[i].SetHighlight(GroundScript.HighlightType.none);
        }
        grounds.Clear();
       
        subrays = new Vector2[(int)(cell_size.x), (int)(cell_size.y)];
        hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 1f, layer);
        if (hit.collider != null)
        {
            mainRay = hit.transform.position;
            onGameBoard = true;
        }
        else
        {
            onGameBoard = false;
            
        }
        fieldFull = false;
        for (int x = 0; x < cell_size.x; x++)
        {
            for (int y = 0; y < cell_size.y; y++)
            {
                tempRay.x = (-(int)(cell_size.x / 2f) + x) * pixel_size;
                tempRay.y = (-(int)(cell_size.y / 2f) + y) * pixel_size;
                tempRay += mainRay;
                hit = Physics2D.Raycast(tempRay, Vector2.zero, 10f, layer);
                if (hit.collider != null)
                {
                    curGround = hit.transform.GetComponent<GroundScript>();
                    if (hit.collider.tag == "ground")
                    {
                        if (!grounds.Contains(curGround))
                            grounds.Add(curGround);
                        if (!curGround.walkable || curGround.hasSoldier )
                            fieldFull = true;
                    }
                }
                else
                {
                    fieldFull = true;
                }
            }
        }
       
        for (int i = 0; i < grounds.Count; i++)
        {
            if (!fieldFull && onGameBoard)
            {
                grounds[i].SetHighlight(GroundScript.HighlightType.type1);
            }
            else if(onGameBoard)
            {
                grounds[i].SetHighlight(GroundScript.HighlightType.type2);
            }
            else
            {
                grounds[i].SetHighlight(GroundScript.HighlightType.none);
            }
        }
        lastPos = Input.mousePosition;
    }

    private void PlaceBuild() // PLACE THE SELECTED BUILD IN THE FIELD
    {
        if (isSelected && !fieldFull)
        {
            placeable.transform.position = grounds[0].transform.position + (grounds[grounds.Count - 1].transform.position - grounds[0].transform.position) / 2f;
            placeable.gameObject.SetActive(true);
            for (int i = 0; i < grounds.Count; i++)
            {
                placeable.groundList.Add(grounds[i]);
                grounds[i].walkable = false;
                grounds[i].hasBuilding=true;
                grounds[i].SetHighlight(GroundScript.HighlightType.none);
            }
            menuProductSelected = false;
            isSelected = false;
        }
    }
    
   
    private void MenuProductCancel()
    {
        if (grounds.Count > 0)
        {
            for (int i = 0; i < grounds.Count; i++)
                grounds[i].SetHighlight(GroundScript.HighlightType.none);
            menuProductSelected = false;
        }
        UIManager.scr.InformationButton(false);
    }
    public void GameProductCancel()
    {
        if (build)
            build.DeActivated();
        gameProductSelected = false;
        if (build)
        {
            UIManager.scr.InformationButton(false);
            UIManager.scr.ProducePanel(false);
        }
        build = null;
    }
    public void SoldierCancel()
    {
        if (soldier)
        {
            soldier.DeActivated();
            soldier = null;
        }
        soldierSelected = false;
    }

    

    
}
