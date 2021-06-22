using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CellRendler : MonoBehaviour
{
    private InputData _inputData;
    private GameControl _GC;
    public void StartLoad(GameControl GC,InputData inputData)
    {
        _GC = GC;
        _inputData = inputData;
    }

    public void Load()
    {
        _inputData.grid.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        _inputData.grid.transform.DOScale(new Vector3(1f, 1f, 1f), 1f);

        _inputData.findText.DOFade(0, 0.01f);
        _inputData.findText.DOFade(1, 4f);

    }

    public void LoadScrean()
    {
        _inputData.loadScrean.DOFade(1, 0.01f);
        _inputData.loadScrean.DOFade(0, 2f);
    }
    public void NewCell(int pack, int[] curentPack)
    {
        if (curentPack.Length > _inputData.grid.transform.childCount)
        {
            for (int i = _inputData.grid.transform.childCount; i < curentPack.Length; i++)
            {
                GameObject GO = Instantiate(_inputData.original);
                GO.transform.SetParent(_inputData.grid.transform);

                GO.GetComponent<CellNumber>()._gControl = _GC;
                GO.GetComponent<CellNumber>().numm = i;

            }
        }
        else if (curentPack.Length < _inputData.grid.transform.childCount)
        {
            for (int i = curentPack.Length; i < _inputData.grid.transform.childCount; i++)
            {
                _inputData.grid.transform.GetChild(i).gameObject.active = false;
            }
        }

        for (int i =0; i < curentPack.Length; i++)
        {
            _inputData.grid.transform.GetChild(i).gameObject.active = true;

            _inputData.grid.transform.GetChild(i).transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Stop();
            _inputData.grid.transform.GetChild(i).transform.GetChild(0).gameObject.GetComponent<Image>().sprite = _inputData.imagePack[pack].image[curentPack[i]];

        }
    }
    public IEnumerator ResetCell()
    {
        for (int i = 0; i < _inputData.grid.transform.childCount; i++)
        {
            _inputData.grid.transform.GetChild(i).transform.GetChild(0).position = _inputData.grid.transform.GetChild(i).position;
        }
        yield return null;
    }

    public IEnumerator WrongCell(int cell)
    {
        Transform GO = _inputData.grid.transform.GetChild(cell).transform.GetChild(0);
        GO.DOMove(new Vector3(GO.position.x - 12, GO.position.y, 0), 0.1f);

        yield return new WaitForSeconds(0.1f);

        Vector3 StartGO = _inputData.grid.transform.GetChild(cell).position;
        GO.DOMove(StartGO, 0.1f);

    }

    public IEnumerator TargetCell(int cell)
    {

        _inputData.grid.transform.GetChild(cell).transform.GetChild(0).localScale = new Vector3(1f, 1f, 1f);

        _inputData.grid.transform.GetChild(cell).transform.GetChild(0).DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.5f);

        _inputData.grid.transform.GetChild(cell).transform.GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();


        yield return new WaitForSeconds(0.5f);

        _inputData.grid.transform.GetChild(cell).transform.GetChild(0).DOScale(new Vector3(1f, 1f, 1f), 0.5f);
        _GC.NewLevel();

        ResetCell();
        yield return null;
    }
}
