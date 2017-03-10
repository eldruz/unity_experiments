using UnityEngine;
using System.Collections;

public class TeleportPadScript : Teleporter
{

	public float distanceFromSource = 5.0f;

	void Start ()
	{
		base.Start ();
	}

	void Update ()
	{
		if (Input.GetButtonDown ("Fire2")) {
			Teleportation ();
		}
	}

	public override void Teleportation ()
	{
		switch (teleportationEnd) {
		case TeleportationEnd.Position:
			toTeleport.transform.position = transform.position;
			break;
		case TeleportationEnd.Navmesh:
			UnityEngine.AI.NavMeshHit hit;
			if (UnityEngine.AI.NavMesh.SamplePosition (transform.position, out hit, 5.0f, UnityEngine.AI.NavMesh.AllAreas)) {
				toTeleport.transform.position = hit.position;
			}
			break;
		default:
			break;
		}
		gameObject.SetActive(false);
	}
}
