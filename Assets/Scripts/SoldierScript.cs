using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoldierScript : BarrackScript
{
    public delegate void  OnPlayerAttack();
    public OnPlayerAttack onPlayerAttack;
    public enum AnimationType { none,idle,walk,attack}
    public Animator myAnim;
    public SoldierType soldierType;
    public float _damage;
    public float _movementSpeed;
    public float _attackRange;
    public float _attackSpeed;
    private void OnEnable()
    {
        onPlayerAttack += Attack;
    }
    private void OnDisable()
    {
        onPlayerAttack -= Attack;
    }
    private void Awake()
    {
        myAnim = GetComponent<Animator>();
    }
    public void Start()
    {
        GetSoldierFeatures();
        GetFeatures();
        
    }

    
    public void GetSoldierFeatures()
    {
        myCard = MySoldierTypes[soldierType.GetHashCode()].soldierCard;
        _damage = MySoldierTypes[soldierType.GetHashCode()]._damage;
        _movementSpeed = MySoldierTypes[soldierType.GetHashCode()]._movementSpeed;
        _attackSpeed = MySoldierTypes[soldierType.GetHashCode()]._attackSpeed;
        _attackRange = MySoldierTypes[soldierType.GetHashCode()]._attackRange;
    }
    

    public GroundScript myGround;
    public List<GroundScript> myPath=new List<GroundScript>();
    private int myPath_index;
    private bool isMove;
    private void Update()
    {
        if (isMove)
        {
            Movement();
        }
    }
    private void Movement()
    {
        transform.position = Vector2.MoveTowards(transform.position, myPath[myPath_index].transform.position, 4f * Time.deltaTime);
        if (myPath[myPath_index].transform.position ==transform.position)
        {
            myGround = myPath[myPath_index];
            myGround.hasSoldier = true;
            transform.position = myPath[myPath_index].transform.position;
            if (myPath_index < myPath.Count-1)
            {
                myGround.hasSoldier = false;
                myPath_index++;
            }
            else
            {
                isMove = false;
                SetAnim(AnimationType.idle);
                myPath_index = 0;
                for (int i = 0; i < myPath.Count; i++)
                {
                    myPath[i].SetHighlight(GroundScript.HighlightType.none);
                }
                myPath.Clear();
            }
        }
    }
    public void GetPath(GroundScript target)
    {
        myPath_index = 0;
        if(myPath.Count>0)
            myPath.Clear();
        Pathfinding.scr.FindPath(myGround, target);
        myPath = Pathfinding.scr.path;

        for (int i = myPath.Count-1; i >= 0; i--)
        {
            if (!myPath[i].walkable)
                myPath.RemoveAt(i);
        }
        if (myPath.Count > 0)
        {
            SetAnim(AnimationType.walk);
            if (myGround)
                myGround.hasSoldier = false;
            isMove = true;
        }
    }
    private void SetAnim(AnimationType type)
    {
        switch (type)
        {
            case AnimationType.none:
                myAnim.SetBool("isWalking",false);
                myAnim.SetBool("isAttack", false);
                break;
            case AnimationType.idle:
                myAnim.SetBool("isWalking", false);
                myAnim.SetBool("isAttack", false);
                break;
            case AnimationType.walk:
                myAnim.SetBool("isWalking", true);
                break;
            case AnimationType.attack:
                myAnim.SetBool("isAttack", true);
                break;
           
        }
    }
    public void Attack()
    {

    }
    public GameObject highlight;
    public override void Activated() 
    {
        //base.Activated();
        highlight.SetActive(true);
    }
    public override void DeActivated()
    {
        //base.DeActivated();
        highlight.SetActive(false);
    }
}
