using UnityEngine;

public static class Bezier {

    public static Vector3 GetQuadraticPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        /* QUADRATIC FORMULA FOR POINT IN BEZIER CURVE
         * B(t) = (1-t)²p0 + 2(1-t)tp1 + t²p2
         */
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return
            oneMinusT * oneMinusT * p0 +
        2f * oneMinusT * t * p1 +
        t * t * p2;
    }

    public static Vector3 GetQuadraticFirstDerivative (Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        /*
         * B'(t) = 2(1-t)(p1 - p0) + 2t(p2 - p1)
         */ 
        t = Mathf.Clamp01(t);
        return
            2f * (1f - t) * (p1 - p0) +
        2f * t * (p2 - p1);
    }

    public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        /*
         * CUBIC FORMULA FOR POINT IN BEZIER CURVE
         * B(t) = (1-t)^3 p0 + 3(1-t)²t p1 + 3(1-t)² p2 + t^3 p3
         */
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return
            oneMinusT * oneMinusT * oneMinusT * p0 +
        3f * oneMinusT * oneMinusT * t * p1 +
        3f * oneMinusT * t * t * p2 +
        t * t * t * p3;
    }

    public static Vector3 GetFirstDerivative (Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        /*
         * B'(t) = 3(1-t)²(p1 - p0) + 6(1-t)t(p2 - p1) + 3t²(p3 - p2)
         */ 
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return
            3f * oneMinusT * oneMinusT * (p1 - p0) +
        6f * oneMinusT * t * (p2 - p1) +
        3f * t * t * (p3 - p2);
    }
}
