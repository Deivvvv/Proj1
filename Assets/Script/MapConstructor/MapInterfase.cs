using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapInterfase : MonoBehaviour
{

    /*
     этот код интерпритирует действия игрока
     
     */

    [SerializeField]
    private MapData mapData;
    [SerializeField]
    private GameObject palliteWindow;
    private bool palliteMood;
    private byte curentPalitte;

    [SerializeField]
    private Button[] button;

    private void ButtonsLoad(int id)
    {
        button[id].onClick.AddListener(() => PressButton(id));
    }

    private void PalliteLoad(int id)
    {

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

        }
        PalliteLoad(curentPalitte);
    }
    // Start is called before the first frame update
    void Start()
    {
        for(int i =0; i < button.Length; i++)
        {
            ButtonsLoad(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
