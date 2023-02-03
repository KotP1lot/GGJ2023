using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomCursor : MonoBehaviour
{

    public Sprite cursorPoint;
    public Sprite cursorClick;
    public Sprite cursorHand;
    public Sprite cursorGrab;
    public Sprite cursorCross;

    private Dictionary<GlobalData.CursorType, Sprite> cursorSprites;

    private Image image;
    public GameObject hoveredObj { get; private set; }

    void Start()
    {
        Cursor.visible = false;

        image = GetComponent<Image>();

        cursorSprites = new Dictionary<GlobalData.CursorType, Sprite>();
        cursorSprites.Add(GlobalData.CursorType.Click, cursorClick);
        cursorSprites.Add(GlobalData.CursorType.Hand, cursorHand);
        cursorSprites.Add(GlobalData.CursorType.Grab, cursorGrab);
        cursorSprites.Add(GlobalData.CursorType.Cross, cursorCross);
        cursorSprites.Add(GlobalData.CursorType.Point, cursorPoint);
    }

    void Update()
    {
        var elements = RaycastMouse();

        if (elements.Count == 0) 
        {
            hoveredObj = null;
            image.sprite = cursorPoint;
        }
        else
        {
            foreach (var element in elements)
            {
                CursorChanger cursorChanger;
                if (element.gameObject.TryGetComponent(out cursorChanger))
                {
                    hoveredObj = cursorChanger.gameObject;
                    image.sprite = cursorSprites[cursorChanger.ChangeTo];
                    break;
                }
                else
                {
                    hoveredObj = null;
                    image.sprite = cursorPoint;
                }
            }
        }
    }

    public List<RaycastResult> RaycastMouse()
    {

        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            pointerId = -1,
        };

        pointerData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        return results;
    }

    public bool isObjectHovered(GameObject gameObject)
    {
        if(hoveredObj == null) return false;
        return hoveredObj.gameObject == gameObject;
    }
}
