using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerJump : MonoBehaviour
{
    Animator anim;
    public LayerMask layerMask;


    Transform sp1;
    Transform sp2;
    float y01;
    float y02;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sp1 = transform.GetChild(0);
        sp2 = transform.GetChild(1);
        y01 = sp1.localPosition.y;
        y02 = sp2.localPosition.y;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        RaycastHit2D hit=
        Physics2D.Raycast(Player.instance.transform.position,Vector2.down,0.36f, layerMask);
        //float angle = Vector2.Angle(Player.instance.body.velocity,Vector2.down);

        //Debug.Log("nhay "+angle);
        //if (angle>-60 && angle <60 && Player.instance.body.velocity.y<=0)
        //if(transform.position.y+0.3f<Player.instance.transform.position.y-0.26f)
        
        if(hit.collider)
        {
            if (hit.collider.name.Contains("PowerJump"))
            {
                anim.Play("PowerJump");
                Debug.Log("play animation ");
            }
            Debug.Log("hit " + hit.collider.name);
           
        }
    }
    public void forceJump()
    {
        Player.instance.body.AddForce(Vector2.up * 500);
    }
    private void Update()
    {
        float dt = Time.deltaTime * 0.2f;
        sp1.localPosition += new Vector3(0, dt,0);
        sp2.localPosition += new Vector3(0, dt, 0);
        if (sp1.localPosition.y > 0.3f)
        {
            sp1.localPosition = new Vector3(0,y01,0);
        }
        if (sp2.localPosition.y > 0.3f)
        {
            sp2.localPosition = new Vector3(0, y02, 0);
        }
    }
}
