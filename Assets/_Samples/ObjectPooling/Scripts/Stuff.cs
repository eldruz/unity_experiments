using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Stuff : PooledObject
{
	public Rigidbody rb { get; private set; }

    private MeshRenderer[] meshRenderers;

    public  void SetMaterial (Material Mathf)
    {
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            meshRenderers [i].material = Mathf;
        }
    }

	void Awake ()
	{
		rb = GetComponent <Rigidbody> ();
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
	}

    void OnEnable()
    {
        // Register the OnLoad function to the sceneLoaded delegate
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnLoad;
    }

    void onDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnLoad;
    }

	void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag ("KillZone")) {
            ReturnToPool();
		}
	}

    void OnLoad(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
//        ReturnToPool();
    }
}
