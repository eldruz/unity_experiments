using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(MeshGeneratorToy))]
public class MeshGeneratorToyEditor : Editor {

    public override void OnInspectorGUI()
    {
        MeshGeneratorToy meshGen = (MeshGeneratorToy)target;

        if (DrawDefaultInspector())
        {
            
        }
        if (GUILayout.Button("Grid"))
        {
            meshGen.type = MeshGeneratorToy.MeshType.Grid;
        }
    }
}
