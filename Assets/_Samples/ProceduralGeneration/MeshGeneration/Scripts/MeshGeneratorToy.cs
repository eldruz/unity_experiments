using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MeshGeneratorToy : MonoBehaviour {

    public int xSize;
    public int ySize;
    public MeshType type;

    [Range(0, 6)]
    public int levelOfDetail = 0;

    private MeshData meshData;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    private void Update()
    {
        switch (type)
        {
            case MeshType.Grid:
                GenerateGrid();
                break;
            case MeshType.Cube:
                GenerateCube();
                break;
            default:
                break;
        }
    }

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void GenerateGrid()
    {
        meshData = MeshGenerator.GenerateFlatGrid(xSize, ySize, levelOfDetail);
        meshFilter.sharedMesh = meshData.CreateMesh();
    }

    public void GenerateCube()
    {
        meshData = null;
    }

    private void OnDrawGizmos()
    {
        if (meshData == null)
        {
            return;
        }
        Gizmos.color = Color.black;
        for (int i = 0; i < meshData.vertices.Length; i++)
        {
            Gizmos.DrawSphere(transform.TransformPoint(meshData.vertices [i]), 0.1f);
        }
    }

    public enum MeshType 
    {
        Grid,
        Cube,
    }
}