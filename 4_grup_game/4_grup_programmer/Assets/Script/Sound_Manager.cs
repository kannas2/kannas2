using UnityEngine;
using System.Collections;

public class Sound_Manager : Singleton<Sound_Manager>
{

    public AudioClip[] ListBGM;
    private AudioSource _BGMAudio;
    public AudioClip[] ListEffect;
    private AudioSource _EffectAudio;

    void Awake()
    {
        ListBGM = new AudioClip[1] 
        {
            Resources.Load("Sounds/main_bgm",typeof(AudioClip)) as AudioClip
        };

        ListEffect = new AudioClip[8]
        {
            Resources.Load("Sounds/car_crash",      typeof(AudioClip)) as AudioClip,             
            Resources.Load("Sounds/car_crash_human",typeof(AudioClip)) as AudioClip,       
            Resources.Load("Sounds/down_steel",     typeof(AudioClip)) as AudioClip,            
            Resources.Load("Sounds/eat",            typeof(AudioClip)) as AudioClip,                   
            Resources.Load("Sounds/ride_car",       typeof(AudioClip)) as AudioClip,              
            Resources.Load("Sounds/throw",          typeof(AudioClip)) as AudioClip,                 
            Resources.Load("Sounds/walk",           typeof(AudioClip)) as AudioClip,                  
            Resources.Load("Sounds/walk_car",       typeof(AudioClip)) as AudioClip
        };

        _BGMAudio = this.gameObject.AddComponent<AudioSource>();
        _BGMAudio.loop = true;

        _EffectAudio = this.gameObject.AddComponent<AudioSource>();
        _EffectAudio.loop = false;
    }

    public void PlayBGM(int num)
    {
        switch (num)
        {
            case 1:
                _BGMAudio.clip = ListBGM[0];
                _BGMAudio.volume = 0.3f;
                break;

            //나중에 다른 bgm을 쓸경우 추가.
        }
        _BGMAudio.Play();
    }

    public void PlayEffect(int num)
    { 
        switch (num)
        {
            case 1:
                _EffectAudio.clip = ListEffect[0];
                _EffectAudio.volume = 0.3f;
                break;
            case 2:
                _EffectAudio.clip = ListEffect[1];
                _EffectAudio.volume = 0.8f;
                break;
            case 3:
                _EffectAudio.clip = ListEffect[2];
                _EffectAudio.volume = 0.9f;
                break;
            case 4:
                _EffectAudio.clip = ListEffect[3];
                _EffectAudio.volume = 0.9f;
                break;
            case 5:
                _EffectAudio.clip = ListEffect[4];
                _EffectAudio.volume = 0.5f;
                break;
            case 6:
                _EffectAudio.clip = ListEffect[5];
                _EffectAudio.volume = 0.5f;
                break;
            case 7:
                _EffectAudio.clip = ListEffect[6];
                _EffectAudio.volume = 0.5f;
                break;
            case 8:
                _EffectAudio.clip = ListEffect[7];
                _EffectAudio.volume = 0.5f;
                break;
        }
        _EffectAudio.Play();
    }
}
