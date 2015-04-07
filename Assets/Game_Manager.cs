using UnityEngine;
using System.Collections;

public class Game_Manager : MonoBehaviour {

	public  GameObject obj_Player_ch;
	private Character  str_Character;

	public  GameObject obj_ride_car;
	private ride_car   str_ride_car;

	private float      m_distance;
	private bool       ride_car_update;

	void Start () 
	{
		ride_car_update = false;
		m_distance = .0f;

		str_Character = obj_Player_ch.GetComponent<Character> ();
		str_ride_car = obj_ride_car.GetComponent<ride_car> ();

		//Init
		str_Character.Character_Init ();
		str_ride_car.ride_car_Init ();
	
	}
	
	void Update ()
	{
		m_distance = Vector2.Distance(obj_Player_ch.transform.position,obj_ride_car.transform.position);

		if (ride_car_update == false)
		{
			str_Character.Character_Update ();
		}
		if (ride_car_update == true)
		{
			str_ride_car.ride_car_Update ();
		}

		if(Input.GetKeyDown(KeyCode.F))
		{
			if(ride_car_update == false)
			{
				if(m_distance < 1.0f)
				{
					obj_Player_ch.GetComponent<Renderer>().enabled = false;
					ride_car_update = true;
					return;
				}
			}
			if(ride_car_update == true)
			{
				obj_Player_ch.GetComponent<Renderer>().enabled = true;
				ride_car_update = false;
				return;
			}
		}
	}
}
