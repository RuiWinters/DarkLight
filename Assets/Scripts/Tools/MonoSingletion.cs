using UnityEngine;
using System.Collections;

public abstract class MonoSingletion<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance 
    {
        get 
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<T>();

                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    instance = obj.AddComponent<T>();
                    obj.name = typeof(T).Name;

                    //切换场景后不销毁
                    DontDestroyOnLoad(obj);
                }
            }
            return instance;
        }
    }
}