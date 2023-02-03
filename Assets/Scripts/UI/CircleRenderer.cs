using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRenderer : MonoBehaviour
{
    private LineRenderer circleRenderer;
    public Camera cam;
    public float radius;
    public bool snapToMouse;

    // Start is called before the first frame update
    void Start()
    {
        circleRenderer = GetComponent<LineRenderer>();

        
    }

    // Update is called once per frame
    void Update()
    {
        DrawCircle(40);
    }

    void DrawCircle(int steps)
    {
        circleRenderer.positionCount = steps;

        for (int i = 0; i < steps; i++)
        {
            float circProgress = (float)i / steps;

            float curRadian = circProgress * 2 * Mathf.PI;

            float xScaled = Mathf.Cos(curRadian);
            float yScaled = Mathf.Sin(curRadian);

            float x = xScaled * radius;
            float y = yScaled * radius;

            Vector3 currentPosition;
            if(snapToMouse) currentPosition = new Vector3 (cam.ScreenToWorldPoint(Input.mousePosition).x+ x, cam.ScreenToWorldPoint(Input.mousePosition).y + y, 0);
            else currentPosition = new Vector3(transform.position.x + x, transform.position.y + y, 0);

            circleRenderer.SetPosition(i, currentPosition);
        }
    }
}
