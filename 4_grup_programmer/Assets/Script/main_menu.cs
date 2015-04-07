using UnityEngine;
using System.Collections;

public class main_menu : MonoBehaviour {

	private const float Camera_size = 3.84f;
	public Sprite[] button;

	void Start()
	{
        Screen.SetResolution(1024, 768, false);
		Camera.main.transform.position = new Vector3(.0f, .0f, -10f);
		Camera.main.orthographicSize = Camera_size;
	}

	void Update ()
	{
		Vector2 word_p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Ray2D ray = new Ray2D (word_p, Vector2.zero);
		RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

		if (hit.collider != null)
		{
			if (Input.GetMouseButtonDown (0))
			{
				if(hit.collider.tag == "Start")
				{
					Application.LoadLevel("4_game");
				}

				if(hit.collider.tag == "Exit")
				{
					Application.Quit();
				}
			}

			//on mouse Enter
			if (hit.collider.tag == "Start")
			{
				hit.collider.transform.GetComponent<SpriteRenderer>().sprite = button[1];
			}
			else
			{
				GameObject.Find("start_false").GetComponent<SpriteRenderer>().sprite = button[0];
			}

			if(hit.collider.tag == "Exit")
			{
				hit.collider.transform.GetComponent<SpriteRenderer>().sprite = button[3];
			}
			else
			{
				GameObject.Find("exit_false").GetComponent<SpriteRenderer>().sprite = button[2];
			}
		}
	}
}