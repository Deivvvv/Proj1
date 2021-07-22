using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GamePlayCore : MonoBehaviour
{

    [SerializeField]
    private GridLayout gridLayout;
    private float _time;
    [SerializeField]
    private TowerData[] _�owerData;
    [SerializeField]
    private TowerClassData[] _�owerClassData;
    [SerializeField]
    private MobAnimator _mobAnimator;


    private int width;
    [SerializeField]
    private float[] _mapPix;
    public GameObject Ghost;
    // Start is called before the first frame update
    void Start()
    {
        _time = 0;
    }
    public void NewMap(Color[] pix1, Color[] pix2, int w)
    {
        width = w;
        _mapPix = new float[pix1.Length];

        for(int i =0;i< pix1.Length; i++)
        {
            float f = 10f;
            if (pix1[i].g > 0)
                f = 1;

            if (pix2[i].r > 0)
            {
                if (pix2[i].b > 0)
                {
                    f -= 0.2f;

                }
                else
                {
                    f += 3f;
                }
            }
            else
            if (pix2[i].b > 0)
            {
                f -= 0.4f;
            }
            _mapPix[i] = f;
        }
    }

    void TowerCall()
    {
        for (int i = 0; i < _�owerData.Length; i++)
        {
            //if (_�owerData[i].Solder > _�owerClassData[_�owerData[i].Tayp].MaxSolder[i])
            //{
            //    _�owerData[i].Solder--;
            //}
        }
    }

    void Call()
    {
        TowerCall();
    }

    void CreatePath(Vector3 v1)
    {
        List<float> speed = new List<float>();
        //int targX;
        //int targY;
        Vector3Int targV = gridLayout.WorldToCell(new Vector3Int(10, 10, 50));//new Vector3Int(0, 0, 50);

        //int x = v1[0];
        //int y = v1[1];
        Vector3Int v2 = new Vector3Int(0, 0, 0);

        Vector3Int v3 = gridLayout.WorldToCell(v1);
        v3 = new Vector3Int(v3[0], v3[1], 50);
        List<Vector3Int> newCor = new List<Vector3Int>();

        Debug.Log(targV);
        Debug.Log(v3);
        while (targV != v3)
        {
            /*
             ������ x ������� ������� �������� �������� ��  y �� 1
             

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
        Debug.Log("New");

        Vector3[] aNewCor = new Vector3[newCor.Count];
        float[] aSpeed = new float[newCor.Count];
        for(int i = 0; i < newCor.Count; i++)
        {
            aNewCor[i] = gridLayout.CellToWorld(newCor[i]);
            //Vector3 v4 = aNewCor[i];
            //aNewCor[i] = new Vector3(v4[1], v4[0], v4[2]);
            aSpeed[i] = speed[i];
            Debug.Log($"{aNewCor[i]} <= {newCor[i]}");
        }

        //Vector3[] aNewCor = newCor.ToArray();
        //float[] aSpeed = speed.ToArray();
        _mobAnimator.Play(aNewCor,aSpeed, Ghost);
        //Debug.Log(newCor.Count);
        //Debug.Log(newCor[0]);
        //  gridLayout
        //Vector3Int    cellPosition = gridLayout.WorldToCell(v1);
        //Vector3 v2 = gridLayout.CellToWorld(v);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
           // mousePos = new Vector3(mousePos.x, mousePos.y, 50);
         //   Vector3Int cellPosition = new Vector3Int(Mathf.RoundToInt(mousePos.x), Mathf.RoundToInt(mousePos.y),0);//= gridLayout.WorldToCell(mousePos);
            CreatePath(mousePos);
          //  Vector3 v1 = new Vector3(0, 0, 0);
        }
    }

    void FixedUpdate()
    {
        _time += 0.1f * Time.deltaTime;
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

    public TowerDataColor TC;
}
[System.Serializable]
public class TowerDataColor
{
    public Tile[] TowerColor;
}

