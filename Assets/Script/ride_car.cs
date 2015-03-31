﻿using UnityEngine;
using System.Collections;

public class ride_car : MonoBehaviour {

	private float      move_speed;
	public  GameObject obj_character;

	private float      directionX;
	private float      directionY;

	//	private Character str_char;
	public bool	ride_on;

	public void ride_car_Init ()
	{
		directionX = .0f;
		directionY = .0f;

		//car set.
		transform.position = new Vector3 (-0.58f, -6.61f, .0f);

		move_speed = 3.0f;
		ride_on = false;
//		str_char = obj_character.GetComponent<Character> (); 
	}
	
	public void ride_car_Update ()
	{
		if (Input.GetKeyDown (KeyCode.LeftArrow))
		{
			transform.rotation = Quaternion.Euler(.0f,.0f,90.0f);;
			directionX = 0;
			directionY = 1;
			//directionX -= 1;
			//directionY = 0;
		}
		if (Input.GetKeyDown (KeyCode.RightArrow))
		{
			transform.rotation =  Quaternion.Euler(.0f,.0f,-90.0f);
			directionX = 0;
			directionY = 1;
			//directionX += 1;
			//directionY = 0;
		}
		if (Input.GetKeyDown (KeyCode.DownArrow))
		{
			transform.rotation =  Quaternion.Euler(.0f,.0f,180.0f);

			directionX = 0;
			directionY = 1;
			//directionX = 0;
			//directionY -= 1;
		}
		if (Input.GetKeyDown (KeyCode.UpArrow))
		{
			transform.rotation =  Quaternion.Euler(.0f,.0f,.0f);
			directionX = 0;
			directionY = 1;
		}
			
		Debug.Log ("X"+directionX);
		Debug.Log ("y"+directionY);
		transform.Translate (new Vector3 (directionX, directionY, 0) * Time.deltaTime * move_speed);
	}
}