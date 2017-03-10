using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Vortexable : MonoBehaviour {
	public List<Vortex> vortexList;

	private bool activated;

	private Rigidbody rb;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		if (activated)
		{
			Vector3 summedForce = new Vector3();
			foreach (Vortex v in vortexList)
			{
				Vector3 vortexPosition = v.transform.position;
				Vector3 forceDirection = vortexPosition - transform.position;
				float distance = Vector3.Distance(transform.position, v.transform.position);
				summedForce += forceDirection.normalized * v.attractionForce / distance;
			}
			rb.AddForce(summedForce);
		}
	}

	private void OnEnable()
	{
		foreach (Vortex v in vortexList)
		{
			v.OnDeactivation += OnVortexDeactivation;
			v.OnExplosion += OnVortexExplosion;
		}
	}

	private void OnDisable()
	{
		foreach (Vortex v in vortexList)
		{
			v.OnDeactivation -= OnVortexDeactivation;
			v.OnExplosion -= OnVortexExplosion;
		}
	}

	private void OnVortexDeactivation(Vortex v)
	{
		vortexList.Remove(v);
		v.OnDeactivation -= OnVortexDeactivation;
		v.OnExplosion -= OnVortexExplosion;
		if (vortexList.Count == 0)
		{
			activated = false;
			rb.useGravity = true;
		}
	}

	private void OnVortexExplosion(Vortex v)
	{
		rb.AddExplosionForce(v.attractionForce / 2f, v.transform.position,
			v.rangeDistance, 2.0f, ForceMode.Impulse);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<Vortex>())
		{
			activated = true;
			rb.useGravity = false;
			Vortex v = other.GetComponent<Vortex>();
			if (!vortexList.Contains(v))
			{
				vortexList.Add(other.GetComponent<Vortex>());
				v.OnDeactivation += OnVortexDeactivation;
				v.OnExplosion += OnVortexExplosion;
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.GetComponent<Vortex>())
		{
			OnVortexDeactivation(other.GetComponent<Vortex>());
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.black;
		foreach (Vortex v in vortexList)
		{
			Gizmos.DrawLine(transform.position, v.transform.position);
		}
	}
}
