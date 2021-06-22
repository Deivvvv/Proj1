using UnityEngine;
using UnityEngine.UI;

public class InputData : MonoBehaviour
{
    [Header("Game Setting")]
    public ImagePack[] imagePack;
    public int[] levelCell;

    [Header ("Scene Data")]
    public GameObject grid;
    public Text findText;
    public Image loadScrean;

    [Header("Prefab")]
    public GameObject original;

    [Header("System Button")]
    public GameObject restartPanel;
}
