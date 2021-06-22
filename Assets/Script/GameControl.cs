using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    private bool _game;
    [SerializeField] private CellRendler _cellRendler;
    [SerializeField] private InputData _inputData;

    private int _curentPack;
    private int _curentLevel;
    private int _buttonNum;

    private List<int> _curentList;
    private List<int> _curentListNum;
    void Start()
    {
        Application.targetFrameRate = 60;

        _game = true;
        _cellRendler.StartLoad(GetComponent<GameControl>(), _inputData);
        NewCell();

        _cellRendler.LoadScrean();
        _cellRendler.Load();


    }

    void ResetList()
    {
        _curentList.Clear();
        _curentListNum.Clear();
    }

    void LoadList(List<int> list)
    {
        int i2 = Random.Range(0, list.Count);

        if (_curentList.Count > 0)
        {
            for (int i3 = 0; i3 < _curentListNum.Count; i3++)
            {
                if (_curentList[i3] == i2)
                {
                    if (_curentListNum[i3] == _curentPack)
                    { 
                        i2++;
                        if (i2 >= list.Count)
                        {
                            i2 = 0;
                        }
                        i3 = 0;
                    }
                }
            } 
        }
        _curentList.Add(list[i2]);
        _curentListNum.Add(_curentPack);
    }

    void NewCell()
    {
        _curentPack = Random.Range(0, _inputData.imagePack.Length);
        List<int> List = new List<int>();
        for (int i = 0; i < _inputData.imagePack[_curentPack].image.Length; i++)
        {
            List.Add(i);
        }


        int CellSize = _inputData.levelCell[_curentLevel];
        if (CellSize > List.Count)
        {
            Debug.Log("–азмер набора изображений меньше требуемого");
        }
        else
        {
            LoadList(List);

            List<int> CurentList = new List<int>();

            int s = _curentList.Count - 1;
            CurentList.Add(List.IndexOf(_curentList[s]));
            List.Remove(_curentList[s]);
            s = _curentList[s];


            for (int i = 1; i < CellSize; i++)
            {
                int i1 = Random.Range(0, List.Count);
                CurentList.Add(List[i1]);
                List.RemoveAt(i1);
            }

            int[] actualCell = new int[CellSize];
            for (int i = 0; i < CellSize; i++)
            {
                int i1 = Random.Range(0, CurentList.Count);
                actualCell[i] = CurentList[i1];
                CurentList.RemoveAt(i1);
                if (actualCell[i] == s)
                {
                    _buttonNum = i;
                }
            }

            _cellRendler.NewCell(_curentPack, actualCell);

            _inputData.findText.text = "Find   "  + _inputData.imagePack[_curentPack].names[s];
        }
    }

    public void NewGame()
    {
        _curentLevel = 0;
        _game = true;
        _inputData.restartPanel.active = false;
        _cellRendler.LoadScrean();
        _cellRendler.Load();
        ResetList();
        NewCell();
    }

    void EndGame()
    {
        _game = false;
        _inputData.restartPanel.active = true;
    }

    public void NewLevel()
    {
        _curentLevel++;
        if (_curentLevel >= _inputData.levelCell.Length)
        {
            EndGame();
        }
        else
        {
            NewCell();
        }
    }

    public void Result(int result)
    {
        if (_game)
        {
            if (result == _buttonNum)
            {
                StartCoroutine(_cellRendler.TargetCell(result));
            }
            else
            {
                StartCoroutine(_cellRendler.WrongCell(result));
            }
        }
    }
}
