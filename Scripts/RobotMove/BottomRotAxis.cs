using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomRotAxis : MonoBehaviour
{
    public Transform bottomBaseRotAxis;
    public Transform forearm;   //팔(첫번째 관절 좌우)
    public Transform palm;  //손(두번째 관절 앞뒤로)

    //public Slider bottomRotSlider;

    float rotZ, forearmX, _forearmX;
    float rotPalmZ, palmX, _palmX;
    float timeLeft = 0.12f;
    float nextTime = 0f;


    void Start()
    {
        rotZ = bottomBaseRotAxis.rotation.z;
        rotPalmZ = -palm.localPosition.z * 100;
    }

    
    void Update()
    {
        //x = forearm.localEulerAngles.x;
        forearmX = Mathf.Clamp(forearm.localPosition.x * 100f, -100f, 100f);
        //Debug.Log(forearmX);
        _forearmX = Mathf.Lerp(-100f, 100f, Mathf.InverseLerp(-1f, 10f, forearmX)); //왼-1,20

        //if (DegreeRobotClass.GetSliderMove())
        //{
        //    bottomRotSlider.value = DegreeRobotClass.GetAxisAngle1();
        //}

        if(Time.time > nextTime)
        {
            nextTime = Time.time + timeLeft;
            //Debug.Log("시간");
            BottomRotAxisMoving(_forearmX);
        }
        
    }


    public void BottomRotAxisMoving(float _move)
    {
        float newValue;
        newValue = rotZ - _move;

        newValue = Mathf.Abs(newValue);
        //Debug.Log(newValue);

        if(newValue >= 3.0f && !BottomUpDownAxis.instance.bottomUpDownMove)
        {
            if (DegreeRobotClass.GetSliderMove() == false)
            {
                bottomBaseRotAxis.Rotate(0f, 0f, (_move - rotZ));
                //bottomBaseRotAxis.rotation = Quaternion.Euler(0f, 0f, -_move);
                rotZ = _move;
            }
        }
    }

}
