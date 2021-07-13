using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CoreGenerator : MonoBehaviour
{
    public float[] AddScale;
    public GameObject grid;
    public GridLayout gridLayout;
    public int xChunk;
    public int yChunk;

    public int Seed;
    public int WorldWater;
    public int WorldBiom;

    public int[] AddBiom;

    public Color[] pix;
    public Texture2D W;
    private MapData mapData;

    // Start is called before the first frame update
    void Start()
    {
        mapData = GetComponent<MapData>();


        mapData.level = new Tilemap[3];
        for (int i = 0; i < mapData.level.Length; i++)
        {
            AddGrid(i);
        }

        Generate();
    }
    void AddTile(int a, int b, int c, Vector3Int D)
    {
        mapData.level[a].SetTile(D, mapData.DataTile[b].Data[c].tile );
    }

    void AddGrid(int i)
    {
        GameObject GO = Instantiate(grid);
        GO.transform.SetParent( gridLayout.transform);
        GO.name = "Grid" + i;

        GO.GetComponent<TilemapRenderer>().sortingOrder = i;
        mapData.level[i] = GO.GetComponent<Tilemap>();
    }
    void Generate()
    {

        if (Seed != 0)
        {
            Random.seed = Seed;
        }
        int cof1 = Random.Range(0, mapData.DataTile[0].Data.Length);
        WorldWater = cof1;

        cof1 = Random.Range(0, mapData.DataTile[1].Data.Length);
        WorldBiom = cof1;

        AddBiom = new int[2];
        bool turn = false;
        for (int ix = 0; ix < AddBiom.Length; ix++)
        {
            AddBiom[ix] = -1;
        }

        while (turn == false)
        {
            cof1 = Random.Range(0, mapData.DataTile[1].Data.Length);
            if(cof1 != WorldBiom)
            {
                AddBiom[0] = cof1;
                turn = true;
            }
        }
        turn = false;

        while (turn == false)
        {
            cof1 = Random.Range(0, mapData.DataTile[1].Data.Length);
            if (cof1 != WorldBiom)
            {
                if (AddBiom[0] != cof1)
                {

                    AddBiom[1] = cof1;
                    turn = true; 
                }
            }
        }

        int CurentAddBiom = AddBiom[0];
        //level[MapI.curentPalitte].SetTile(cellPosition, 

        //    mapData.DataTile[MapI.curentPalitte].Data[MapI.curentTile].tile
        //    );
        //  Texture2D World = new Texture2D(xChunk, yChunk);

        Texture2D xc = new Texture2D(xChunk, yChunk);
      //  xc.filterMode = FilterMode.Point;
       // Color[] 
            pix = new Color[xc.width * xc.height];
        //int[] pixInt = new int[pix.Length];






        float scale = Mathf.Sqrt(xChunk * xChunk + yChunk * yChunk);


        //WaterGen
        int xOrg = (int)Random.Range(0, 1512);
        int yOrg = (int)Random.Range(0, 1512);

        int i = 0;
        float y = 0.0F;
        while (y < xc.height)
        {
            float x = 0.0F;
            while (x < xc.width)
            {
               // int cof1 = Random.Range(0, 100);
                float xCoord = xOrg + x / xc.width * (scale * AddScale[0]);
                float yCoord = yOrg + y / xc.height * (scale * AddScale[0]);

                float sample = Mathf.PerlinNoise(xCoord, yCoord);
                Vector3Int D = new Vector3Int((int)x, (int)y, 50);
                Color C1 = new Color(WorldWater + 1, 0, 0);

                AddTile(0, 0, WorldWater, D);
                if (sample > 0.7f)
                {
                //    AddTile(0,0,WorldWater, D);
                    //  //  mapData.level[0]
                    //    C1 = new Color(WorldWater +1, 0, 0);
                    //}
                    //else if (sample > 0.5f)
                    //    {
                    //        AddTile(1, 1, WorldBiom, D);
                    //        AddTile(0, 0, WorldWater, D);
                    //        //  mapData.level[0]
                    //        C1 = new Color(WorldWater + 1, WorldBiom + 1, 0);
                }
                    else
                    {
                    AddTile(1, 1, WorldBiom, D);
                          C1 = new Color(WorldWater + 1, WorldBiom + 1, 0);
                   // C1 = new Color(0, WorldBiom + 1, 0);
                    }

                pix[i] = C1;
                x++;
                i++;
            }
            y++;
        }

        //Add Biom
        xOrg = (int)Random.Range(0, 1512);
        yOrg = (int)Random.Range(0, 1512);

        i = 0;
        y = 0.0F;
        while (y < xc.height)
        {
            float x = 0.0F;
            while (x < xc.width)
            {
                // int cof1 = Random.Range(0, 100);
                float xCoord = xOrg + x / xc.width * (scale * AddScale[1]);
                float yCoord = yOrg + y / xc.height * (scale * AddScale[1]);

                float sample = Mathf.PerlinNoise(xCoord, yCoord);

                Color C1 = new Color(pix[i].r, pix[i].g, 0);
                if (pix[i].g > 0)
                {
                    Vector3Int D = new Vector3Int((int)x, (int)y, 50);
                    if (sample > 0.7f)
                    {
                        AddTile(2, 1, AddBiom[0], D);

                        C1 = new Color(pix[i].r, pix[i].g, AddBiom[0]+1);
                    }
                    else if (sample < 0.3f)
                    {
                        AddTile(2, 1, AddBiom[1], D);
                        C1 = new Color(pix[i].r, pix[i].g, AddBiom[1] + 1);
                    }
                }

                pix[i] = C1;
                x++;
                i++;
            }
            y++;
        }


        xc.SetPixels(pix);
        xc.Apply();
        W = xc;
        Transf();
    }

    void Transf()
    {
        for (int i =0; i<mapData.level.Length; i++)
        {

            mapData.level[i].RefreshAllTiles();
        }
    }
}
