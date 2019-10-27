using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRotate : MonoBehaviour
{
    Vector3 centerPos;
    float angle = 0;
    public TargetJoint2D targetJoint;
    // Start is called before the first frame update
    void Start()
    {
        centerPos = transform.position + new Vector3(0,1,0);
    }

    // Update is called once per frame
    void Update()
    {
        angle += Time.deltaTime;
        targetJoint.target = centerPos + new Vector3(Mathf.Cos(angle),Mathf.Sin(angle));
    }
}
