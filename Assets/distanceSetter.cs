using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class distanceSetter : MonoBehaviour
{
    public int id = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (id != 0)
        {
            if (transform.tag == "blueTan")
            {
                if(id > 1)
                {
                    transform.localPosition = new Vector3(0, -((Vector3.Distance(Testing.tangents[id-2].transform.position, transform.parent.transform.position) / 18.7f) / 2.5f), 0);
                }
            }
            else
            {
                if (Testing.tangents.Count > id)
                {
                    //print(Vector3.Distance(Testing.tangents[id].transform.position, transform.parent.transform.position));
                    transform.localPosition = new Vector3(0, (Vector3.Distance(Testing.tangents[id].transform.position, transform.parent.transform.position) / 18.7f) / 2.5f, 0);
                }
            }
            
        }
    }
}
