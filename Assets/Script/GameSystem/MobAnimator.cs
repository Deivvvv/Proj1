using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.AI;
using DG.Tweening;

public class MobAnimator : MonoBehaviour
{
    //private GridLayout gridLayout;
    //public List<Vector3> target;
  //  private Sequence s;
    //private float speed;
    //private int targetNum;
    // Start is called before the first frame update
    

    // Update is called once per frame
   public void Play(Vector3[] target, float[] speed,GameObject GO)//FixedUpdate()
    {
        Sequence s = GO.GetComponent<MobData>().s;
       // s.Pause();
       s.Kill();
        // GO.transform.Reset();
        //var 
        s = DOTween.Sequence();
        //  s = DOTween.Sequence();
        //var Cube1RunTime = 1.0f;
        //var Cube2RunTime = 1.0f;
       // Debug.Log(target[0]);
        GO.transform.position = target[0];
        Vector3 v = target[0];
        Vector3 v1 = target[1];
        Vector3 v2 = new Vector3((v[0] + v1[0]) / 2, (v[1] + v1[1]) / 2, 0);

        s.Append(GO.transform.DOMove(v2, speed[0], false));

        float f = speed[0] *0.9f;
        s.Insert(f, GO.transform.DOMove(target[1], speed[1], false));
        f += speed[1] * 0.9f;


        //bool xScale = false;
        //if (v2[0]< GO.transform.position.x)
        //{
        //    xScale = true;
        //    //GO.transform.DOScaleX(-GO.transform.localScale.x, 0);
        //}
        ////else
        ////{
        ////    GO.transform.DOScaleX(GO.transform.localScale.x, 0);
        ////}
        //if (xScale)
        //{
        //    GO.transform.DOScaleX(-GO.transform.localScale.x, 0);
        //}
        // s.Append(this.m_Trans.DOLocalMoveX(-3.42f, Cube1RunTime));
        if (target.Length > 1)
        {
          //  Vector3 v1 = target[0];
          //  float f = 0;
            for (int i = 2; i < target.Length; i++)
            {
                v = target[i - 1];
                v1 = target[i];
                v2 = new Vector3((v[0] + v1[0]) / 2, (v[1] + v1[1]) / 2, 0);

                f += speed[i - 1] * 0.9f;

                s.Insert(f, GO.transform.DOMove(v2, speed[i - 1], false));

                f += speed[i] * 0.9f;

                s.Insert(f, GO.transform.DOMove(target[i], speed[i], false));

                //if (v[0] < v1[1])
                //{
                //    if (xScale)
                //    {
                //        xScale = false;
                //        s.Insert(f, GO.transform.DOScaleX(-GO.transform.localScale.x, 0));  //  GO.transform.DOScaleX(-GO.transform.localScale.x, 0);
                //    }
                //    //    if (GO.transform.localScale.x)
                //    //s.Insert(f, GO.transform.DOScaleX(-GO.transform.localScale.x, 0));
                //}
                //else
                //{
                //    if (!xScale)
                //    {
                //        xScale = true;
                //        s.Insert(f, GO.transform.DOScaleX(-GO.transform.localScale.x, 0));  //  GO.transform.DOScaleX(-GO.transform.localScale.x, 0);
                //    }
                //}
                //else
                //{
                //    s.Insert(f, GO.transform.DOScaleX(GO.transform.localScale.x, 0));
                //}

                //if (xScale)
                //{
                //    s.Insert(f, GO.transform.DOScaleX(-GO.transform.localScale.x, 0));  //  GO.transform.DOScaleX(-GO.transform.localScale.x, 0);
                //}
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
