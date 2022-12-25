using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Build Card",menuName ="BuildCard")]
public class BuildCard : ScriptableObject
{
    
    public Sprite icon;
    public string _name;
    public Vector2 cell_size;
    public GameManager.BuildType buildType;
    public BarrackScript.SoldierType soldierType;
    public string description;



}


