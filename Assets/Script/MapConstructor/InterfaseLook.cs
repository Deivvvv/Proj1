using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InterfaseLook : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    private MapRedactor mapRedactor;

    void Start()
    {
        GameObject GO = GameObject.Find("Canvas");
        mapRedactor = GO.GetComponent<MapRedactor>();
    }
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        mapRedactor.LoadKey(true);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        mapRedactor.LoadKey(false);
    }
}
