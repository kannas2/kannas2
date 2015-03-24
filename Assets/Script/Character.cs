using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
	
	public GameObject[] player_p;
	public GameObject[] player_p_dir;
	
	public  GameObject 	ice_cream;
	private int 		ice_cream_direction;

	public 	GameObject	obj_ride_car;
	private ride_car 	str_ride_car;

	protected Animator  animator;
	
	private float 		directionX = 0;
	private float		directionY = 0;
	
	private bool 		walking = false;
	public  bool 		char_on = true;
	
	public float		C_speed = 1.0f;


	public void	Character_Init ()
	{
		//Random p_num = new Random ();

		str_ride_car = obj_ride_car.GetComponent<ride_car> ();

		//player p and dir set
		for (int i=0; i<4; i++) 
		{
			player_p[i].renderer.enabled = false;
			
			player_p[i].transform.position = new Vector3(transform.position.x,
			                                             transform.position.y,
			                                             transform.position.z+1.0f);
			
			player_p_dir[i].transform.position = new Vector3(transform.position.x,
			                                                 transform.position.y,
			                                                 transform.position.z+1.0f);
		}
		
		//player p_dir set
		for(int i=0; i<4; i++)
		{
			float x_value = Random.Range(-1.0f,1.0f);
			float y_value = Random.Range(0.5f,1.0f);
			
			player_p_dir[i].transform.position = new Vector3(transform.position.x + x_value,
			                                                 transform.position.y + y_value,
			                                                 transform.position.z + 1.0f);
		}
		
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
			
			if (GameObject.Find ("Player_ice_cream")) {
				//ice cream postion update.
				ice_cream_ (ice_cream_direction);
				
				//Debug.Log (ice_cream.transform.position);
				
				//eat ice_cream;
				if (Input.GetKeyDown (KeyCode.F)) {
					DestroyObject (ice_cream);
				}
			}
		}
	}
	
	void ice_cream_(int position)
	{
		switch (position)
		{
			//right
		case 1: ice_cream.transform.position = new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z);
			break;
			//left
		case 2:	ice_cream.transform.position = new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z);
			break;
			//front
		case 3:	ice_cream.transform.position = new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z);
			break;
			//back
		case 4:	ice_cream.transform.position = new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z);
			break;
		}
	}
	
	void OnTriggerStay2D  (Collider2D coll)
	{
		if (coll.gameObject.tag == "ride_car") 
		{
			//Debug.Log("car");
			if(Input.GetKeyDown(KeyCode.F))
			{
				Debug.Log ("in the car");
				//char_on = false;
				//str_ride_car.ride_on = true;

				if(char_on == true)
				{
					if(GameObject.Find("Player"))
					{
						Debug.Log("들어오긴함");
						transform.renderer.enabled = false;
						char_on = false;
						//str_ride_car.ride_on = true;
						//캐릭터 움직임 꺼주고 자동차 클래스 on 해줘서 자동차가 움직이게.
					}
				}
			}
		}
	}
}