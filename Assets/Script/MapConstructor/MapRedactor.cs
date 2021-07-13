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


    public GridLayout gridLayout;
    public int x;
    public int y;
    public float cell =0.8659766f;
    [SerializeField]
    private Tile nullTile;


    private int WorldBiom;

    private Texture2D Map1;
    private Texture2D Map2;
    private Texture2D Map3;
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

    public void PushMap()
    {

    }
    public void TranfMap(int w, Texture2D M1, Texture2D M2, Texture2D M3)
    {
        WorldBiom = w;
        Map1 = M1;
        Map2 = M2;
        Map3 = M3;
    }
    // Update is called once per frame


    void loadTile(int palitte ,Tile NewTile, TileBase NewTileBase)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y, 50);
        Vector3Int cellPosition = gridLayout.WorldToCell(mousePos);
        // level[MapI.curentPalitte].ClearAllTiles();

        if (NewTile != null)
        {
            mapData.level[palitte].SetTile(cellPosition, NewTile);
        }
        else
        {
            mapData.level[palitte].SetTile(cellPosition, NewTileBase);
        }

        mapData.level[palitte].RefreshAllTiles();

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
        //if (MapI.curentTile != -1)
        //{
        //    if (Input.GetMouseButtonDown(1))
        //    {
        //        //if (MapI.curentTile != -1)
        //        //{
        //        MapI.curentTile = -1;
        //    }
        //}
        //else 
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
            //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //mousePos = new Vector3(mousePos.x, mousePos.y, 50);
            //Vector3Int cellPosition = gridLayout.WorldToCell(mousePos);
            //mapData.level[MapI.curentPalitte].SetTile(cellPosition, nullTile);
            //mapData.level[MapI.curentPalitte].RefreshAllTiles();


            //   SetTile(cellPosition, mapData.DataTile[MapI.curentPalitte].Data[MapI.curentTile].tile);
        }


        //int a = 1920;
        //int b = 1080;
        //float fg = a/ b;
        //Debug.Log($"{fg} =   {Screen.width}/ {Screen.height}");
        //Vector3 mousePos =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector3Int mousePos1 = new Vector3Int(Mathf.RoundToInt(mousePos.y), Mathf.RoundToInt((mousePos.x * fg)), 50);
        //  Vector3 mousePos1 = new Vector3(mousePos.x, mousePos.y, 50);




        /////https://habr.com/ru/post/147082/
        ///

        //    level[MapI.curentPalitte].ClearAllTiles();
        //   level[MapI.curentPalitte].SetTile(mousePos1, mapData.DataTile[MapI.curentPalitte].Data[MapI.curentTile].tile);
        //  Ghost.transform.position = mousePos1;//Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //   RaycastHit hit;
        ////   Vector3Int mousePos1 = new Vector3Int(0, 0, 0);
        ////   Vector3 objectHit;
        //   Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        //   Debug.Log(ray.GetPoint);
        //   hit = Physics.Raycast(ray);
        //   if (Physics.Raycast(ray, out hit))
        //   {
        //       Vector3Int mousePos1 = new Vector3Int(
        //         Mathf.RoundToInt(hit.transform.position.x),///(int)(mousePos.x - 0.2f),
        //        Mathf.RoundToInt(hit.transform.position.y)//(int)(mousePos.y - 0.5f)
        //         , 50);
        //       Ghost.transform.position = mousePos1;
        //       //    objectHit = hit.transform.position;
        //   }
        //  int x =
        //  Mathf.Round(float f)


        //   Ghost.transform.position = mousePos1;//Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //////if (Input.GetMouseButtonDown(0))
        //////{
        //////    RaycastHit hit;\
        //////    Debug.Log(ray);
        //////    if (Physics.Raycast(ray, out hit))
        //////    {

        //////        Transform objectHit = hit.transform;
        //////        Debug.Log(objectHit);
        //////    }

        //////}
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    st = !st;
        //}
        //    if (st)
        //{
        //    Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        //    Vector3Int mousePos1 = new Vector3Int((int)(mousePos.x - 0.2f), (int)(mousePos.y - 0.5f), 50);
        //    x = (int)mousePos.y;
        //    y = (int)mousePos.x;
        //    Ghost.transform.position = new Vector3Int((int)(mousePos.x -0.3f), (int)(mousePos.y +0.5f), 50);//Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //                                                                                                    // Debug.Log($"{x}  {y}");
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        mousePos1 = new Vector3Int(x, y, 50);
        //        // .. Debug.Log($"{x}  {y}");
        //        level[MapI.curentPalitte].SetTile(mousePos1, mapData.DataTile[MapI.curentPalitte].Data[MapI.curentTile].tile);


        //        //   .thisTile = mapData.DataTile[MapI.curentPalitte].Data[curentTile].tile;
        //    }
        //}
    }
}
