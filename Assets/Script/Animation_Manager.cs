using UnityEngine;
using System.Collections;

/*
public class Animation_Manager : MonoBehaviour {

    //캐릭터 sprite back0~3 // front3~5 // left 6~8 // right 9~ 11
    enum dir_ { front, back, left, right };

    public Sprite[]        character_ani;
    public GameObject      chacracter;
    private SpriteRenderer spriteRenderer;
    private bool char_loop;

	void Start () 
    {
        char_loop = false;
        spriteRenderer = GameObject.Find("Player").GetComponent<SpriteRenderer>();
	}
	
	void Update () 
    {
        Debug.Log("업데이트는 됨?");

        if(Input.GetKeyDown(KeyCode.A))
        {
            if(char_loop == false)
                Character_Ani(dir_.front);
        }
	}
    void Character_Ani(dir_ dir)
    {
        switch(dir)
        {
            case dir_.front :
                char_loop = true;
                for (int i = 3; i < 6; i++)
                {
                    Debug.Log("돌고있음?");
                    StartCoroutine(ani_time());
                    spriteRenderer.sprite = character_ani[i];
                }
                char_loop = false;
                break;

            case dir_.back :
                for (int i = 0; i < 3; i++)
                {
                    StartCoroutine(ani_time());
                    spriteRenderer.sprite = character_ani[i];
                }
                break;

            case dir_.left :
                for (int i = 6; i < 9; i++)
                {
                    StartCoroutine(ani_time());
                    spriteRenderer.sprite = character_ani[i];
                }
                break;

            case dir_.right :
                for (int i = 9; i < 12; i++)
                {
                    StartCoroutine(ani_time());
                    spriteRenderer.sprite = character_ani[i];
                }
                break;
        }
    }
    IEnumerator ani_time()
    {
        yield return new WaitForSeconds(1.5f);
    }
}
*/