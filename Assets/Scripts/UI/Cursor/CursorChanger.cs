using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChanger : MonoBehaviour
{
    public GlobalData.CursorType ChangeTo;
    [HideInInspector] public GlobalData.CursorType selectedType;

    public bool Enabled = true;

    private CustomCursor cursor;
    private bool soundPlay = false;
    public bool isHovered { get; private set; }

    private void Start()
    {
        selectedType = ChangeTo;
        cursor = GameObject.Find("Cursor").GetComponent<CustomCursor>();
    }

    private void Update()
    {
        if (Enabled)
        {

            ChangeTo = selectedType;
            isHovered = cursor.isObjectHovered(gameObject);
            if (isHovered && !soundPlay)
            {
                AudioManager.instance.Play("UIHover");
                soundPlay = true;
            }
            if (!isHovered)
            {
                soundPlay = false;
            }
        }
        else
        {
            ChangeTo = GlobalData.CursorType.Point;
            isHovered = false;
           
        }
    }
}
