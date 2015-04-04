using UnityEngine;
using System.Collections;

public class Game_Manager : MonoBehaviour {

	public GameObject  Main_camera;

	public  GameObject obj_Player_ch;
	private Character  str_Character;

	public  GameObject obj_ride_car;
	private ride_car   str_ride_car;

	public GameObject obj_trash;

	private float      c_distance;//차까지의 거리.
	private float      t_distance;//쓰래기통의 거리.

	private bool       ride_car_update;
    private bool       shoot_;

    private Camera_Manager str_Camera_Mag;
    private Event_Manager  str_Event_Mag;

	void Start () 
	{
        str_Event_Mag = Event_Manager.Instance; //사용할때 변수 안쓰고 쓸라면 (Event_Manager.Instance).
        str_Camera_Mag = Camera_Manager.Instance;


        //타 클래스 초기화.
        str_Event_Mag.Event_Init();

        //메인 카메라 매니저 나올떄 수정예정.
		Main_camera.transform.position = new Vector3 (obj_Player_ch.transform.position.x,
		                                             obj_Player_ch.transform.position.y,
		                                             obj_Player_ch.transform.position.z - 10);

		ride_car_update = false;
        shoot_ = false;
		c_distance = .0f;
		t_distance = .0f;

		str_Character = obj_Player_ch.GetComponent<Character> ();
		str_ride_car = obj_ride_car.GetComponent<ride_car> ();

		//Init
		str_Character.Character_Init ();
		str_ride_car.ride_car_Init ();

		obj_trash.transform.position = new Vector3 (-3.764f, 1.493f, .0f);
	}
	
	void Update ()
	{
        //타 클래스 업데이트.
        str_Event_Mag.Event_Update();

		//플레이어 오브젝트와 자동차 오브젝트의 거리 계산.
		c_distance = Vector2.Distance(obj_Player_ch.transform.position,obj_ride_car.transform.position);
		//플레이어 오브젝트와 쓰래기통의 거리 계산.
		t_distance = Vector2.Distance (obj_Player_ch.transform.position, obj_trash.transform.position);

		//캐릭터가 ON일 경우.
		if (ride_car_update == false)
		{
			str_Character.Character_Update ();

			//camera set
            str_Camera_Mag.player_camera_set(str_Character.transform);

		}
		//자동차가 ON일 경우. //나중에 카메라 매니저 생성시 변경예정.
		if (ride_car_update == true)
		{
			str_ride_car.ride_car_Update ();

			//camera set
            str_Camera_Mag.player_camera_set(str_ride_car.transform);
		}

		if(Input.GetKeyDown(KeyCode.F))
		{
            if (t_distance <= 0.5f)
            {
                if (GameObject.Find("trash_con"))
                {
                    GameObject trash = GameObject.Find("trash_con");
                    Destroy(trash);
                    Debug.Log("쓰래기 버림");
                }
            }
            else
            {
                if (GameObject.Find("Player_ice_cream") && shoot_ == false)
                {
                    Debug.Log("아이스크림 먹음");
                    GameObject eat_ice = GameObject.Find("Player_ice_cream");
                    DestroyObject(eat_ice); //지금은 지워버리지만 텍스쳐를 변경해주는걸로.
                    shoot_ = true;
                    return;
                }
            }

            if (shoot_ == true)
            {
                Debug.Log("쓰래기던짐");
                //캐릭터가 보는 방향 뭐 dir 같은거 만들어서 그 위치로 가게끔 설정해주자.
            }

			if(ride_car_update == false)
			{
				//거리 1.0f보다 작을 경우.
				if(c_distance <= 0.5f)
				{
					obj_Player_ch.GetComponent<Renderer>().enabled = false;
					ride_car_update = true;
					return;
				}
			}
			else
			{
				obj_Player_ch.GetComponent<Renderer>().enabled = true;
				ride_car_update = false;
				return;
			}
		}
	}
}
