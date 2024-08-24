using System.Collections;
using UnityEngine;
using System;

public static class Noise
{
    public static float[,] GenerateNoiseMap(Vector2Int size, float scale, int octaves, float persistance, float lacunarity, float seed)
    {
        float[,] noiseMap = new float[size.x, size.y];

        // Raw map gen
        if (scale <= 0) { scale = 0.0001f; }

        for (int y = 0; y < size.y; y++)
            for (int x = 0; x < size.x; x++)
            {

                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = x / scale * frequency + (seed * 1000);
                    float sampleY = y / scale * frequency + (seed * 1000);

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                noiseMap[x, y] = noiseHeight;
            }
        return noiseMap;
    }

    public static float[,] Smoothing(float[,] rawNoiseMap, int smoothingSteps, Vector2Int size)
    {
        // Smoothing

        float[,] noiseMap = new float[size.x, size.y];
        for (int i = 0; i < smoothingSteps; i++)
        {
            noiseMap = rawNoiseMap;
            for (int y = 1; y < size.y - 1; y++)
                for (int x = 1; x < size.x - 1; x++)
                {
                    float sourceValue = 0;
                    for (int subY = -1; subY <= 1; subY++)
                        for (int subX = -1; subX <= 1; subX++)
                        {
                            sourceValue += rawNoiseMap[x + subX, y + subY];
                        }
                    sourceValue /= 9;
                    noiseMap[x, y] = sourceValue;
                }
            rawNoiseMap = noiseMap;
        }
        if (smoothingSteps <= 0) { noiseMap = rawNoiseMap; }

        return noiseMap;
    }

    public static float[,] AddRivers(float[,] noiseMap, float[,] riverNoiseMap, float riverHeight, float beachHeight, float beachDepth, Vector2Int size)
    {
        for (int y = 0; y < size.y; y++)
            for (int x = 0; x < size.x; x++)
            {
                float newValue = riverNoiseMap[x, y];
                newValue -= 0.5f;
                newValue = Math.Abs(newValue);

                if (newValue < riverHeight && noiseMap[x, y] > 0.5f) { noiseMap[x, y] = 0.54f; }
                else if (newValue < beachHeight && noiseMap[x, y] > 0.5f)
                {
                    if (noiseMap[x, y] - beachDepth > 0.6) { noiseMap[x, y] -= beachDepth; }
                    else { noiseMap[x, y] = 0.59f; }
                }

            }
        return noiseMap;
    }

}
