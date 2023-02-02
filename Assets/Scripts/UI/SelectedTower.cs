using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedTower : MonoBehaviour
{
    public GameObject selectedTower;

    public Sprite squirrel;
    public Sprite bigroot;
    public Sprite rose;
    public Sprite shroom;
    public Sprite bear;

    private Dictionary<string, Sprite> spriteNames = new Dictionary<string, Sprite>();
    private Image image;
    private CursorChanger cursorChanger;
    void Start()
    {
        image = selectedTower.GetComponent<Image>();
        image.enabled = false;
        cursorChanger = selectedTower.GetComponent<CursorChanger>();
        cursorChanger.enabled = false;

        spriteNames.Add("Squirrel", squirrel);
        spriteNames.Add("Bigroot", bigroot);
        spriteNames.Add("Rose", rose);
        spriteNames.Add("Shroom", shroom);
        spriteNames.Add("Bear", bear);
    }

    void Update()
    {
        transform.position = new Vector2(Input.mousePosition.x,Input.mousePosition.y);

        if (image.enabled && (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0))) {
            image.enabled = false;
            cursorChanger.enabled = false;
        }
    }

    public void SelectTower(GlobalData.TowerType type)
    {
        image.enabled = true;
        cursorChanger.enabled = true;
        image.sprite = spriteNames[type.ToString()];
    }
}
