using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager scr;
    public Event OnOpen;

    private void Awake()
    {
       if(scr==null) // SINGLETON
        {
            scr = this;
        }
        else
        {
            Destroy(this);
        }
    }
    
    [Header("Game")]
    [Space(4)]
    public BarrackScript curBarrack;
    [Header("PoolSettings")]
    [Space(4)]
    public List<BuildScript> barrackList;
    public int barrack_index;
    public List<BuildScript> powerplantList;
    public int powerplant_index;
    public List<SoldierScript> soldierList;
    public int soldier_index;

    
    private void Start()
    {
        Spawner();
    }
    public List<PoolSettings> poolObjects;
    public enum BuildType { Barrack, Soldier,PowerPlant }
    public enum ProductType {none, Soldier,Wood}
    [System.Serializable]
    public class PoolSettings
    {
        public BuildType _type;
        public int spawnCount;
        public Transform poolParent;
    }

    GameObject temp;
    private void Spawner()
    {
        for (int i = 0; i < poolObjects.Count; i++)
        {
            GameObject obj = Resources.Load<GameObject>("Prefabs/" + poolObjects[i]._type.ToString());
            
            for (int j = 0; j < poolObjects[i].spawnCount; j++)
            {
                temp = Instantiate(obj, poolObjects[i].poolParent);
                switch (poolObjects[i]._type)
                {
                    case BuildType.Barrack:
                        barrackList.Add(temp.GetComponent<BuildScript>());
                        break;
                    
                    case BuildType.PowerPlant:
                        powerplantList.Add(temp.GetComponent<BuildScript>());
                        break;
                    case BuildType.Soldier:
                        soldierList.Add(temp.GetComponent<SoldierScript>());
                        break;

                }
                temp.gameObject.SetActive(false);
            }
        }
    }
    private void OnApplicationQuit()
    {
        Application.Quit();
    }
}
