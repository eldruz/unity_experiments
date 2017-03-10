using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrajectoryVisualiserScript : MonoBehaviour
{
	public TrajectoryType trajectoryType;

	[Range (0.0f, 10.0f)]
	public float factor = 1.0f;
	[Range (2, 20)]
	public int numberOfSteps = 2;
	public Vector3 controlPointOffset;

	public Transform startPoint;
	public Transform endPoint;

	public LineRenderer trajectoryLine;

	public bool debugPoints = false;

	private List<Vector3> points;

	public List<Vector3> Points {
		set { points = value; }
	}

	private List<Vector3> gizmos;
	
	// Update is called once per frame
	void Update ()
	{
		switch (trajectoryType) {
		case TrajectoryType.Parabolic:
			points = Trajectories.Calculus.ParabolicTrajectory (startPoint.position, endPoint.position,
				controlPointOffset, numberOfSteps);
			break;
		case TrajectoryType.None:
		default:
			break;
		}
		
		if (debugPoints && points != null) {
			gizmos = points;
		}

		if (points != null) {
			trajectoryLine.SetVertexCount (points.Count);
			trajectoryLine.SetPositions ((points.ToArray ()));
		}
	}

	public void OnDrawGizmos ()
	{
		if (debugPoints && gizmos != null) {
			for (int i = 0; i < gizmos.Count; i++) {
				Gizmos.DrawWireSphere (gizmos [i], 0.15f);
			}	
		}
	}
}

/*
	** Parabolic : Calculates a parabolic trajectory from start to end
	** None : Just draws a line using the points list, provided by a third party
	*/
public enum TrajectoryType
{
	Parabolic,
	None,
}