using UnityEngine;
using System.Collections;

public class Camera_Manager : Singleton<Camera_Manager>
{
    //Height/2
    private const float Camera_size = 3.84f;
    private const float Camera_X = -1.1f;
    public Sprite[] button;

    public void Camera_Init()
    {
        //camera 초기 위치.
        Camera.main.transform.position = new Vector3(-1.117f, -7.68f, -10);

        //Camera.ort((float) Screen.width / Screen.height);
        Camera.main.orthographic = true;
        Camera.main.orthographicSize = Camera_size;
        Application.targetFrameRate = 60;
    }

    public void Camera_Update()
    {
        Vector2 word_p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Ray2D ray = new Ray2D(word_p, Vector2.zero);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider.tag == "re_start")
                {
                    Application.LoadLevel("4_game");
                }
            }
            //on mouse Enter
            if (hit.collider.tag == "re_start")
            {
               hit.collider.transform.GetComponent<SpriteRenderer>().sprite = button[1];
            }
            else
            {
                if (GameObject.Find("re_start"))
                {
                    GameObject.Find("re_start").GetComponent<SpriteRenderer>().sprite = button[0];
                }
            }
        }
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
