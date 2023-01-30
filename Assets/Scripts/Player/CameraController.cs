using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Parent;
    public Transform Obj;
    public float Radius = 4.5f;
    float Dist;
    Vector3 MousePos1, MousePos2, ScreenMouse, MouseOffset;
    public void Update()
    {
        MousePos1 = Input.mousePosition;
        ScreenMouse = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(MousePos1.x, MousePos1.y, Obj.position.z - GetComponent<Camera>().transform.position.z));
        MouseOffset = ScreenMouse - Parent.position;
        MousePos2 = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -transform.position.z));
        Obj.position = new Vector3((MousePos2.x - Parent.position.x) / 2.0f + Parent.position.x, (MousePos2.y - Parent.position.y) / 2.0f + Parent.position.y, Obj.position.z);

        Dist = Vector2.Distance(new Vector2(Obj.position.x, Obj.position.y), new Vector2(Parent.position.x, Parent.position.y));

        if (Dist > Radius)
        {
            var norm = MouseOffset.normalized;
            Obj.position = new Vector3(norm.x * Radius + Parent.position.x, norm.y * Radius + Parent.position.y, Obj.position.z);
        }
    }
}
