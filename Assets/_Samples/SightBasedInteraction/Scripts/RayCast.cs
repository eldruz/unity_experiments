using UnityEngine;
using System.Collections;

public abstract class RayCast : MonoBehaviour
{
	public Transform raySource;
	public Transform rayDestination;
	public float rayLength;

	abstract public bool castRay ();

}
