using UnityEngine;

public class TeleporterTossScript : MonoBehaviour
{

	public GameObject teleporterPrefab;
	public Transform tossPoint;

	[Range (1f, 20f)]
	public float thrust = 4f;

	private GameObject teleporterInstance;

	void Start ()
	{
		teleporterInstance = (GameObject)Instantiate (teleporterPrefab);
		teleporterInstance.GetComponent <Teleporter> ().toTeleport = gameObject;
		teleporterInstance.SetActive (false);
	}

	void Update ()
	{
		if (Input.GetButtonDown ("Fire1")) {
			teleporterInstance.transform.position = tossPoint.position;
			teleporterInstance.transform.rotation = tossPoint.rotation;
			Vector3 direction = tossPoint.forward;
			teleporterInstance.GetComponent<Rigidbody>().velocity = Vector3.zero;
			teleporterInstance.SetActive (true);
			teleporterInstance.GetComponent <Rigidbody> ().AddForce (direction * thrust, ForceMode.Impulse);
		}
	}
}
