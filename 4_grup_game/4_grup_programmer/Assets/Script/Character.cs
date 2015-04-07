using UnityEngine;
using System.Collections;

public class Character : Singleton<Character>
{
    public GameObject ice_cream;
    public GameObject clear_map;

    public Sprite front_char;

    public Animator animator;

    // 벽 체크 변수.
    public Transform[] wall_check;

    public int move_max = 0;
    public int move_pos = 0;

    private float directionX = 0;
    private float directionY = 0;

    private bool walking = false;
    public bool char_on = true;

    private bool c_move;

    public float C_speed = 2.0f;

    private Game_Manager str_Game_mag;
    private Camera_Manager str_camera_mag;
    private Sound_Manager str_sound_mag;

    public void Character_Init()
    {
        c_move = false;

        str_Game_mag = Game_Manager.Instance;
        str_camera_mag = Camera_Manager.Instance;
        str_sound_mag = Sound_Manager.Instance;

        //character start postion
        transform.position = new Vector3(1.29f, -7.62f, -1.0f);


        //위치 초기화.
        wall_check[0].transform.position = new Vector3(transform.position.x,
            transform.position.y,
            transform.position.z);

        animator = GetComponent<Animator>();
        animator.applyRootMotion = false;
    }

    public void Character_Update()
    {
        if (char_on == true)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            walking = true;

            //right
            if (h > 0)
            {
                directionX = 1;
                directionY = 0;
                wall_check[0].transform.position = new Vector3(transform.position.x + 0.5f,
                    transform.position.y,
                    transform.position.z);
            }//left
            else if (h < 0)
            {
                directionX = -1;
                directionY = 0;
                wall_check[0].transform.position = new Vector3(transform.position.x - 0.5f,
                    transform.position.y,
                    transform.position.z);
            }//front 
            else if (v > 0)
            {
                directionX = 0;
                directionY = 1;
                wall_check[0].transform.position = new Vector3(transform.position.x,
                    transform.position.y + 1.1f,
                    transform.position.z);
            }//back 
            else if (v < 0)
            {
                directionX = 0;
                directionY = -1;
                wall_check[0].transform.position = new Vector3(transform.position.x,
                    transform.position.y - 1.1f,
                    transform.position.z);
            }
            else
            {
                str_sound_mag.PlayEffect(7);
                walking = false;
            }

            if (walking)
            {
                transform.Translate(new Vector3(directionX, directionY, 0) * Time.deltaTime * C_speed);
            }

            animator.SetFloat("DirectionX", directionX);
            animator.SetFloat("DirectionY", directionY);
            animator.SetBool("Walking", walking);
        }
        
        c_move = Physics2D.Linecast(transform.position, wall_check[0].transform.position, 1 << LayerMask.NameToLayer("Wall"));

        //캐릭터 벽과 충돌 처리 예외처리.
        if (c_move)
        {
            C_speed = .0f;
        }
        else
        {
            C_speed = 2.0f;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "cross")
        {
            //캐릭터 업데이트 off.
            str_Game_mag.char_update = false;
            animator.enabled = false;
            animator.SetBool("Walking", false); //못움직이게.
            GetComponent<Collider2D>().enabled = false;

            //캐릭터 차량쪽 방향을 보게..
            transform.GetComponent<SpriteRenderer>().sprite = front_char;

            //manager loop on
            str_Game_mag.cross_Event = true;
        }

        if (coll.gameObject.tag == "goal_point")
        {
            if(!GameObject.Find("Player_ice_cream"))
            {
                //모든 동작 off.
                str_Game_mag.ride_car_Evnet = false;
                str_Game_mag.F_key_ = false;
                str_Game_mag.char_update = false;
                str_Game_mag.ride_car_update = false;

                if (GameObject.Find("inven"))
                {
                    GameObject.Find("inven").SetActive(false);

                    //위치 셋팅
                    Vector3 postion = new Vector3(14.48f, 0.005f, .0f);

                    //프리팹 생성.
                    Instantiate(clear_map, postion, transform.rotation);
                    str_camera_mag.transform.position = new Vector3(14.48f, 0.005f, -10f);
                }
            }
        }
    }
}