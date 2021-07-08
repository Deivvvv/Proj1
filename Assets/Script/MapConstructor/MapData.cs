using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapData : MonoBehaviour
{
    public SubMapData1[] DataTile;

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
    public st Tayp;

    public enum st
    {
        Field,
        Forest,
        Mountain

    }
}
