using UnityEngine;

public class MapGenerator : MonoBehaviour {
    public enum DrawMode {NoiseMap, ColorMap, Mesh};
    public DrawMode drawMode;

    private const int mapChunkSize = 241;
    public static int MapChunkSize
    {
        get
        {
            return mapChunkSize;
        }
    }
    [Range(0,6)] public int levelOfDetail;

    [Range(0.001f, 100.0f)] public float noiseScale;

    [Range(0, 10)] public int octaves;
    [Range(0.0f, 1.0f)] public float persistance;
    [Range(1.0f, 10.0f)] public float lacunarity;

    public int seed;
    public Vector2 offset;

    public float meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;

    public bool autoUpdate = true;

    public TerrainType[] regions;

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapChunkSize, mapChunkSize, seed,
            noiseScale, octaves, persistance, lacunarity, offset);

        Color[] colorMap = new Color[mapChunkSize * mapChunkSize];
        for (int y = 0; y < mapChunkSize; y++)
        {
            for (int x = 0; x < mapChunkSize; x++)
            {
                float currentHeight = noiseMap [x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight <= regions[i].height)
                    {
                        colorMap [y * mapChunkSize + x] = regions [i].color;
                        break;
                    }
                }
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();
        switch (drawMode) 
        {
            case DrawMode.NoiseMap:
                display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
                break;
            case DrawMode.ColorMap:
                display.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, mapChunkSize, mapChunkSize));
                break;
            case DrawMode.Mesh:
                display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplier, meshHeightCurve, levelOfDetail),
                   TextureGenerator.TextureFromColorMap(colorMap, mapChunkSize, mapChunkSize));
                break;
        }
    }

    void OnValidate()
    {
        lacunarity = Mathf.Max(lacunarity, 1f);
        octaves = Mathf.Max(octaves, 0);
    }

}

[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color color;
}
