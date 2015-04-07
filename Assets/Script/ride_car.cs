using UnityEngine;
using System.Collections;

public class ride_car : Singleton<ride_car> {

	private float      move_speed;
	public  GameObject obj_character;

	private float      directionX;
	private float      directionY;

    //private Event_Manager str_Event_Mag;
    private Game_Manager str_Game_Mag;

	public void ride_car_Init ()
	{
        //str_Event_Mag = Event_Manager.Instance;
        str_Game_Mag = Game_Manager.Instance;

		directionX = .0f;
		directionY = .0f;

		//car set.
        transform.position = new Vector3(-0.42f, -6.8f, -1.0f);

		move_speed = 200.0f;
	}
	
	public void ride_car_Update ()
	{
        //상하만 요청함.
        float v = Input.GetAxisRaw("Vertical");

        //back
        if (v < 0)
        {
            transform.rotation = Quaternion.Euler(.0f, .0f, 180.0f);

            directionX = 0;
            directionY = 1;

            move_speed = 3.0f;
        }
        else if (v > 0) //front
        {
            transform.rotation = Quaternion.Euler(.0f, .0f, .0f);
            directionX = 0;
            directionY = 1;

            move_speed = 3.0f;
        }

        if (move_speed > 0)
        {
            move_speed -= 1.0f * Time.deltaTime ;
        }
        else
        {
            move_speed = 0;
        }

        //자동차가 방향키를 손에서 때면 서서히 멈추게 끔 하기를 기획자가 요구함.
        transform.Translate(new Vector3(directionX, directionY, 0) * Time.deltaTime * move_speed);
        

        ////////////////////////////////////이동관련////////////////////////////////////////////////

        if (transform.position.y >= -1.55f)
        {
            //str_Event.character_event(Event_Manager.Event_.trash);
            //str_Game_Mag.ride_car_update = false; //호출은 계속 해야하니 다른곳에서 off시키는 걸로.
            str_Game_Mag.char_update = false; //충돌하면 사람은 움직일 필요없고.
            str_Game_Mag.ride_car_update = false; //차량도 움직일 필요없음.
            str_Game_Mag.ride_car_Evnet = true;  //매니저에서 이벤트 loop
        }
	}
}