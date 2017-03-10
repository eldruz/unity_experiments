using UnityEngine;
using System.Collections;

public class InteractableObject : MonoBehaviour, IInteractable
{
	private Renderer myRenderer;

	public Color colorOnFocus;
	public Color colorOnBlur;
	public float timeForActivation = 3f;

	private bool isActivated = false;

	// Use this for initialization
	void Start ()
	{
		myRenderer = GetComponent <Renderer> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void onFocus ()
	{
		myRenderer.material.color = colorOnFocus;
	}

	public void onBlur ()
	{
		myRenderer.material.color = colorOnBlur;
	}

	public void onActivate ()
	{
		isActivated = !isActivated;
		myRenderer.material.color = Random.ColorHSV ();
	}
}
