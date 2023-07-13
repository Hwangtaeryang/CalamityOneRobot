using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerMoveCtrl : MonoBehaviour
{
    public static FingerMoveCtrl instance { get; private set; }


    //시뮬레이터 움직일 집게손
    public Transform handFinger1;
    public Transform handFinger2;

    //움직일 값받아올 손가락
    public Transform finger1;
    public Transform finger2;

    //높낮이 확인하는 위치
    public Transform handPos;

    float firstFinger, rightFingerTr, _rightFingerTr, _leftFingerTr;
    public float bottomMove;
    public bool fingerMove;


    float timeLeft = 0.04f;
    float nextTime = 0f;
    public float bottomY;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    void Start()
    {
        firstFinger = handFinger1.transform.localPosition.x;
        firstFinger = handFinger1.rotation.x;
    }


    void Update()
    {
        //rightFingerTr = Vector3.Distance(finger1.transform.localPosition, finger2.transform.localPosition);
        //Debug.Log(rightFingerTr);
        //_rightFingerTr = Mathf.Lerp(0.035f, 0.016f, Mathf.InverseLerp(0.65f, 0.1f, rightFingerTr));
        //_leftFingerTr = Mathf.Lerp(-0.069f, -0.052f, Mathf.InverseLerp(0.65f, 0.1f, rightFingerTr));
        //Debug.Log("핸드위치 "+handPos.position.y);

        rightFingerTr = Vector3.Distance(finger1.transform.eulerAngles, finger2.transform.eulerAngles);
        //Debug.Log(bottomY + " 왼쪽 " + rightFingerTr + "손가락 " + finger1.transform.eulerAngles);

        _rightFingerTr = Mathf.Lerp(0.035f, 0.016f, Mathf.InverseLerp(330f, 130f, rightFingerTr));
        _leftFingerTr = Mathf.Lerp(-0.069f, -0.052f, Mathf.InverseLerp(330f, 130f, rightFingerTr));

        //로봇 팔이 외전하는 방향에 따라 값이 변경된다.
        if (bottomY <= -0.4f)
        {
            
        }

        //로봇 팔이 회전하는 방향에 따라 값이 변경된다.
        //if (bottomY <= -0.4f)   //왼쪾
        //{
        //    rightFingerTr = Vector3.Distance(finger1.transform.eulerAngles, finger2.transform.eulerAngles);

        //    //Debug.Log("왼쪽"+ rightFingerTr);
        //    _rightFingerTr = Mathf.Lerp(0.035f, 0.016f, Mathf.InverseLerp(295f, 315f, rightFingerTr));
        //    _leftFingerTr = Mathf.Lerp(-0.069f, -0.052f, Mathf.InverseLerp(295f, 315f, rightFingerTr));
        //}
        //else if (bottomY > -0.5f && bottomY < 0.2f)  //정면
        //{
        //    rightFingerTr = Vector3.Distance(finger1.transform.eulerAngles, finger2.transform.eulerAngles);

        //    //if (rightFingerTr <= 280)
        //    //    rightFingerTr = 280;
        //    //Debug.Log("중앙" + rightFingerTr);
        //    _rightFingerTr = Mathf.Lerp(0.035f, 0.016f, Mathf.InverseLerp(300f, 280f, rightFingerTr));
        //    _leftFingerTr = Mathf.Lerp(-0.069f, -0.052f, Mathf.InverseLerp(300f, 280f, rightFingerTr));
        //}
        //else if (bottomY >= 0.2f)    //오른쪽
        //{
        //    rightFingerTr = Vector3.Distance(finger1.transform.eulerAngles, finger2.transform.eulerAngles);

        //    //Debug.Log("오른쪽" + rightFingerTr);
        //    _rightFingerTr = Mathf.Lerp(0.035f, 0.016f, Mathf.InverseLerp(420, 330f, rightFingerTr));
        //    _leftFingerTr = Mathf.Lerp(-0.069f, -0.052f, Mathf.InverseLerp(420f, 330f, rightFingerTr));
        //}




        if (Time.time > nextTime)
        {
            nextTime = Time.time + timeLeft;
            FingerTransform(_rightFingerTr, _leftFingerTr);
        }
        
    }

    public void FingerTransform(float _rightmove, float _leftmove)
    {
        float newValue;
        newValue = firstFinger - _rightmove;
        newValue = Mathf.Abs(newValue * 1000);

        //Debug.Log((newValue) + " ::: " + bottomMove);
        //Debug.Log((firstFinger) + " ::: " + _rightmove + " :::: " + (firstFinger - _rightmove));
        if (newValue > 2.0f)//< 0.5f)//> 0.0003f)// && bottomMove < 0.01f)
        {
            fingerMove = true;
            handFinger1.localPosition = new Vector3(handFinger1.localPosition.x, handFinger1.localPosition.y, _rightmove);
            handFinger2.localPosition = new Vector3(handFinger2.localPosition.x, handFinger2.localPosition.y, _leftmove);
            firstFinger = _rightmove;
        }
        else
        {
            fingerMove = false;
            firstFinger = _rightmove;
        }

    }
}
