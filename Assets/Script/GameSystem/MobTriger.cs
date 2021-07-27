using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobTriger : MonoBehaviour
{
    private MobData _mobData;
    private GamePlayCore Sys;
    // Start is called before the first frame update
    void Awake()
    {
        _mobData = gameObject.GetComponent<MobData>();
    }
    public void SetSystem(GamePlayCore sys)
    {
        Sys = sys;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
      //  Debug.Log("144");
        if (!_mobData.Pause)
        {
            if (col.gameObject.GetComponent<MobData>())
            {
                Sys.DateArmy(_mobData, col.gameObject.GetComponent<MobData>(), true);
            }
            else if (col.gameObject.GetComponent<TowerTriger>())
            {
                Sys.DateTower(_mobData, col.gameObject.GetComponent<TowerTriger>().num);
            }
        }
        //Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        //spriteMove = -0.1f;
    }

    void OnCollisionExit2D(Collision2D col)
    {
      //  Debug.Log("133");
        if (col.gameObject.GetComponent<MobData>())
        {
            Sys.DateArmy(_mobData, col.gameObject.GetComponent<MobData>(), false);
        }
        //Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        //spriteMove = -0.1f;
    }
    //void OnCollisionStay2D(Collision2D col)
    //{

    //    Debug.Log("111");
    //}
}
