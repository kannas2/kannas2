using UnityEngine;
using System.Collections;

public class signal_lamp : MonoBehaviour {

	public GameObject red_signal_lamp;
	public GameObject green_signal_lamp;

	public GameObject check_cross_line;

	void Start () 
	{
		//postion set.
		red_signal_lamp.transform.position = new Vector3 (-2.97f, 0.91f, .0f);
		green_signal_lamp.transform.position = new Vector3 (-2.97f, 1.6f, .0f);
	
		red_signal_lamp.GetComponent<SpriteRenderer> ().enabled = false;
		green_signal_lamp.GetComponent<SpriteRenderer> ().enabled = false;
	}
	
	void Update () 
	{

		//loop 돌아버려 버리니까 1회씩 실행되게끔 예외처리를 해주는 것 해놓을 것. 
		StartCoroutine (Signal_time_check ());
	}

	IEnumerator Signal_time_check()
	{
		//초록불 7초 간격으로 빨간불로 바뀌게 하고 
		green_signal_lamp.GetComponent<SpriteRenderer> ().enabled = true;
		yield return new WaitForSeconds (3.0f);
		//4초 남았을때 깜빡이는 효과.

		yield return new WaitForSeconds (1.5f);
		green_signal_lamp.GetComponent<SpriteRenderer>().enabled = false;

		yield return new WaitForSeconds (1.5f);
		green_signal_lamp.GetComponent<SpriteRenderer>().enabled = true;

		//다시 빨간불 
		red_signal_lamp.GetComponent<SpriteRenderer> ().enabled = true;
		green_signal_lamp.GetComponent<SpriteRenderer> ().enabled = false;

		//7초뒤 다시 초록불.
		yield return new WaitForSeconds (7.0f);
	}
}
