using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopUpDownAxis : MonoBehaviour
{
    public static TopUpDownAxis instance { get; private set; }

    public Transform topRotAxis;
    public Transform palm;

    float rotX, palmX, _palmX;
    float timeLeft = 0.05f;
    float nextTime = 0f;

    public bool topMove;
    public float bottomY;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }


    void Start()
    {
        rotX = topRotAxis.rotation.x;
    }

    
    void Update()
    {
        //palmX = Mathf.Clamp(Mathf.DeltaAngle(0, palm.localEulerAngles.x), -36f, -5f);
        //Debug.Log("+++"+bottomY);

        //palmX = UnityEditor.TransformUtils.GetInspectorRotation(palm).x;

        //로봇 손 방향에 따른 헤드 꺾이는 값이 변경됨
        if (bottomY <= -0.4f)   //왼쪾
        {
            palmX = Mathf.Clamp(palmX, -65f, 40f);
            //Debug.Log(palmX);
            _palmX = Mathf.Lerp(-40f, 40f, Mathf.InverseLerp(-65f, -45f, palmX));
        }
        else if(bottomY > -0.5f && bottomY < 0.2f)  //정면
        {
            palmX = Mathf.Clamp(palmX, -40f, 40f);
            //Debug.Log(palmX);
            _palmX = Mathf.Lerp(-40f, 40f, Mathf.InverseLerp(-30f, -13f, palmX));
        }
        else if(bottomY >= 0.2f)    //오른쪽
        {
            palmX = Mathf.Clamp(palmX, -40f, 40f);
            //Debug.Log(palmX);
            _palmX = Mathf.Lerp(-40f, 40f, Mathf.InverseLerp(-25f, 9f, palmX));
        }
        
        

        if (Time.time > nextTime)
        {
            nextTime = Time.time + timeLeft;
            TopUpDownAxisMoving(_palmX);
        }
    }

    public void TopUpDownAxisMoving(float _move)
    {
        float newValue;
        newValue = rotX - _move;
        newValue = Mathf.Abs(newValue);
        //Debug.Log(newValue);
        if(newValue >= 8f)// && !FingerMoveCtrl.instance.fingerMove)
        {
            //if (DegreeRobotClass.GetSliderMove() == false)
            //{
                topMove = true;
                topRotAxis.Rotate(0f, -(_move - rotX), 0f);
                rotX = _move;
            //}
        }
        else
        {
            topMove = false;
        }

    }
}
