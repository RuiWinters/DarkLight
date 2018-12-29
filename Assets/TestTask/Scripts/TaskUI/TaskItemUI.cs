using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TaskItemUI : MonoBehaviour {

    public Task task;//对应的任务逻辑

    [SerializeField]
    /// <summary>
    /// 任务名称
    /// </summary>
    private Text taskName;
    /// <summary>
    /// 任务描述
    /// </summary>
    [SerializeField]
    private Text taskDes;
    
    public Text buttonText;

    [SerializeField]
    private GameObject processGobj;
    public List<TaskItemProcess> processText = new List<TaskItemProcess>();

    [SerializeField]
    private GameObject rewardGobj;
    public List<TaskItemReward> rewardText = new List<TaskItemReward>();

    [SerializeField]
    private Transform processParent;
    [SerializeField]
    private Transform rewardParent;

    [SerializeField]
    private Button btnCancel;
    [SerializeField]
    private Button btnState;

    void Start()
    {
        btnCancel = transform.Find("ButtonCancel").GetComponent<Button>();
        btnState = transform.Find("ButtonState").GetComponent<Button>();

        btnCancel.onClick.AddListener(Cancel);
        btnState.onClick.AddListener(Reward);
    }

    public void Init(TaskEventArgs e)
    {
        processGobj = Resources.Load("UIPrefab/Task/Process") as GameObject;
        rewardGobj = Resources.Load("UIPrefab/Task/Reward") as GameObject;

        task = TaskManager.Instance.GetTaskInfoByID(e.taskID);

        taskName.text = task.taskName;
        taskDes.text = task.description;

        //任务条件
        for (int i = 0; i < task.taskConditions.Count; i++)
        {
            GameObject a = Instantiate(processGobj) as GameObject;
            a.transform.SetParent(processParent);
            a.transform.localPosition = Vector3.zero;
            a.transform.localScale = Vector3.one;

            TaskItemProcess tP = a.GetComponent<TaskItemProcess>();
            processText.Add(tP);

            tP.id.text = task.taskConditions[i].id;
            tP.now.text = task.taskConditions[i].nowAmount.ToString();
            tP.target.text = task.taskConditions[i].targetAmount.ToString();
        }

        //任务奖励
        for (int i = 0; i < task.taskRewards.Count; i++)
        {
            GameObject a = Instantiate(rewardGobj) as GameObject;
            a.transform.SetParent(rewardParent);
            a.transform.localPosition = Vector3.zero;
            a.transform.localScale = Vector3.one;

            TaskItemReward tR = a.GetComponent<TaskItemReward>();
            rewardText.Add(tR);

            tR.id.text = task.taskRewards[i].id;
            tR.amount.text = task.taskRewards[i].amount.ToString();
        }
    }

    /// <summary>
    /// 修改条件的当前进度
    /// </summary>
    /// <param name="id"></param>
    /// <param name="amount"></param>
    public void Modify(TaskEventArgs e)
    {
        for (int i = 0; i < processText.Count; i++)
        {
            if (processText[i].id.text == e.id)
            {
                processText[i].now.text = e.amount.ToString();
            }               
        }     
    }

    /// <summary>
    /// 修改任务进度
    /// </summary>
    /// <param name="id"></param>
    public void Modify(string id)
    {
        for (int i = 0; i < processText.Count; i++)
        {
            if (processText[i].id.text == id)
            {
                processText[i].now.text = task.taskConditions[i].nowAmount.ToString();
            }
        }
    }

    /// <summary>
    /// 完成任务后，修改UI
    /// </summary>
    /// <param name="isFinish"></param>
    public void Finish(bool isFinish)
    {
        if (isFinish)
            buttonText.text = "完成了";
        else
            buttonText.text = "未完成";
    }

    /// <summary>
    /// 获取奖励
    /// </summary>
    public void Reward()
    {
        if (buttonText.text == "完成了")
        //task.Reward();
        {
            TaskEventArgs e = new TaskEventArgs();
            e.taskID = task.taskID;
            TaskManager.Instance.GetReward(e);
        }
    }

    /// <summary>
    /// 取消任务
    /// </summary>
    public void Cancel()
    {
        //task.Cancel();
        TaskEventArgs e = new TaskEventArgs();
        e.taskID = task.taskID;
        TaskManager.Instance.CancelTask(e);
    }
}
