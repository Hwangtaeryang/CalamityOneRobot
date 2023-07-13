using Leap.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class OneArmTransform : MonoBehaviour
{
    public GameObject LoPolyHandRight;
    private bool isSave_init_rate;   // 현재 각도 비율 저장 초기화시

    [Header("ID 11")]
    public Transform ID11;
    //public float ID11_Angle;      // 비율에 따른 값
    //private float ID11_prev_rate;  // 이전 각도 비율
    public float ID11_goal_rate;     // 현재 각도 비율

    [Header("ID 12")]
    public Transform ID12;
    //public float ID12_Angle;

    //private float ID12_prev_rate;
    public float ID12_goal_rate;
    //public Transform R_forearm_rot;    // 팔 Rotation 값 가져오기
    //private float x_R_forearm_rot;     // float 값으로 전달
    //public float _x_R_forearm_rot;     // float 값으로 전달

    //private float x_R_prev_forearm_rot_rate;
    //private float x_R_goal_forearm_rot_rate;


    [Header("ID 13")]
    //public float _z_R_forearm_pos;     // float 값으로 전달
    //private float z_R_forearm_pos;     // float 값으로 전달

    //private float z_R_prev_forearm_rate;  // 이전 각도 비율
    //private float _z_R_goal_forearm_rate;     // 현재 각도 비율
    public Transform ID13;
    //public float ID13_Angle;     // float 값으로 전달
    //private float ID13_prev_rate;
    public float ID13_goal_rate;

    [Header("ID 14")]
    public Transform palm;
    //public float ID14_Angle;     // float 값으로 전달
    private float ID14_data;
    //private float ID14_prev_rate;  // 이전 각도 비율
    public float ID14_goal_rate;     // 현재 각도 비율

    [Header("ID 15")]
    //public Transform ID15;
    public Transform left;    // 엄지
    public Transform right;    // 중지
    private float finger_distance;     // float 값으로 전달
    //public float _finger_distance;     // float 값으로 전달
    public float ID15_goal_rate;
    //private float R_prev_finger_rate;  // 이전 각도 비율
    //private float R_goal_finger_rate;     // 현재 각도 비율
    public static OneArmTransform Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else
            Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Repeat_per_cycle", 0.1f);   // 모터의 주기를 100ms or 200ms 주기위한 코루틴 함수
    }

    IEnumerator Repeat_per_cycle(float sec)
    {
        WaitForSeconds ws = new WaitForSeconds(sec);

        while (true)
        {
            if (LoPolyHandRight.activeSelf)
            {
                #region Dynamixel ID 11
                /// <summary>
                /// 0 ~2048 ~4095
                /// 2048 디폴트
                /// UnityEditor.TransformUtils.GetInspectorRotation(ID11).y)
                /// </summary>
                ID11_goal_rate = (float)Mathf.InverseLerp(-90f, 90f, UnityEditor.TransformUtils.GetInspectorRotation(ID11).y);   // 목표위치 비율 설정

                #endregion

                #region Dynamixel ID 12
                /// <summary>
                /// 700 ~ 1800 ~ 3400
                /// 1800 디폴트
                /// </summary>

                ID12_goal_rate = (float)Mathf.InverseLerp(10f, -180f, UnityEditor.TransformUtils.GetInspectorRotation(ID12).x);   // 목표위치 비율 설정

                #endregion

                #region Dynamixel ID 13
                /// <summary>
                /// 700 ~ 1800 ~ 3400
                /// 1800 디폴트
                /// </summary>
                ID13_goal_rate = (float)Mathf.InverseLerp(100f, 0f, UnityEditor.TransformUtils.GetInspectorRotation(ID13).x);   // 목표위치 비율 설정

                #endregion

                #region Dynamixel ID 14

                /// <summary>
                /// 638 ~ 2676 ~ 3253
                /// 1748 디폴트
                /// </summary>

                ID14_data = Mathf.Clamp(Mathf.DeltaAngle(0, -palm.localEulerAngles.x), -50f, 40f);
                ID14_goal_rate = (float)Mathf.InverseLerp(40, -50f, ID14_data);

                #endregion

                #region Dynamixel ID 15
                /// <summary>
                /// 1460(닫음) ~ 2817(열림)
                /// 2817 디폴트
                /// </summary>

                //Debug.Log("left.position : " + left.position.x);
                //Debug.Log("right.position : " + right.position.x);

                finger_distance = (left.localPosition - right.localPosition).magnitude; // 손가락 사이 거리 계산
                //finger_distance = Mathf.Clamp(finger_distance * 100f, 4.5f, 22f);
                //Debug.Log("left x : " + left.localPosition.x);
                //Debug.Log("right x : " + right.localPosition.x);
                //Debug.Log("finger_distance : " + finger_distance);
                ID15_goal_rate = (float)Mathf.InverseLerp(0.067f, 0.1092f, finger_distance);

                //Debug.Log("_finger_distance : " + _finger_distance);
                //Debug.Log("R_goal_finger_rate : " + R_goal_finger_rate);
                //Debug.Log("R_prev_finger_rate : " + R_prev_finger_rate);

                //if (isSave_init_rate)
                //{
                //    R_prev_finger_rate = R_goal_finger_rate;    // 현재 위치 비율저장 : 한번만 쓰임
                //}

                ////Debug.Log("x_R_goal_Palm_rate 15 : " + R_goal_finger_rate);   // 모터각도 목표위치 저장 );
                ////Debug.Log("x_R_prev_Palm_rate 15 : " + R_prev_finger_rate);   // 모터각도 목표위치 저장 );

                //if (Mathf.Abs(Mathf.Abs(R_goal_finger_rate) - Mathf.Abs(R_prev_finger_rate)) > 0.035f || isSave_init_rate)
                //{
                //    _finger_distance = Mathf.Lerp(1460f, 2817f, R_goal_finger_rate);   // 모터각도 목표위치 저장 
                //    R_prev_finger_rate = R_goal_finger_rate;    // 현재 위치 저장
                //    Debug.Log("result 15 : " + _finger_distance);   // 모터각도 목표위치 저장 );
                //}
                #endregion

                yield return ws;    // 위에서 주어진 new WaitForSeconds(sec) 초 만큼 지연시킴

            }
            yield return null;
        }
    }
}