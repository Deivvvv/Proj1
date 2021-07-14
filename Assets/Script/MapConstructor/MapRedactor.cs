using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapRedactor : MonoBehaviour
{
    [SerializeField]
    private bool look;
    private MapData mapData;
    private MapInterfase MapI;
   // public Camera camera;

    [SerializeField]
    private Tilemap[] level;


    [SerializeField]
    private GridLayout gridLayout;
    [SerializeField]
    private Tile nullTile;


    [SerializeField]
    private int WorldBiom;

    [SerializeField]
    private int width;
    private Color[] Map1;
    private Color[] Map2;
    private Color[] Map3;
    // Start is called before the first frame update
    void Start()
    {
        mapData = GetComponent<MapData>();
        MapI = GetComponent<MapInterfase>();
    }

    public void LoadKey(bool key)
    {
        look = !key;
    }

    public void Save()
    {
        MapI.Save(WorldBiom,Map1,Map2,Map3,width);
    }

    //load
    public void TranfMap(int wb, Color[] M1, Color[] M2, Color[] M3, int w)
    {
        width = w;
        WorldBiom = wb;
        Map1 = new Color[M1.Length];
        Map2 = new Color[M1.Length];
        Map3 = new Color[M1.Length];
        Map1 = M1;
        Map2 = M2;
        Map3 = M3;

        for (int i = 0; i < mapData.level.Length; i++)
        {
            mapData.level[i].ClearAllTiles();
        }

        for (int i =0;i < Map1.Length; i++)
        {
          //  Debug.Log(Map1[i]);
            Vector3Int cellPosition = new Vector3Int(i % w, i / w, 50);
            if (Map1[i].r > 0)
            {
                //Debug.Log(Map1.Length);
                //Debug.Log(i);
                //Debug.Log(cellPosition);
                mapData.level[0].SetTile(cellPosition, mapData.DataTile[0].Data[(int)((Map1[i].r-0.004f) * 255)].tile);
             //   mapData.level[0].SetTile(cellPosition, mapData.DataTile[0].Data[(int)Map1.r].tile);
            }
            if (Map1[i].g > 0)
            {
                mapData.level[1].SetTile(cellPosition, mapData.DataTile[1].Data[(int)((Map1[i].g - 0.004f) * 255)].tile);
                //   mapData.level[0].SetTile(cellPosition, mapData.DataTile[0].Data[(int)Map1.r].tile);
            }
            if (Map1[i].b > 0)
            {
                //Debug.Log(Map1[i].g);
                //Debug.Log(Map1[i].b);
                mapData.level[2].SetTile(cellPosition, mapData.DataTile[1].Data[(int)((Map1[i].b - 0.004f) * 255)].tile);
                //   mapData.level[0].SetTile(cellPosition, mapData.DataTile[0].Data[(int)Map1.r].tile);
            }

            if (Map2[i].r > 0)
            {
                //Debug.Log(Map2[i].r);
                //Debug.Log(mapData.DataTile[4].Data[(int)(Map2[i].r-1)].tile);
                mapData.level[5].SetTile(cellPosition, mapData.DataTile[4].Data[(int)((Map2[i].r - 0.004f) * 255)].tile);
                //   mapData.level[0].SetTile(cellPosition, mapData.DataTile[0].Data[(int)Map1.r].tile);
            }
            if (Map2[i].g > 0)
            {
                mapData.level[3].SetTile(cellPosition, mapData.DataTile[2].Data[(int)((Map2[i].g - 0.004f) * 255)].tile);
                //   mapData.level[0].SetTile(cellPosition, mapData.DataTile[0].Data[(int)Map1.r].tile);
            }
            if (Map2[i].b > 0)
            {
                mapData.level[4].SetTile(cellPosition, mapData.DataTile[3].Data[(int)((Map2[i].b - 0.004f) * 255)].tile);
                //   mapData.level[0].SetTile(cellPosition, mapData.DataTile[0].Data[(int)Map1.r].tile);
            }
        }


        for (int i = 0; i < mapData.level.Length; i++)
        {

            mapData.level[i].RefreshAllTiles();
        }
    }
    // Update is called once per frame


    void loadTile(int palitte ,Tile NewTile, TileBase NewTileBase)
    {


        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y, 50);
        Vector3Int cellPosition = gridLayout.WorldToCell(mousePos);

        int id = cellPosition[1] * width + cellPosition[0];
        if (id < Map1.Length)
        {
            if (cellPosition[0] >= 0)
            {
                if (cellPosition[1] >= 0)
                {
                    // level[MapI.curentPalitte].ClearAllTiles();

                    //    int id = Vector3Int.x Map1.width


                    if (NewTile != null)
                    {
                        mapData.level[palitte].SetTile(cellPosition, NewTile);
                    }
                    else
                    {
                        mapData.level[palitte].SetTile(cellPosition, NewTileBase);
                    }

                    mapData.level[palitte].RefreshAllTiles();

                    //  sourceTex.GetPixels(x, y, width, height);
                switch (palitte)
                {
                    case (0):
                        Map1[id] = new Color((MapI.curentTile)/255f +0.008f, Map1[id].g, Map1[id].b);
                        break;
                    case (1):
                        Map1[id] = new Color(Map1[id].r, (MapI.curentTile) / 255f + 0.008f, Map1[id].b);
                        break;
                    case (2):
                        Map1[id] = new Color(Map1[id].r, Map1[id].g, (MapI.curentTile) / 255f + 0.008f);
                        break;
                    case (5):
                        Map2[id] = new Color((MapI.curentTile) / 255f + 0.008f, Map2[id].g, Map2[id].b);
                        break;
                    case (3):
                        Map2[id] = new Color(Map2[id].r, (MapI.curentTile) / 255f + 0.008f, Map2[id].b);
                        break;
                    case (4):
                        Map2[id] = new Color(Map2[id].r, Map2[id].g, (MapI.curentTile) / 255f + 0.008f);
                        break;
                }
            }
        }
        }

    }

    void FixedUpdate()
    {
        if (look)
        {
            if (Input.GetMouseButton(0))
            {
                if (MapI.curentTile != -1)
                {
                    TileBase NewTile = mapData.DataTile[MapI.curentPalitte].Data[MapI.curentTile].tile;
                    if (MapI.curentPalitte == 0)
                    {
                        loadTile(MapI.curentPalitte,null, NewTile);
                     
                    }
                    else if (MapI.curentPalitte == 1)
                    {
                        if (MapI.curentTile == WorldBiom)
                        {
                            loadTile(MapI.curentPalitte, null, NewTile);
                        }
                        else
                        {
                            loadTile(MapI.curentPalitte, null, mapData.DataTile[MapI.curentPalitte].Data[WorldBiom].tile);
                            loadTile(MapI.curentPalitte + 1, null, NewTile);
                        }
                    }
                    else
                    {

                        loadTile(MapI.curentPalitte+1, null, NewTile);

                    }
                }
            }
        }

        if (Input.GetMouseButton(1))
        {
           // MapI.curentTile = -1;
            if (MapI.curentPalitte == 0)
            {
                loadTile(MapI.curentPalitte, nullTile, null);
            }
            else
            if (MapI.curentPalitte == 1)
            {
                if (MapI.curentTile == WorldBiom)
                {
                    loadTile(MapI.curentPalitte, nullTile, null);
                    //   loadTile(MapI.curentPalitte, null, NewTile);
                }
                else
                {
                    loadTile(MapI.curentPalitte+1, nullTile, null);
                    // loadTile(MapI.curentPalitte, null, mapData.DataTile[MapI.curentPalitte].Data[WorldBiom].tile);
                    //   loadTile(MapI.curentPalitte + 1, null, NewTile);
                }


                //loadTile(MapI.curentPalitte, nullTile, null);
                //loadTile(MapI.curentPalitte+1, nullTile, null);
            }
            else
            {
                loadTile(MapI.curentPalitte + 1, nullTile, null);
            }

        }
    }
}
