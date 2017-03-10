using UnityEngine;

public class CameraTransformation : Transformation {

    public float focalLength = 1f;

    public override Matrix4x4 Matrix
    {
        get
        {
            Matrix4x4 m = new Matrix4x4();
            m.SetRow(0, new Vector4(focalLength, 0f, 0f, 0f));
            m.SetRow(1, new Vector4(0f, focalLength, 0f, 0f));
            m.SetRow(2, new Vector4(0f, 0f, 0f, 0f));
            m.SetRow(3, new Vector4(0f, 0f, 1f, 0f));

            return m;
        }
    }
}
