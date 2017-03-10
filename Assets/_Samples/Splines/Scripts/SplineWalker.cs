using UnityEngine;
using System.Collections;

public class SplineWalker : MonoBehaviour {

    public enum SplineWalkerMode
    {
        Once,
        Loop,
        PingPong,
    }

    public BezierSpline spline;
    public SplineWalkerMode mode;

    public float duration;
    public bool lookForward;

    private float progress;
    private bool goingForward = true;
	
	void Update () {
        if (goingForward)
        {
            progress += Time.deltaTime / duration;
            if (progress > 1f)
            {
                if (mode == SplineWalkerMode.Once)
                {
                    progress = 1f;
                }
                else if (mode == SplineWalkerMode.Loop)
                {
                    progress -= 1f;
                }
                else if (mode == SplineWalkerMode.PingPong)
                {
                    progress = 2f - progress;
                    goingForward = false;
                }
            }
        }
        else
        {
            progress -= Time.deltaTime / duration;
            if (progress < 0f)
            {
                progress = -progress;
                goingForward = true;
            }
        }

        Vector3 position = spline.GetPoint(progress);
        transform.localPosition = position;
        if (lookForward && goingForward == true)
        {
            transform.LookAt(position + spline.GetDirection(progress));
        }
        else
        {
            transform.LookAt(position - spline.GetDirection(progress));
        }
	}
}
