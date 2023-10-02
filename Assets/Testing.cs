using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Testing : MonoBehaviour
{
    public static float interpolateAmount;

    public GameObject circle;
    public GameObject tangent;

    public GameObject follower;
    public GameObject follower2;
    public GameObject follower3;

    public static List<GameObject> mainPoints = new List<GameObject>();
    public static List<GameObject> tangents = new List<GameObject>();

    public static List<GameObject> followers = new List<GameObject>();
    public static List<GameObject> followers2 = new List<GameObject>();
    public static List<GameObject> followers3 = new List<GameObject>();

    public TMP_InputField SampleMecDriveInput;
    public TMP_InputField trajNameInput;


    Boolean mouseUp = true;

    private int counter;



    private void Start()
    {
        mouseUp = true;
        counter = 0;
        moveMainPoints.toggle = true;

    }
    void Update()
    {
        interpolateAmount = (interpolateAmount + Time.deltaTime * 0.5f) % 1;
        
        if (Input.GetMouseButtonDown(0))
        {
            Boolean anyClicked = false;
            foreach (GameObject tans in tangents)
            {
                if (tans.GetComponentInChildren<rotateAroundPoint>().clicked())
                {
                    anyClicked = true;
                }
                if (tans.GetComponentInChildren<rotateAroundPointBlue>().clicked())
                {
                    anyClicked = true;
                }
            }
            foreach (GameObject points in mainPoints)
            {
                if (points.GetComponent<moveMainPoints>().clicked())
                {
                    anyClicked = true;
                }
            }
            if (!anyClicked && !moveMainPoints.toggle)
            {
                GameObject clone;
                GameObject TClone;
                if (Input.GetKey(KeyCode.RightShift)|| Input.GetKey(KeyCode.LeftShift))
                {
                    clone = Instantiate(circle, new Vector3(Mathf.Round(Mathf.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).x / 6) * 6), Mathf.Round(Mathf.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).y / 6) * 6), 0), Quaternion.identity);
                    TClone = Instantiate(tangent, new Vector3(Mathf.Round(Mathf.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).x / 6) * 6), Mathf.Round(Mathf.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).y / 6) * 6), 0), Quaternion.identity);

                }
                else
                {
                    clone = Instantiate(circle, new Vector3(Mathf.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).x), Mathf.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).y), 0), Quaternion.identity);
                    TClone = Instantiate(tangent, new Vector3(Mathf.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).x), Mathf.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).y), 0), Quaternion.identity);

                }

                mainPoints.Add(clone);
                tangents.Add(TClone);
                clone.GetComponent<moveMainPoints>().axisObj = TClone;
                TClone.GetComponentInChildren<rotateAroundPoint>().id = counter;
                TClone.GetComponentInChildren<rotateAroundPoint>().pivot = clone;
                TClone.GetComponentInChildren<rotateAroundPointBlue>().pivot = clone;
                TClone.GetComponentsInChildren<distanceSetter>()[0].id = tangents.Count;
                TClone.GetComponentsInChildren<distanceSetter>()[1].id = tangents.Count;

                counter++;
                mouseUp = false;
            }
        }
        if (!mouseUp)
        {
            
            mainPoints[mainPoints.Count - 1].transform.up = new Vector2(
            Camera.main.ScreenToWorldPoint(Input.mousePosition).x - mainPoints[mainPoints.Count - 1].transform.position.x,
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y - mainPoints[mainPoints.Count - 1].transform.position.y
            );

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                mainPoints[mainPoints.Count - 1].transform.eulerAngles = new Vector3(0, 0, Mathf.Round(mainPoints[mainPoints.Count - 1].transform.eulerAngles.z / 15) * 15);
            }
            else
            {
                mainPoints[mainPoints.Count - 1].transform.eulerAngles = new Vector3(0, 0, (int)mainPoints[mainPoints.Count - 1].transform.eulerAngles.z);
            }
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            mouseUp = true;
        }
        if (Input.GetButtonDown("Submit") && !moveMainPoints.toggle)
        {
            interpolateAmount = 0;
            for (int i = 0; i < (mainPoints.Count - 1) * 3; i++)
            {
                followers.Add(Instantiate(follower, new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0), Quaternion.identity));
            }
            for (int i = 0; i < (followers.Count - 1); i++)
            {
                followers2.Add(Instantiate(follower2, new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0), Quaternion.identity));
            }
            for (int i = 0; i < (followers2.Count - 1); i++)
            {
                followers3.Add(Instantiate(follower3, new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0), Quaternion.identity));
            }
        }
        if (Input.GetButton("Submit") && !moveMainPoints.toggle)
        {
            for (int i = 0; i < mainPoints.Count - 1; i++)
            {
                print(i * 3);
                followers[i * 3].transform.position = Vector3.Lerp(mainPoints[i].transform.position, tangents[i].transform.GetChild(0).transform.position, interpolateAmount);
                print(i * 3 + 1);
                followers[i * 3 + 1].transform.position = Vector3.Lerp(tangents[i].transform.GetChild(0).transform.position, tangents[i + 1].transform.GetChild(1).transform.position, interpolateAmount);
                print(i* 3 +2);
                followers[i * 3 + 2].transform.position = Vector3.Lerp(tangents[i + 1].transform.GetChild(1).transform.position, mainPoints[i + 1].transform.position, interpolateAmount);
            }
            for (int i = 0; i < followers.Count - 1; i++)
            {
                followers2[i].transform.position = Vector3.Lerp(followers[i].transform.position, followers[i+1].transform.position, interpolateAmount);
            }
            for (int i = 0; i < followers2.Count - 1; i++)
            {
                if (i == 0 || i % 3 == 0)
                {
                    followers3[i].transform.position = Vector3.Lerp(followers2[i].transform.position, followers2[i + 1].transform.position, interpolateAmount);
                }
                else
                {
                    followers3[i].transform.localScale = new Vector3(0,0,0);
                }
            }

        }else if (Input.GetKeyDown("d") && !moveMainPoints.toggle && mainPoints.Count > 0)
        {
            print("DELETE");
            Destroy(mainPoints[mainPoints.Count-1]);
            mainPoints.RemoveAt(mainPoints.Count-1);
            Destroy(tangents[tangents.Count - 1]);
            tangents.RemoveAt(tangents.Count - 1);
        }else if (Input.GetKeyDown("q") && !moveMainPoints.toggle)
        {
            int length = mainPoints.Count;
            for (int i = 0; i < length -1; i++)
            {
                Destroy(mainPoints[0]);
                mainPoints.RemoveAt(0);
                Destroy(tangents[0]);
                tangents.RemoveAt(0);
            }
            tangents[0].GetComponentsInChildren<distanceSetter>()[0].id = 1;
            tangents[0].GetComponentsInChildren<distanceSetter>()[1].id = 1;
        }
        if (Input.GetButtonUp("Submit") && !moveMainPoints.toggle)
        {
            foreach (GameObject followGuy in followers)
            {
                Destroy(followGuy);
            }
            followers.Clear();

            foreach (GameObject followGuy in followers2)
            {
                Destroy(followGuy);
            }
            followers2.Clear();

            foreach (GameObject followGuy in followers3)
            {
                Destroy(followGuy);
            }
            followers3.Clear();
        }
        if (Input.GetKeyDown(KeyCode.C) && mainPoints.Count > 0)
        {
            String tempCopy = SampleMecDriveInput.text + ".setPoseEstimate(new Pose2d(" + mainPoints[0].transform.position.y + "," + -mainPoints[0].transform.position.x + ", Math.toRadians(" + Mathf.Round(mainPoints[0].transform.localRotation.eulerAngles.z) + ")));\r\n";
            tempCopy += "Trajectory "+ trajNameInput.text + " = "+ SampleMecDriveInput.text +".trajectoryBuilder("+ SampleMecDriveInput.text + ".getPoseEstimate(), Math.toRadians(" + Mathf.Round(tangents[0].transform.localRotation.eulerAngles.z) +")) \r\n";
            for (int i = 1; i < mainPoints.Count; i++)
            {
                tempCopy+= ".splineToSplineHeading(new Pose2d("+ mainPoints[i].transform.position.y + "," + -mainPoints[i].transform.position.x +", Math.toRadians(" + Mathf.Round(mainPoints[i].transform.localRotation.eulerAngles.z) + ")), Math.toRadians(" + Mathf.Round(tangents[i].transform.localRotation.eulerAngles.z) + "))\r\n";
            }
            GUIUtility.systemCopyBuffer = tempCopy + ".build();" ;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (moveMainPoints.toggle) {
                moveMainPoints.toggle = false;
            }else{
                moveMainPoints.toggle = true;
            }
        }
    }
}
