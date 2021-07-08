using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapRedactor : MonoBehaviour
{
    private MapData mapData;
    private MapInterfase MapI;

    [SerializeField]
    private Tilemap[] level;

 //   public grid gridd; Cell Size
    public GameObject Ghost;
    public int x;
    public int y;
    public float cell =0.8659766f;

    private bool st;
    // Start is called before the first frame update
    void Start()
    {
        mapData = GetComponent<MapData>();
        MapI = GetComponent<MapInterfase>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            st = !st;
        }
            if (st)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            Vector3Int mousePos1 = new Vector3Int((int)(mousePos.x - 0.2f), (int)(mousePos.y - 0.5f), 50);
            x = (int)mousePos.y;
            y = (int)mousePos.x;
            Ghost.transform.position = new Vector3Int((int)(mousePos.x -0.3f), (int)(mousePos.y +0.5f), 50);//Camera.main.ScreenToWorldPoint(Input.mousePosition);
                                                                                                            // Debug.Log($"{x}  {y}");
            if (Input.GetMouseButtonDown(0))
            {
                mousePos1 = new Vector3Int(x, y, 50);
                // .. Debug.Log($"{x}  {y}");
                level[MapI.curentPalitte].SetTile(mousePos1, mapData.DataTile[MapI.curentPalitte].Data[MapI.curentTile].tile);


                //   .thisTile = mapData.DataTile[MapI.curentPalitte].Data[curentTile].tile;
            }
        }
    }
}
