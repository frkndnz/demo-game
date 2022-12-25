using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrackScript : BuildScript
{
    [Header("Barrack")]
    [Space(4)]
    public bool isActive;
    private GroundScript spawnPoint;
    public void Start()
    {
        GetFeatures();
        childCol.size = new Vector2((cell_size.x * 0.32f) + 0.16f, (cell_size.y * 0.32f) + 0.16f);
        

    }
    public enum SoldierType {  swordman, archer }
    public List<SoldierFeatures> MySoldierTypes;
    [System.Serializable]
    public class SoldierFeatures
    {
        public SoldierType _soldierType;
        public BuildCard soldierCard;
        public float _damage;
        public float _movementSpeed;
        public float _attackRange;
        public float _attackSpeed;


    }
    private void Update()
    {
       

        if (spawnPointSelection) // SPAWN POINT SELECTION
        {
            
            if (curGround )
                curGround.SetHighlight(GroundScript.HighlightType.type3);
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, 10f, PlayerController.scr.layer);
            if (hit.collider != null)
            {
                if (hit.collider.tag == "ground")
                {
                    curGround = hit.transform.GetComponent<GroundScript>();
                    if (spawnPointList.Contains(curGround) && curGround.walkable)
                    {
                        curGround.SetHighlight(GroundScript.HighlightType.type1);

                        if (PlayerController.scr.isSelected && PlayerController.scr.gameProductSelected)
                        {
                            spawnPointSelection = false;
                            if (spawnPoint)
                            {
                                spawnPoint.walkable = true;
                                spawnPoint.hasSpawnPoint = false;
                            }
                            spawnPoint = curGround;
                            for (int i = 0; i < spawnPointList.Count; i++)
                            {
                                spawnPointList[i].SetHighlight(GroundScript.HighlightType.none);
                            }
                            curGround.SetHighlight(GroundScript.HighlightType.type4);
                            curGround.hasSpawnPoint = true;
                            curGround.walkable = false;
                            curGround = null;
                            isActive = false;
                            PlayerController.scr.GameProductCancel();

                        }
                    }
                    else
                    {
                        curGround = null;
                        if (PlayerController.scr.isSelected && PlayerController.scr.gameProductSelected)
                        {
                            DeActivated();
                        }
                    }
                }
            }
        }
    }
    public BoxCollider2D childCol;
    public override void Activated()
    {
        base.Activated();
        if (isActive)
            return;
        isActive = true;
        
        GameManager.scr.curBarrack = this;
        SpawnPointArea();
        
        SendProductPanel();
        InfoScript.scr.SetInfo(GameManager.BuildType.Barrack);
    }
    private void SendProductPanel()
    {
        ProducePanel.scr.ResetCards();
        for (int i = 0; i < MySoldierTypes.Count; i++)
        {
            ProducePanel.scr.cardList.Add(MySoldierTypes[i].soldierCard);
        }
        ProducePanel.scr.ProductsActive(MySoldierTypes.Count);
    }
    

    public override void DeActivated()
    {
        base.DeActivated();
        if (!isActive)
            return;
        for (int i = 0; i < spawnPointList.Count; i++)
        {
            spawnPointList[i].SetHighlight(GroundScript.HighlightType.none);
        }
        isActive = false;
        spawnPointSelection = false;
        if(spawnPoint)
            spawnPoint.SetHighlight(GroundScript.HighlightType.type4);
        PlayerController.scr.GameProductCancel();
    }
   
    public List<GroundScript> spawnPointList;
    private bool spawnPointSelection;
    private GroundScript curGround;
    private RaycastHit2D hit;
    private void SpawnPointArea() // GET GROUNDS
    {
        
        childCol.enabled = false;
        for (int i = 0; i < spawnPointList.Count; i++)
        {
            if(!spawnPointList[i].hasBuilding)
                spawnPointList[i].SetHighlight(GroundScript.HighlightType.type3);
        }
        spawnPointSelection = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ground")
        {
            if (collision.GetComponent<GroundScript>().walkable)
            {
                spawnPointList.Add(collision.GetComponent<GroundScript>());
                if (!spawnPoint && spawnPointList.Count==20)
                {
                    spawnPoint = spawnPointList[7];
                    spawnPoint.SetHighlight(GroundScript.HighlightType.type4);
                    spawnPoint.hasSpawnPoint = true;
                    spawnPoint.walkable = false;


                    GameManager.scr.curBarrack = this;
                    SendProductPanel();

                }
            }
        }
    }

    
    public void SoldierSpawn(SoldierType type)
    {
        if (!spawnPoint)
            return;
        SoldierScript curSoldier = GameManager.scr.soldierList[GameManager.scr.soldier_index];
        curSoldier.MySoldierTypes = MySoldierTypes;
        curSoldier.soldierType = type;
        curSoldier.myGround = spawnPoint;
        curSoldier.myGround.hasSoldier = true;
        curSoldier.transform.position = spawnPoint.transform.position;
        spawnPoint.walkable = false;
        curSoldier.gameObject.SetActive(true);
        GameManager.scr.soldier_index++;
    }
}
