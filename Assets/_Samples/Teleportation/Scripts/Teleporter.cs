using UnityEngine;

public abstract class Teleporter : MonoBehaviour
{
	public GameObject toTeleport;
	public TeleportationEnd teleportationEnd;

	protected Rigidbody rb;
	// Use this for initialization
	protected void Init ()
	{
		rb = GetComponent <Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public abstract void Teleportation ();

}

public enum TeleportationEnd
{
	Position,
	Navmesh,
}