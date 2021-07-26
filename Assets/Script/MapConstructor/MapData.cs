using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapData : MonoBehaviour
{
    public SubMapData1[] DataTile;
    public Tilemap[] level;
    public Tilemap[] ColorPlayer;
    public Color[] Player;
}

[System.Serializable]
public class SubMapData1
{
   // public TileBase tile;
    public SubMapData[] Data;
}
[System.Serializable]
public class SubMapData
{
    public Sprite headSprite;
    public TileBase tile;
    public float TileSpeed;
  //  public st Tayp;

    /*
     * Laver
     * water - 1(0)
     * ground - 2(1)
     * biom - 2 (2)
     * 
     * river - 3(4)
     * road - 4(5)
     * brige - 5(6)
     * 
     * forest - 6(7)
     * building
     */
  
    public enum st
    {
        WaterNorth,
        Water,
        Snow,
        Mud,
        Grass,
        GrassLand,
        Field
        //    ,
        //Forest,



        //Mountain,
        //Hole


        //     Road,
        //River,
        //Brige,

        //Forest
    }

}
