using UnityEngine;

public class CrosshairGUI : MonoBehaviour
{
    public Texture2D crosshairTexture;
    public bool drawCrosshair = true;

    private Rect position;

    private void Start()
    {
        position = new Rect((Screen.width - crosshairTexture.width) / 2, (Screen.height -
            crosshairTexture.height) / 2, crosshairTexture.width, crosshairTexture.height);
    }

    private void OnGUI()
    {
        if (drawCrosshair)
        {
            GUI.DrawTexture(position, crosshairTexture);
        }
    }

}
