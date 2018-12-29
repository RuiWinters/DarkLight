using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tools
{
    /// <summary>
    /// 先经过Loading界面，然后加载指定场景
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="sceneName"></param>
    public static void LoadSceneByLoading(string sceneName)
    {
        SceneManager.LoadScene("Loading");
        GameCtrl.Instance.nextSceneName = sceneName;
    }

    public static T FindInChildren<T>(GameObject go) where T :Component 
    {
        if (go == null) return null;
        var comp = go.GetComponent<T>();

        if (comp != null)
            return comp;

        for (int i = 0; i < go.transform.childCount; i++)
        {
            comp = FindInChildren<T>(go.transform.GetChild(i).gameObject);
            if (comp != null)
                return comp;
        }

        return comp;
    }

}
