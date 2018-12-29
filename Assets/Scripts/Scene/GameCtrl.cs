using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCtrl:MonoBehaviour
{
    private static GameCtrl _instance = null;

    public string nextSceneName;//要加载的下个场景的名字

    //共有的唯一的，全局访问点
    public static GameCtrl Instance
    {
        get
        {
            if (_instance == null)
            {    //查找场景中是否已经存在单例
                _instance = GameObject.FindObjectOfType<GameCtrl>();
                if (_instance == null)
                {    //创建游戏对象然后绑定单例脚本
                    GameObject go = new GameObject("GameCtrl");
                    _instance = go.AddComponent<GameCtrl>();
                    
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {    //防止存在多个单例
        if (_instance == null) _instance = this;
        else Destroy(this);
        DontDestroyOnLoad(gameObject);
    }
}
