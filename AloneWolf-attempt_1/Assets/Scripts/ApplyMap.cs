using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public enum BiomeLookup
{
    //Wet            Regular         Dry
    Rainforest, Savana_Fields, Desert,          // Hot
    Marshy_Fields, Fields, Woods, Dry_Plains,   // Mid Temp
    Snowy_Fields, Snowy_Woods, Tundra,          // Cold
    Sea, Shore, Beach, River, RiverShore, NULL  // Misc
}

public float seed = 0.0f;
public Tilemap Tilemap;
public Tilemap FlowerTilemap;
public Tilemap TreesTilemap;
public Tile[] Tiles;
public Tile[] FlowerTiles;
public Vector2Int Size;

public class ApplyMap : MonoBehaviour
{

    
    public float noiseScale;
    public int octaves;
    public float persistance;
    public float lacunarity;
    public int smoothingSteps;
    

    public BiomeLookup[,] Biomes =
    {
        {BiomeLookup.Rainforest,BiomeLookup.Savana_Fields,BiomeLookup.Desert},
        {BiomeLookup.Marshy_Fields,BiomeLookup.Fields,BiomeLookup.Dry_Plains},
        {BiomeLookup.Snowy_Fields,BiomeLookup.Snowy_Woods,BiomeLookup.Tundra}
    };

    public void Awake()
    {
        Tilemap.ClearAllTiles();

        while (seed > 988) { seed /= 10; } // Cap seed to 998w

        float[,] heightMap = Noise.GenerateNoiseMap(Size, noiseScale, octaves, persistance, lacunarity, seed);
        float[,] riversMap = Noise.GenerateNoiseMap(Size, noiseScale, 3, 0.3f, 1.5f, seed + 5);
        heightMap = Noise.AddRivers(heightMap, riversMap, 0.04f, 0.055f, 0.2f, Size);
        heightMap = Noise.Smoothing(heightMap, smoothingSteps, Size);

        float[,] heatMap = Noise.Smoothing( Noise.GenerateNoiseMap(Size, noiseScale * 2, 3, 0.3f, 1.5f, seed ), 1, Size);
        float[,] waterMap = Noise.Smoothing( Noise.GenerateNoiseMap(Size, noiseScale * 2, 3, 0.3f, 1.5f, seed+5 ), 1, Size);
        float[,] SubBiomeMap = Noise.Smoothing(Noise.GenerateNoiseMap(Size, noiseScale / 10, 3, 0.3f, 1.5f, seed + 10), 1, Size);
        float[,] SubMap1 = Noise.Smoothing( Noise.GenerateNoiseMap(Size, noiseScale /10, 3, 0.3f, 1.5f, seed+10 ), 1, Size);
        float[,] SubMap2 = Noise.Smoothing( Noise.GenerateNoiseMap(Size, noiseScale /10, 3, 0.3f, 1.5f, seed+11 ), 1, Size);
        float[,] SubMap3 = Noise.Smoothing( Noise.GenerateNoiseMap(Size, noiseScale /10, 3, 0.3f, 1.5f, seed+12 ), 1, Size);

        BiomeLookup[,] BiomeMap = new BiomeLookup[Size.x, Size.y];

        for (int y = 0; y < Size.y; y++)
            for (int x = 0; x < Size.x; x++)
            {

                if (heightMap[x, y] < 0.5) { Tilemap.SetTile(new Vector3Int(x, y, 0), Tiles[0]); }
                else if (heightMap[x, y] < 0.55) { Tilemap.SetTile(new Vector3Int(x, y, 0), Tiles[1]); }


                else if (heightMap[x, y] < 0.9) // Land and Beaches
                {

                    if (heightMap[x, y] < 0.6) { Tilemap.SetTile(new Vector3Int(x, y, 0), Tiles[2]); } // Beaches

                    else
                    {
                        int heatLoc = Convert.ToInt32(heatMap[x, y] * 3);
                        int waterLoc = Convert.ToInt32(waterMap[x, y] * 3);
                        int subBiomeLoc = Convert.ToInt32(SubBiomeMap[x, y] * 10);
                        int subBiomeLoc1 = Convert.ToInt32(SubMap1[x, y] * 3);
                        int subBiomeLoc2 = Convert.ToInt32(SubMap2[x, y] * 3);
                        int subBiomeLoc3 = Convert.ToInt32(SubMap3[x, y] * 3);

                        
                        if (heatLoc > 2) { heatLoc = 2; }
                        if (waterLoc > 2) { waterLoc = 2; }
                        if (subBiomeLoc > 10) { subBiomeLoc = 10; }
                        if (subBiomeLoc1 > 2) { subBiomeLoc1 = 2; }
                        if (subBiomeLoc2 > 2) { subBiomeLoc2 = 2; }
                        if (subBiomeLoc3 > 2) { subBiomeLoc3 = 2; }

                        BiomeLookup BiomeType = Biomes[heatLoc, waterLoc]; // Declare the biome type for a switch later

                        // All multi-case biomes

                        if (BiomeType == BiomeLookup.Fields)
                        {
                            if (subBiomeLoc <= 3)
                            {
                                BiomeType = BiomeLookup.Woods;
                            }
                        }


                        switch (BiomeType)
                        {
                            case BiomeLookup.Desert:
                                Tilemap.SetTile(new Vector3Int(x, y, 0), Tiles[2]);
                                break;
                            case BiomeLookup.Rainforest:
                                Tilemap.SetTile(new Vector3Int(x, y, 0), Tiles[7]);
                                if (UnityEngine.Random.value > 0.8) { FlowerTilemap.SetTile(new Vector3Int(x, y, 0), FlowerTiles[10]); }
                                break;
                            case BiomeLookup.Savana_Fields:
                                Tilemap.SetTile(new Vector3Int(x, y, 0), Tiles[2]);
                                if (UnityEngine.Random.value > 0.8) { FlowerTilemap.SetTile(new Vector3Int(x, y, 0), FlowerTiles[10]); }
                                break;

                            case BiomeLookup.Marshy_Fields:
                                Tilemap.SetTile(new Vector3Int(x, y, 0), Tiles[7]);
                                break;
                            case BiomeLookup.Fields:
                                Tilemap.SetTile(new Vector3Int(x, y, 0), Tiles[3]);
                                break;
                            case BiomeLookup.Woods:
                                Tile[] BiomeDetails = {FlowerTiles[10],FlowerTiles[15],FlowerTiles[0],FlowerTiles[1],FlowerTiles[2],FlowerTiles[3],FlowerTiles[4],FlowerTiles[5]};
                                CreateBiome.Woods(x, y, Tiles, BiomeDetails, Tilemap, FlowerTilemap, TreesTilemap, SubMap1, SubMap2, SubMap3, heightMap);
                                break;
                            case BiomeLookup.Dry_Plains:
                                Tilemap.SetTile(new Vector3Int(x, y, 0), Tiles[8]);
                                if (UnityEngine.Random.value > 0.97) { FlowerTilemap.SetTile(new Vector3Int(x, y, 0), FlowerTiles[15]); }
                                break;

                            case BiomeLookup.Snowy_Fields:
                                Tilemap.SetTile(new Vector3Int(x, y, 0), Tiles[5]);
                                break;
                            case BiomeLookup.Snowy_Woods:
                                Tilemap.SetTile(new Vector3Int(x, y, 0), Tiles[5]);
                                break;
                            case BiomeLookup.Tundra:
                                Tilemap.SetTile(new Vector3Int(x, y, 0), Tiles[5]);
                                break;
                        }
                    }



                }

                else if (heightMap[x, y] < 0.99) { Tilemap.SetTile(new Vector3Int(x, y, 0), Tiles[4]); }
                else { Tilemap.SetTile(new Vector3Int(x, y, 0), Tiles[5]); }
            }
    }
}
