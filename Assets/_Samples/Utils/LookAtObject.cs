using UnityEngine;
using System.Collections;

public class LookAtObject : MonoBehaviour {

	public Transform toFollow;

	void Update () {
		transform.LookAt(toFollow);
	}
}
