using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pro_Gen : MonoBehaviour
{

    public Vector2Int XYChunkSize;
    public int HeatZoom, WaterZoom;
    public Tilemap Tilemap;
    public string Seed;


    /*
     *  MAKE TILE LOOKUP TABLE
     *  ----------------------
     *
     *  By using TilesArr[Tiles.Grass], we can use the Grass tile
     *  (Enum value provides array index of the tile)
     */

    public Tile[] TilesArr;         // Should Contain:     Grass, MarshyGrass, Sand, WetMud, DryMud, Ice, Snow, Stone, TreeStump, Corruption, Water1, Water2, Water3, DeepWater1, DeepWater2, DeepWater3

    public enum Tiles
    {
        Grass,
        MarshyGrass,
        Sand,
        WetMud,
        DryMud,
        Ice,
        Snow,
        Stone,
        TreeStump,
        Corruption,
        Water1, Water2, Water3,
        DeepWater1, DeepWater2, DeepWater3
    }


    /*
     *  MAKE BIOME LOOKUP TABLE
     *  -----------------------
     *
     *  By using BiomeLookup[BiomeValueDef[HeatValueGrid, RainValueGrid]] we can get the Biome type dependent on the heat and rain values
     *
     */

    public enum BiomeLookup
    {
     //Wet            Regular         Dry
      Rainforest,    Savana_Fields,  Desert,        // Hot
      Marshy_Fields, Fields,         Dry_Plains,    // Mid Temp
      Snowy_Fields,  Snowy_Woods,    Tundra         // Cold
    }

    public int[,] BiomeValueDef =
    {
        {1,2,3},
        {4,5,6},
        {7,8,9}
    };


    private float[,] HeatValueGrid, RainValueGrid;

    public void MakeChunk(Vector2Int XYChunkPos)
    {
        for (int x = 0; x < XYChunkSize.x; x++)
            for (int y = 0; y < XYChunkSize.y; y++)
            {
                HeatValueGrid[x, y] = Mathf.PerlinNoise(x/HeatZoom, y) * 3;
                RainValueGrid[x, y] = Mathf.PerlinNoise(x, y) * 3;
            }
    }



    private void Nothing()
    {
      for (int x = 0; x < XYChunkSize.x; x++)
          for (int y = 0; y < XYChunkSize.y; y++)
          {
            HeatValueGrid[x, y] = Mathf.PerlinNoise(x, y) * 3;
            RainValueGrid[x, y] = Mathf.PerlinNoise(x, y) * 3;
          }
    }

}
