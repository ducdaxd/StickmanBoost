using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlatform : MonoBehaviour
{
    BoxCollider2D col2D;
    // Start is called before the first frame update
    void Start()
    {
        col2D = GetComponents<BoxCollider2D>()[0];
        col2D.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (Player.instance.transform.position.y-0.2f>transform.position.y)
        //{
        //    col2D.enabled = true;
        //}

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Player.instance.body.velocity.y < 0)
        {
            col2D.enabled = true;
            Player.instance.direction = Vector3.zero;
            Player.instance.body.drag = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        col2D.enabled = false;
    }
}
