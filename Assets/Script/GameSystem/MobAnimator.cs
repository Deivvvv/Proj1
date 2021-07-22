using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.AI;
using DG.Tweening;

public class MobAnimator : MonoBehaviour
{
    //private GridLayout gridLayout;
    //public List<Vector3> target;

    //private float speed;
    //private int targetNum;
    // Start is called before the first frame update
    void Start()
    {
       // targetNum = 0;
        Vector3[] target1 = new Vector3[3];
        target1[0] = new Vector3(1,-120, 0);
        target1[1] = new Vector3(10, 16, 0);
        target1[2] = new Vector3(-10, 0, 0);


        float[] speed1 = new float[3];
        speed1[0] = 10f;
        speed1[1] = 3f;
        speed1[2] = 10f;

        Play(target1, speed1, gameObject);
    }

    // Update is called once per frame
   public void Play(Vector3[] target, float[] speed,GameObject GO)//FixedUpdate()
    {
        var s = DOTween.Sequence();
        //var Cube1RunTime = 1.0f;
        //var Cube2RunTime = 1.0f;

        s.Append(GO.transform.DOMove(target[0], speed[0], false));
        Vector3 v = target[0];
        if (v[0]< GO.transform.position.x)
        {
            GO.transform.DOScaleX(-1, 0);
        }
        else
        {
            GO.transform.DOScaleX(1, 0);
        }
        // s.Append(this.m_Trans.DOLocalMoveX(-3.42f, Cube1RunTime));
        if (target.Length > 0)
        {
            Vector3 v1 = target[0];
            float f = 0;
            for (int i = 1; i < target.Length; i++)
            {
                f += speed[i - 1];
                s.Insert(f, GO.transform.DOMove(target[i], speed[i], false));

                v = target[i - 1];
                v1 = target[i];
                if (v[0] < v1[1])
                {
                    s.Insert(f, GO.transform.DOScaleX(-1, 0));
                }
                else
                {
                    s.Insert(f, GO.transform.DOScaleX(1, 0));
                }
            }
        }
        //s.Insert(speed[0], GO.transform.DOMove(target[1], speed[1], false));
        //s.Insert(speed[1]+ speed[0], GO.transform.DOMove(target[2], speed[2], false));

        //     gameObject.transform.DOMove(target[targetNum], 10, false);
        //if (target[targetNum] != null) 
        //{
        //    //Debug.Log(target[0]);
        //    //Vector3 mousePos = gameObject.transform.position;
        //    ////mousePos = new Vector3(mousePos.x, mousePos.y, 50);
        //    //Vector3Int cellPosition = gridLayout.WorldToCell(mousePos);
        //    //Vector3Int targetPosition = gridLayout.WorldToCell(mousePos);
        //    //if (cellPosition != target[targetNum])
        //    //{
        //    //    float F1 = 0;
        //    //    float F2 = 0;
        //    //    float F3 = 0;
        //    //  //  if()

        //    //        gameObject.transform.position = new Vector3(gameObject.transform.position + speed * F1 * Time.deltaTime, gameObject.transform.position + speed * F2 * Time.deltaTime, 0);
        //    //    // if(target[0])

        //    //} 
        //}
    }
}
