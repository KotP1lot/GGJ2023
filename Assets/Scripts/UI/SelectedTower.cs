using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class SelectedTower : MonoBehaviour
{
    public GameObject selectedTower;
    public BuildTowers builder;

    [Space(6)]
    [Header("Sprites")]
    public Sprite squirrel;
    public Sprite bigroot;
    public Sprite rose;
    public Sprite shroom;
    public Sprite bear;

    [Space(6)]
    [Header("Prefabs")]
    public GameObject squirrelObj;
    public GameObject bigrootObj;
    public GameObject roseObj;
    public GameObject shroomObj;
    public GameObject bearObj;

    private Dictionary<string, Sprite> spriteNames = new Dictionary<string, Sprite>();
    private Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();
    private Image image;
    private CursorChanger cursorChanger;
    private GlobalData.TowerType currentSelect;

    public CircleRenderer towerRange;

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

        prefabs.Add("Squirrel", squirrelObj);
        prefabs.Add("Bigroot", bigrootObj);
        prefabs.Add("Rose", roseObj);
        prefabs.Add("Shroom", shroomObj);
        prefabs.Add("Bear", bearObj);
    }

    void Update()
    {
        transform.position = new Vector2(Input.mousePosition.x,Input.mousePosition.y);

        if (image.enabled && Input.GetMouseButtonDown(0))
        {
            image.enabled = false;
            cursorChanger.enabled = false;

            builder.BuildNewTower( prefabs[currentSelect.ToString()], builder.mainCamera.ScreenToWorldPoint(Input.mousePosition));

            GameObject.Find("Overlay").GetComponent<TilemapRenderer>().enabled = false;
            towerRange.gameObject.SetActive(false);
        }
        if (image.enabled && Input.GetMouseButtonDown(1)) {
            image.enabled = false;
            cursorChanger.enabled = false;

            GameObject.Find("Overlay").GetComponent<TilemapRenderer>().enabled = false ;
            towerRange.gameObject.SetActive(false);
        }
    }

    public void SelectTower(GlobalData.TowerType type, float range)
    {
        image.enabled = true;
        cursorChanger.enabled = true;
        image.sprite = spriteNames[type.ToString()];
        currentSelect = type;

        GameObject.Find("Overlay").GetComponent<TilemapRenderer>().enabled = true;

        //towerRange.gameObject.SetActive(true);
        //towerRange.radius = range;
    }
}
