using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using VisualDebugging;

public class TestDraw : MonoBehaviour
{

    // Use this for initialization
    [ContextMenu("Run")]
    void Start()
    {

        for (int i = 0; i < 3; i++)
        {
            Debug.DrawRay(Vector3.up * i, Vector3.right*10, Color.red,100);
            print(i);
        }

    }
}
