using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum STATE{
    NORMAL,
    UNDERWATER,
    DUDAY
};
public class Player : MonoBehaviour
{
    public Rigidbody2D body;
    public Vector2 direction;
    bool isOnAir;

    static public Player instance;

    [SerializeField]
    GameObject bloodEffect;

    Animator anima;
    int countJump;

    float dirClimb;


    STATE state;
    Action actUpdate;
    bool isOnGround;
    bool isSitDown;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        body = GetComponent<Rigidbody2D>();
        anima = GetComponent<Animator>();

        state = STATE.NORMAL;
        actUpdate = updateNormal;
    }

    public void jump()
    {
        //if (isOnAir) return;
        
        if (countJump > 1)
        {
            //countJump = 0;
            return;
            
        }
        body.isKinematic = false;
       
        //Debug.Log("jumping...");
        isOnAir = true;
        isOnGround = false;
        body.drag = 0.2f;
        if (countJump == 1)
        {
            anima.Play("RollAir");
        }
        else
        {
            body.AddForce(Vector2.up * 200);
            anima.Play("Jump");
        }
       
        countJump++;


    }
    public void sitdown()
    {
        anima.Play("SitDown");
        isSitDown = true;
    }

    float angleUnderWater;
    float forceUnderWater;
    void updateControlUnderWater()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            angleUnderWater = 1;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            angleUnderWater = -1;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            forceUnderWater = 1;
            Debug.Log("an phim w");

        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            angleUnderWater = 0;
            //body.velocity = new Vector2(0,body.velocity.y);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            forceUnderWater = 0;
        }
        if(forceUnderWater>0)
            anima.CrossFade("Swimming",0.1f);
        transform.localEulerAngles += new Vector3(0,0,angleUnderWater);
        body.AddForce(transform.up*forceUnderWater*20);
    }

    void updateNormal()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //body.AddForce(Vector2.left*100);
            direction = Vector2.left;
            body.drag = 0.2f;
            transform.localScale = new Vector3(-1, 1, 1);

            if (dirClimb * transform.localScale.x < 0)
            {
                endClimb();
            }

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            //body.AddForce(Vector2.right*100);
            direction = Vector2.right;
            body.drag = 0.2f;
            transform.localScale = new Vector3(1, 1, 1);

            if (dirClimb * transform.localScale.x < 0)
            {
                endClimb();
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("dx "+direction.x);
            if (direction.x != 0.0f)
            {
                anima.Play("Slide");
            }
            else
            {
                if (isOnGround)
                    sitdown();
            }
            
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            jump();
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            direction = Vector2.zero;
            //body.velocity = new Vector2(0,body.velocity.y);
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            if (isSitDown)
            {
                anima.Play("Idle");
                isSitDown = false;
            }
            
        }

        body.AddForce(direction * 10);
    }
    // Update is called once per frame
    void Update()
    {
        actUpdate();

    }

    public void checkClimb()
    {
        float angle = Vector2.Angle(body.velocity, Vector2.right);
        //Debug.Log("sao ko an "+angle);

        if (angle > 160 || angle < 30)
        {
            body.drag = 16.2f;
            anima.Play("Climp");
            dirClimb = transform.localScale.x;
            countJump = 1;

        }
        else
        {
            body.drag = 0.2f;
            anima.Play("Idle");
        }
    }
    public void endClimb()
    {
        body.drag = 0.2f;
        anima.Play("Idle");
        body.isKinematic = false;
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOnAir = false;

        //Debug.Log("ssss"+collision.gameObject.name +"  ---  "+collision.relativeVelocity);
        isOnGround = true;
        countJump = 0;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        body.drag = 0.2f;
       
    }
    public void setArchored()
    {
        body.isKinematic = true;
        body.velocity = Vector2.zero;
        isOnAir = false;
        anima.Play("Climp");
        countJump = 0;
    }

    public void truotday()
    {
        //body.isKinematic = true;
        anima.Play("MovingWire");
    }
    public void stoptruotday()
    {
        //body.isKinematic = false;
        anima.Play("Idle");
    }


    IEnumerator dissbleBlood()
    {
        yield return new WaitForSeconds(2);
        bloodEffect.SetActive(false);
    }
    public void setDied()
    {
        bloodEffect.SetActive(true);
        StartCoroutine(dissbleBlood());
    }

    IEnumerator coFadeOutGravity()
    {
       
        while (body.gravityScale>0)
        {
            body.gravityScale -= Time.deltaTime;
            body.velocity *= 0.8f;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        body.gravityScale = 0;
    }
    public void setUnderWater()
    {
        state = STATE.UNDERWATER;

        StartCoroutine(coFadeOutGravity());
        actUpdate = updateControlUnderWater;
    }

    public void setNormalControl()
    {
        state = STATE.NORMAL;
        actUpdate = updateNormal;
        body.gravityScale = 1;
        transform.localEulerAngles = Vector3.zero;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position,transform.position+new Vector3(0,-0.25f,0));
    }

}
