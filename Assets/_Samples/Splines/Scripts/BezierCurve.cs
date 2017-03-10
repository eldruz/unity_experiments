﻿using UnityEngine;
using System.Collections.Generic;

public class BezierCurve : MonoBehaviour {

    public List<Vector3> points;

    public void Reset ()
    {
        points = new List<Vector3>();
        points.Add(new Vector3(1f, 0f, 0f));
        points.Add(new Vector3(2f, 0f, 0f));
        points.Add(new Vector3(3f, 0f, 0f));
        points.Add(new Vector3(4f, 0f, 0f));
    }

    public Vector3 GetQuadraticPoint(float t)
    {
        return transform.TransformPoint(Bezier.GetQuadraticPoint(points [0], points [1], points [2], t));
    }

    public Vector3 GetQuadraticVelocity(float t)
    {
        return transform.TransformPoint(Bezier.GetQuadraticFirstDerivative(points [0], points [1], points [2], t)) -
            transform.position;
    }

    public Vector3 GetPoint(float t)
    {
        return transform.TransformPoint(Bezier.GetPoint(points [0], points [1], points [2], points[3], t));
    }

    public Vector3 GetVelocity(float t)
    {
        return transform.TransformPoint(Bezier.GetFirstDerivative(points [0], points [1], points [2], points[3], t)) -
            transform.position;
    }

    public Vector3 GetDirection(float t)
    {
        return GetVelocity(t).normalized;
    }
}
