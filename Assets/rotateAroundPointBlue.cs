using System;
using UnityEngine;

public class rotateAroundPointBlue : MonoBehaviour
{
    public float angle;
    public int id;
    public GameObject pivot;

    private Boolean clicking = false;

    // Update is called once per frame
    void Update()
    {
        if (pivot != null)
        {

            if (Input.GetMouseButtonDown(0) && Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 3f && !moveMainPoints.toggle)
            {
                clicking = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                clicking = false;
            }
            if (clicking)
            {
                transform.parent.parent.transform.up = new Vector2(
                -(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.parent.parent.transform.position.x),
                -(Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.parent.parent.transform.position.y)
                );
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    transform.parent.parent.transform.eulerAngles = new Vector3(0, 0, Mathf.Round(transform.parent.parent.transform.eulerAngles.z / 15) * 15);
                }
                else
                {
                    transform.parent.parent.transform.eulerAngles = new Vector3(0, 0, (int)transform.parent.parent.transform.eulerAngles.z);
                }
            }
        }

    }
    public Boolean clicked()
    {
        if (Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 3f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
