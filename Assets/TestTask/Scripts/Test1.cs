using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Test1 : MonoBehaviour {

    public GameObject taskPanel;

    //public Image testImage;

    void Start()
    {
        //testImage.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 250);
    }

    void OnGUI()
    {
        //if (GUILayout.Button("接受任务Task1"))
        //{
        //    TaskManager.Instance.AcceptTask("T001");
        //}

        //if (GUILayout.Button("接受任务Task2"))
        //{
        //    TaskManager.Instance.AcceptTask("T002");
        //}

        //if (GUILayout.Button("接受任务T003"))
        //{
        //    TaskManager.Instance.AcceptTask("T003");
        //}

        if (GUILayout.Button("打怪Enemy1"))
        {
            TaskEventArgs e = new TaskEventArgs();
            e.id = "Enemy1";
            e.amount = 1;
            MesManager.Instance.Check(e);
        }

        if (GUILayout.Button("打怪Enemy2"))
        {
            TaskEventArgs e = new TaskEventArgs();
            e.id = "Enemy2";
            e.amount = 1;
            MesManager.Instance.Check(e);
        }

        if (GUILayout.Button("获取物体Item1"))
        {
            TaskEventArgs e = new TaskEventArgs();
            e.id = "Item1";
            e.amount = 1;
            MesManager.Instance.Check(e);
        }

        if (GUILayout.Button("获取物体Item2"))
        {
            TaskEventArgs e = new TaskEventArgs();
            e.id = "Item2";
            e.amount = 1;
            MesManager.Instance.Check(e);
        }

        if (GUILayout.Button("丢弃物体Item1"))
        {
            TaskEventArgs e = new TaskEventArgs();
            e.id = "Item1";
            e.amount = -1;
            MesManager.Instance.Check(e);
        }

        if (GUILayout.Button("丢弃物体Item2"))
        {
            TaskEventArgs e = new TaskEventArgs();
            e.id = "Item2";
            e.amount = -1;
            MesManager.Instance.Check(e);
        }

        if (GUILayout.Button("打开任务面板"))
        {
            taskPanel.SetActive(true);
        }

        if (GUILayout.Button("关闭任务面板"))
        {
            taskPanel.SetActive(false);
        }
    }
}
