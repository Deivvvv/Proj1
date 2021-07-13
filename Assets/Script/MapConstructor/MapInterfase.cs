using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapInterfase : MonoBehaviour
{

    /*
     этот код интерпритирует действия игрока
     
     */

 //   [SerializeField]
    private MapData mapData;

    [SerializeField]
    private GameObject palliteWindow;
    [SerializeField]
    private GameObject origButton;
    [SerializeField]
    private Image curentTileImage;

    public bool palliteMood;
    public byte curentPalitte;
    public int curentTile;

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
        curentTileImage.sprite = mapData.DataTile[curentPalitte].Data[curentTile].headSprite;
    }

    private void PressButton(int id)
    {
        switch(id)
        {
            case (0):
                palliteMood = false;
                break;
            case (1):
                palliteMood = true;
                break;
            case (2):
                curentPalitte = 0;//BG tail
                break;
            case (3):
                curentPalitte = 1;//RiverTail
                break;
            case (4):
                curentPalitte = 2;//RoadTail
                break;
            case (5):
                curentPalitte = 3;//TowerTail
                break;
            case (6):
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
        Application.targetFrameRate = 30;
        for (int i =0; i < button.Length; i++)
        {
            ButtonsLoad(i);
        }
        PalliteLoad(curentPalitte);
    }

}
