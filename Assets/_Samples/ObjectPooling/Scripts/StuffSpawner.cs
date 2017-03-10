using UnityEngine;
using System.Collections;

public class StuffSpawner : MonoBehaviour
{

	public FloatRange timeBetweenSpawns;
	public FloatRange scale;
	public FloatRange randomVelocity;
	public FloatRange angularVelocity;
	public float velocity;

	public Stuff[] stuffPrefabs;
    public Material stuffMaterial;

	private float timeSinceLastSpawn;

	private float currentSpawnDelay;

	void FixedUpdate ()
	{
		timeSinceLastSpawn += Time.deltaTime;
		if (timeSinceLastSpawn >= currentSpawnDelay) {
			timeSinceLastSpawn -= currentSpawnDelay;
			currentSpawnDelay = timeBetweenSpawns.RandomInRange;
			SpawnStuff ();
		}
	}

	void SpawnStuff ()
	{
		Stuff prefab = stuffPrefabs [Random.Range (0, stuffPrefabs.Length)];
        Stuff spawn = prefab.GetPooledInstance<Stuff>();

        spawn.SetMaterial(stuffMaterial);

		spawn.transform.localPosition = transform.position;
		spawn.transform.localScale = Vector3.one * scale.RandomInRange;
		spawn.transform.localRotation = Random.rotation;

		spawn.rb.velocity = transform.up * velocity + Random.onUnitSphere * randomVelocity.RandomInRange;
		spawn.rb.angularVelocity = Random.onUnitSphere * angularVelocity.RandomInRange;
	}
}
