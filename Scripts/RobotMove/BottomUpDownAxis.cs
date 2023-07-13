using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomUpDownAxis : MonoBehaviour
{
    public static BottomUpDownAxis instance { get; private set; }

    public Transform bottomRotAxis;
    public Transform forearm;

    float rotZ, forearmZ, _forearmZ;
    float timeLeft = 0.15f;
    float nextTime = 0f;

    public bool bottomUpDownMove;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else instance = this;
    }

    void Start()
    {
        rotZ = bottomRotAxis.localPosition.z;
    }

    
    void Update()
    {
        //팔뚝 핸드 사용 값
        //forearmZ = Mathf.Clamp(forearm.localPosition.z * 100, -60f, 30f);
        //Debug.Log(forearmZ);
        //_forearmZ = Mathf.Lerp(-60f, 30f, Mathf.InverseLerp(-30f, 2f, forearmZ));

        //손 사용 값
        forearmZ = Mathf.Clamp(-forearm.localPosition.z * 100, -35f, 40f);
        //Debug.Log(forearmZ);
        _forearmZ = Mathf.Lerp(-35f, 40f, Mathf.InverseLerp(-5f, 10f, forearmZ)); //왼 -11, 6

        if (Time.time > nextTime)
        {
            nextTime = Time.time + timeLeft;
            BottomUpDownAxisMoving(_forearmZ);
        }
    }

    public void BottomUpDownAxisMoving(float _move)
    {
        
        float newValue;
        newValue = rotZ - _move;

        newValue = Mathf.Abs(newValue);
        Debug.Log(newValue + ":::");

        
        if(newValue >= 4.5f)// && CenterUpDownAxis.instance.centerUpDownMove == false)// && CenterUpDownAxis.instance.plamNewValue <= 5.0f)
        {
            if (DegreeRobotClass.GetSliderMove() == false)
            {
                bottomUpDownMove = true;
                bottomRotAxis.Rotate(0f, 0f, -(rotZ - _move));
                rotZ = _move;
            }
        }
        else
        {
            bottomUpDownMove = false;
        }
    }

}
