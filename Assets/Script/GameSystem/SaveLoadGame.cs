using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Tilemaps;

public class SaveLoadGame : MonoBehaviour
{
    [SerializeField]
    private MapCase Data;
    [SerializeField]
    private MapData mapData;
    [SerializeField]
    private GamePlayCore GC;
    //[SerializeField]
    //private MapInterfase MapI;
    //[SerializeField]
    //private MapRedactor MR;

    int colorSize = 255;
    //[SerializeField]
    //private GameObject origButton;

    //[SerializeField]
    //private GameObject window;
    [SerializeField]
    private GameObject grid;
    [SerializeField]
    private GridLayout gridLayout;
    // Start is called before the first frame update
    void Start()
    {


        mapData.level = new Tilemap[9];
        for (int i = 0; i < mapData.level.Length; i++)
        {
            AddGrid(i);
        }

        mapData.ColorPlayer = new Tilemap[mapData.Player.Length];
        for (int i = 0; i < mapData.Player.Length; i++)
        {
            AddColorGrid(i);
        }
        Load("C1");
    }

    void AddColorGrid(int i)
    {
        GameObject GO = Instantiate(grid);
        GO.transform.SetParent(gridLayout.transform);
        GO.name = "Color" + i;

        GO.GetComponent<Tilemap>().color = mapData.Player[i];
        GO.GetComponent<TilemapRenderer>().sortingOrder = 21;
        mapData.ColorPlayer[i] = GO.GetComponent<Tilemap>();
    }

    void AddGrid(int i)
    {
        GameObject GO = Instantiate(grid);
        GO.transform.SetParent(gridLayout.transform);
        GO.name = "Grid" + i;

        GO.GetComponent<TilemapRenderer>().sortingOrder = i;
        mapData.level[i] = GO.GetComponent<Tilemap>();
    }

    public void TranfMap(Color[] M1, Color[] M2, Color[] M3, int w)
    {
       // width = w;
        Color[] Map1 = new Color[M1.Length];
        Color[] Map2 = new Color[M1.Length];
        Color[] Map3 = new Color[M1.Length];
        Map1 = M1;
        Map2 = M2;
        Map3 = M3;

        for (int i = 0; i < mapData.level.Length; i++)
        {
            mapData.level[i].ClearAllTiles();
        }

        for (int i = 0; i < Map1.Length; i++)
        {

            Vector3Int cellPosition = new Vector3Int(i % w, i / w, 50);
            int id = 0;
            if (Map1[i].r > 0)
            {
                id = (int)Map1[i].r - 1;
                mapData.level[0].SetTile(cellPosition, mapData.DataTile[0].Data[id].tile);
            }
            if (Map1[i].g > 0)
            {
                id = (int)Map1[i].g - 1;
                mapData.level[1].SetTile(cellPosition, mapData.DataTile[1].Data[id].tile);
            }
            if (Map1[i].b > 0)
            {
                id = (int)Map1[i].b - 1;
                mapData.level[2].SetTile(cellPosition, mapData.DataTile[1].Data[id].tile);
            }

            if (Map2[i].r > 0)
            {
                id = (int)Map2[i].r - 1;
                mapData.level[5].SetTile(cellPosition, mapData.DataTile[4].Data[id].tile);
            }
            if (Map2[i].g > 0)
            {
                id = (int)Map2[i].g - 1;
                mapData.level[3].SetTile(cellPosition, mapData.DataTile[2].Data[id].tile);
            }
            if (Map2[i].b > 0)
            {
                id = (int)Map2[i].b - 1;
                mapData.level[4].SetTile(cellPosition, mapData.DataTile[3].Data[id].tile);
            }

            if (Map3[i].r > 0)
            {
                id = (int)Map3[i].r - 1;
                mapData.level[5].SetTile(cellPosition, mapData.DataTile[5].Data[id].tile);

                if (Map3[i].g > 0)
                {
                    int id2 = (int)Map3[i].g;

                    mapData.ColorPlayer[id2].SetTile(cellPosition, mapData.DataTile[6].Data[id].tile);

                }
            }
        }


        for (int i = 0; i < mapData.level.Length; i++)
        {

            mapData.level[i].RefreshAllTiles();
        }

        GC.LoadDataMap(M1, M2,  M3, w);

    }
    //public void Save(string Name, Color[] M1, Color[] M2, Color[] M3, int w, int wb)
    //{
    //    for (int i = 0; i < M1.Length; i++)
    //    {
    //        M1[i] = new Color(M1[i].r / colorSize, M1[i].g / colorSize, M1[i].b / colorSize);
    //        M2[i] = new Color(M2[i].r / colorSize, M2[i].g / colorSize, M2[i].b / colorSize);
    //        M3[i] = new Color(M3[i].r / colorSize, M3[i].g / colorSize, M3[i].b / colorSize);
    //    }


    //    var currentDate = System.DateTime.Now;
    //    string date = "";
    //    date += currentDate.Day.ToString();
    //    date += ".";
    //    date += currentDate.Month.ToString();
    //    date += " : ";
    //    date += currentDate.Hour.ToString();
    //    date += ":";
    //    date += currentDate.Minute.ToString();


    //    int ix = Data.Name.Count;
    //    bool newName = true;
    //    for (int i = 0; i < ix; i++)
    //    {
    //        if (Data.Name[i] == Name)
    //        {
    //            //   i = ix;
    //            ix = i;
    //            newName = false;
    //        }
    //    }

    //    if (!Directory.Exists(Application.dataPath + $"/Map/"))
    //        Directory.CreateDirectory(Application.dataPath + $"/Map/");
    //    if (!Directory.Exists(Application.dataPath + $"/Map/{Name}/"))
    //        Directory.CreateDirectory(Application.dataPath + $"/Map/{Name}/");


    //    if (newName)
    //    {
    //        Data.Name.Add(Name);
    //        Data.WorldBiom.Add(wb);
    //        Data.WorldWidth.Add(w);
    //        Data.DataTime.Add(date);
    //    }
    //    else
    //    {
    //        //  Data.Name[](Name);
    //        Data.WorldBiom[ix] = wb;
    //        Data.WorldWidth[ix] = w;
    //        Data.DataTime[ix] = date;


    //    }


    //    //if (Directory.Exists(Application.dataPath + $"/Map/Global/g{g3}/"))
    //    //    Directory.Delete(Application.dataPath + $"/Map/Global/g{g3}/", true);
    //    //Directory.CreateDirectory(Application.dataPath + $"/Map/Global/g{g3}/");
    //    //if (!Directory.Exists(Application.dataPath + $"/Map/"))
    //    //{

    //    //}
    //    Texture2D Map1 = new Texture2D(w, (M1.Length + 1) / w);
    //    Texture2D Map2 = new Texture2D(w, (M1.Length + 1) / w);
    //    Texture2D Map3 = new Texture2D(w, (M1.Length + 1) / w);


    //    Map1.SetPixels(M1);
    //    Map2.SetPixels(M2);
    //    Map3.SetPixels(M3);

    //    // Debug.Log(M1[15]);


    //    Map1.Apply();
    //    Map2.Apply();
    //    Map3.Apply();
    //    //rend.material.mainTexture = xc;
    //    byte[] bytes = Map1.EncodeToPNG();
    //    File.WriteAllBytes(Application.dataPath + $"/Map/{Name}/Map1.png", bytes);

    //    bytes = Map2.EncodeToPNG();
    //    File.WriteAllBytes(Application.dataPath + $"/Map/{Name}/Map2.png", bytes);

    //    bytes = Map3.EncodeToPNG();
    //    File.WriteAllBytes(Application.dataPath + $"/Map/{Name}/Map3.png", bytes);

    //    ReLoadData();
    //    for (int i = 0; i < M1.Length; i++)
    //    {
    //        M1[i] = new Color(Mathf.RoundToInt(M1[i].r * colorSize), Mathf.RoundToInt(M1[i].g * colorSize), Mathf.RoundToInt(M1[i].b * colorSize));
    //        M2[i] = new Color(Mathf.RoundToInt(M2[i].r * colorSize), Mathf.RoundToInt(M2[i].g * colorSize), Mathf.RoundToInt(M2[i].b * colorSize));
    //        M3[i] = new Color(Mathf.RoundToInt(M3[i].r * colorSize), Mathf.RoundToInt(M3[i].g * colorSize), Mathf.RoundToInt(M3[i].b * colorSize));
    //    }
    //}

    //public void Del(string Name)
    //{
    //    int ix = Data.Name.Count;
    //    bool newName = true;
    //    for (int i = 0; i < ix; i++)
    //    {
    //        if (Data.Name[i] == Name)
    //        {
    //            //   i = ix;
    //            ix = i;
    //            newName = false;
    //        }
    //    }

    //    if (newName)
    //    {
    //        //Directory.CreateDirectory(Application.dataPath + $"/Map/Global/g{g3}/");

    //    }
    //    else
    //    {
    //        Directory.Delete(Application.dataPath + $"/Map/{Name}/", true);
    //        Data.Name.RemoveAt(ix);
    //        Data.WorldBiom.RemoveAt(ix);
    //        Data.WorldWidth.RemoveAt(ix);
    //        Data.DataTime.RemoveAt(ix);
    //        //  Data.Name[](Name);
    //        //Data.WorldBiom[ix] = wb;
    //        //Data.WorldWidth[ix] = w;
    //        //Data.DataTime[ix] = date;


    //    }
    //}

    public void Load(string Name)
    {
        
        int ix = Data.Name.Count;
        bool newName = true;
        for (int i = 0; i < ix; i++)
        {
            if (Data.Name[i] == Name)
            {
                //   i = ix;
                ix = i;
                newName = false;
            }
        }

        if (newName)
        {
            //Directory.CreateDirectory(Application.dataPath + $"/Map/Global/g{g3}/");

        }
        else
        {

            byte[] img = File.ReadAllBytes(Application.dataPath + $"/Map/{Name}/Map1.png");
            Texture2D noiseTex = new Texture2D(1, 1);
            noiseTex.LoadImage(img);
            noiseTex.Apply();
            Color[] pix1 = noiseTex.GetPixels(0, 0, noiseTex.width, noiseTex.height);

            // Debug.Log(pix1[15]);


            img = File.ReadAllBytes(Application.dataPath + $"/Map/{Name}/Map2.png");
            noiseTex.LoadImage(img);
            noiseTex.Apply();
            Color[] pix2 = noiseTex.GetPixels(0, 0, noiseTex.width, noiseTex.height);

            img = File.ReadAllBytes(Application.dataPath + $"/Map/{Name}/Map3.png");
            noiseTex.LoadImage(img);
            noiseTex.Apply();
            Color[] pix3 = noiseTex.GetPixels(0, 0, noiseTex.width, noiseTex.height);

            for (int i = 0; i < pix1.Length; i++)
            {
                pix1[i] = new Color(Mathf.RoundToInt(pix1[i].r * colorSize), Mathf.RoundToInt(pix1[i].g * colorSize), Mathf.RoundToInt(pix1[i].b * colorSize));
                pix2[i] = new Color(Mathf.RoundToInt(pix2[i].r * colorSize), Mathf.RoundToInt(pix2[i].g * colorSize), Mathf.RoundToInt(pix2[i].b * colorSize));
                pix3[i] = new Color(Mathf.RoundToInt(pix3[i].r * colorSize), Mathf.RoundToInt(pix3[i].g * colorSize), Mathf.RoundToInt(pix3[i].b * colorSize));
            }
            // Debug.Log(pix1[15]);

            TranfMap(pix1, pix2, pix3, noiseTex.width);
           // MR.TranfMap(Data.WorldBiom[ix], pix1, pix2, pix3, Data.WorldWidth[ix]);

        }
    }
}
