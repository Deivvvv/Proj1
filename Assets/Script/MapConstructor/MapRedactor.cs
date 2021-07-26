using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    private int WorldBiom;

    [SerializeField]
    private int width;
    private Color[] Map1;
    private Color[] Map2;
    private Color[] Map3;



    [SerializeField]
    private Button[] towerButton;
    [SerializeField]
    private Button[] towerLevel;
    [SerializeField]
    private Text[] text;
    private int TowerCor =-1;
    // Start is called before the first frame update
    void Start()
    {
        mapData = GetComponent<MapData>();
        MapI = GetComponent<MapInterfase>();

        StartTowerRedactor();
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
            int id = 0;
            if (Map1[i].r > 0)
            {
                id = (int)Map1[i].r - 1;
                //Debug.Log(Map1.Length);
                //Debug.Log(i);
                //Debug.Log(cellPosition);
                //Debug.Log(Map1[i].r);
                //Debug.Log(Map1[i].r);
                mapData.level[0].SetTile(cellPosition, mapData.DataTile[0].Data[id].tile);
             //   mapData.level[0].SetTile(cellPosition, mapData.DataTile[0].Data[(int)Map1.r].tile);
            }
            if (Map1[i].g > 0)
            {
               // id = (int)Map1[i].g - 1;
                mapData.level[1].SetTile(cellPosition, mapData.DataTile[1].Data[WorldBiom].tile);
                //   mapData.level[0].SetTile(cellPosition, mapData.DataTile[0].Data[(int)Map1.r].tile);
            }
            if (Map1[i].b > 0)
            {
                id = (int)Map1[i].b - 1;
                //Debug.Log(Map1[i].g);
                //Debug.Log(Map1[i].b);
                mapData.level[2].SetTile(cellPosition, mapData.DataTile[1].Data[id].tile);
                //   mapData.level[0].SetTile(cellPosition, mapData.DataTile[0].Data[(int)Map1.r].tile);
            }

            if (Map2[i].r > 0)
            {
                id = (int)Map2[i].r - 1;
                //Debug.Log(Map2[i].r);
                //Debug.Log(mapData.DataTile[4].Data[(int)(Map2[i].r-1)].tile);
                mapData.level[5].SetTile(cellPosition, mapData.DataTile[4].Data[id].tile);
                //   mapData.level[0].SetTile(cellPosition, mapData.DataTile[0].Data[(int)Map1.r].tile);
            }
            if (Map2[i].g > 0)
            {
                id = (int)Map2[i].g - 1;
                mapData.level[3].SetTile(cellPosition, mapData.DataTile[2].Data[id].tile);
                //   mapData.level[0].SetTile(cellPosition, mapData.DataTile[0].Data[(int)Map1.r].tile);
            }
            if (Map2[i].b > 0)
            {
                id = (int)Map2[i].b - 1;
                mapData.level[4].SetTile(cellPosition, mapData.DataTile[3].Data[id].tile);
                //   mapData.level[0].SetTile(cellPosition, mapData.DataTile[0].Data[(int)Map1.r].tile);
            }

            if (Map3[i].r > 0)
            {
              //  Debug.Log(Map3[i].r);
                id = (int)Map3[i].r - 1;
                //Debug.Log(Map2[i].r);
                //Debug.Log(mapData.DataTile[4].Data[(int)(Map2[i].r-1)].tile);
               // Debug.Log(id);
                mapData.level[5].SetTile(cellPosition, mapData.DataTile[5].Data[id].tile);
                //   mapData.level[0].SetTile(cellPosition, mapData.DataTile[0].Data[(int)Map1.r].tile);

                if (Map3[i].g > 0)
                {
                   int id2 = (int)Map3[i].g;

                    mapData.ColorPlayer[id2].SetTile(cellPosition, mapData.DataTile[6].Data[id].tile);

                    //   mapData.level[0].SetTile(cellPosition, mapData.DataTile[0].Data[(int)Map1.r].tile);
                }
            }
            
            //if (Map3[i].b > 0)
            //{
            //    id = (int)Map3[i].b - 1;
            //    mapData.level[7].SetTile(cellPosition, mapData.DataTile[7].Data[id].tile);
            //    //   mapData.level[0].SetTile(cellPosition, mapData.DataTile[0].Data[(int)Map1.r].tile);
            //}
        }


        for (int i = 0; i < mapData.level.Length; i++)
        {

            mapData.level[i].RefreshAllTiles();
        }


       // gridLayout.GetComponent<GamePlayCore>().NewMap(Map1,Map2,width);
    }
    // Update is called once per frame


    void loadTile(int palitte, TileBase NewTileBase)
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


                    mapData.level[palitte].SetTile(cellPosition, NewTileBase);

                    mapData.level[palitte].RefreshAllTiles();

                    //  sourceTex.GetPixels(x, y, width, height);
                    int i = MapI.curentTile + 1;
                    if (NewTileBase == null)
                    {
                        i = 0;
                    }



                    switch (palitte)
                    {
                        case (0):
                            Map1[id] = new Color(i, Map1[id].g, Map1[id].b);
                            break;
                        case (1):
                            Map1[id] = new Color(Map1[id].r, i, Map1[id].b);
                            break;
                        case (2):
                            Map1[id] = new Color(Map1[id].r, Map1[id].g, i);
                            break;

                        case (5):
                            Map2[id] = new Color(i, Map2[id].g, Map2[id].b);
                            break;
                        case (3):
                            Map2[id] = new Color(Map2[id].r, i, Map2[id].b);
                            break;
                        case (4):
                            Map2[id] = new Color(Map2[id].r, Map2[id].g, i);
                            break;

                        case (6):
                            Map3[id] = new Color(i, Map2[id].g, Map3[id].b);
                            break;
                        case (7):
                            Map3[id] = new Color(Map3[id].r, i, Map3[id].b);
                            break;
                        case (8):
                            Map3[id] = new Color(Map3[id].r, Map3[id].g, i);
                            break;
                    }
                }
        }
        }

    }

    void TowerNewColor(int id)
    {
      //  Debug.Log(TowerCor);
        if (TowerCor > -1)
        {
            if (TowerCor < 0)
            {
                Map3[TowerCor].g = 0;
            }
            else
            {
                Map3[TowerCor].g = id;
            }
            Vector3Int cellPosition = new Vector3Int(TowerCor % width, TowerCor / width, 50);
           // Debug.Log(cellPosition);
            TileBase NewTile = mapData.DataTile[6].Data[(int)Map3[TowerCor].r-1].tile;
            for (int i = 0; i < mapData.ColorPlayer.Length; i++)
            {if(id == 0)
                {
                    mapData.ColorPlayer[i].SetTile(cellPosition, null);
                }
                else
                if (i == id) 
                {
                    mapData.ColorPlayer[i].SetTile(cellPosition, NewTile);
                }
                else
                {
                    mapData.ColorPlayer[i].SetTile(cellPosition, null);
                }
                    
            }
        } 
    }
    void AddLevelTower(bool lv)
    {
        if (TowerCor > -1)
        {
            if (lv)
            {
                if (Map3[TowerCor].b < 2)
                {
                    Map3[TowerCor].b++;
                }
            }
            else
            {
                if (Map3[TowerCor].b > 0)
                {
                    Map3[TowerCor].b--;
                }
            }

            text[1].text = "" + Map3[TowerCor].b;
        }
    }
    void StartTowerRedactor()
    {
        towerLevel[0].onClick.AddListener(() => AddLevelTower(false));
        towerLevel[1].onClick.AddListener(() => AddLevelTower(true));

        towerButton[0].onClick.AddListener(() => TowerNewColor(0));
        towerButton[1].onClick.AddListener(() => TowerNewColor(1));
        towerButton[2].onClick.AddListener(() => TowerNewColor(2));
        towerButton[3].onClick.AddListener(() => TowerNewColor(3));
        towerButton[4].onClick.AddListener(() => TowerNewColor(4));
        //for(int i = 0; i < towerLevel; i++)
        //{
        // }
    }
    void TowerLoad(bool Create)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y, 50);
        Vector3Int cellPosition = gridLayout.WorldToCell(mousePos);

        text[0].text = "" + cellPosition[0] + "  " + cellPosition[1];

        int id = cellPosition[1] * width + cellPosition[0];
        if (id > -1)
        {
            if (id < Map1.Length)
            {
                if (Create)
                {
                    if (Map3[id].r > 0)
                    {
                       // Debug.Log("OK");
                        TowerCor = id;

                        text[1].text = "" + Map3[TowerCor].b;

                    }
                }
                else
                {
                    TowerNewColor(-1);
                }
            }
        }
    }
    void FixedUpdate()
    {
        if (Map1 != null)
        {

            if (Input.GetMouseButtonDown(2))
            {
                TowerLoad(true);
            }


            if (look)
            {
                if (Input.GetMouseButton(0))
                {
                    if (MapI.curentTile != -1)
                    {
                        TileBase NewTile = mapData.DataTile[MapI.curentPalitte].Data[MapI.curentTile].tile;
                        if (MapI.curentPalitte == 0)
                        {
                            loadTile(MapI.curentPalitte, NewTile);

                        }
                        else if (MapI.curentPalitte == 1)
                        {
                            if (MapI.curentTile == WorldBiom)
                            {
                                loadTile(MapI.curentPalitte, NewTile);
                            }
                            else
                            {
                                loadTile(MapI.curentPalitte, mapData.DataTile[MapI.curentPalitte].Data[WorldBiom].tile);
                                loadTile(MapI.curentPalitte + 1, NewTile);
                            }
                        }
                        else
                        {

                            loadTile(MapI.curentPalitte + 1, NewTile);
                            if (MapI.curentPalitte + 1 == 6)
                            {
                                //NewTile = mapData.DataTile[MapI.curentPalitte+1].Data[MapI.curentTile].tile;
                                //loadTile(MapI.curentPalitte + 2, NewTile);
                                TowerLoad(false);
                            }

                        }
                    }
                }


                if (Input.GetMouseButton(1))
                {
                    // MapI.curentTile = -1;
                    if (MapI.curentPalitte == 0)
                    {
                        loadTile(MapI.curentPalitte,  null);
                    }
                    else
                    if (MapI.curentPalitte == 1)
                    {
                        if (MapI.curentTile == WorldBiom)
                        {
                            loadTile(MapI.curentPalitte,  null);
                            //   loadTile(MapI.curentPalitte, null, NewTile);
                        }
                        else
                        {
                            loadTile(MapI.curentPalitte + 1, null);
                            if(MapI.curentPalitte + 1 == 6)
                            {
                                loadTile(MapI.curentPalitte + 2,  null);
                            }
                            // loadTile(MapI.curentPalitte, null, mapData.DataTile[MapI.curentPalitte].Data[WorldBiom].tile);
                            //   loadTile(MapI.curentPalitte + 1, null, NewTile);
                        }


                        //loadTile(MapI.curentPalitte,  null);
                        //loadTile(MapI.curentPalitte+1,  null);
                    }
                    else
                    {
                        loadTile(MapI.curentPalitte + 1,  null);
                    }
                }

            }
        }
    }
}
