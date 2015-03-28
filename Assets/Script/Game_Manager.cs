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

	void Start () 
	{
		Main_camera.transform.position = new Vector3 (obj_Player_ch.transform.position.x,
		                                             obj_Player_ch.transform.position.y,
		                                             obj_Player_ch.transform.position.z - 10);

		ride_car_update = false;
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
		//플레이어 오브젝트와 자동차 오브젝트의 거리 계산.
		c_distance = Vector2.Distance(obj_Player_ch.transform.position,obj_ride_car.transform.position);
		//플레이어 오브젝트와 쓰래기통의 거리 계산.
		t_distance = Vector2.Distance (obj_Player_ch.transform.position, obj_trash.transform.position);

		//캐릭터가 ON일 경우.
		if (ride_car_update == false)
		{
			str_Character.Character_Update ();

			//camera set
			Main_camera.transform.position = new Vector3(str_Character.transform.position.x,
			                                             str_Character.transform.position.y,
			                                             str_Character.transform.position.z - 10);
		}
		//자동차가 ON일 경우.
		if (ride_car_update == true)
		{
			str_ride_car.ride_car_Update ();

			//camera set
			Main_camera.transform.position = new Vector3(str_ride_car.transform.position.x,
			                                             str_ride_car.transform.position.y,
			                                             str_ride_car.transform.position.z - 10);
		}

		if(Input.GetKeyDown(KeyCode.F))
		{
			if(t_distance <= 0.5f)
			{
				if(GameObject.Find("trash_con"))
				{
					GameObject trash = GameObject.Find("trash_con");
					Destroy(trash);
					Debug.Log("쓰래기 버림");
				}
			}
			else
			{
				str_Character.eat_ice = true;
			}

			if(ride_car_update == false)
			{
				//거리 1.0f보다 작을 경우.
				if(c_distance <= 0.5f)
				{
					obj_Player_ch.GetComponent<Renderer>().enabled = false;
					ride_car_update = true;
					str_Character.eat_ice = false;
					return;
				}
			}
			else
			{
				obj_Player_ch.GetComponent<Renderer>().enabled = true;
				ride_car_update = false;
				str_Character.eat_ice = true;
				return;
			}
		}
	}
}
