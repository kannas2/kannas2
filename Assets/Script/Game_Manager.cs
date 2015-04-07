using UnityEngine;
using System.Collections;

public class Game_Manager : Singleton<Game_Manager>
{
    public GameObject trash_con;    //프리팹
    public Sprite sprite_trash_con; //스프라이트.

    public GameObject obj_Player_ch;
    private Character str_Character;

    public GameObject obj_ride_car;
    private ride_car str_ride_car;

    public GameObject obj_trash;

    private float c_distance;//차까지의 거리.
    private float t_distance;//쓰래기통의 거리.
    private float trash_dis; //쓰래기 거리 날라가는거.
    private Vector3 dir_con; //쓰래기 던지는거 방향.

    public bool ride_car_update;
    public bool ride_car_Evnet;
    public bool trash_Event;
    public bool char_update;
    public bool cross_Event;

    private bool shoot_;
    private bool eat_ice_cream;
    public bool ice_cream;
    private bool trash_check;
    private bool car_check;
    private bool trash_shoot_;
    private bool steel_down;
    public bool F_key_;

    private Camera_Manager str_Camera_Mag;
    private Event_Manager str_Event_Mag;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        str_Event_Mag = Event_Manager.Instance; //사용할때 변수 안쓰고 쓸라면 (Event_Manager.Instance).
        str_Camera_Mag = Camera_Manager.Instance;

        //타 클래스 초기화.
        str_Event_Mag.Event_Init();

        eat_ice_cream = true;
        ice_cream = false;
        trash_check = false;
        car_check = false;
        F_key_ = true;

        ride_car_update = false;
        ride_car_Evnet = false;
        char_update = true;
        trash_Event = false;
        cross_Event = false;
        steel_down = false;

        shoot_ = false;
        trash_shoot_ = false;
        dir_con = new Vector3(0, 0, 0);

        c_distance = .0f;
        t_distance = .0f;
        trash_dis = 4.0f;

        str_Character = obj_Player_ch.GetComponent<Character>();
        str_ride_car = obj_ride_car.GetComponent<ride_car>();
        spriteRenderer = GameObject.Find("Player_ice_cream").GetComponent<SpriteRenderer>();

        //Init
        str_Character.Character_Init();
        str_ride_car.ride_car_Init();

        //오브젝트 위치 set
        obj_trash.transform.position = new Vector3(-5.66f, 3.94f, -1.0f);
    }

    void Update()
    {
        Event_loop();

        //타 클래스 업데이트.
        str_Event_Mag.Event_Update();

        //플레이어 오브젝트와 자동차 오브젝트의 거리 계산.
        c_distance = Vector2.Distance(obj_Player_ch.transform.position, obj_ride_car.transform.position);
        //플레이어 오브젝트와 쓰래기통의 거리 계산.
        t_distance = Vector2.Distance(obj_Player_ch.transform.position, obj_trash.transform.position);

        //거리에 따라 true 와 false를 update해주는.
        car_check = if_check(c_distance);

        //쓰래기통.
        trash_check = if_check(t_distance);

        //캐릭터가 ON일 경우.
        if (char_update == true)
        {
            str_Character.Character_Update();

            //camera set
            str_Camera_Mag.player_camera_set(str_Character.transform);

        }
        //자동차가 ON일 경우.
        if (ride_car_update == true)
        {
            str_ride_car.ride_car_Update();

            //camera set
            str_Camera_Mag.player_camera_set(str_ride_car.transform);
        }

        if (Input.GetKeyDown(KeyCode.F) && F_key_ == true)
        {
            //쓰래기 버리는 체크.
            if (trash_check)
            {
                //아이스크림을 안 먹었을경우.
                if (GameObject.Find("Player_ice_cream") && ice_cream == false)
                {
                    ice_cream = true;
                    spriteRenderer.sprite = sprite_trash_con;

                    return;
                }

                if (GameObject.Find("Player_ice_cream"))
                {
                    GameObject trash = GameObject.Find("Player_ice_cream");
                    Destroy(trash);
                    steel_down = true;
                    Debug.Log("쓰래기 버림");
                    //변경된 텍스쳐 지워주는걸로.
                }
                shoot_ = false; //쓰래기를 버렸으니 던지지 못하게.
                return; //조건이 맞으면 밖으로 나가야함 쓰래기 던지는 조건 때문에.
            }

            if (ride_car_update == false)
            {
                //거리 1.0f보다 작을 경우.
                if (car_check)
                {
                    //차를 탑승.
                    obj_Player_ch.GetComponent<Renderer>().enabled = false;

                    //업데이트 교환.
                    ride_car_update = true;
                    char_update = false;

                    eat_ice_cream = false;
                    return;
                }
            }
            else
            {
                //차에서 내림
                obj_Player_ch.GetComponent<Renderer>().enabled = true;

                //캐릭터가 차량에서 내릴경우 시작할 위치.
                str_Character.transform.position = new Vector3(str_ride_car.transform.position.x + 1.0f,
                                                               str_ride_car.transform.position.y,
                                                               str_ride_car.transform.position.z);
                eat_ice_cream = true;
                ride_car_update = false;
                char_update = true;

                return;
            }

            //아이스크림 먹는 체크.
            if (GameObject.Find("Player_ice_cream") && eat_ice_cream == true && ice_cream == false)
            {
                ice_cream = true; //위에 쓰래기통 앞에서 안먹엇을경우와 길가다 먹는 경우를 구분하기 위해서.
                spriteRenderer.sprite = sprite_trash_con;
                shoot_ = true;
                return;
            }

            if (shoot_ == true)
            {
                //아이콘에 있던 쓰래기 콘을 없애주고
                if (GameObject.Find("Player_ice_cream"))
                {
                    GameObject trash = GameObject.Find("Player_ice_cream");
                    Destroy(trash);
                }
                //프리팹 콘 생성
                Instantiate(trash_con, str_Character.transform.position, str_Character.transform.rotation);

                GameObject C_trash_con = GameObject.Find("trash_con(Clone)");
                dir_con = (str_Character.wall_check[0].transform.position - C_trash_con.transform.position).normalized;

                trash_shoot_ = true;
                shoot_ = false;

                //C_trash_con.transform.Translate(dir_con * (3.0f * Time.deltaTime));
                //캐릭터가 보는 방향 뭐 dir 같은거 만들어서 그 위치로 가게끔 설정해주자.
            }
        }

        if (trash_shoot_ == true)
        {
            if (GameObject.Find("trash_con(Clone)"))
            {
                GameObject C_trash_con = GameObject.Find("trash_con(Clone)");
                //던졋을때 거리를 비교하기 위해서.
                float trash_shoot_dis = Vector2.Distance(obj_trash.transform.position, C_trash_con.transform.position);

                if (trash_dis >= .0f)
                {
                    //GameObject C_trash_con = GameObject.Find("trash_con(Clone)");
                    C_trash_con.transform.Translate(dir_con * (8.0f * Time.deltaTime));
                    trash_dis -= 0.2f;
                }
                else if (trash_shoot_dis <= 1.0f)
                {
                    steel_down = true;
                    Debug.Log("쓰래기 버림");
                    Destroy(C_trash_con);
                }
                else if (trash_shoot_dis >= 1.0f)
                {
                    char_update = false;  //못움직이게
                    trash_shoot_ = false; //루프 안돌게
                    trash_Event = true;   //이벤트 실행
                }
            }
        }
    }


    //거리 체크해서 bool 반환.
    bool if_check(float dis_)
    {
        if (dis_ <= 1.5f)
        {
            return true;
        }
        else return false;
    }

    void Event_loop()
    {
        //자동차 탑승중 이벤트 발생.
        if (ride_car_Evnet == true)
        {
            str_ride_car.GetComponent<BoxCollider2D>().enabled = false;
            str_Event_Mag.character_event(Event_Manager.Event_.ride_car);

            //camera set //위에서 업데이트가 false로 바뀔 경우.
            str_Camera_Mag.player_camera_set(str_ride_car.transform);
        }

        //쓰래기 던졌을 경우 이벤트 발생.
        if (trash_Event == true)
        {
            str_ride_car.GetComponent<BoxCollider2D>().enabled = false;
            str_Event_Mag.character_event(Event_Manager.Event_.trash);
        }

        //캐릭터 cross 호출
        if (cross_Event == true)
        {
            str_ride_car.GetComponent<BoxCollider2D>().enabled = false;
            str_Event_Mag.character_event(Event_Manager.Event_.cross_line);
        }
        //캐릭터가 쓰래기를 정상적으로 버렸을 경우 호출 되는 이벤트
        if (steel_down == true)
        {
            str_ride_car.GetComponent<BoxCollider2D>().enabled = false;
            str_Event_Mag.steel_down_p();
        }

        //일사병 호출(계속 검사)
        str_Event_Mag.character_event(Event_Manager.Event_.sunstroke);
    }
}