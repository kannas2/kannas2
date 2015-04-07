using UnityEngine;
using System.Collections;

public class Camera_Manager : Singleton<Camera_Manager>
{
    //Height/2
    private const float Camera_size = 3.84f;
    private const float Camera_X = -1.1f;

    public void Camera_Init()
    {
        //camera 초기 위치.
        Camera.main.transform.position = new Vector3(-1.1f, -7.68f, -10);

        //Camera.ort((float) Screen.width / Screen.height);
        Camera.main.orthographic = true;
        Camera.main.orthographicSize = Camera_size;
        Application.targetFrameRate = 60;
    }

    public void Camera_Update()
    {
    }

    //캐릭터와 자동차한테 transform 값을 받아와 카메라 셋팅.
    public void player_camera_set(Transform postion)
    {
        if (postion.position.y >= 7.54f) //y축 max
        {
            transform.position = new Vector3(Camera_X, 7.54f, postion.position.z - 10.0f);
        }
        else if (postion.position.y <= -7.6f) //y축 min
        {
            transform.position = new Vector3(Camera_X, -7.6f, postion.position.z - 10.0f);
        }
        else
            //캐릭터건 자동차건.
            transform.position = new Vector3(Camera_X, postion.position.y, postion.position.z - 10.0f);
    }
}
