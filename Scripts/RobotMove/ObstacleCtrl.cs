using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCtrl : MonoBehaviour
{
    public Transform obstacle;
    public Transform pos;

    Rigidbody r; 
    public bool RightTouchState, LeftTouchState;


    void Start()
    {
        r = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(RightTouchState && LeftTouchState)
        {
            Debug.Log("여긴-_-....");
            obstacle.position = pos.transform.position;
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("RightFinger"))
        {
            Debug.Log("RightColl 잡다");
            RightTouchState = true;
        }
        if (other.gameObject.CompareTag("LeftFinger"))
        {
            Debug.Log("LeftColl 잡다");
            LeftTouchState = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("RightFinger"))
        {
            Debug.Log("RightColl 놓다");
            RightTouchState = false;
        }
        if (other.gameObject.CompareTag("LeftFinger"))
        {
            Debug.Log("LeftColl 놓다");
            LeftTouchState = false;
        }
    }


    //void Update()
    //{
    //    if(touchState)
    //    {
    //        Debug.Log("시작");
    //        //if (obstacle == null) return;

    //        r.useGravity = false;
    //        r.isKinematic = true;
    //        obstacle = pos.transform;
    //    }
    //    else
    //    {
    //        //if (obstacle == null) return;

    //        r.useGravity = true;
    //        r.isKinematic = false;
    //    }

    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.CompareTag("Finger"))
    //    {
    //        Debug.Log("잡다");

    //        if (obstacle != null) return;

    //        obstacle = pos.transform;
    //        touchState = true;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if(other.CompareTag("Finger"))
    //    {
    //        Debug.Log("놓다");

    //        if (obstacle == null) return;
    //        obstacle = null;

    //        touchState = false;
    //    }
    //}

    //private void OnCollisionEnter(Collision other)
    //{
    //    if(other.gameObject.CompareTag("Finger"))
    //    {
    //        Debug.Log("Coll 잡다");
    //        if (obstacle != null) return;

    //        obstacle = pos.transform;
    //        touchState = true;
    //    }
    //}

    //private void OnCollisionExit(Collision other)
    //{
    //        if(other.gameObject.CompareTag("Finger"))
    //        {
    //            Debug.Log("Coll 놓다");

    //            if (obstacle == null) return;
    //            obstacle = null;

    //            touchState = false;
    //        }
    //}
}
