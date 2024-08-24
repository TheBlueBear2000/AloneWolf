using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    public Vector2Int chunkSize;
    public float noiseScale;
    public int itterations;
    public int octaves;
    public float persistance;
    public float lacunarity;

    public void GenerateWorldMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(chunkSize, noiseScale, octaves, persistance, lacunarity, 1);


    }
}
