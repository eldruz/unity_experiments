using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class NucleonScript : MonoBehaviour {

    public float attractionForce;

    private Rigidbody rb;

	void Start () {
        rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate () {
        rb.AddForce(transform.localPosition * -attractionForce);
        // rb.AddForce(transform.position * -attractionForce);
	}
}
