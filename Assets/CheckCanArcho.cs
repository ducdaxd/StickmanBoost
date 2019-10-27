using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCanArcho : MonoBehaviour
{
    public Player player;
    public LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Default"))
        {
            return;
        }
        Player.instance.checkClimb();
        if (player.body.velocity.y < 0)
        {

            //RaycastHit2D hit =
            //Physics2D.Raycast(transform.position, Vector2.down, 0.4f,LayerMask.NameToLayer("Default"));

           // Collider2D col = Physics2D.OverlapPoint(transform.position, layerMask);
            Collider2D col2 = Physics2D.OverlapPoint(transform.position+new Vector3(0.1f*Player.instance.transform.localScale.x,0.1f,0),layerMask);
            if (col2==null)
            {
                player.setArchored();
            }
            //Debug.Log("collide wall gggg "+col.name);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("exit climb");
        Player.instance.endClimb();
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.gray;
    //    Gizmos.DrawWireSphere(transform.position + new Vector3(0.1f, 0.1f, 0), 0.05f);
    //}
}
