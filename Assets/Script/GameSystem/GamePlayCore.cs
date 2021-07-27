using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
using DG.Tweening;

public class GamePlayCore : MonoBehaviour
{

    [SerializeField]
    private GridLayout gridLayout;
    private float _time;
    //[SerializeField]
    [SerializeField]
    private MapData mapData;

    private List<TowerData> _towerData;
    private List<GameObject> _mob;
    [SerializeField]
    private TowerClassData[] _òowerClassData;

    [SerializeField]
    private SaveLoadGame SLG;

    [SerializeField]
    private GameObject BaseIndicator;


    [SerializeField]
    private MobAnimator _mobAnimator;


    private int width;
    [SerializeField]
    private float[] _mapPix;
    [SerializeField]
    private GameObject Ghost;
    [SerializeField]
    private GameObject GhostTower;

    private int _targetTower;
    // Start is called before the first frame update
    void Start()
    {
        _mob = new List<GameObject>();
        _targetTower = -1;
        _time = 0;
    }
    
    public void SetTargetTower(Vector3 Poz)
    {
        Vector3Int cellPosition = gridLayout.WorldToCell(Poz);
        for(int i =0; i < _towerData.Count; i++)
        {
            if (_towerData[i].V3 == cellPosition)
            {
                if (_towerData[i].Team == 1)
                {
                    _targetTower = i;// _towerData[i].Id;
                    i = _towerData.Count;
                }
            }
        }
    }
    public void CallTargetTower(Vector3 Poz)
    {
        if (_targetTower != -1)
        {
            Vector3Int cellPosition = gridLayout.WorldToCell(Poz);
            int newId = -1;
            for (int i = 0; i < _towerData.Count; i++)
            {
                if (_towerData[i].V3 == cellPosition)
                {
                   newId = i;//_towerData[i].Id;
                    i = _towerData.Count;
                }
            }
            if((newId != -1)&&(newId != _targetTower))
            {
                CallArmy(newId,0);
            }
        }
        _targetTower = -1;
    }
    void CallArmy(int id, int Tayp)
    {
        if ((_towerData[_targetTower].Solder[0] != 0) && (_towerData[_targetTower].Solder[Tayp] != 0)) 
        {
            GameObject GO = Instantiate(Ghost);
            GO.transform.SetParent(gameObject.transform);
            GO.transform.position = new Vector3(-10,-10,-10) ;
            GO.name = "Mob";
            GO.GetComponent<MobTriger>().SetSystem(GetComponent<GamePlayCore>());


            _mob.Add(GO);
            GO.GetComponent<MobData>().Tayp = Tayp;
            GO.GetComponent<MobData>().Team = _towerData[_targetTower].Team;

            if (_towerData[_targetTower].Solder[Tayp] > _towerData[_targetTower].Solder[0])
            {
                GO.GetComponent<MobData>().Size = _towerData[_targetTower].Solder[0];
            }
            else
            {
                GO.GetComponent<MobData>().Size = _towerData[_targetTower].Solder[Tayp];
            }

            _towerData[_targetTower].Solder[0] -= GO.GetComponent<MobData>().Size;

            if(Tayp != 0)
                _towerData[_targetTower].Solder[Tayp] -= GO.GetComponent<MobData>().Size;



            GO.GetComponent<MobData>().StartTowerID = _targetTower;
            GO.GetComponent<MobData>().EndTowerID = id;
            //if ()
            //GO.GetComponent<MobAnimator>().Play(aNewCor, aSpeed, GO);
            CreatePath(_towerData[id].V3,GO);
            //_mobAnimator.Play(aNewCor, aSpeed, GO);

            UpdateTower(_targetTower);
        }
    }

    private IEnumerator Stay(MobData Army2, float value)
    {
        Army2.Pause = true;
        Army2.s.Pause();
        yield return new WaitForSeconds(value);
        Army2.s.Play();
    }
    private IEnumerator Shot(MobData Army1, MobData Army2, float value)
    {
        Army2.Size -= Army1.Size;
        Army1.Shot = true;

        if (Army2.Size <= 0)
            RemoveBot(Army2.gameObject);

        yield return new WaitForSeconds(value);
        Army1.Shot = false;
    }

    void RemoveBot(GameObject GO)
    {
        _mob.Remove(GO);
        Destroy(GO);
    }

    public void DateArmy(MobData Army1, MobData Army2, bool Enter)
    {
        if (Enter)
        {
          
            ////Sequence s = Army2.s;
            ////s.Pause();
            ////Army2.s = s;
            //Army2.s.Pause();
            if (Army1.Team != Army2.Team)
            {
                if (Army1.Tayp == Army2.Tayp)
                {
                    int size = Army1.Size;
                    Army1.Size -= Army2.Size;
                    Army2.Size -= size;
                }
                //else if ((Army1.Tayp ==2)())
                //{

                //}

                if (Army1.Size <= 0)
                    RemoveBot(Army1.gameObject);
                if (Army2.Size <= 0)
                    RemoveBot(Army2.gameObject);
            }
            else
            {
                //if (Army1.Tayp == Army2.Tayp)
                //{
                //    Co_WaitForSeconds(Army2, 2);
                //    Army2.Pause = true;
                //}
                //else 
                if (Army1.Tayp < Army2.Tayp)
                {
                    Stay(Army1, 2);
                }
                else
                {
                    Stay(Army2, 2);
                }

                
                if (Army1.EndTowerID == Army2.EndTowerID)
                {
                    // if() // åñëè ïðèâûøèí ðàçìåð îòðÿäà, òî ïåðåíîñèì áîéöîâ òîëüêî äî ïðèäåëà, à îñòàòîê îñòàâëÿåì âî âòîðîì îòðÿäå
                    if (Army1.Tayp == Army2.Tayp)
                    {
                        Army1.Size += Army2.Size;
                        RemoveBot(Army2.gameObject);
                    }
                }
            }
        }
        else
        {
            Army2.Pause = false;
            ////Sequence s = Army2.s;
            ////s.Pause();
            ////Army2.s = s;
            //Army2.s.Pause();
        }
    }
    public void DateTower(MobData Army1, int id)
    {
        if (Army1.Team != _towerData[id].Team)
        {
            int size = _towerData[id].Solder[0];
            _towerData[id].Solder[0] -= Army1.Size;
            Army1.Size -= size;

            if (_towerData[id].Solder[0] < 0)
            {
                _towerData[id].Solder[0] = -_towerData[id].Solder[0];

                _towerData[id].Team = Army1.Team;

            }


            UpdateTower(id);
            RemoveBot(Army1.gameObject);

        }
        else if(Army1.EndTowerID ==id)
        {
            _towerData[id].Solder[Army1.Tayp] += Army1.Size;
            RemoveBot(Army1.gameObject);
        }
    }


    public void LoadDataMap(Color[] M1, Color[] M2, Color[] M3, int w)
    {
        width = w;
        NavigationMap(M1, M2);
        AddTower(M3);
    }

    void AddTower(Color[] M3)
    {
        _towerData = new List<TowerData>();
        for (int i = 0; i < M3.Length; i++)
        {

            Vector3Int cellPosition = new Vector3Int(i % width, i / width, 50);
            int id = 0;
            
            if (M3[i].r > 0)
            {
                TowerData TD = new TowerData();

                id = (int)M3[i].r - 1;
                mapData.level[5].SetTile(cellPosition, mapData.DataTile[5].Data[id].tile);

                if (M3[i].g > 0)
                {
                    int id2 = (int)M3[i].g;
                    mapData.ColorPlayer[id2].SetTile(cellPosition, mapData.DataTile[6].Data[id].tile);
                    TD.Team = id2;

                }
                if (M3[i].b > 0)
                {
                    int id3 = (int)M3[i].b;
                   // mapData.ColorPlayer[id2].SetTile(cellPosition, mapData.DataTile[6].Data[id].tile);
                    TD.TowerLevel= id3;

                }

                TD.ResGen = _òowerClassData[id].GenerateResource[TD.TowerLevel];
                TD.ResTayp = _òowerClassData[id].Resource;

                TD.MaxSolder = _òowerClassData[id].MaxSolder[TD.TowerLevel];
                TD.Solder = new int[4];
                if (TD.ResTayp == 0)
                {
                    TD.Solder[0] = TD.MaxSolder;
                }
                else
                {
                    TD.Solder[0] = TD.MaxSolder / 2;
                    TD.Solder[TD.ResTayp] = TD.MaxSolder / 2;

                }
                TD.Id = id;
                TD.V3 = cellPosition;

                GameObject GO = Instantiate(BaseIndicator);
                //     GO.transform.position = new Vector3(cellPosition[1], cellPosition[0], 50);
                GO.transform.position = gridLayout.CellToWorld(cellPosition);// new Vector3(cellPosition[0],cellPosition[1]-0.5);
                GO.transform.position = new Vector3(GO.transform.position.x, GO.transform.position.y-0.5f, GO.transform.position.z);
                //  TMP_Text m_TextComponent =



                //   UpdateTower(i);
                GO.GetComponent<SpriteRenderer>().color = mapData.ColorPlayer[TD.Team].GetComponent<Tilemap>().color;
                GO.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = $"{TD.Solder[0]}/{ TD.MaxSolder}";

                GO.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = mapData.ColorPlayer[TD.Team].GetComponent<Tilemap>().color;
                GO.transform.GetChild(1).GetChild(0).gameObject.GetComponent<TMP_Text>().text = $"{TD.TowerLevel}";


                // TMP_Text m_TextComponent = GetComponent<TMP_Text>();

                // Change the text on the text component.
                //  m_TextComponent.text = "Some new line of text.";

                TD.Indicator = GO;
                
                GO = Instantiate(GhostTower); 
                GO.transform.position = gridLayout.CellToWorld(cellPosition);
                GO.GetComponent<TowerTriger>().num = _towerData.Count;

                _towerData.Add(TD);

            }
        }
       
    }
    void NewColor(int id)
    {
        //  mapData.ColorPlayer[0].active = false;
        Vector3Int cellPosition = _towerData[id].V3;
        var tile = mapData.ColorPlayer[0].GetTile(cellPosition);

        for (int i = 0; i < mapData.ColorPlayer.Length; i++)
        {
            if (id == 0)
            {
                // mapData.ColorPlayer[i].SetTile(cellPosition, null);
            }
            else
            if (i == id)
            {
                mapData.ColorPlayer[i].SetTile(cellPosition, tile);
            }
            else
            {
                mapData.ColorPlayer[i].SetTile(cellPosition, null);
            }

        }
    }
    void UpdateTower(int id) 
    {
        TowerData TD = _towerData[id];

        TD.Indicator.GetComponent<SpriteRenderer>().color = mapData.ColorPlayer[TD.Team].GetComponent<Tilemap>().color;
        TD.Indicator.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = $"{TD.Solder[0]}/{ TD.MaxSolder}";

        TD.Indicator.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = mapData.ColorPlayer[TD.Team].GetComponent<Tilemap>().color;
        TD.Indicator.transform.GetChild(1).GetChild(0).gameObject.GetComponent<TMP_Text>().text = $"{TD.TowerLevel}";
    }

    void NavigationMap(Color[] pix1, Color[] pix2)
    {
        _mapPix = new float[pix1.Length];

        for(int i =0;i< pix1.Length; i++)
        {
            float f = 0.5f;
            //  float f = 10f;
            //if (pix1[i].g > 0)
            //    f = 1;

            //if (pix2[i].r > 0)
            //{
            //    if (pix2[i].b > 0)
            //    {
            //        f -= 0.2f;

            //    }
            //    else
            //    {
            //        f += 3f;
            //    }
            //}
            //else
            //if (pix2[i].b > 0)
            //{
            //    f -= 0.4f;
            //}
            _mapPix[i] = f;
        }
    }

    void TowerCall()
    {
        //Debug.Log(_towerData.Count);
        for (int i = 0; i < _towerData.Count; i++)
        {
            if (_towerData[i].MaxSolder > _towerData[i].Solder[0])
            {
                _towerData[i].Solder[0] += _towerData[i].ResGen;
                if (_towerData[i].MaxSolder < _towerData[i].Solder[0])
                {
                    _towerData[i].Solder[0] = _towerData[i].MaxSolder;
                }
            }
            else if (_towerData[i].MaxSolder < _towerData[i].Solder[0])
            {
                _towerData[i].Solder[0]--;
            }

            if (_towerData[i].ResTayp != 0)
            {
                if (_towerData[i].MaxSolder /2 > _towerData[i].Solder[_towerData[i].ResTayp])
                {
                    _towerData[i].Solder[_towerData[i].ResTayp] += _towerData[i].ResGen;
                    if (_towerData[i].MaxSolder /2 < _towerData[i].Solder[_towerData[i].ResTayp])
                    {
                        _towerData[i].Solder[_towerData[i].ResTayp] = _towerData[i].MaxSolder/2;
                    }
                }
                else if (_towerData[i].MaxSolder/2 < _towerData[i].Solder[_towerData[i].ResTayp])
                {
                    _towerData[i].Solder[_towerData[i].ResTayp]--;
                }
            }



           

            UpdateTower(i);
           // Debug.Log(i);
            //if (_òowerData[i].Solder > _òowerClassData[_òowerData[i].Tayp].MaxSolder[i])
            //{
            //    _òowerData[i].Solder--;
            //}
        }
    }

    void Call()
    {
        TowerCall();
    }

    void CreatePath(Vector3Int v3, GameObject GO)
    {
        List<float> speed = new List<float>();
        //int targX;
        //int targY;
        Vector3Int targV = _towerData[_targetTower].V3;//gridLayout.WorldToCell(new Vector3Int(10, 10, 50));//new Vector3Int(0, 0, 50);

        //int x = v1[0];
        //int y = v1[1];
        Vector3Int v2 = new Vector3Int(0, 0, 0);

       // Vector3Int v3 = gridLayout.WorldToCell(v1);
        v3 = new Vector3Int(v3[0], v3[1], 50);
        List<Vector3Int> newCor = new List<Vector3Int>();

      //  Debug.Log(targV);
      //  Debug.Log(v3);
        while (targV != v3)
        {
            /*
             êàæäûé x ñìåùàåò ïîçèöèþ áûñòðîãî ïåðåõîäà íà  y íà 1
             

            (2.2)  (x, y)
            1.2    (x - 1, y)
            1.3    (x - 1, y + 1)
            2.1    (x, y - 1)
            2.3    (x, y + 1)
            3.1    (x + 1, y - 1)
            3.2    (x + 1, y)

             public IEnumerable<Vector3Int> GetCellsAround(Vector3Int cell)
    {
        yield return new Vector3Int(cell.x, cell.y + 1, cell.z);
        yield return new Vector3Int(cell.x, cell.y - 1, cell.z);
        yield return new Vector3Int(cell.x + 1, cell.y, cell.z);
        yield return new Vector3Int(cell.x - 1, cell.y, cell.z);
        yield return new Vector3Int(cell.x - 1, cell.y + 1, cell.z);
        yield return new Vector3Int(cell.x - 1, cell.y - 1, cell.z);
    }
             */
            float f = 0;

            if (targV[1] < v3[1])
            {//(x+(y+1))
                targV[1]++;
                if (targV[1] % 2 != 1)
                {//(x+1+(y+1))
                    if (targV[0] < v3[0])
                    {
                        targV[0]++;

                    }
                }
                else //if (targV[1] % 2 == 1)
                {//(x-1+(y-1))
                    if (targV[0] > v3[0])
                    {
                        targV[0]--;

                    }
                }
            }
            else
            if (targV[1] > v3[1])
            {//(x+1+(y-1))
                targV[1]--;
                if (targV[1] % 2 == 1)
                {//(x-1+(y-1))
                    if (targV[0] > v3[0])
                    {
                        targV[0]--;

                    }
                }
                else //if (targV[1] % 2 != 1)
                {//(x+1+(y+1))
                    if (targV[0] < v3[0])
                    {
                        targV[0]++;

                    }
                }

            }
            else if (targV[0] < v3[0])
            {
                targV[0]++;

            }
            else
           if (targV[0] > v3[0])
            {
                targV[0]--;

            }

            f = _mapPix[(targV[1] * width + targV[0])];

            newCor.Add(targV);
            speed.Add(f);


        }
      //  Debug.Log("New");

        Vector3[] aNewCor = new Vector3[newCor.Count];
        float[] aSpeed = new float[newCor.Count];
        for(int i = 0; i < newCor.Count; i++)
        {
            aNewCor[i] = gridLayout.CellToWorld(newCor[i]);
            //Vector3 v4 = aNewCor[i];
            //aNewCor[i] = new Vector3(v4[1], v4[0], v4[2]);
            aSpeed[i] = speed[i];
            //Debug.Log($"{aNewCor[i]} <= {newCor[i]}");
        }

        //Vector3[] aNewCor = newCor.ToArray();
        //float[] aSpeed = speed.ToArray();

        _mobAnimator.Play(aNewCor, aSpeed, GO);

        //GameObject GO = Instantiate(Ghost);
        //GO.transform.SetParent(gameObject.transform);
        //GO.name = "Mob";

        //GO.GetComponent<MobAnimator>().Play(aNewCor,aSpeed, GO);
        //Debug.Log(newCor.Count);
        //Debug.Log(newCor[0]);
        //  gridLayout
        //Vector3Int    cellPosition = gridLayout.WorldToCell(v1);
        //Vector3 v2 = gridLayout.CellToWorld(v);
    }
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //   // mousePos = new Vector3(mousePos.x, mousePos.y, 50);
        // //   Vector3Int cellPosition = new Vector3Int(Mathf.RoundToInt(mousePos.x), Mathf.RoundToInt(mousePos.y),0);//= gridLayout.WorldToCell(mousePos);
        //   // CreatePath(mousePos);
        //  //  Vector3 v1 = new Vector3(0, 0, 0);
        //}
    }
   
    void FixedUpdate()
    {
        _time += 0.5f * Time.deltaTime;
        if(_time >= 1)
        {
            _time = 0;
            Call();
        }

    }
}

[System.Serializable]
public class TowerClassData
{
    public int[] MaxSolder;
    public int[] GenerateResource;
    public int Resource;
}

