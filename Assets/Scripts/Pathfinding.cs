using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public static Pathfinding scr;
    public List<GroundScript> path;
    public GroundScript seeker, target;

    public List<GroundScript> openSet;
    public List<GroundScript> openSet2;
    public HashSet<GroundScript> closedSet;
    public bool active;
    private void Awake()
    {
        scr = this;
    }
    private void Start()
    {

    }
    private void Update()
    {
        //if (active)
        //{
        //    //FindPath(seeker, target);
        //    StartCoroutine(FindPath2(seeker,target));

        //    active = false;
        //}
    }
    public void FindPath(GroundScript _startGround, GroundScript _targetGround)
    {
        for (int i = 0; i < GridManager.scr.grounds.Count; i++)
        {
            GridManager.scr.grounds[i].SetHighlight(GroundScript.HighlightType.none);

        }
        GroundScript startGround = _startGround;
        GroundScript targetGround = _targetGround;

        openSet = new List<GroundScript>();
        closedSet = new HashSet<GroundScript>();
        openSet2 = new List<GroundScript>();
        openSet.Add(startGround);

        while (openSet.Count > 0)
        {
            GroundScript ground = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < ground.fCost || openSet[i].fCost == ground.fCost)
                {
                    if (openSet[i].hCost < ground.hCost)
                        ground = openSet[i];
                }
            }

            openSet.Remove(ground);
            closedSet.Add(ground);
            openSet2.Add(ground);

            if (ground == targetGround)
            {
                RetracePath(startGround, targetGround);
                return;
                
            }
            else
            {
                foreach (GroundScript neighbour in ground.neighboursList)
                {
                    if (neighbour.hasSoldier|| neighbour.hasSpawnPoint || !neighbour.walkable || closedSet.Contains(neighbour))
                    {
                        
                            continue;

                        
                    }
                    float newCostToNeighbour = ground.gCost + GetDistance(ground, neighbour);
                    if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetGround);
                        neighbour.parent = ground;

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                    }
                }
            }
           
            
        }
        
        
    }
    private IEnumerator FindPath2(GroundScript _startGround, GroundScript _targetGround)
    {
        for (int i = 0; i < GridManager.scr.grounds.Count; i++)
        {
            GridManager.scr.grounds[i].SetHighlight(GroundScript.HighlightType.none);
            
        }
        GroundScript startGround = _startGround;
        GroundScript targetGround = _targetGround;

        openSet = new List<GroundScript>();
        closedSet = new HashSet<GroundScript>();
        openSet2 = new List<GroundScript>();
        openSet.Add(startGround);

        while (openSet.Count > 0)
        {
            GroundScript ground = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < ground.fCost || openSet[i].fCost == ground.fCost)
                {
                    if (openSet[i].hCost < ground.hCost)
                        ground = openSet[i];
                }
            }

            openSet.Remove(ground);
            closedSet.Add(ground);
            openSet2.Add(ground);

            if (ground == targetGround)
            {
                RetracePath(startGround, targetGround);
                break;
                //return;
            }
            else
            {
                foreach (GroundScript neighbour in ground.neighboursList)
                {
                    if (neighbour.hasBuilding || !neighbour.walkable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }


                    float newCostToNeighbour = ground.gCost + GetDistance(ground, neighbour);
                    if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetGround);
                        neighbour.parent = ground;

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                    }
                }
            }
            ground.SetHighlight(GroundScript.HighlightType.type3);
            yield return null;
        }
        for (int i = 0; i < openSet2.Count; i++)
        {
            openSet2[i].SetHighlight(GroundScript.HighlightType.none);
            yield return null;
        }
        for (int i = 0; i < path.Count; i++)
        {
            path[i].SetHighlight(GroundScript.HighlightType.type1);
            yield return null;
        }
    }
   
    void RetracePath(GroundScript startGround, GroundScript targetGround)
    {
        List<GroundScript> _path = new List<GroundScript>();
        GroundScript currentGround = targetGround;

        while (currentGround != startGround)
        {
            _path.Add(currentGround);
            currentGround = currentGround.parent;
        }
        _path.Reverse();

        path = _path;
        for (int i = 0; i < path.Count; i++)
        {
            //path[i].SetHighlight(GroundScript.HighlightType.type1);

        }
        active = false;

    }

    float GetDistance(GroundScript groundA, GroundScript groundB)
    {
        float dstX = Mathf.Abs(groundA.transform.position.x - groundB.transform.position.x);
        float dstY = Mathf.Abs(groundA.transform.position.y - groundB.transform.position.y);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
   
}
