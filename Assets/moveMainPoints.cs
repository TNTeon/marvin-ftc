using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class moveMainPoints : MonoBehaviour
{
    // Start is called before the first frame update
    private Boolean dragging = false;
    private Boolean turning = false;
    public GameObject axisObj;

    public static float length;
    public static float width;

    public TMP_InputField lengthInput;
    public TMP_InputField widthInput;

    public GameObject canvas;

    public static bool toggle;

    void Start()
    {
        dragging = false;
        turning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (toggle)
        {
            canvas.SetActive(true);
        }
        else
        {
            canvas.SetActive(false);
        }
        length = float.Parse(lengthInput.text);
        width = float.Parse(widthInput.text);

        if (Input.GetMouseButtonDown(0) && Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 6f)
        {
            if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
            {
                turning = true;
            }
            else
            {
                dragging = true;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            turning = false;
            dragging = false;
        }
        if (dragging && axisObj != null && toggle != true)
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            {
                transform.position = new Vector3(Mathf.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).x / 6) * 6, Mathf.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).y / 6) * 6, 0);
                axisObj.transform.position = new Vector3(Mathf.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).x / 6) * 6, Mathf.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).y / 6) * 6, 0);
                print("MOVING AND ROUNDING");
            }
            else
            {
                transform.position = new Vector3(Mathf.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).x * 100) / 100, Mathf.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).y * 100) / 100, 0);
                axisObj.transform.position = new Vector3(Mathf.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).x*100)/100, Mathf.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).y * 100) / 100, 0);
               
            }
        }
        if (turning && axisObj != null && toggle != true)
        {
            transform.up = new Vector2(
            Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x,
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y - transform.position.y
            );

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                transform.eulerAngles = new Vector3(0, 0, Mathf.Round(transform.eulerAngles.z / 15) * 15);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, (int)transform.eulerAngles.z);
            }
        }

        transform.localScale = new Vector3(width/18, length / 18);
        
    }

    public Boolean clicked()
    {
        if (Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < 6f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
