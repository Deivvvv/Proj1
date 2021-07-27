using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayInterfase : MonoBehaviour
{
    private GamePlayCore GPC;
    // Start is called before the first frame update
    void Start()
    {
        GPC = GetComponent<GamePlayCore>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos = new Vector3(mousePos[0],mousePos[1],50);
            GPC.SetTargetTower(mousePos);
        }
        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos = new Vector3(mousePos[0], mousePos[1], 50);
            GPC.CallTargetTower(mousePos);
        }
    }
}
