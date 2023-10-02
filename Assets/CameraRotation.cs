using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!moveMainPoints.toggle) {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.Rotate(0, 0, 90);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.Rotate(0, 0, -90);
            }
        }
    }
}
