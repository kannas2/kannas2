using UnityEngine;
using System.Collections;

public class speed_test : MonoBehaviour {

	public GameObject dump_car;
	
	private float car_speed;

	private float dump_direction;
	private bool  dump_on;

	public float car_roll;
	public bool car_attack;

	public float distance;
	public Vector3 _dir;
	
	void Start () 
	{
		car_speed = 6.0f;
		car_roll = 20.0f;
		
		dump_direction = 10.0f;
		car_attack = false;
		dump_on = false;

		distance = .0f;
		_dir = new Vector3 (0, 0, 0);
	}
	
	void Update () 
	{
		if(car_speed>0 && car_attack == true)
		{
			//speed * time = 거리.
			transform.Translate(Vector2.up * (car_speed * Time.deltaTime));
			transform.Rotate(0,0,car_roll*Time.deltaTime);
			car_speed -= 3 * Time.deltaTime;
		}
		
		//충돌시 false로.
		if (car_attack == false) {
			dump_car.transform.position = new Vector3 (transform.position.x,
			                                           transform.position.y - 10.0f,
			                                           transform.position.z);
			//create_frefab
			if(dump_on == false)
			{
				dump_on = true;
				Instantiate (dump_car, dump_car.transform.position, transform.rotation);

				distance = Vector2.Distance(transform.position,dump_car.transform.position);
				_dir = (transform.position - dump_car.transform.position).normalized;
			}
		} 

		if (distance >= 1.0f) 
		{
			if (GameObject.Find ("dump_car(Clone)"))
			{
				GameObject Dump_car = GameObject.Find("dump_car(Clone)");

				//distance = Vector2.Distance(transform.position,Dump_car);

				Dump_car.transform.Translate ( _dir * (dump_direction * Time.deltaTime));

				distance = Vector2.Distance(transform.position,Dump_car.transform.position);
				Debug.Log (distance);

				//dump_direction -= 3 * Time.deltaTime;
			}
		} 
		else 
		{
			car_attack = true;
		}
		//생성된뒤에 자동차 위로 올라감. 충돌시 멈추고 충돌한 차량이 튕겨나감.
	}
}
