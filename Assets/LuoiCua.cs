using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuoiCua : MonoBehaviour
{
    Transform ImgLuoiCua;
    float angle;
    // Start is called before the first frame update
    void Start()
    {
        ImgLuoiCua = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        angle += Time.deltaTime*60;
        ImgLuoiCua.localEulerAngles = new Vector3(0,0,angle);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player.instance.setDied();
    }
     
}
