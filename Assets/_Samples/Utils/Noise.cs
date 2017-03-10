using UnityEngine;
using System.Collections;

public static class Noise {

    // 0 <= persistance <= 1
    // lacunarity >= 1
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed,
        float scale, int octaves, float persistance, float lacunarity,
        Vector2 offset)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        System.Random rng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];
        for (int i = 0; i < octaves; i++)
        {
            float offsetX = rng.Next(-100000, 100000) + offset.x;
            float offsetY = rng.Next(-100000, 100000) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        if (scale <= 0)
            scale = 0.001f;

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float amplitude = 1.0f;
                float frequency = 1.0f;
                float noiseHeight = 0f;

                for (int i = 0; i < octaves; i++)
                {
                    // halfXXX : changing the scale zooms towards the center
                    float sampleX = (x-halfWidth) / scale * frequency + octaveOffsets[i].x;
                    float sampleY = (y-halfHeight) / scale * frequency + octaveOffsets[i].y;
                    
                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                noiseMap[x,y] = noiseHeight;
                maxNoiseHeight = Mathf.Max(noiseHeight, maxNoiseHeight);
                minNoiseHeight = Mathf.Min(noiseHeight, minNoiseHeight);
            }
        }

        // Normalization of the noisemap
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                noiseMap[x,y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x,y]);
            }
        }

        return noiseMap;
    }

}
