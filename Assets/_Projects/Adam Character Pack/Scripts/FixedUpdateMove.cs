﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedUpdateMove : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.Translate(0, 0, Time.deltaTime);
    }
}
