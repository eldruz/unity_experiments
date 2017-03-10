using UnityEngine;
using System.Collections;

[RequireComponent (typeof(FPSCounter))]
public class FPSDisplay : MonoBehaviour
{
	[System.Serializable]
	private struct FPSColor
	{
		public Color color;
		public int minimumFPS;
	}

	public UnityEngine.UI.Text highestFPSLabel;
	public UnityEngine.UI.Text lowestFPSLabel;
	public UnityEngine.UI.Text averageFPSLabel;

	private FPSCounter fpsCounter;
	[SerializeField]
	private FPSColor[] coloring;

	void Awake ()
	{
		fpsCounter = GetComponent <FPSCounter> ();
	}

	void Update ()
	{
		Display (averageFPSLabel, fpsCounter.AverageFPS);
		Display (lowestFPSLabel, fpsCounter.LowestFPS);
		Display (highestFPSLabel, fpsCounter.HighestFPS);
	}

	void Display (UnityEngine.UI.Text label, int fps)
	{
		label.text = Mathf.Clamp (fps, 0, 99).ToString ();
		for (int i = 0; i < coloring.Length; i++) {
			if (fps >= coloring [i].minimumFPS) {
				label.color = coloring [i].color;
				break;
			}
		}
	}

}
