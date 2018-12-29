using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TinyTeam.UI;

public class TaskListPanel : TTUIPage
{

    Button btn1, btn2, btn3;
    //关闭按钮
    private Button buttonClose;

    public TaskListPanel():base(UIType.Normal,UIMode.HideOther,UICollider.None)
    {
        uiPath = "UIPrefab/TaskListPanel";
    }

    public override void Awake(GameObject go)
    {
        base.Awake(go);
        //关闭按钮
        buttonClose = transform.Find("ButtonClose").GetComponent<Button>();
        buttonClose.onClick.AddListener(Hide);
        btn1 = transform.Find("ButtonTask1").GetComponent<Button>();
        btn2 = transform.Find("ButtonTask2").GetComponent<Button>();
        btn3 = transform.Find("ButtonTask3").GetComponent<Button>();

        btn1.onClick.AddListener(() => { TaskManager.Instance.AcceptTask("10001"); });
        btn2.onClick.AddListener(() => { TaskManager.Instance.AcceptTask("10002"); });
        btn3.onClick.AddListener(() => { TaskManager.Instance.AcceptTask("10003"); });
    }
}
