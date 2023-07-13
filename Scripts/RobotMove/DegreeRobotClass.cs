
public class DegreeRobotClass 
{
    static bool sliderMove = false;
    static float axisAngle1, axisAngle2, axisAngle3, axisAngle4, axisAngle5;



    //슬라이더 움직이여 여부 Get/Set
    public static bool GetSliderMove()
    {
        return sliderMove;
    }

    //슬라이더 자동으로 이동하는지 여부
    public static void SetSliderMove(bool sliderbool)
    {
        sliderMove = sliderbool;
    }

    //각 포인터가 움직인 각도를 구하기 위한 메서드 Set/Get
    public static float GetAxisAngle1()
    {
        return axisAngle1;
    }

    public static void SetAxisAngle1(float _angle)
    {
        axisAngle1 = _angle;
    }

    public static float GetAxisAngle2()
    {
        return axisAngle2;
    }

    public static void SetAxisAngle2(float _angle)
    {
        axisAngle2 = _angle;
    }

    public static float GetAxisAngle3()
    {
        return axisAngle3;
    }

    public static void SetAxisAngle3(float _angle)
    {
        axisAngle3 = _angle;
    }

    public static float GetAxisAngle4()
    {
        return axisAngle4;
    }

    public static void SetAxisAngle4(float _angle)
    {
        axisAngle4 = _angle;
    }

    public static float GetAxisAngle5()
    {
        return axisAngle5;
    }

    public static void SetAxisAngle5(float _angle)
    {
        axisAngle5 = _angle;
    }
}
