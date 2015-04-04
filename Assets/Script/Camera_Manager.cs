using UnityEngine;
using System.Collections;

public class Camera_Manager : Singleton<Camera_Manager>
{
    //Height/2
    private const float Camera_size = 3.84f;

    public void Camera_Init()
    {
        Camera.main.orthographic = true;
        Camera.main.orthographicSize = Camera_size;
        Application.targetFrameRate = 60;

        //camera_set 캐릭터로부터 transform을 받아와 카메라에 셋팅.
        
    }
	
	public void Camera_Update () 
    {
	}


    public void player_camera_set(Transform postion)
    {

        //캐릭터건 자동차건.
        transform.position = new Vector3(postion.position.x,
                                         postion.position.y,
                                         postion.position.z - 10.0f);
    }
}
