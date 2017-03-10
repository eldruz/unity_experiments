using UnityEngine;

public class Vortex : MonoBehaviour {
	public float attractionForce = 1f;
	public float rangeDistance = 5f;
	public float nucleusRadius = 1f;

	private bool isVortexActive;
	public bool IsVortexActive
	{
		get
		{
			return isVortexActive;
		}
		set
		{
			isVortexActive = value;
			activationRange.gameObject.SetActive(value);
			nucleus.gameObject.SetActive(value);

			if (!isVortexActive && OnDeactivation != null)
			{
				OnDeactivation(this);
			}
		}
	}

	private SphereCollider activationRange;
	private SphereCollider nucleus;

	public delegate void DeActivateVortex(Vortex v);
	public event DeActivateVortex OnDeactivation;

	public delegate void ExplodeVortex(Vortex v);
	public event ExplodeVortex OnExplosion;

	private void Awake()
	{
		isVortexActive = true;

		activationRange = gameObject.AddComponent<SphereCollider>();
		activationRange.isTrigger = true;
		activationRange.radius = rangeDistance;

		nucleus = gameObject.AddComponent<SphereCollider>();
		nucleus.isTrigger = false;
		nucleus.radius = nucleusRadius;
	}

	private void Update()
	{
		activationRange.radius = rangeDistance;
		nucleus.radius = nucleusRadius;
	}

	public void Explode()
	{
		if (OnExplosion != null)
		{
			OnExplosion(this);
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, rangeDistance);
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(transform.position, nucleusRadius);
	}

}
