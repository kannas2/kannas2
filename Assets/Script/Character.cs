using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
	
	public GameObject[] player_p;
	public GameObject[] player_p_dir;
	
	public  GameObject 	ice_cream;
	private int 		ice_cream_direction;

	protected Animator  animator;
	
	private float 		directionX = 0;
	private float		directionY = 0;
	
	private bool 		walking = false;
	public  bool 		char_on = true;
	public 	bool		eat_ice = false;
	
	public  float		C_speed = 5.0f;
	public  bool		dir_p_set;

	//파편 방향과 거리.
	private float[]     dis_p = new float[4];
	private Vector3[]   dir_p = new Vector3[4];

	public void	Character_Init ()
	{
		dir_p_set = false;

		//character set postion
		//transform.position = new Vector3 (2.18f, -6.64f, .0f);

		//player p and dir set
		for (int i=0; i<4; i++) 
		{
			player_p[i].GetComponent<Renderer>().enabled = false;
			
			player_p[i].transform.position = new Vector3(transform.position.x,
			                                             transform.position.y,
			                                             transform.position.z+1.0f);
			
			player_p_dir[i].transform.position = new Vector3(transform.position.x,
			                                                 transform.position.y,
			                                                 transform.position.z+1.0f);
		}
		
		//player p_dir set
		/* 함수로 빼놨음.
		for(int i=0; i<4; i++)
		{
			float x_value = Random.Range(-0.7f,0.7f);
			float y_value = Random.Range(0.5f,2.0f);
			
			player_p_dir[i].transform.position = new Vector3(transform.position.x + x_value,
			                                                 transform.position.y + y_value,
			                                                 transform.position.z + 1.0f);
		}
		*/
		/*
		for (int i=0; i<4; i++) 
		{	//player p - dir.
			dis_p[i] = Vector2.Distance(player_p_dir[i].transform.position, player_p[i].transform.position);
			dir_p[i] = (player_p_dir[i].transform.position - player_p[i].transform.position).normalized;
		}
		*/

		animator = GetComponent<Animator>();
	}
	
	public void Character_Update () 
	{
		for (int i=0; i<4; i++)
		{
			dis_p [i] = Vector2.Distance (player_p_dir [i].transform.position, player_p [i].transform.position);
			dir_p [i] = (player_p_dir [i].transform.position - player_p [i].transform.position).normalized;
		}

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
				ice_cream_ (ice_cream_direction);
				

					if(eat_ice == true)
					{
						Debug.Log("아이스크림 먹음");
						DestroyObject (ice_cream);
					}
				}
		}
		//test;
		if (dir_p_set == false) {
			dir_set (2);
		}

		dir_character_p ();
	}
	/*
	 *함수로 빼지말고 그냥 방향키마다 포지션값 주면 되는거아닌가...
	 *아이스크림이 없어졌을 경우 때문에 함수로 따로 빼놓음.
	 */
	void ice_cream_(int position)
	{
		switch (position)
		{
			//right
		case 1: ice_cream.transform.position = new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z);
			break;
			//left
		case 2:	ice_cream.transform.position = new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z);
			break;
			//front
		case 3:	ice_cream.transform.position = new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z);
			break;
			//back
		case 4:	ice_cream.transform.position = new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z);
			break;
		}
	}

	void dir_set(int num)
	{
		float x_value = .0f;
		float y_value = .0f;

		//캐릭터 파편 위치 set
		switch(num)
		{
		case 1:
			//y만 랜덤이고 x는 일정 간격.
			x_value = -1.0f;
			for(int i=0; i<4; i++)
			{
				x_value+= 0.5f;
				y_value = Random.Range(0.5f,2.0f);

				player_p_dir[i].transform.position = new Vector3(transform.position.x + x_value,
				                                                 transform.position.y + y_value,
				                                                 transform.position.z - 0.5f);

				Debug.Log("1번 파편 위치 : " + x_value);
			}
			dir_p_set = true;
			break;

		case 2:
			//랜덤으로 4방향 퍼지게 하는 코드. 근데 if문이 너무 많아서 좋지 않을것 같음.
			for(int i=0; i<4; i++)
			{
				if(i==0)
				{
					x_value = Random.Range(.0f,-1.2f);
					y_value = Random.Range(.0f, 1.2f);
				}
				else if(i==1)
				{
					x_value = Random.Range(.0f, 1.2f);
					y_value = Random.Range(.0f, 1.2f);
				}
				else if(i==2)
				{
					x_value = Random.Range(.0f,-1.2f);
					y_value = Random.Range(.0f,-1.2f);
				}
				else if(i==3)
				{
					x_value = Random.Range(.0f, 1.2f);
					y_value = Random.Range(.0f,-1.2f);
				}

				player_p_dir[i].transform.position = new Vector3(transform.position.x + x_value,
				                                                 transform.position.y + y_value,
				                                                 transform.position.z - 0.5f);
			}
			/* 일정 간격으로 파편이 튕기게끔 하는 코드.
			for(int i=0; i<4; i++)
			{
				player_p_dir[i].transform.position = new Vector3(transform.position.x + x_value,
				                                                 transform.position.y + y_value,
				                                                 transform.position.z - 0.5f);
				if(i<2)
				{
					//이거 초기화 되는지 나중에 확인해볼것.
					x_value += 1.5f;
				}
				else if(i>=2)
				{
					if(i==2)
					{
						x_value -= 0.5f;
						return;
					}
					y_value -= 0.5f;
				}
				Debug.Log("2번 파편 위치 : " + x_value);
			}
			*/
			dir_p_set = true;
			break;

		}
		//dir_character_p 호출.
	}

	//파편 움직이는 함수.
	void dir_character_p()
	{
		for (int i=0; i<4; i++) 
		{
			player_p [i].GetComponent<Renderer> ().enabled = true;

			if (dis_p [i] >= 0.1f)
			{
				player_p [i].transform.Translate (dir_p [i] * 0.20f);
			}
		}
	}
}


