using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager scr;
    private void Awake()
    {
        scr = this;
    }
    public Animator buildPanel, informationAnim;
    public Animator producePanelAnim;
    public void BuildButton()
    {
        if (buildPanel.GetBool("active"))
        {
            buildPanel.SetBool("active", false);
            buildPanel.SetBool("deactive", true);

        }
        else
        {
            buildPanel.SetBool("deactive", false);
            buildPanel.SetBool("active", true);
            
            producePanelAnim.SetBool("active", false);
            producePanelAnim.SetBool("deactive", true);
        }
    }
    public void ProducePanel(bool active)
    {
        
        producePanelAnim.SetBool("active", active);
        producePanelAnim.SetBool("deactive", !active);
        buildPanel.SetBool("active", !active);
        buildPanel.SetBool("deactive", active);

    }
    public void InformationButton(bool active)
    {
        informationAnim.SetBool("active", active);
        informationAnim.SetBool("deactive", !active);

    }
}
