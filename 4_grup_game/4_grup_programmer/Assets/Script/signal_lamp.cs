using UnityEngine;
using System.Collections;

public class signal_lamp : MonoBehaviour {

	public  GameObject red_signal_lamp;
	public  GameObject green_signal_lamp;
    public  GameObject check_cross_line;

    private SpriteRenderer red_signal;
    private SpriteRenderer green_signal;
    private BoxCollider2D  check_cross_;

	private bool	       loop_lamp;	

	void Start () 
	{
		//signal_lmap_postion_set.
        red_signal_lamp.transform.position = new Vector3(1.044f, 5.666f, -2f);
        green_signal_lamp.transform.position = new Vector3(1.044f, 4.688f, -2f);


        //get
        red_signal = red_signal_lamp.GetComponent<SpriteRenderer>();
		green_signal = green_signal_lamp.GetComponent<SpriteRenderer>();
        check_cross_ =check_cross_line.GetComponent<BoxCollider2D>();

        //signal off
        red_signal.enabled = false;
        green_signal.enabled = false;

		loop_lamp = false;
	}
	
	void Update () 
	{
		//loop  
		if (loop_lamp == false) {
			StartCoroutine (Signal_time_check ());
		}
	}

	IEnumerator Signal_time_check()
	{
		loop_lamp = true;
		//초록불 7초 간격으로 빨간불로 바뀌게 하고 
		green_signal.enabled = true;
        check_cross_.enabled = false;
		yield return new WaitForSeconds (3.0f);
		//4초 남았을때 깜빡이는 효과.

		for (int i=0; i<4; i++) {
			yield return new WaitForSeconds (0.5f);
            green_signal.enabled = false;
			yield return new WaitForSeconds (0.5f);
            green_signal.enabled = true;
		}

		//다시 빨간불 
        green_signal.enabled = false;
		
        yield return new WaitForSeconds (0.5f);
        check_cross_.enabled = true;
		red_signal.enabled = true;
		
        //7초뒤 다시 초록불.
		yield return new WaitForSeconds (7.0f);
		red_signal.enabled = false;
		loop_lamp = false;
	}
}
