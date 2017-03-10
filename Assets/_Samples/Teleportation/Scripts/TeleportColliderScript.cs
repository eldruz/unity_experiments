using UnityEngine;
using System.Collections;

public class TeleportColliderScript : Teleporter
{
	private Collider collider;

	void Start ()
	{
		base.Start ();
		collider = GetComponent <Collider> ();
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") {
			return;
		}

		Teleportation ();
	}

	public override void Teleportation ()
	{
		rb.velocity = Vector3.zero;
		toTeleport.transform.position = transform.position;
		gameObject.SetActive (false);
	}
}
