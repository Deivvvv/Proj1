using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class MapInterfase : MonoBehaviour
{

    /*
     этот код интерпритирует действия игрока
     
     */

    //   [SerializeField]
    [SerializeField]
    private GameObject grid;[SerializeField]
    private GridLayout gridLayout;

    [SerializeField]
    private SaveMenager SM;
    private MapRedactor MR;
    private MapData mapData;
    [SerializeField]
    private SaveMenager saveMenager;

    [SerializeField]
    private GameObject palliteWindow;
    [SerializeField]
    private GameObject origButton;
    [SerializeField]
    private Image curentTileImage;

    public bool palliteMood;
    public byte curentPalitte;
    public int curentTile;


    public InputField textField;
    [SerializeField]
    private Button[] button;

    private void ButtonsLoad(int id)
    {
        button[id].onClick.AddListener(() => PressButton(id));
    }
    private void ButtonsLoad(int id, GameObject go)
    {
        go.GetComponent<Button>().onClick.AddListener(() => PressButtonTile(id));
    }

    private void PalliteLoad(int id)
    {

        if (palliteWindow.transform.childCount > 0)
        {
            for (int i = palliteWindow.transform.childCount; i > 0; i--)
            {
                Destroy(palliteWindow.transform.GetChild(i-1).gameObject);
            }
        }

        if (!palliteMood)
        {
            for (int i = 0; i < mapData.DataTile[id].Data.Length; i++)
            {
                GameObject go = Instantiate(origButton);
                go.transform.SetParent(palliteWindow.transform);
                ButtonsLoad(i, go);

                go.transform.GetChild(0).GetComponent<Image>().sprite = mapData.DataTile[curentPalitte].Data[i].headSprite;
            }
        }
        //else
        //{
        //    for (int i = 0; i < mapData.DataPack[id].Data.Length; i++)
        //    {
        //        GameObject go = Instantiate(origButton);
        //        go.transform.SetParent(palliteWindow.transform);
        //        ButtonsLoad(i, go);
        //    }
        //}

    }

    private void PressButtonTile(int id)
    {
        curentTile = id;
       // curentTileImage.sprite = mapData.DataTile[curentPalitte].Data[curentTile].headSprite;
    }


    public void Save(int wb, Color[] M1, Color[] M2, Color[] M3, int w) 
    {
        Debug.Log(M1[1]);
        SM.Save(textField.text,M1,M2,M3,w,wb);
    }
    private void PressButton(int id)
    {
        curentTile = 0;
        switch (id)
        {
            case (0):
                palliteMood = false;
                break;
            case (1):
                palliteMood = true;
                break;
            case (2):

                if (textField.text != "")
                    MR.Save();

                break;
            case (3):

                if(textField.text !="") 
                    SM.Load(textField.text);

                break;
            case (4):
                if (textField.text != "")
                    SM.Del(textField.text);

                break;
            case (5):
                curentPalitte = 0;//BG tail
                break;
            case (6):
                curentPalitte = 1;//RiverTail
                break;
            case (7):
                curentPalitte = 2;//RoadTail
                break;
            case (8):
                curentPalitte = 3;//TowerTail
                break;
            case (9):
                curentPalitte = 4;//TowerTail
                break;
            //case (7):
            //    curentPalitte = 5;//TowerTail
            //    break;

        }
        PalliteLoad(curentPalitte);
    }
    // Start is called before the first frame update
    void Start()
    {
        mapData  = GetComponent<MapData>();
        MR = GetComponent<MapRedactor>();
        SM = GetComponent<SaveMenager>();
        Application.targetFrameRate = 30;
        for (int i =0; i < button.Length; i++)
        {
            ButtonsLoad(i);
        }
        PalliteLoad(curentPalitte);

        SM.ReLoadData();

        if(GetComponent<CoreGenerator>().enabled != true) 
            PreLoad();
    }
    void PreLoad()
    {
        mapData.level = new Tilemap[6];
        for (int i = 0; i < mapData.level.Length; i++)
        {
            AddGrid(i);
        }
    }

    void AddGrid(int i)
    {
        GameObject GO = Instantiate(grid);
        GO.transform.SetParent(gridLayout.transform);
        GO.name = "Grid" + i;

        GO.GetComponent<TilemapRenderer>().sortingOrder = i;
        mapData.level[i] = GO.GetComponent<Tilemap>();
    }
}
