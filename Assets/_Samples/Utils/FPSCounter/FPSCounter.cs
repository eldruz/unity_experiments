using UnityEngine;

public class FPSCounter : MonoBehaviour
{
	public int frameRange = 60;

	public int AverageFPS { get; private set; }

	public int HighestFPS { get; private set; }

	public int LowestFPS { get; private set; }

	private int[] fpsBuffer;
	private int fpsBufferIndex;

	void InitializeBuffer ()
	{
		if (frameRange <= 0) {
			frameRange = 1;
		}
		fpsBuffer = new int[frameRange];
		fpsBufferIndex = 0;
	}

	void Update ()
	{
		if (fpsBuffer == null || fpsBuffer.Length != frameRange) {
			InitializeBuffer ();
		}
		UpdateBuffer ();
		CalculateFPS ();
	}

	void UpdateBuffer ()
	{
		fpsBuffer [fpsBufferIndex++] = (int)(1f / Time.unscaledDeltaTime);
		if (fpsBufferIndex >= frameRange) {
			fpsBufferIndex = 0;
		}
	}

	void CalculateFPS ()
	{
		int sum = 0;
		int highest = int.MinValue;
		int lowest = int.MaxValue;
		for (int i = 0; i < frameRange; i++) {
			int fps = fpsBuffer [i];
			sum += fps;
			highest = Mathf.Max (highest, fps);
			lowest = Mathf.Min (lowest, fps);
		}
		AverageFPS = sum / frameRange;
		HighestFPS = highest;
		LowestFPS = lowest;
	}

}
