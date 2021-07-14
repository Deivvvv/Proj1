using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SaveMenager : MonoBehaviour
{
    [SerializeField]
    private MapCase Data;
    [SerializeField]
    private MapInterfase MapI;
    [SerializeField]
    private MapRedactor MR;

    [SerializeField]
    private GameObject origButton;

    [SerializeField]
    private GameObject window;

    void switchData(int id)
    {
        MapI.textField.text = Data.Name[id];
    }
    void ButtonsLoad(int i,GameObject go)
    {
        go.GetComponent<Button>().onClick.AddListener(() => switchData(i));
        go.transform.GetChild(0).gameObject.GetComponent<Text>().text = $"{Data.DataTime[i]} | {Data.Name[i]}";
    }
    public void ReLoadData()
    {//пересчитывает библиотеку

        if (window.transform.childCount > 0)
        {
            for (int i = window.transform.childCount; i > 0; i--)
            {
                Destroy(window.transform.GetChild(i - 1).gameObject);
            }
        }

        //if (!palliteMood)
        //{
        for (int i = 0; i < Data.Name.Count; i++)
        {
            GameObject go = Instantiate(origButton);
            go.transform.SetParent(window.transform);
            ButtonsLoad(i, go);

          //  go.transform.GetChild(0).GetComponent<Image>().sprite = mapData.DataTile[curentPalitte].Data[i].headSprite;
        }
        //}
    }

    public void Save(string Name, Color[] M1, Color[] M2, Color[] M3, int w, int wb)
    {
        var currentDate = System.DateTime.Now;
        string date = "";
        date += currentDate.Day.ToString();
        date += ".";
        date += currentDate.Month.ToString();
        date += " : ";
        date += currentDate.Hour.ToString();
        date += ":";
        date += currentDate.Minute.ToString();


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

        if (!Directory.Exists(Application.dataPath + $"/Map/"))
            Directory.CreateDirectory(Application.dataPath + $"/Map/");
        if (!Directory.Exists(Application.dataPath + $"/Map/{Name}/"))
            Directory.CreateDirectory(Application.dataPath + $"/Map/{Name}/");


        if (newName)
        {
            Data.Name.Add(Name);
            Data.WorldBiom.Add(wb);
            Data.WorldWidth.Add(w);
            Data.DataTime.Add(date);
        }
        else
        {
          //  Data.Name[](Name);
            Data.WorldBiom[ix] =wb;
            Data.WorldWidth[ix] = w;
            Data.DataTime[ix] = date;


        }


        //if (Directory.Exists(Application.dataPath + $"/Map/Global/g{g3}/"))
        //    Directory.Delete(Application.dataPath + $"/Map/Global/g{g3}/", true);
        //Directory.CreateDirectory(Application.dataPath + $"/Map/Global/g{g3}/");
        //if (!Directory.Exists(Application.dataPath + $"/Map/"))
        //{

        //}
        Texture2D Map1 = new Texture2D(w, (M1.Length + 1) / w);
        Texture2D Map2 = new Texture2D(w, (M1.Length + 1) / w);
        Texture2D Map3 = new Texture2D(w, (M1.Length + 1) / w);


        Map1.SetPixels(M1);
        Map2.SetPixels(M2);
        Map3.SetPixels(M3);

        Debug.Log(M1[15]);


        Map1.Apply();
        Map2.Apply();
        Map3.Apply();
        //rend.material.mainTexture = xc;
        byte[] bytes = Map1.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + $"/Map/{Name}/Map1.png", bytes);

        bytes = Map2.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + $"/Map/{Name}/Map2.png", bytes);

        bytes = Map3.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + $"/Map/{Name}/Map3.png", bytes);

        ReLoadData();
        //// For testing purposes, also write to a file in the project folder
    }

    public void Del(string Name)
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
            Directory.Delete(Application.dataPath + $"/Map/{Name}/", true);
            Data.Name.RemoveAt(ix);
            Data.WorldBiom.RemoveAt(ix);
            Data.WorldWidth.RemoveAt(ix);
            Data.DataTime.RemoveAt(ix);
            //  Data.Name[](Name);
            //Data.WorldBiom[ix] = wb;
            //Data.WorldWidth[ix] = w;
            //Data.DataTime[ix] = date;


        }
    }
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

            Debug.Log(pix1[15]);


            img = File.ReadAllBytes(Application.dataPath + $"/Map/{Name}/Map2.png");
            noiseTex.LoadImage(img);
            noiseTex.Apply();
            Color[] pix2 = noiseTex.GetPixels(0, 0, noiseTex.width, noiseTex.height);

            img = File.ReadAllBytes(Application.dataPath + $"/Map/{Name}/Map3.png");
            noiseTex.LoadImage(img);
            noiseTex.Apply();
            Color[] pix3 = noiseTex.GetPixels(0, 0, noiseTex.width, noiseTex.height);

            MR.TranfMap(Data.WorldBiom[ix], pix1, pix2, pix3, Data.WorldWidth[ix]);

        }
        ReLoadData();
    }
}
