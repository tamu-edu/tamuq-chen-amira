using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rope_new : MonoBehaviour
{
    public Transform ball;
    public int segments = 10;
    private LineRenderer line;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = segments;
    }

    private void Update()
    {
        for (int i = 0; i < segments; i++)
        {
            float t = i / (float)(segments - 1);
            Vector3 position = Vector3.Lerp(transform.position, ball.position, t);
            line.SetPosition(i, position);
        }
    }
}

