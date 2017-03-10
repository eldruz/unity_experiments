using UnityEngine;
using System.Collections.Generic;

namespace Trajectories
{

	public static class Physics
	{
		public static Vector3 CalculateBestThrowSpeed (Vector3 origin, Vector3 target, float timeToTarget)
		{
			// calculate vectors
			Vector3 toTarget = target - origin;
			Vector3 toTargetXZ = toTarget;
			toTargetXZ.y = 0;

			// calculate xz and y
			float y = toTarget.y;
			float xz = toTargetXZ.magnitude;

			// calculate starting speeds for xz and y.
			// deltaX = v0 * t + 1/2 * a * t²
			// where a is "gravity" on the y axis only
			// xz = v0xz *t => v0xz = xz / t
			// y = v0y * t - 1/2 * gravity * t² => v0y * t = y + 1/2 * gravity * t² => v0y = y/t + 1/2 * gravity * t
			float t = timeToTarget;
			float v0y = y / t + 0.5f * UnityEngine.Physics.gravity.magnitude * t;
			float v0xz = xz / t;

			// create result vector for calculated starting speeds
			Vector3 result = toTargetXZ.normalized;		// get direction of xz but with magnitude 1
			result *= v0xz;								// set magnitude of xz to v0xz (starting speed in xz plane)
			result.y = v0y;								// set y to v0y (starting speed of y plane)

			return result;
		}

		public static List<Vector3> SimulateThrow (Vector3 origin, Vector3 segVelocity,
		                                           int segmentCount, float segmentScale, float timeToTarget)
		{
			List<Vector3> segments = new List<Vector3> ();

			segments.Add (origin);

			Collider hitObject = null;

			for (int i = 1; i < segmentCount; i++) {
				float segTime = (segVelocity.sqrMagnitude != 0f) ? segmentScale / ((float)segmentCount / timeToTarget) : 0;
				//float segTime = (float)segmentCount / timeToTarget;
				segVelocity = segVelocity + UnityEngine.Physics.gravity * segTime;

				// Check for bounces
				RaycastHit hit;
				if (UnityEngine.Physics.Raycast (segments [i - 1], segVelocity, out hit, segmentScale))
				{
					// Remember what we hit
					hitObject = hit.collider;

					// set next position to the position where we hit the physics object
					segments.Add (segments [i - 1] + segVelocity.normalized * hit.distance);
					return segments;
					// correct ending velocity, since we didn't actually travel an entire segment
					// segVelocity = segVelocity - UnityEngine.Physics.gravity * (segmentScale - hit.distance) / segVelocity.magnitude;
					// flip the velocity to simulate a bounce
					// segVelocity = Vector3.Reflect (segVelocity, hit.normal);

					// TODO: check the properties of the object we hit
				}
				// If we dont' hit anything
				// else
				// {
					segments.Add (segments [i - 1] + segVelocity * segTime);
				// }
			}

			return segments;
		}
	}

	public static class Calculus
	{

		public static Vector3 GetQuadraticCoordinates (Vector3 p0, Vector3 p1, Vector3 c0, float t, float factor = 1.0f)
		{
			return Mathf.Pow (1 - t, 2) * p0 + 2 * t * (1 - t) * factor * c0 + Mathf.Pow (t, 2) * p1;
		}

		public static List<Vector3> ParabolicTrajectory (Vector3 start, Vector3 end,
		                                                 Vector3 controlPointOffset, int numberOfSteps)
		{
			List<Vector3> points = new List<Vector3> ();

			// calculate the directional vector between start and end
			Vector3 heading = end - start;
			Vector3 direction = Vector3.Normalize (heading);
			float distance = Vector3.Distance (end, start);

			Vector3 intermediate = start + (distance / 2) * direction + controlPointOffset;

			for (int i = 0; i < numberOfSteps; i++) {
				float t = (float)i / (float)(numberOfSteps - 1);
				points.Add (GetQuadraticCoordinates (start, end, intermediate, t));
			}
			return points;
		}
	}
}