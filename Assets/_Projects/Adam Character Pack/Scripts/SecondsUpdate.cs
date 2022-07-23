using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondsUpdate : MonoBehaviour
{
    public float  timeStartOffset = 0;
    public bool gotStartTime = false;

    void Update()
    {
        if (!gotStartTime)
        {
            timeStartOffset = Time.realtimeSinceStartup;
            gotStartTime = true;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, Time.realtimeSinceStartup - timeStartOffset);
    }
}
