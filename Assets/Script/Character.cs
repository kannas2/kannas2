using UnityEngine;
using System.Collections;

public class Character : Singleton<Character>
{
	public  GameObject 	ice_cream;
	private int 		ice_cream_direction;

	protected Animator  animator;
	
	private float 		directionX = 0;
	private float		directionY = 0;
	
	private bool 		walking = false;
	public  bool 		char_on = true;
	
	public  float		C_speed = 5.0f;

    private Event_Manager str_Event;

	public void	Character_Init ()
	{
        str_Event = Event_Manager.Instance;

		//character start postion
		//transform.position = new Vector3 (2.18f, -6.64f, .0f);

		animator = GetComponent<Animator>();
	}
	
	public void Character_Update () 
	{
		if (char_on == true) {
			float h = Input.GetAxisRaw ("Horizontal");
			float v = Input.GetAxisRaw ("Vertical");
			
			walking = true;
			
			//right
			if (h > 0) {
				directionX = 1;
				directionY = 0;
				ice_cream_direction = 1;
			}//left
			else if (h < 0) {
				directionX = -1;
				directionY = 0;
				ice_cream_direction = 2;
			}//front 
			else if (v > 0) {
				directionX = 0;
				directionY = 1;
				ice_cream_direction = 3;
			}//back 
			else if (v < 0) {
				directionX = 0;
				directionY = -1;
				ice_cream_direction = 4;
			} else {
				walking = false;
			}
			if (walking) {
				transform.Translate (new Vector3 (directionX, directionY, 0) * Time.deltaTime * C_speed);
			}
			animator.SetFloat ("DirectionX", directionX);
			animator.SetFloat ("DirectionY", directionY);
			animator.SetBool ("Walking", walking);
			
			if (GameObject.Find ("Player_ice_cream")) 
			{
				//ice cream postion update.
				//ice_cream_ (ice_cream_direction);
			}
		}
        str_Event.character_event(Event_Manager.Event_.trash);
	}
	/*
	 *함수로 빼지말고 그냥 방향키마다 포지션값 주면 되는거아닌가...
	 *아이스크림이 없어졌을 경우 때문에 함수로 따로 빼놓음.
	void ice_cream_(int position)
	{
		switch (position)
		{
			//right
		case 1: ice_cream.transform.position = new Vector3(transform.position.x, transform.position.y - 0.07f , transform.position.z - 1.0f);
			break;
			//left
		case 2:	ice_cream.transform.position = new Vector3(transform.position.x, transform.position.y- 0.07f, transform.position.z-1.0f);
			break;
			//front
		case 3:	ice_cream.transform.position = new Vector3(transform.position.x + 0.095f, transform.position.y-0.07f, transform.position.z);
			break;
			//back
		case 4:	ice_cream.transform.position = new Vector3(transform.position.x -0.093f, transform.position.y-0.07f, transform.position.z-1.0f);
			break;
		}
	}
    */
}


