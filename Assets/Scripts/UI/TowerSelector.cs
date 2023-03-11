using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSelector : MonoBehaviour
{
    public StatWindow statWindow;
    public SelectedTower selected;
    public GlobalData.TowerType type;
    public Tower tower;

    public string header { get; private set; }
    public int cost { get; private set; }
    public float DMG { get; private set; }
    public float SPD { get; private set; }
    public float RNG { get; private set; }
    public string description { get; private set; }

    public bool isHighlighted { get; private set; } 

    private Animator animator;
    private Button button;
    private CursorChanger cursorChanger;
    void Start()
    {
        animator = GetComponent<Animator>();
        button = GetComponent<Button>();
        cursorChanger = GetComponent<CursorChanger>();

        var info = tower.GetLvLInfo(0);
        header = tower.towerName;
        description = tower.towerDescription;
        cost = info.LvLCost;
        DMG = info.Damage;
        SPD = info.AttackSpeed;
        RNG = info.Range;
    }

    void Update()
    {
        animator.SetBool("canBuy", GlobalData.instance.bones >= cost);

        if (GlobalData.instance.bones < cost) cursorChanger.ChangeTo = GlobalData.CursorType.Cross;
        else cursorChanger.ChangeTo = GlobalData.CursorType.Hand;

        if(cursorChanger.isHovered)
        {
            isHighlighted = true;
            animator.SetBool("highlighted", true);
        }
        else if(!cursorChanger.isHovered && !animator.IsInTransition(0))
        {
            isHighlighted =false;
            animator.SetBool("highlighted", false);
        }

        if(animator.GetBool("pressed") && (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0)))
        {
            animator.SetBool("pressed", false);
        }
    }

    public void Select()
    {
        if (GlobalData.instance.bones >= cost)
        {
            selected.SelectTower(type,RNG);
            button.Select();
            animator.SetBool("pressed",true);
        }
    }

    //void Update()
    //{
    //    animator.SetBool("canBuy", GlobalData.instance.bones >= cost);

    //    if (GlobalData.instance.bones < cost) cursorChanger.ChangeTo = GlobalData.CursorType.Cross;
    //    else cursorChanger.ChangeTo = GlobalData.CursorType.Hand;

    //    if (animator.GetBool("Highlighted"))
    //    {
    //        isHighlighted = true;
    //    }
    //    else if (!animator.GetCurrentAnimatorClipInfo(0)[0].clip.name.StartsWith("Highlighted") && !animator.IsInTransition(0))
    //    {
    //        isHighlighted = false;

    //    }
    //}

    //public void Select()
    //{
    //    if (GlobalData.instance.bones >= cost)
    //    {
    //        selected.SelectTower(type, RNG);
    //        button.Select();
    //    }
    //}
}
