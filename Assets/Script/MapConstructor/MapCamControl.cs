using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCamControl : MonoBehaviour
{
    private Camera Cam;
    public float speed =1;
    // Start is called before the first frame update
    void Start()
    {
        Cam = GetComponent<Camera>(); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float translation = Input.GetAxis("Vertical") * Cam.orthographicSize/30;
        float rotation = Input.GetAxis("Horizontal") * Cam.orthographicSize/30;

        float mw = -Input.GetAxis("Mouse ScrollWheel");
        if(mw < 0)
        {
            if(Cam.orthographicSize > 1)
            {
                Cam.orthographicSize += mw;
            }
            else
            {
                Cam.orthographicSize = 1;
            }
        }else if (mw > 0)
        {
            if (Cam.orthographicSize < 10)
            {
                Cam.orthographicSize += mw;
            }
            else
            {
                Cam.orthographicSize = 10;
            }
        }

        gameObject.transform.position += new Vector3(rotation, translation, 0);
    }
}
