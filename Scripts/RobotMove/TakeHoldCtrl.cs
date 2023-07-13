using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeHoldCtrl : MonoBehaviour
{
    public Transform holdPos;
    public Transform gripLeft;
    public Transform gripRight;

    Vector3 gripLeftStart, gripRightStart;
    bool contactLeftState, contactRightState;  //장애물을 잡았는지 여부
    bool close, open;
    

    void Start()
    {
        gripLeftStart = gripLeft.position;
        gripRightStart = gripRight.position;
        Debug.Log("gripLeftStart " + gripLeft.position + "gripRightStart " + gripRight.position);
    }

    // Update is called once per frame
    void Update()
    {
        ClickHold();
    }


    void ClickHold()
    {
        //집게 담힘
        if(Input.GetKey(KeyCode.E))
        {
            Debug.Log("close" + close);
            if (!close)
                close = true;
        }
        else if(Input.GetKey(KeyCode.R))
        {
            if (!open)
                open = true;
        }

        if(close)
        {
            Debug.Log("열려라" + contactLeftState + "contactRightState " + contactRightState);
            //gripLeft.AddTorque(5 * gripLeft.mass * gripLeft.transform.up);
            if (!contactLeftState && !contactRightState)
            {
                if (gameObject.name == "Left")
                    gripLeft.transform.position = Vector3.Lerp(gripLeft.position, holdPos.position, Time.deltaTime * 0.1f);
                else if (gameObject.name == "Right")
                    gripRight.transform.position = Vector3.Lerp(gripRight.position, holdPos.position, Time.deltaTime * 0.1f);

            }
            else if(contactLeftState && contactRightState)
            {
                close = false;
                Debug.Log("??gripLeftStart " + gripLeft.position + "gripRightStart " + gripRight.position);
            }
        }

        if(open)
        {
            if (gameObject.name == "Left")
                gripLeft.transform.position = gripLeftStart;
            else if (gameObject.name == "Right")
                gripRight.transform.position = gripRightStart;
        }


        //else if(swichOnOff == 0 && gripLeft.position == gripLeftStart && gripRight.position == gripRightStart)
        //{

        //}
    }


    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("여기???");
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (gameObject.name == "Left")
            {
                contactLeftState = true;
                Debug.Log("Left잡다");
            }
            else if (gameObject.name == "Right")
            {
                contactRightState = true;
                Debug.Log("Right잡다");
            }
                
            
        }
    }

    //public void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Obstacle"))
    //    {
    //        Debug.Log("잡다");
    //        //other.transform.position = holdPos.position;
    //    }
    //}

    //public void OnTriggerExit(Collider other)
    //{
    //    if(other.CompareTag("Obstacle"))
    //    {

    //    }
    //}
}
