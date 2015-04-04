using UnityEngine;
using System.Collections;

public class Event_Manager : Singleton<Event_Manager>
{
    public enum Event_ { ride_car, cross_line, trash, sunstroke };

    public GameObject dump_car;      //충돌 차량.
    public GameObject[] player_p;    //파편 위치. 1~4 몸체 초기화
    public GameObject[] player_p_dir;//파편 방향. 1~4 몸체 파편 방향 //
    public GameObject steel_;        //충돌 철근.

    private float car_speed;         //차량 속도
    private float dump_speed;        //충돌 차량 속도
    private float steel_down_speed;  //낙하 속도.

    private bool    dump_on;         //1회생성 
    private bool    car_attack;      //충돌 여부.
    private float   car_dis;         //차량거리
    private Vector3 car_dir;         //차량방향
    private float   car_roll;        //차량회전

    //파편 방향과 거리.
    private float[] dis_p = new float[4];       //파편과 목적의 거리.
    private Vector3[] dir_p = new Vector3[4];   //파편 방향
    private bool dir_p_set;

    //steel_ 거리,방향변수.
    private float   dis_steel_;  //거리
    private Vector3 dir_steel_;  //방향
    private bool steel_attack;   //캐릭터와 충돌여부 
    private bool steel_on;       //1회생성

    private ride_car  str_ride_car;
    private Character str_char;

    public void Event_Init()
    {
        str_ride_car = ride_car.Instance;
        str_char = Character.Instance;

        //1회만 셋팅.
        dir_p_set = false;
        steel_on = false;

        //충돌 차량 관련 변수
        car_speed = 6.0f;
        dump_speed = 10.0f;
        car_attack = false;
        dump_on = false;
        car_dis = .0f;
        car_dir = new Vector3(0, 0, 0);
        car_roll = 20.0f;

        dis_steel_ = .0f;
        dir_steel_ = new Vector3(0,0,0);
        steel_attack = false;
        steel_down_speed = 5.0f;

        //파편과 파편방향 초기화.
        for (int i = 0; i < 4; i++)
        {
            player_p[i].GetComponent<Renderer>().enabled = false;

            player_p[i].transform.position = new Vector3(str_char.transform.position.x,
                                                         str_char.transform.position.y,
                                                         str_char.transform.position.z + 1.0f);

            player_p_dir[i].transform.position = new Vector3(str_char.transform.position.x,
                                                             str_char.transform.position.y,
                                                             str_char.transform.position.z + 1.0f);
        }
    }

    public void Event_Update()
    {
        //파편 거리 ,방향 업데이트.
        for (int i = 0; i < 4; i++)
        {
            dis_p[i] = Vector2.Distance(player_p_dir[i].transform.position, player_p[i].transform.position);
            dir_p[i] = (player_p_dir[i].transform.position - player_p[i].transform.position).normalized;
        }

        Debug.Log("steel dis :" + dis_steel_);
    }

   public void character_event(Event_ event_)
    {
        //랜덤에 쓰일 변수.
        float x_value = .0f;
        float y_value = .0f;

        switch (event_)
        {
            //차에 타고있다가 차에 치였을 경우.
            case Event_.ride_car:
            {
                if (car_speed >= 0 && car_attack == true)
                {
                    //speed * time = 거리.
                    str_ride_car.transform.Translate(Vector2.up * (car_speed * Time.deltaTime));
                    str_ride_car.transform.Rotate(0, 0, car_roll * Time.deltaTime);
                    car_speed -= 3 * Time.deltaTime;
                }

                if (car_attack == false) //생성할때 포지션 설정.
                {
                    dump_car.transform.position = new Vector3(str_ride_car.transform.position.x,
                                                              str_ride_car.transform.position.y - 10.0f,
                                                              str_ride_car.transform.position.z);
                }
                //create_frefab 거리 방향 구함.
                if (dump_on == false)
                {
                    dump_on = true; //1회 생성. 프리팹,위치,회전
                    Instantiate(dump_car, dump_car.transform.position, str_ride_car.transform.rotation);
                    //플레이어 차량은 멈춰있는 상태.
                    car_dis = Vector2.Distance(str_ride_car.transform.position, dump_car.transform.position);
                    car_dir = (str_ride_car.transform.position - dump_car.transform.position).normalized;
                }
                //여기 까지는 충돌 차량 설정.
                if (car_dis >= 1.0f)
                {
                    if (GameObject.Find("dump_car(Clone)"))
                    {
                        GameObject Dump_car = GameObject.Find("dump_car(Clone)");
                        Dump_car.transform.Translate(car_dir * (dump_speed * Time.deltaTime));
                        car_dis = Vector2.Distance(str_ride_car.transform.position, Dump_car.transform.position);
                        Debug.Log(car_dis);
                    }
                }
                else
                {
                    //true가 되는순간 충돌차량은 뒤로 밀려남.
                    car_attack = true;
                }
                break;
            }

            case Event_.cross_line:
                {
                    if (dir_p_set == false)
                    {
                        //y만 랜덤이고 x는 일정 간격.
                        x_value = -1.0f;
                        y_value = .0f;

                        for (int i = 0; i < 4; i++)
                        {
                            x_value += 0.5f;
                            y_value = Random.Range(0.5f, 2.0f);

                            player_p_dir[i].transform.position = new Vector3(str_char.transform.position.x + x_value,
                                                                             str_char.transform.position.y + y_value,
                                                                             str_char.transform.position.z - 0.5f);
                        }
                        dir_p_set = true;
                    }
                    dir_character_p(); //파편 이동함수.
                    break;
                }

            case Event_.trash:
                {
                    x_value = 0f;
                    y_value = 0f;

                    steel_down();

                    if(dir_p_set == false && steel_attack == true)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (i == 0)
                            {
                                x_value = Random.Range(.0f, -1.2f);
                                y_value = Random.Range(.0f, 1.2f);
                            }
                            else if (i == 1)
                            {
                                x_value = Random.Range(.0f, 1.2f);
                                y_value = Random.Range(.0f, 1.2f);
                            }
                            else if (i == 2)
                            {
                                x_value = Random.Range(.0f, -1.2f);
                                y_value = Random.Range(.0f, -1.2f);
                            }
                            else if (i == 3)
                            {
                                x_value = Random.Range(.0f, 1.2f);
                                y_value = Random.Range(.0f, -1.2f);
                            }

                            player_p_dir[i].transform.position = new Vector3(str_char.transform.position.x + x_value,
                                                                             str_char.transform.position.y + y_value,
                                                                             str_char.transform.position.z - 0.5f);
                        }
                        dir_p_set = true;
                    }
                    dir_character_p(); //파편 이동함수.
                    break;
                }

            case Event_.sunstroke:
                {
                    //아이스크림을 일정시간 먹지 않을경우 캐릭터가 죽는.
                    break;
                }
            default:
                return;
        }
    }

   //철근이 캐릭터에게 떨어지는 함수.
   void steel_down()
   {
       if (steel_on == false)
       {
           steel_on = true;
           //철근이 생성될 위치 1회.
           steel_.transform.position = new Vector3(str_char.transform.position.x,
               str_char.transform.position.y + 10.0f,
               str_char.transform.position.z);

           //철근 생성. 1회.
           Instantiate(steel_, steel_.transform.position, steel_.transform.rotation);

           //철과 캐릭터의 거리방향 1회.
           dis_steel_ = Vector2.Distance(str_char.transform.position, steel_.transform.position);
           dir_steel_ = (str_char.transform.position - steel_.transform.position).normalized;
       }

       //철근 충돌 설정. loop
       if (dis_steel_ >= 1.0f)
       {
           if (GameObject.Find("steel_(Clone)"))
           {
               //object find
               GameObject c_steel_ = GameObject.Find("steel_(Clone)");
               //거리 update.
               dis_steel_ = Vector2.Distance(str_char.transform.position,c_steel_.transform.position);
               //생성된 프리팹 이동.
               c_steel_.transform.Translate(dir_steel_ * (steel_down_speed * Time.deltaTime));
           }
       }
       else
       {
           //true가 되는순간 캐릭터가 4방향으로 파편이 튕김.
           steel_attack = true;
       }
   }
    
    //파편 움직이는 함수.
    void dir_character_p()
    {
        for (int i = 0; i < 4; i++)
        {
            player_p[i].GetComponent<Renderer>().enabled = true;

            if (dis_p[i] >= 0.1f)
            {
                player_p[i].transform.Translate(dir_p[i] * 0.20f);
            }
        }
    }
}

