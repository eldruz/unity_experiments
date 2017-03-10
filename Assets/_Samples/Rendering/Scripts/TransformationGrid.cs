using UnityEngine;
using System.Collections.Generic;

public class TransformationGrid : MonoBehaviour {

    public Transform prefab;

    public int gridResolution = 10;

    private Transform[] grid;
    private List<Transformation> transformationList;
    private Matrix4x4 transformation;

    void Awake()
    {
        grid = new Transform[gridResolution * gridResolution * gridResolution];
        for (int i=0, z=0; z < gridResolution; z++)
        {
            for (int y = 0; y < gridResolution; y++) {
                for (int x = 0; x < gridResolution; x++, i++) {
                    grid [i] = CreateGridPoint(x, y, z);
                }
            }
        }
        transformationList = new List<Transformation>();
    }

    private void Update()
    {
        UpdateTransformation();
        for (int i=0, z=0; z < gridResolution; z++)
        {
            for (int y = 0; y < gridResolution; y++) {
                for (int x = 0; x < gridResolution; x++, i++) {
                    grid [i].localPosition = TransformPoint(x, y, z);
                }
            }
        }
    }

    private void UpdateTransformation() 
    {
        GetComponents<Transformation>(transformationList);
        if (transformationList.Count > 0)
        {
            transformation = transformationList [0].Matrix;
            for (int i = 1; i < transformationList.Count; i++)
            {
                transformation = transformationList [i].Matrix * transformation;
            }
        }
    }

    private Transform CreateGridPoint(int x, int y, int z)
    {
        Transform point = Instantiate<Transform>(prefab);
        point.transform.SetParent(transform, false);
        point.localPosition = GetCoordinates(x, y, z);
        point.GetComponent<MeshRenderer>().material.color = new Color(
            (float)x / gridResolution,
            (float)y / gridResolution,
            (float)z / gridResolution
        );
        return point;
    }

    private Vector3 GetCoordinates(int x, int y, int z)
    {
        return new Vector3(
            x - (gridResolution - 1) * 0.5f,
            y - (gridResolution - 1) * 0.5f,
            z - (gridResolution - 1) * 0.5f
        );
    }

    private Vector3 TransformPoint (int x, int y, int z)
    {
        Vector3 coordinates = GetCoordinates(x, y, z);
        return transformation.MultiplyPoint(coordinates);
    }
}
