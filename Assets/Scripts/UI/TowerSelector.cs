using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSelector : MonoBehaviour
{
    public StatWindow statWindow;
    public SelectedTower selected;
    public GlobalData.TowerType type;

    [Header("Info")]
    public string header;
    public int cost;
    public float DMG;
    public float SPD;
    public float RNG;
    [TextArea]
    public string description;

    public bool isHighlighted { get; private set; } 

    private Animator animator;
    private Button button;
    private CursorChanger cursorChanger;
    void Start()
    {
        animator = GetComponent<Animator>();
        button = GetComponent<Button>();
        cursorChanger = GetComponent<CursorChanger>();
    }

    void Update()
    {
        animator.SetBool("canBuy", GlobalData.instance.bones >= cost);

        if (GlobalData.instance.bones < cost) cursorChanger.ChangeTo = GlobalData.CursorType.Cross;
        else cursorChanger.ChangeTo = GlobalData.CursorType.Hand;
        //isHighlighted = animator.GetBool("Highlighted");//animator.GetCurrentAnimatorClipInfo(0)[0].clip.name.StartsWith("Highlighted");

        if(animator.GetBool("Highlighted"))
        {
            isHighlighted = true;
        }
        else if(!animator.GetCurrentAnimatorClipInfo(0)[0].clip.name.StartsWith("Highlighted") && !animator.IsInTransition(0))
        {
            isHighlighted =false;
            
        }
    }

    public void Select()
    {
        if (GlobalData.instance.bones >= cost)
        {
            selected.SelectTower(type);
            button.Select();
        }
    }
}
