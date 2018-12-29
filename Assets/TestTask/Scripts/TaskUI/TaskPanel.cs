using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TinyTeam.UI;
using UnityEngine.UI;

/// <summary>
/// 任务面板
/// </summary>
public class TaskPanel:TTUIPage
{
    private Dictionary<string, TaskItemUI> taskUIDic = new Dictionary<string, TaskItemUI>();//id,taskItem

    private GameObject contentParent;//内容
    private GameObject itemPrefab;//列表项
    Button buttonClose;

    public TaskPanel():base(UIType.Normal,UIMode.HideOther,UICollider.None)
    {
        uiPath = "UIPrefab/TaskPanel";
    }

    public override void Awake(GameObject go)
    {
        base.Awake(go);

        if (!contentParent)
        {
            contentParent = transform.Find("Scroll View/Viewport/Content").gameObject;
        }
        if (!itemPrefab)
        {
            itemPrefab = Resources.Load("UIPrefab/Task/TaskItem") as GameObject;
        }
        buttonClose = transform.Find("ButtonClose").GetComponent<Button>();
        buttonClose.onClick.AddListener(Hide);

        TaskManager.Instance.OnGetEvent += AddItem;
        TaskManager.Instance.OnRewardEvent += RemoveItem;
        TaskManager.Instance.OnFinishEvent += FinishTaskItem;
        TaskManager.Instance.OnCancelEvent += RemoveItem;
        TaskManager.Instance.OnCheckEvent += CheckTaskItem;
    }

    /// <summary>
    /// 更新任务UI
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void CheckTaskItem(TaskEventArgs e)
    {
        string tempTaskId = e.taskID;
        if(taskUIDic.ContainsKey(tempTaskId))
        {
            taskUIDic[tempTaskId].Modify(e);
        }
    }

    public void FinishTaskItem(TaskEventArgs e)
    {
        string tempTaskId = e.taskID;
        if (taskUIDic.ContainsKey(tempTaskId))
        {
            taskUIDic[tempTaskId].Finish(true);
        }
    }

    /// <summary>
    /// 添加列表项
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void AddItem(TaskEventArgs e)
    {
        if(taskUIDic.ContainsKey(e.taskID))
        {
            Debug.LogError("已经接受了这个任务！");
            return;
        }
        GameObject taskGobj = GameObject.Instantiate(itemPrefab) as GameObject;
        taskGobj.transform.SetParent(contentParent.transform);
        taskGobj.transform.localPosition = Vector3.zero;
        taskGobj.transform.localScale = Vector3.one;

        TaskItemUI t = taskGobj.GetComponent<TaskItemUI>();
        taskUIDic.Add(e.taskID,t);
        t.Init(e);
    }

    /// <summary>
    /// 移除列表项
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void RemoveItem( TaskEventArgs e)
    {
        if (taskUIDic.ContainsKey(e.taskID))
        {
            TaskItemUI t = taskUIDic[e.taskID];
            taskUIDic.Remove(e.taskID);
            GameObject.Destroy(t.gameObject);
        }      
    }
}
