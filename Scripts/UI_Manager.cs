using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance { get; private set; }

    public GameObject tank; //로봇
    public GameObject[] fire;   //불 위치
    public GameObject water;    //물 파티클
    public GameObject leapMotionCtrl;   //일시정지를 위한 립모션 컨트롤러
    public GameObject handPalm; //초기 위치를 위한 IK의 타켓
    public GameObject robotcamera;   //카메라
    public GameObject[] battery;    //밧데리
    public GameObject[] viewBattery;    //뷰밧데리
    public GameObject simulationViewOut;    //시뮬레이션뷰 화면 안보이게하는 뷰
    public GameObject realViewOut;  //리얼뷰 화면 안보이게하는 뷰

    public Text distanceText;   //거리 텍스트
    public Text resistanceText; //온도 텍스트
    public Text cameraNumberText;   //카메라번호 텍스트
    public Text batteryText;    //밧데리 텍스트

    public Toggle simulationToggle; //시뮬레이션뷰토글
    public Toggle realToggle;   //리얼뷰토글

    public Transform[] cameraPoint; //카메라 이동 포인트

    public bool realViewState;  //리얼뷰 화면 on/off상태

    bool waterCome; //물 나오는지 여부
    bool pauseState; //일시정지

    Vector3 palmStart;  //손 처음 위치
    Vector3 startPos;   //처음 위치(과거 위치)
    float disSub, disTotal; //이동거리, 총이동거리
    float fireDis;  //불과의 거리
    float baseResistance = 26;   //기본 온도
    float disStart; //움직인 거리(전)
    float batteryTime;  //밧데리 소모시간 현재
    float batteryStartTime = 60f;    //밧데리 소모시간

    int cameraNumber = 1;   //카메라번호



    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }

    void Start()
    {
        batteryTime = batteryStartTime; //밧데리 소모 시간 초기화
        realViewState = true;   //리얼뷰 토글 오픈(화면을 보이게한다)
        cameraNumberText.text = "1번 카메라";
        palmStart = handPalm.transform.localPosition;   //손 위치
        //Debug.Log(handPalm.transform.localPosition);
        startPos = tank.transform.position; //로봇 처음 위치 저장
        resistanceText.text = baseResistance.ToString();

        water.SetActive(false);
        StartCoroutine(BatteryUse());
    }

    
    void Update()
    {
        FireDistanceTemperature();
        
    }

    private void FixedUpdate()
    {
        //이동거리 측정 조건
        //처음위치와 현재 위치가 다를경우(움직였다는 조건)
        if(startPos != tank.transform.position)
        {
            //현재 움직인 거리와 과거 위치 거리를 측정
            disSub = Vector3.Distance(startPos, tank.transform.position);
            
            disTotal += disSub; //거리총 값에 위에서 구한 거리를 더해줌
            //Debug.Log(disTotal);
            distanceText.text = (Mathf.FloorToInt(disTotal*0.1f)).ToString();
            //Debug.Log(Mathf.FloorToInt(disTotal));
            startPos = tank.transform.position; //현재 위치를 처음위치 변수값에 넣어줌.
        }


        //키 눌렀을 때 이벤트 처리
        //물 나오는 이벤트
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            ComForwardWater();
        }

        //일시정지 이벤트
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            RobotPause();
        }

        //카메라 이동 이벤트 
        CameraViewChangeKey();

        //초기화 모드 이벤트
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StandbyMode();
        }
    }

    //불 거리를 측정해서 온도를 보내는 함수
    void FireDistanceTemperature()
    {
        float closestDistSqr = Mathf.Infinity;//Mathf.Infinity 무한한 양의 값(거리)
        Transform closeFire = null;
        bool upStart;

        foreach (GameObject firetarget in fire)
        {
            Vector3 objPos = firetarget.transform.position;
            fireDis = (objPos - tank.transform.position).sqrMagnitude;

            //Debug.Log("fireDis " + fireDis);
            //특정 거리 안으로 들어올때(온도를 변화를 위한 조건식)
            if (fireDis < 166f && fireDis >= 20f)
            {
                //Debug.Log("fireDis " + fireDis + " closestDistSqr " + closestDistSqr);
                //그 거리가 제곱한 최단 거리보다 작으면
                if (fireDis < closestDistSqr)
                {
                    //Debug.Log("fireDis " + Mathf.FloorToInt(fireDis));
                    closestDistSqr = fireDis;
                    closeFire = firetarget.transform;

                    //로봇 움직임이 있으면
                    if (disStart != Mathf.FloorToInt(fireDis))
                    {
                        if (Mathf.FloorToInt(fireDis) % 1 == 0)
                        {
                            //불에 가까워 질수록 온도는 올라간다.
                            baseResistance += 1;
                            resistanceText.text = baseResistance.ToString();
                        }
                        disStart = Mathf.FloorToInt(fireDis);   //움직이기 전 위치에 움직인 위치를 넘겨준다.
                    }
                }
            }

        }
    }

    //물 나오는 버튼 눌렀을 때 
    public void ComForwardWater()
    {
        //Debug.Log("물");
        if(!waterCome)
        {
            //Debug.Log("물");
            //물 나온다
            waterCome = true;
        }
        else
        {
            //안나온다
            waterCome = false;
        }

        //물 나온다
        if(waterCome)
        {
            
            water.SetActive(true);
            //Debug.Log("물-_-" + water.activeSelf);
        }
        else
        {
            //안나온다
            water.SetActive(false);
        }
    }

    //일시정지
    public void RobotPause()
    {
        if(!pauseState)
        {
            pauseState = true;
        }
        else
        {
            pauseState = false;
        }

        if(pauseState)
        {
            leapMotionCtrl.SetActive(false);    //모션 인식을 하지 못하기 
            Time.timeScale = 0; //일시정지
        }
        else
        {
            Time.timeScale = 1; //일시정지 해제
            leapMotionCtrl.SetActive(true);
        }
    }

    //초기화모드 버튼 눌렀을 때
    public void StandbyMode()
    {
        handPalm.transform.localPosition = palmStart;
    }

    //카메라 이동(키 클릭)
    void CameraViewChangeKey()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            CameraViewChange();
        }
    }

    //카메라 이동(버튼클릭)
    public void CameraViewChange()
    {
        //누를때마다 카메라 번호가 바껴야한다.
        if (cameraNumber == 1)
            cameraNumber = 2;
        else if (cameraNumber == 2)
            cameraNumber = 3;
        else if (cameraNumber == 3)
            cameraNumber = 1;

        if(cameraNumber == 1 )
        {
            robotcamera.transform.position = cameraPoint[0].position;
            cameraNumberText.text = "1번 카메라";
        }
        else if(cameraNumber == 2 )
        {
            robotcamera.transform.position = cameraPoint[1].position;
            cameraNumberText.text = "2번 카메라";
            //robotcamera.transform.parent = cameraPoint[1];
        }
        else if(cameraNumber == 3 )
        {
            robotcamera.transform.position = cameraPoint[2].position;
            cameraNumberText.text = "3번 카메라";
        }
    }

    //밧데리
    IEnumerator BatteryUse()
    {
        while(batteryTime > 0)
        {
            batteryTime -= Time.deltaTime;
            //Debug.Log(batteryTime);
            yield return new WaitForEndOfFrame();
        }
        batteryTime = 0f;
        if (battery[0].activeSelf == true)
        {
            battery[0].SetActive(false);
            viewBattery[0].SetActive(false);
            batteryText.text = "80%";
            batteryTime = batteryStartTime;
        }
        else if(battery[1].activeSelf == true)
        {
            battery[1].SetActive(false);
            viewBattery[1].SetActive(false);
            batteryText.text = "60%";
            batteryTime = batteryStartTime;
        }
        else if (battery[2].activeSelf == true)
        {
            battery[2].SetActive(false);
            viewBattery[2].SetActive(false);
            batteryText.text = "40%";
            batteryTime = batteryStartTime;
        }
        else if (battery[3].activeSelf == true)
        {
            battery[3].SetActive(false);
            viewBattery[3].SetActive(false);
            batteryText.text = "20%";
            batteryTime = batteryStartTime;
        }
        else if (battery[4].activeSelf == true)
        {
            battery[4].SetActive(false);
            viewBattery[4].SetActive(false);
            batteryText.text = "0%";
            batteryTime = batteryStartTime;
        }
        if(battery[4].activeSelf != false)
            StartCoroutine(BatteryUse());
    }

    //시뮬레이션뷰토글
    public void SimulationToggle()
    {
        if(simulationToggle.isOn)
        {
            simulationViewOut.SetActive(false);
            cameraNumberText.gameObject.SetActive(true);
        }
        else
        {
            simulationViewOut.SetActive(true);
            cameraNumberText.gameObject.SetActive(false);
        }
    }

    //리얼뷰토글
    public void RealViewToggle()
    {
        if(realToggle.isOn)
        {
            realViewOut.SetActive(false);
        }
        else
        {
            realViewOut.SetActive(true);
        }
    }


    private void OnParticleTrigger()
    {
        
    }

    //IEnumerator UpResistance(int maxResistance) n 
    //{
    //    while(baseResistance <= maxResistance)
    //    {
    //        baseResistance += 1;
    //        resistanceText.text = baseResistance.ToString();
    //        yield return new WaitForSeconds(0.5f);
    //    } 
    //}

    void UpResistance(float maxResistance)
    {
        //if(maxResistance)
        {

        }
    }
}
