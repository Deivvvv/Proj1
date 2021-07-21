using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.AI;

public class MobController : MonoBehaviour
{
    private GridLayout gridLayout;
    public List<Vector3Int> target;

    private float speed;
    private int targetNum;
    // Start is called before the first frame update
    void Start()
    {
        targetNum = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target[targetNum] != null) 
        {
            Debug.Log(target[0]);
            Vector3 mousePos = gameObject.transform.position;
            //mousePos = new Vector3(mousePos.x, mousePos.y, 50);
            Vector3Int cellPosition = gridLayout.WorldToCell(mousePos);
            Vector3Int targetPosition = gridLayout.WorldToCell(mousePos);
            if (cellPosition != target[targetNum])
            {
                float F1 = 0;
                float F2 = 0;
                float F3 = 0;
                if()

                    gameObject.transform.position = new Vector3(gameObject.transform.position + speed * F1 * Time.deltaTime, gameObject.transform.position + speed * F2 * Time.deltaTime, 0);
                // if(target[0])

            } 
        }
    }
}
