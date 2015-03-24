using UnityEngine;
using System.Collections;

public class Game_Manager : MonoBehaviour {

	public  GameObject obj_Player_ch;
	private Character  str_Character;

	public  GameObject obj_ride_car;
	private ride_car   str_ride_car;

	void Start () 
	{
		str_Character = obj_Player_ch.GetComponent<Character> ();
		str_ride_car = obj_ride_car.GetComponent<ride_car> ();

		//Init
		str_Character.Character_Init ();
		str_ride_car.ride_car_Init ();
	
	}
	
	void Update ()
	{
		str_Character.Character_Update ();

		if (str_Character.char_on == false)
		{
			str_ride_car.ride_car_Update ();

			if(Input.GetKeyDown(KeyCode.F))
			{

			}
		}
	}
}
