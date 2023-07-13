using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterUpDownAxis : MonoBehaviour
{
    public static CenterUpDownAxis instance { get; private set; }


    public Transform centerRotAxis;
    public Transform forearm;   //팔
    public Transform palm;  //손

    float rotZ, forearmZ, _forearmZ;    //팔
    float rotPalmY, palmY, _palmY;    //손
    float timeLeft = 0.15f;
    float nextTime = 0f;
    public float newValueBttomUpDown;
    public float plamNewValue;
    public bool centerUpDownMove;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    void Start()
    {
        rotZ = centerRotAxis.localPosition.z;
        rotPalmY = forearm.localPosition.y * 100;

    }

    
    void Update()
    {
        forearmZ = Mathf.Clamp(forearm.localPosition.y * 100f, 0f, 90f);
        //Debug.Log(forearmZ);
        _forearmZ = Mathf.Lerp(0f, 90f, Mathf.InverseLerp(8f, 12f, forearmZ));

        palmY = palm.localPosition.y * 100;// Mathf.Clamp(forearm.localPosition.y * 100, -60f, 30f);
        //Debug.Log(palmY);
        _palmY = Mathf.Lerp(-60f, 30f, Mathf.InverseLerp(-11f, 6f, palmY));


        if (Time.time > nextTime)
        {
            nextTime = Time.time + timeLeft;
            CenterUpDownAxisMoving(_forearmZ, palmY);
        }
        
    }

    public void CenterUpDownAxisMoving(float _move, float _palmy)
    {
        ///팔 아래 위로 움직이기
        //팔 움직일 때 차이
        float newValue;
        newValue = rotZ - _move;
        newValue = Mathf.Abs(newValue);
        newValueBttomUpDown = newValue;
        //Debug.Log(newValue);

        //손 움직일 때 차이
        plamNewValue = rotPalmY - _palmy;
        plamNewValue = Mathf.Abs(plamNewValue);
        //Debug.Log(plamNewValue);

        //팔 움직이는 차이가 이보다 크고, 손 움직이는 차이가 이보다 클때
        if (newValue >= 3.0f)// && plamNewValue >= 2.5f)
        {
            if (DegreeRobotClass.GetSliderMove() == false)
            {
                centerUpDownMove = true;
                centerRotAxis.Rotate(0f, 0f, (_move - rotZ));
                rotZ = _move;
                rotPalmY = _palmy;
            }
        }
        else if(plamNewValue <= 1.2f)
        {
            centerUpDownMove = false;
        }

    }
}
