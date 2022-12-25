using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager scr;
    public List<Sprite> tileList;
    private void Awake()
    {
        scr = this;
       
    }
    public GameObject prefab, movePrefab;
    
    public Camera mycam;
    public int column_lenght;
    public int row_lenght;
    public float cellSize;
    Vector2 spawnPoint;
    Transform instantiated;

    public List<GroundScript> grounds = new List<GroundScript>();
    private void Start()
    {
        CreateGrid();
    }
    int corner_index, edge_index;
    GroundScript curGround;
    private void CreateGrid()
    {
        for (int x = 0; x < row_lenght; x++)
        {
            for (int y = 0; y < column_lenght; y++)
            {
                spawnPoint.x = (-(row_lenght / 2f) + x + 0.5f) * cellSize;
                spawnPoint.y = (-(column_lenght / 2f) + y + 0.5f) * cellSize;
                instantiated = Instantiate(prefab, transform).transform;
                instantiated.position = spawnPoint;
                curGround = instantiated.GetComponent<GroundScript>();
                grounds.Add(curGround);


                if ((x == 0 || x == row_lenght - 1) && (y == 0 || y == column_lenght - 1))
                {
                    curGround.s.sprite = tileList[2];
                    curGround.walkable = false;
                    curGround.transform.eulerAngles = Vector3.forward * (-90) * (corner_index > 1 ? -(corner_index-1) : corner_index);
                    //corner_index+=corner_index==1 ? 0:1;
                    corner_index++;
                    
                    

                }
                else if ((x == 0 || y==0 || y == column_lenght - 1 || x == row_lenght - 1) )
                {
                    curGround.s.sprite = tileList[1];
                    curGround.walkable = false;
                    switch (corner_index)
                    {
                        case 1:
                            curGround.transform.eulerAngles = Vector3.forward * (90);
                            break;
                        case 2:
                            curGround.transform.eulerAngles = Vector3.forward * (180) * (y > 15 ? 0 : -1);
                            break;
                        case 3:
                            curGround.transform.eulerAngles = Vector3.forward * (-90);
                            break;
                        case 4:
                            break;


                    }
                }
            }
        }

        StartCoroutine(FindNeighbours());

    }
    private IEnumerator FindNeighbours()
    {
        yield return null;
        for (int i = 0; i < grounds.Count; i++)
        {
            grounds[i].FindNeighbours();
        }
    }
    
   
}
