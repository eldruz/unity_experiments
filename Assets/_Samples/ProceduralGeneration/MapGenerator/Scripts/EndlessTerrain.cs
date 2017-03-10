using UnityEngine;
using System.Collections.Generic;

public class EndlessTerrain : MonoBehaviour {

    public const float maxViewDist = 450f;
    public Transform viewer;

    public static Vector2 viewerPosition;

    private int chunkSize;
    private int chunksVisibleInViewDist;

    private Dictionary<Vector2, TerrainChunk> terrainChunkDictionay = new Dictionary<Vector2, TerrainChunk>();
    private List<TerrainChunk> terrainChunkVisibleLastUpdate = new List<TerrainChunk>();
    void Start()
    {
        chunkSize = MapGenerator.MapChunkSize - 1;
        chunksVisibleInViewDist = Mathf.RoundToInt(maxViewDist / chunkSize);
    }

    void Update()
    {
        viewerPosition = new Vector2(viewer.position.x, viewer.position.z);
        UpdateVisibleChunks(); 
    }

    void UpdateVisibleChunks()
    {
        // Taking care of the chunks that were visible last frame
        for (int i = 0; i < terrainChunkVisibleLastUpdate.Count; i++)
        {
            terrainChunkVisibleLastUpdate [i].SetVisible(false);
        }
        terrainChunkVisibleLastUpdate.Clear();

        int currentChunkCoordX = Mathf.RoundToInt(viewerPosition.x / chunkSize);
        int currentChunkCoordY = Mathf.RoundToInt(viewerPosition.y / chunkSize);
        for (int yOffest = -chunksVisibleInViewDist; yOffest <= chunksVisibleInViewDist; yOffest++)
        {
            for (int xOffset = -chunksVisibleInViewDist; xOffset <= chunksVisibleInViewDist; xOffset++)
            {
                Vector2 viewedChunkCoord = new Vector2(currentChunkCoordX + xOffset, currentChunkCoordY + yOffest); 

                if (terrainChunkDictionay.ContainsKey(viewedChunkCoord))
                {
                    terrainChunkDictionay[viewedChunkCoord].UpdateTerrainChunk();
                    if (terrainChunkDictionay[viewedChunkCoord].IsVisible())
                    {
                        terrainChunkVisibleLastUpdate.Add(terrainChunkDictionay [viewedChunkCoord]);
                    }
                } else
                {
                    terrainChunkDictionay.Add(viewedChunkCoord, new TerrainChunk(viewedChunkCoord, chunkSize, transform));
                }
            }
        }
    }

    public class TerrainChunk
    {
        GameObject meshObject;
        Vector2 position;
        Bounds bounds;

        public TerrainChunk(Vector2 coord, int size, Transform parent)
        {
            position = coord * size;
            bounds = new Bounds(position, Vector2.one * size);
            Vector3 positionV3 = new Vector3(position.x, 0, position.y);

            meshObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
            meshObject.transform.position = positionV3;
            meshObject.transform.localScale = Vector3.one * size / 10f;
            meshObject.transform.parent = parent;
            SetVisible(false);
        }

        public void UpdateTerrainChunk()
        {
            float viewerDistanceFromNearestEdge = Mathf.Sqrt(bounds.SqrDistance(viewerPosition));
            bool visible = viewerDistanceFromNearestEdge <= maxViewDist;
            SetVisible(visible);
        }

        public void SetVisible(bool visible)
        {
            meshObject.SetActive(visible);
        }

        public bool IsVisible()
        {
            return meshObject.activeSelf;
        }
    }
}
