using UnityEngine;
using System.Collections.Generic;

public class FireCanonScript : MonoBehaviour
{
	public Transform firePoint;
	public Transform target;
	public float height = 4f;
	public float timeToTarget = 1f;
	public float launchVelocity;

	public GameObject bulletPrefab;
	private GameObject bullet;

	public Camera camera;

	public GameObject explosion;

	private List<Vector3> points;
	private TrajectoryVisualiserScript trajectoryVis;
	private Vector3 lastMousePosition;

	private Vector3 throwSpeed;

	// Use this for initialization
	void Start ()
	{
		trajectoryVis = GetComponent <TrajectoryVisualiserScript> ();
		trajectoryVis.trajectoryType = TrajectoryType.None;
		trajectoryVis.enabled = false;

		points = new List<Vector3> ();

		bullet = (GameObject)Instantiate (bulletPrefab);
		bullet.SetActive (false);
	}

	List<Vector3> ComputeTrajectory ()
	{
		return Trajectories.Physics.SimulateThrow (firePoint.position, throwSpeed, 10, 1f, timeToTarget);
	}

	void FixedUpdate ()
	{
		if (!Vector3.Equals (Input.mousePosition, lastMousePosition)) {
			lastMousePosition = Input.mousePosition;

			Ray mouseRay = camera.ScreenPointToRay (Input.mousePosition);

			RaycastHit hitInfo;
			if (Physics.Raycast (mouseRay, out hitInfo)) {
				target.transform.position = hitInfo.point;

				// trajectory calculations
				throwSpeed = Trajectories.Physics.CalculateBestThrowSpeed
					(firePoint.position, target.transform.position, timeToTarget);
				launchVelocity = throwSpeed.magnitude;
				points = ComputeTrajectory ();
				trajectoryVis.Points = points;
				trajectoryVis.enabled = true;

				// Input Management
			} else {
				trajectoryVis.enabled = false;
			}
		} 
	}

	void Update ()
	{
		if (Input.GetButtonDown ("Fire1")) {
			// bullet toss
			bullet.transform.position = firePoint.position;
			bullet.transform.rotation = firePoint.rotation;
			Rigidbody bulletRb = bullet.GetComponent <Rigidbody> ();
			bulletRb.velocity = Vector3.zero;
			throwSpeed = Trajectories.Physics.CalculateBestThrowSpeed
				(firePoint.position, target.transform.position, timeToTarget);
			bullet.SetActive (true);
			bulletRb.AddForce (throwSpeed, ForceMode.VelocityChange);
			// Instantiate(explosion, firePoint.position, Quaternion.identity);
		}
	}

}
