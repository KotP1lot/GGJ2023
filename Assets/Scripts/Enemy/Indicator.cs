using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public GameObject indicator;
    private GameObject target;

    private float rotationSpeed = 720;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!spriteRenderer.isVisible)
        {
            if (!indicator.GetComponent<SpriteRenderer>().enabled)
            {
                indicator.GetComponent<SpriteRenderer>().enabled = true;
            }

            Vector2 direction = target.transform.position - transform.position;

            RaycastHit2D ray = Physics2D.Raycast(transform.position, direction, Mathf.Infinity,10);

            Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, direction);
            indicator.transform.rotation = Quaternion.RotateTowards(indicator.transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

            if (ray.collider != null)
            {
                indicator.transform.position = ray.point;
            }
        }
        else
        {
            if (indicator.GetComponent<SpriteRenderer>().enabled)
            {
                indicator.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
}
