using UnityEngine;
using System.Collections;

public class ride_car : MonoBehaviour {


	private float move_speed;

	public GameObject obj_character;
	private Character str_char;

	public bool	ride_on;

	public void ride_car_Init ()
	{
		//car set.

		ride_on = false;
		str_char = obj_character.GetComponent<Character> (); 
	}
	
	public void ride_car_Update ()
	{
			if (Input.GetKeyDown (KeyCode.LeftArrow))
			{
				Debug.Log ("왼쪽");
			}
			if (Input.GetKeyDown (KeyCode.RightArrow))
			{
				Debug.Log ("right");
			}
			if (Input.GetKeyDown (KeyCode.DownArrow))
			{
			}
			if (Input.GetKeyDown (KeyCode.UpArrow))
			{
			}
	}
}
		//transform.Translate (new Vector3 (directionX, directionY, 0) * Time.deltaTime * move_speed);
