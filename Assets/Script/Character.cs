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
	
	public  float		C_speed = 1.0f;

	private float[]     dis_p = new float[4];
	private Vector3[]   dir_p = new Vector3[4];

	public void	Character_Init ()
	{
		//dis_p = {.0f,.0f,.0f,.0f};

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
		for(int i=0; i<4; i++)
		{
			float x_value = Random.Range(-1.0f,2.0f);
			float y_value = Random.Range(0.5f,2.0f);
			
			player_p_dir[i].transform.position = new Vector3(transform.position.x + x_value,
			                                                 transform.position.y + y_value,
			                                                 transform.position.z + 1.0f);
		}

		for (int i=0; i<4; i++) 
		{	//player p - dir.
			dis_p[i] = Vector2.Distance(player_p_dir[i].transform.position, player_p[i].transform.position);
			dir_p[i] = (player_p_dir[i].transform.position - player_p[i].transform.position).normalized;
		}

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
				
				if (Input.GetKeyDown (KeyCode.F)) {
					DestroyObject (ice_cream);
				}
			}
		}

		dir_character_p ();
	}
	
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

	void dir_character_p()
	{
		for (int i=0; i<4; i++)
		{
			player_p [i].GetComponent<Renderer> ().enabled = true;

			if (dis_p [0] >= 0.51f)
			{
				//player_p [i].GetComponent<Renderer> ().enabled = true;
				player_p [0].transform.Translate (dir_p [0] * (0.5f * Time.deltaTime));
			}

			if (dis_p [1] >= 0.51f)
			{
				//player_p [i].GetComponent<Renderer> ().enabled = true;
				player_p [1].transform.Translate (dir_p [1] * (0.5f * Time.deltaTime));
			}

			if (dis_p [2] >= 0.51f)
			{
				//player_p [i].GetComponent<Renderer> ().enabled = true;
				player_p [2].transform.Translate (dir_p [2] * (0.5f * Time.deltaTime));
			}

			if (dis_p [3] >= 0.51f)
			{
				//player_p [i].GetComponent<Renderer> ().enabled = true;
				player_p [3].transform.Translate (dir_p [3] * (0.5f * Time.deltaTime));
			}
		}
	}
}

