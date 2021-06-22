using UnityEngine;

public class CellNumber : MonoBehaviour
{
    public int numm;
    public GameControl _gControl;
    public void Result()
    {
        _gControl.Result(numm);
    }
}
