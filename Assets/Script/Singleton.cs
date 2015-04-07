using UnityEngine;
using System.Collections;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance = null;
    private static object _lock = new object();

    public static T Instance
    {
        get
        {
            //다른 오브젝트 인스턴스/
            _instance = GameObject.FindObjectOfType<T>() as T;

            lock (_lock)
            {
                if (!_instance)
                {
                    GameObject obj = new GameObject();  //new obj
                    obj.name = typeof(T).ToString();    //obj name class
                    _instance = obj.AddComponent<T>();  //obj add class script
                }
                return _instance;
            }
        }
    }
}

/*
 private static T _instance = null;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject();
                _instance = obj.AddComponent<T>();
                obj.name = typeof(T).ToString();
                DontDestroyOnLoad(obj);
            }

            return _instance;
        }
    }
*/