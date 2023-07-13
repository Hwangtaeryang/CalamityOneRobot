using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllHoldCtrl : MonoBehaviour
{
    public static AllHoldCtrl instance { get; private set; }

    public Transform gripLeft;
    public Transform gripRight;
    public Transform gripTarget;
    public Transform leftTarget;
    public Transform rightTarget;

    //Vector3 gripLeftStart, gripRightStart;
    bool close, open;


    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }

    void Start()
    {
        //gripLeftStart = gripLeft.position;
        //gripRightStart = gripRight.position;
    }

    
    void Update()
    {
        //집게 닫힘
        if (Input.GetKey(KeyCode.E))
        {
            Debug.Log("---");
            close = true;
        }
        //집게 열림
        else if (Input.GetKey(KeyCode.R))
        {
            open = true;
        }

        HandHoldMove();
    }

    void HandHoldMove()
    {
        //집게 닫힘 신호 
        if(close)
        {
            //Debug.Log("여기 들어옴");
            //집게가 사물에 접촉되지 않는 동안
            if(!LeftHoldCtrl.instance.contactLeftState || !RightHoldCtrl.instance.contactRightState)
            {
                //if (!LeftHoldCtrl.instance.contactLeftState)
                    gripLeft.transform.position = Vector3.Lerp(gripLeft.position, gripTarget.position, Time.deltaTime * 0.1f);
                //Debug.Log("gripLeft.transform.position : " + gripLeft.transform.position);
                //Debug.Log("gripLeft.transform.position x : " + gripLeft.transform.position.x);
                //Debug.Log("gripLeft.transform.position y : " + gripLeft.transform.position.y);
                //Debug.Log("gripLeft.transform.position z : " + gripLeft.transform.position.z);

                //if (!RightHoldCtrl.instance.contactRightState)
                gripRight.transform.position = Vector3.Lerp(gripRight.position, gripTarget.position, Time.deltaTime * 0.1f);
                //Debug.Log("gripRight.transform.position : " + gripRight.transform.position);
                //Debug.Log("gripRight.transform.position x : " + gripRight.transform.position.x);
                //Debug.Log("gripRight.transform.position y : " + gripRight.transform.position.y);
                //Debug.Log("gripRight.transform.position z : " + gripRight.transform.position.z);
            }
            
            else if(LeftHoldCtrl.instance.contactLeftState && RightHoldCtrl.instance.contactRightState)
            {
                Debug.Log("??????");
                close = false;
            }
            
        }

        //집게 열림 신호
        if(open)
        {
            gripLeft.transform.position = Vector3.Lerp(gripLeft.position, leftTarget.position, Time.deltaTime * 2.5f);
            gripRight.transform.position = Vector3.Lerp(gripRight.position, rightTarget.position, Time.deltaTime * 2.5f);

            //Debug.Log("gripLeft.transform.position : " + gripLeft.transform.position);
            //Debug.Log("2gripLeft.transform.position x: " + gripLeft.transform.position.x);
            //Debug.Log("2gripLeft.transform.position y: " + gripLeft.transform.position.y);
            //Debug.Log("2gripLeft.transform.position z: " + gripLeft.transform.position.z);

            LeftHoldCtrl.instance.contactLeftState = false;
            RightHoldCtrl.instance.contactRightState = false;

            close = false;
            //open = false;
            StartCoroutine(OpenTime());
        }
    }

    IEnumerator OpenTime()
    {
        yield return new WaitForSeconds(1f);
        open = false;
    }
}
