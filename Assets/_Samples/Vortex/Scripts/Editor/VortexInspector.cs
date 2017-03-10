using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Vortex))]
public class VortexInspector : Editor
{

    public override void OnInspectorGUI()
    {
        Vortex v = (Vortex)target;

        if (DrawDefaultInspector())
        {
        }
        if (GUILayout.Button("Toggle Activation"))
        {
            bool state = v.IsVortexActive;
            v.IsVortexActive = !state;
        }
        if (GUILayout.Button("EXPLOSION"))
        {
            v.Explode();
        }
    }
}
