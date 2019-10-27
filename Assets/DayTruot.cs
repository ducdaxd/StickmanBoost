using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayTruot : MonoBehaviour
{
    public GameObject effector;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        //if (Player.instance.body.velocity.y<0)
        //{
        //    return;
        //}
        Player.instance.truotday();
        effector.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        effector.SetActive(false);
        Player.instance.stoptruotday();
    }
}
