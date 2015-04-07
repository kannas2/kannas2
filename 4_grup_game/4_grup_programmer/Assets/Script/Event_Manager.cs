using UnityEngine;
using System.Collections;

public class Event_Manager : Singleton<Event_Manager>
{
    public enum Event_ { ride_car, cross_line, trash, sunstroke };

    public AudioClip[] Effect_Event;

    public GameObject[] ending_map;

    public Sprite damage_car;
    public Sprite[] blood_;

    public GameObject dump_car;      //충돌 차량.
    public GameObject[] player_p;    //파편 위치. 1~4 몸체 초기화
    public GameObject[] player_p_dir;//파편 방향. 1~4 몸체 파편 방향 //
    public GameObject steel_;        //충돌 철근.
    public Transform steel_down_postion;

    //파티클 프리팹
    public GameObject fire_char;
    public GameObject explosion_car;
    public Transform boom_car;
    public Transform burning_car;
    public GameObject fire_car;

    private float car_speed;         //차량 속도
    private float dump_speed;        //충돌 차량 속도
    private float steel_down_speed;  //낙하 속도.

    private bool dump_on;         //1회생성 
    private bool car_attack;      //충돌 여부.
    private float car_dis;         //차량거리
    private Vector3 car_dir;         //차량방향
    private float car_roll;        //차량회전

    //파편 방향과 거리.
    private float[] dis_p = new float[4];       //파편과 목적의 거리.
    private Vector3[] dir_p = new Vector3[4];   //파편 방향
    private bool dir_p_set;

    //steel_ 거리,방향변수.
    private float dis_steel_;  //거리
    private Vector3 dir_steel_;  //방향
    private bool steel_attack;   //캐릭터와 충돌여부 
    private bool steel_on;       //1회생성
    private bool crash_human;    //1회재생

    private ride_car str_ride_car;
    private Character str_char;
    private Camera_Manager str_camera;
    private Game_Manager str_Game_mag;
    private Sound_Manager str_sound_mag;

    public void Event_Init()
    {
        str_ride_car = ride_car.Instance;
        str_char = Character.Instance;
        str_Game_mag = Game_Manager.Instance;
        str_camera = Camera_Manager.Instance;
        str_sound_mag = Sound_Manager.Instance;

        //1회만 셋팅.
        dir_p_set = false;
        steel_on = false;
        crash_human = false;

        //충돌 차량 관련 변수
        car_speed = 6.0f;
        dump_speed = 10.0f;
        car_attack = false;
        dump_on = false;
        car_dis = .0f;
        car_dir = new Vector3(0, 0, 0);
        car_roll = 20.0f;

        dis_steel_ = .0f;
        dir_steel_ = new Vector3(0, 0, 0);
        steel_attack = false;
        steel_down_speed = 5.0f;

        //파편과 파편방향 초기화.
        for (int i = 0; i < 4; i++)
        {
            player_p[i].GetComponent<Renderer>().enabled = false;

            player_p[i].transform.position = str_char.transform.position;
            player_p_dir[i].transform.position = str_char.transform.position;
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
                    //충돌차량 생성.
                    dump_car_create(str_ride_car.transform);

                    if (car_dis >= 2.5f)
                    {
                        if (GameObject.Find("dump_car(Clone)"))
                        {
                            GameObject Dump_car = GameObject.Find("dump_car(Clone)");
                            car_dis = Vector2.Distance(str_ride_car.transform.position, Dump_car.transform.position);
                            Dump_car.transform.Translate(car_dir * (dump_speed * Time.deltaTime));
                        }
                    }
                    else
                    {
                        //충돌 폭파 실행. 프리팹 생성 1회실행
                        if (car_attack == false)
                        {
                            //충돌 사운드 실행
                            str_sound_mag.PlayEffect(1);

                            //충돌 이미지 변경.
                            str_ride_car.transform.GetComponent<SpriteRenderer>().sprite = damage_car;

                            //폭파 이미지 생성
                            Instantiate(explosion_car, boom_car.transform.position, boom_car.transform.rotation);

                            //true가 되는순간 충돌차량은 뒤로 밀려남.
                            car_attack = true;
                        }
                    }

                    ///////////////////////////////////여기 까지는 충돌 차량 설정///////////////////////////////////

                    if (car_speed >= 0 && car_attack == true)
                    {
                        //speed * time = 거리.
                        str_ride_car.transform.Translate(Vector2.up * (car_speed * Time.deltaTime));
                        str_ride_car.transform.Rotate(0, 0, car_roll * Time.deltaTime);
                        car_speed -= 3 * Time.deltaTime;

                        if (car_speed <= 0)
                        {
                            //불나게 카툰 렌더 
                            fire_car.GetComponent<ParticleSystemRenderer>().enabled = true;
                            Instantiate(fire_car, burning_car.transform.position, burning_car.transform.rotation);

                            //엔딩화면 호출
                            StartCoroutine(end_ing(1));
                        }
                    }
                    break;
                }

            case Event_.cross_line:
                {
                    //충돌 차량 생성
                    dump_car_create(str_char.transform);

                    if (car_dis >= 2.4f)
                    {
                        if (crash_human == false)
                        {
                            crash_human = true;
                            str_sound_mag.PlayEffect(2);
                        }
                        if (GameObject.Find("dump_car(Clone)"))
                        {
                            GameObject Dump_car = GameObject.Find("dump_car(Clone)");
                            car_dis = Vector2.Distance(str_char.transform.position, Dump_car.transform.position);
                            Dump_car.transform.Translate(car_dir * (dump_speed * Time.deltaTime));
                        }
                    }
                    else if (car_dis <= 2.4f)
                    {
                        if (GameObject.Find("Player"))
                        {
                            str_char.animator.enabled = false;
                            GameObject.Find("Player").GetComponent<SpriteRenderer>().sprite = blood_[1];
                        }
                        //true가 되는순간 캐릭터 파편 실행
                        car_attack = true;
                    }

                    if (dir_p_set == false)
                    {
                        //y만 랜덤이고 x는 일정 간격.
                        x_value = -1.0f;
                        y_value = .0f;

                        for (int i = 0; i < 4; i++)
                        {
                            x_value += 0.5f;
                            y_value = Random.Range(0.5f, 2.0f);

                            player_p_dir[i].transform.position = new Vector3(str_char.transform.localPosition.x + x_value,
                                                                             str_char.transform.localPosition.y + y_value,
                                                                             str_char.transform.localPosition.z - 2);
                        }
                        dir_p_set = true;
                    }
                    if (car_attack == true)
                    {
                        dir_character_p(0); //파편 이동함수.
                    }
                    break;
                }

            case Event_.trash:
                {
                    x_value = 0f;
                    y_value = 0f;

                    steel_down();

                    if (dir_p_set == false && steel_attack == true)
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

                            player_p_dir[i].transform.position = new Vector3(str_char.transform.localPosition.x + x_value,
                                                                             str_char.transform.localPosition.y + y_value,
                                                                             str_char.transform.localPosition.z - 2.0f);
                        }
                        dir_p_set = true;
                    }
                    if (steel_attack == true)
                    {
                        dir_character_p(3); //파편 이동함수.
                    }
                    break;
                }

            case Event_.sunstroke:
                {
                    //아이스크림을 일정 높이까지 갔는데 먹지 않을경우 캐릭터가 죽는.
                    if (str_Game_mag.ice_cream == false)
                    {
                        if (str_char.transform.position.x <= -3.06f && str_char.transform.position.y >= 6.0f)
                        {
                            //캐릭터 죽음
                            str_Game_mag.char_update = false;
                            GameObject.Find("Player").GetComponent<Animator>().enabled = false;
                            str_char.transform.rotation = Quaternion.Euler(.0f, .0f, 90f);
                            
                            //캐릭터한테 불붙임.
                            if (crash_human == false) //다른 이벤트 조건에서 사용되는 1회용bool인데 중복이 되지 않음으로 가져와 썻는데 다음에는 무언가 공통적으로 쓸수있게끔 변수명을 붙여 줘야겠다.
                            {
                                crash_human = true;
                                Instantiate(fire_char, str_char.transform.position, transform.rotation);
                            }
                            
                            StartCoroutine(end_ing(2));
                        }
                    }
                    break;
                }

            default:
                return;
        }
    }

    //충돌차량 생성 함수.
    void dump_car_create(Transform pos)
    {
        //충돌 차량 생성
        if (dump_on == false)
        {
            dump_on = true; //1회 생성. 프리팹,위치,회전

            //위치 set
            dump_car.transform.position = new Vector3(pos.transform.position.x,
                                                      pos.transform.position.y - 10.0f,
                                                      pos.transform.position.z);

            //프리팹 생성.
            Instantiate(dump_car, dump_car.transform.position, pos.transform.rotation);

            //플레이어 차량은 멈춰있는 상태.
            car_dis = Vector2.Distance(pos.transform.position, dump_car.transform.position);
            car_dir = (pos.transform.position - dump_car.transform.position).normalized;
        }
    }

    //철근이 지정 위치에 떨어지는 함수.
    public void steel_down_p()
    {
        //생성 및 셋팅
        steel_set_postion(steel_down_postion.transform);

        //transform.GetComponent<AudioSource>().PlayOneShot(Effect_Event[0]);

        //철근 충돌 설정. loop
        if (dis_steel_ >= 1.0f)
        {
            if (GameObject.Find("steel_(Clone)"))
            {
                //object find
                GameObject c_steel_ = GameObject.Find("steel_(Clone)");
                //거리 update.
                dis_steel_ = Vector2.Distance(steel_down_postion.transform.position, c_steel_.transform.position);
                //생성된 프리팹 이동.
                c_steel_.transform.Translate(dir_steel_ * (steel_down_speed * Time.deltaTime));

                steel_down_speed += 1.0f;
            }
        }
    }

    void steel_set_postion(Transform pos)
    {
        if (steel_on == false)
        {
            //str_sound_mag.PlayEffect(3);
            transform.GetComponent<AudioSource>().PlayOneShot(Effect_Event[0]);

            steel_on = true;
            //철근이 생성될 위치 1회.
            steel_.transform.position = new Vector3(pos.transform.position.x,
                pos.transform.position.y + 10.0f,
                pos.transform.position.z + -1.0f);

            //철근 생성. 1회.
            Instantiate(steel_, steel_.transform.position, steel_.transform.rotation);

            //철과 캐릭터의 거리방향 1회.
            dis_steel_ = Vector2.Distance(pos.transform.position, steel_.transform.position);
            dir_steel_ = (pos.transform.position - steel_.transform.position).normalized;
        }
    }

    //철근이 캐릭터에게 떨어지는 함수.
    void steel_down()
    {
        //생성및 세팅
        steel_set_postion(str_char.transform);

        //철근 충돌 설정. loop
        if (dis_steel_ >= 1.0f)
        {
            if (GameObject.Find("steel_(Clone)"))
            {
                //object find
                GameObject c_steel_ = GameObject.Find("steel_(Clone)");
                //거리 update.
                dis_steel_ = Vector2.Distance(str_char.transform.position, c_steel_.transform.position);
                //생성된 프리팹 이동.
                c_steel_.transform.Translate(dir_steel_ * (steel_down_speed * Time.deltaTime));

                steel_down_speed += 0.3f;
            }
        }
        else if (dis_steel_ <= 1.0f)
        {
            if (GameObject.Find("Player"))
            {
                //blood 로 변경
                str_char.animator.enabled = false;
                GameObject.Find("Player").GetComponent<SpriteRenderer>().sprite = blood_[0];
            }

            if (GameObject.Find("steel_(Clone)"))
            {
                GameObject steel = GameObject.Find("steel_(Clone)");
                Destroy(steel);
            }

            //true가 되는순간 캐릭터가 4방향으로 파편이 튕김.
            steel_attack = true;
        }
    }

    //파편 움직이는 함수.
    void dir_character_p(int num)
    {
        for (int i = 0; i < 4; i++)
        {
            player_p[i].GetComponent<Renderer>().enabled = true;

            if (dis_p[i] >= 0.2f)
            {
                player_p[i].transform.Translate(dir_p[i] * 0.20f);
            }
        }

        StartCoroutine(end_ing(num));
    }

    public IEnumerator end_ing(int select)
    {
        //동작 off.
        str_Game_mag.ride_car_Evnet = false;
        str_Game_mag.F_key_ = false;

        if (GameObject.Find("inven"))
        {
            GameObject.Find("inven").SetActive(false);

            yield return new WaitForSeconds(2.5f);

            //위치 셋팅
            Vector3 postion = new Vector3(14.48f, 0.005f, .0f);

            //1.길건너기, 2.짧은거리 3.일사병 4.쓰래기버리기

            //프리팹 생성.
            Instantiate(ending_map[select], postion, transform.rotation);
            str_camera.transform.position = new Vector3(14.48f, 0.005f, -10f);
        }
    }
}


