using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public static class CreateBiome
{

    public static BiomeLookup PickBiome(int tileX, int tileY, float[,] heightMap, float[,] riversMap, BiomeLookup[,] CurrentBiomes)
    {
        float riversMapValue = Math.Abs(riversMap[x, y] - 0.5);
        float heightMapValue = heightMap[x, y];

        // No need for else ifs since each statement returns

        if (heightMap< 0.5) { return BiomeLookup.Sea; }
        if (heightMap[x, y] < 0.55) { return BiomeLookup.Shore; }
        if (heightMap[x, y] < 0.55) { return BiomeLookup.Beach; }
        if (riversMapValue < 0.04) { return BiomeLookup.River; }
        if (riversMapValue < 0.055) { return BiomeLookup.RiverShore; }

        int valuesBrowsed = 0;
        for (int localY = -1; localY < 1; localY++)
            for (int localX = -1; localX < 1; localX++)
                valuesBrowsed += 1;
                if (!(localX = 0 || localY = 0))
                {
                    return;
                }


    }

/*

Methods need: BaseTiles, Details, BaseTilemap, FlowerTilemap, TreeTilemap, SubMap1, SubMap2, SubMap3, HeightMap

BaseTiles consists of ALL base tiles in normal order
Details consists of a different array for each biome, contains all the details needed
SubMaps are small perlin maps, used for things like Flower Patterns, Oasis', Swamp marshes etc

*/


    public static void Desert(int tileX, int tileY, Tile[] BaseTiles, Tile[] Details, Tilemap BaseTilemap, Tilemap FlowerTilemap, Tilemap TreeTilemap, float[,] SubMap1, float[,] SubMap2, float[,] SubMap3, float[,] HeightMap)
    {
        if (SubMap1[tileX,tileY] > 0.97)
        {
            //Oasis
        }
        else
        {
            BaseTilemap.SetTile(new Vector3Int(tileX, tileY, 0), BaseTiles[3]);
            if (UnityEngine.Random.value > 0.8)
            {
                if (UnityEngine.Random.value > 0.98)
                {
                    FlowerTilemap.SetTile(new Vector3Int(tileX, tileY, 0), Details[Convert.ToInt32((UnityEngine.Random.value * 2.999f) + 2)]); // Details[2-4] are skull tile
                }
                else
                {
                    FlowerTilemap.SetTile(new Vector3Int(tileX, tileY, 0), Details[Convert.ToInt32((UnityEngine.Random.value * 2.999f) + 2)]); // Details[0-1] are cacti tiles
                }
            }
        }
    }

    public static void Fields(int tileX, int tileY, Tile[] BaseTiles, Tile[] Details, Tilemap BaseTilemap, Tilemap FlowerTilemap, Tilemap TreeTilemap, float[,] SubMap1, float[,] SubMap2, float[,] SubMap3, float[,] HeightMap)
    {

    }

    public static void Woods(int tileX, int tileY, Tile[] BaseTiles, Tile[] Details, Tilemap BaseTilemap, Tilemap FlowerTilemap, Tilemap TreeTilemap, float[,] SubMap1, float[,] SubMap2, float[,] SubMap3, float[,] HeightMap)
    {
      BaseTilemap.SetTile(new Vector3Int(tileX, tileY, 0), BaseTiles[3]);
      if (UnityEngine.Random.value > 0.8) // If value is above 0.8, a detail will attempt to spawn (so 20% chance)
      {
          if (UnityEngine.Random.value < 0.05) { BaseTilemap.SetTile(new Vector3Int(tileX, tileY, 0), BaseTiles[6]); }   // Tree Stump
          else {
              float detailValue = SubMap1[tileX, tileY] * 0.65f; // Multiplier has to be last value of selector - - - SubMap1 is used here to create patches
              if (detailValue < 0.25) { FlowerTilemap.SetTile(new Vector3Int(tileX, tileY, 0), Details[0]); }            // Small Grass
              else if (detailValue < 0.25) { FlowerTilemap.SetTile(new Vector3Int(tileX, tileY, 0), Details[1]); }       // Small Rocks
              else if (detailValue < 0.4)  { FlowerTilemap.SetTile(new Vector3Int(tileX, tileY, 0), Details[2]); }       // Flower 1 (Rose)
              else if (detailValue < 0.45) { FlowerTilemap.SetTile(new Vector3Int(tileX, tileY, 0), Details[3]); }       // Flower 2 (Daisies)
              else if (detailValue < 0.5)  { FlowerTilemap.SetTile(new Vector3Int(tileX, tileY, 0), Details[4]); }       // Flower 3 (Small Sunflower)
              else if (detailValue < 0.55) { FlowerTilemap.SetTile(new Vector3Int(tileX, tileY, 0), Details[5]); }       // Flower 4 (Snowdrop)
              else if (detailValue < 0.6)  { FlowerTilemap.SetTile(new Vector3Int(tileX, tileY, 0), Details[6]); }       // Flower 5 (Dahlia)
              else if (detailValue < 0.65) { FlowerTilemap.SetTile(new Vector3Int(tileX, tileY, 0), Details[7]); }       // Flower 6 (Forget-Me-Not)
          }
      }
    }

    public static void Savana(int tileX, int tileY, Tile[] BaseTiles, Tile[] Details, Tilemap BaseTilemap, Tilemap FlowerTilemap, Tilemap TreeTilemap, float[,] SubMap1, float[,] SubMap2, float[,] SubMap3, float[,] HeightMap)
    {

    }

    public static void Rainforest(int tileX, int tileY, Tile[] BaseTiles, Tile[] Details, Tilemap BaseTilemap, Tilemap FlowerTilemap, Tilemap TreeTilemap, float[,] SubMap1, float[,] SubMap2, float[,] SubMap3, float[,] HeightMap)
    {

    }

    public static void MarshyFields(int tileX, int tileY, Tile[] BaseTiles, Tile[] Details, Tilemap BaseTilemap, Tilemap FlowerTilemap, Tilemap TreeTilemap, float[,] SubMap1, float[,] SubMap2, float[,] SubMap3, float[,] HeightMap)
    {

    }

    public static void DryPlains(int tileX, int tileY, Tile[] BaseTiles, Tile[] Details, Tilemap BaseTilemap, Tilemap FlowerTilemap, Tilemap TreeTilemap, float[,] SubMap1, float[,] SubMap2, float[,] SubMap3, float[,] HeightMap)
    {

    }

    public static void SnowyFields(int tileX, int tileY, Tile[] BaseTiles, Tile[] Details, Tilemap BaseTilemap, Tilemap FlowerTilemap, Tilemap TreeTilemap, float[,] SubMap1, float[,] SubMap2, float[,] SubMap3, float[,] HeightMap)
    {

    }

    public static void SnowyWoods(int tileX, int tileY, Tile[] BaseTiles, Tile[] Details, Tilemap BaseTilemap, Tilemap FlowerTilemap, Tilemap TreeTilemap, float[,] SubMap1, float[,] SubMap2, float[,] SubMap3, float[,] HeightMap)
    {

    }

    public static void Tundra(int tileX, int tileY, Tile[] BaseTiles, Tile[] Details, Tilemap BaseTilemap, Tilemap FlowerTilemap, Tilemap TreeTilemap, float[,] SubMap1, float[,] SubMap2, float[,] SubMap3, float[,] HeightMap)
    {

    }


}