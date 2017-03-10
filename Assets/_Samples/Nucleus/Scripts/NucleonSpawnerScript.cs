using UnityEngine;

public class NucleonSpawnerScript : MonoBehaviour {

    public float timeBetweenSpawns;
    public float spawnDistance;
    public NucleonScript[] nucleonPrefabs;

    private float timeSinceLastSpawn;

	void FixedUpdate () 
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= timeBetweenSpawns) {
            timeSinceLastSpawn -= timeBetweenSpawns;
            SpawnNucleon();
        }
	}

    void SpawnNucleon()
    {
        NucleonScript prefab = nucleonPrefabs [Random.Range(0, nucleonPrefabs.Length)];
        NucleonScript spawn = Instantiate<NucleonScript>(prefab);
        spawn.transform.SetParent(transform, false);
        spawn.transform.localPosition = Random.onUnitSphere * spawnDistance;
    }
}
