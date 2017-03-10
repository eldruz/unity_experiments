using UnityEngine;
using System.Collections;

public class RayTracer : RayCast
{

	public Transform rayEmitter;
	public LineRenderer laser;
	public bool displayLaser = false;

	private InteractableObject lastObjectHit;
	private InteractableObject currentObjectHit;
	private float timeElapsedOnObject = 0f;

	// Use this for initialization
	void Start ()
	{
		laser.enabled = false;
	}

	public override bool castRay ()
	{
		Ray sightRay = new Ray (raySource.position, raySource.forward);
		RaycastHit hitObject;
		InteractableObject interactableHit = null;
		// Check if we hit something
		bool rayResult = Physics.Raycast (sightRay, out hitObject);

		if (displayLaser) {
			laser.enabled = true;
			laser.SetPositions (new Vector3[] { rayEmitter.position, sightRay.GetPoint (rayLength) });
		} else {
			laser.enabled = false;
		}

		// If we do, check it has an InteractableObject component
		if (rayResult) {
			interactableHit = hitObject.collider.gameObject.GetComponent < InteractableObject> ();
		}

		if (interactableHit) {
			currentObjectHit = interactableHit;
			return true;
		} else {
			currentObjectHit = null;
			return false;
		}
	}

	// Update is called once per frame
	void Update ()
	{

		if (Input.GetButtonDown ("Fire1")) {
			if (lastObjectHit) {
				lastObjectHit.onActivate ();
			}
		}

		bool hit = castRay ();

		// If we hit an interactable object
		if (hit && currentObjectHit != lastObjectHit) {
			if (lastObjectHit) {
				lastObjectHit.onBlur ();
			}
			// store the object hit and call its onFocus() method
			lastObjectHit = currentObjectHit;
			lastObjectHit.onFocus ();

		} else if (currentObjectHit && currentObjectHit == lastObjectHit) {
			timeElapsedOnObject += Time.deltaTime;
			if (timeElapsedOnObject > currentObjectHit.timeForActivation)
			{
				currentObjectHit.onActivate();
			}	
		} else {
			timeElapsedOnObject = 0f;
			if (lastObjectHit) {
				lastObjectHit.onBlur ();	
				lastObjectHit = null;
			}
		}
	}

}