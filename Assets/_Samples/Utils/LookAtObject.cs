using UnityEngine;

public class LookAtObject : MonoBehaviour {

	public Transform toFollow;

	void Update () {
		transform.LookAt(toFollow);
	}
}
