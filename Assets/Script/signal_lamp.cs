using UnityEngine;
using System.Collections;

public class signal_lamp : MonoBehaviour {

	public  GameObject red_signal_lamp;
	public  GameObject green_signal_lamp;
	
    public  GameObject check_cross_line;
	private bool	   loop_lamp;	

	void Start () 
	{
		//postion set.
        red_signal_lamp.transform.position = new Vector3(1.044f, 5.666f, -2f);
        green_signal_lamp.transform.position = new Vector3(1.044f, 4.688f, -2f);
	
		red_signal_lamp.GetComponent<SpriteRenderer> ().enabled = false;
		green_signal_lamp.GetComponent<SpriteRenderer> ().enabled = false;
		check_cross_line.GetComponent<BoxCollider2D> ().enabled = false;

		loop_lamp = false;
	}
	
	void Update () 
	{
		//loop 돌아버려 버리니까 1회씩 실행되게끔 예외처리를 해주는 것 해놓을 것. 
		if (loop_lamp == false) {
			StartCoroutine (Signal_time_check ());
		}
	}

	IEnumerator Signal_time_check()
	{
		loop_lamp = true;
		//초록불 7초 간격으로 빨간불로 바뀌게 하고 
		green_signal_lamp.GetComponent<SpriteRenderer> ().enabled = true;
		yield return new WaitForSeconds (3.0f);
		//4초 남았을때 깜빡이는 효과.

		for (int i=0; i<4; i++) {
			yield return new WaitForSeconds (0.5f);
			green_signal_lamp.GetComponent<SpriteRenderer> ().enabled = false;
			yield return new WaitForSeconds (0.5f);
			green_signal_lamp.GetComponent<SpriteRenderer> ().enabled = true;
		}

		//다시 빨간불 
		green_signal_lamp.GetComponent<SpriteRenderer> ().enabled = false;
		check_cross_line.GetComponent<BoxCollider2D> ().enabled = true;
		yield return new WaitForSeconds (0.5f);

		red_signal_lamp.GetComponent<SpriteRenderer> ().enabled = true;
		//7초뒤 다시 초록불.
		yield return new WaitForSeconds (7.0f);
		check_cross_line.GetComponent<BoxCollider2D> ().enabled = false;
		red_signal_lamp.GetComponent<SpriteRenderer> ().enabled = false;
		loop_lamp = false;
	}
}
