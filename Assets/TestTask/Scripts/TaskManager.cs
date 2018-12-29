using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System;
using Newtonsoft.Json;

public class TaskManager : MonoSingletion<TaskManager> 
{
    /// <summary>
    /// 当前任务列表
    /// </summary>
    private Dictionary<string, Task> currentTaskDic = new Dictionary<string, Task>();

    public event Action<TaskEventArgs> OnGetEvent;//接受任务时,更新任务到任务面板等操作
    public event Action<TaskEventArgs> OnCheckEvent;//更新任务信息
    public event Action<TaskEventArgs> OnFinishEvent;//完成任务时,提示完成任务等操作
    public event Action<TaskEventArgs> OnRewardEvent;//得到奖励时,显示获取的物品等操作
    public event Action<TaskEventArgs> OnCancelEvent;//取消任务时,显示提示信息等操作

    void Start()
    {
        MesManager.Instance.checkEvent += CheckTask;
        TaskAnalysis();
    }

    /// <summary>
    /// 任务数据解析
    /// </summary>
    void TaskAnalysis()
    {
        TextAsset goodsTA = Resources.Load("Setting/MyTask") as TextAsset;
        if (!goodsTA)
        {
            Debug.Log("TaskItemList文件不存在！");
            return;
        }
        else
        {
            currentTaskDic = JsonConvert.DeserializeObject<Dictionary<string, Task>>(goodsTA.text);
        }     
    }


    /// <summary>
    /// 获取当前的任务信息
    /// </summary>
    /// <param name="taskID"></param>
    /// <returns></returns>
    public Task GetTaskInfoByID(string taskID) 
    {
        if(currentTaskDic.ContainsKey(taskID))
        {
            return currentTaskDic[taskID];
        }
        else
        {
            Debug.LogError("你没有接这个任务！");
            return null;
        }
    }

    /// <summary>
    /// 接任务Task
    /// </summary>
    /// <param name="taskID"></param>
    public void AcceptTask(string taskID)
    {
        if (currentTaskDic.ContainsKey(taskID))
        {
            Debug.Log("你已经接了这个任务了，快去完成吧！");
            return;
        }
        else
        {           
            Task t = DataManager.Instance.GetTaskByID(taskID);
            if(t==null)
            {
                Debug.LogError("不存在该任务！！");
                return;
            }
            currentTaskDic.Add(taskID,t);//添加到当前任务列表
            //更新数据
            Save.SaveTask(currentTaskDic);

            TaskEventArgs e = new TaskEventArgs();
            e.taskID = taskID;
            if (OnGetEvent!=null)
            {
                OnGetEvent(e);
            }  
        }
    }



    /// <summary>
    /// 更新任务进度
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void CheckTask(TaskEventArgs e)
    {
        foreach (KeyValuePair<string, Task> kv in currentTaskDic)
        {
            TaskCondition tc;
            for (int i = 0; i < kv.Value.taskConditions.Count; i++)
            {
                tc = kv.Value.taskConditions[i];
                if (tc.id == e.id)
                {
                    if(tc.nowAmount>=tc.targetAmount)
                    {
                        return;
                    }
                    tc.nowAmount += e.amount;
                    e.taskID = kv.Value.taskID;
                    e.amount = tc.nowAmount;

                    if (tc.nowAmount < 0) tc.nowAmount = 0;
                    if (tc.nowAmount >= tc.targetAmount)
                    {
                        tc.nowAmount = tc.targetAmount;
                        tc.isFinish = true;                        
                    }
                    else
                    {
                        tc.isFinish = false;
                    }

                    //更新UI
                    if (OnCheckEvent != null)
                    {
                        OnCheckEvent(e);
                    }  
                }
            }
  
            for (int i = 0; i < kv.Value.taskConditions.Count; i++)
            {
                tc = kv.Value.taskConditions[i];
                if (!tc.isFinish)
                {
                    return;
                }
            }
            FinishTask(e);
        }
    }

    /// <summary>
    /// 任务完成
    /// </summary>
    /// <param name="e"></param>
    public void FinishTask(TaskEventArgs e)
    {
        if(OnFinishEvent!=null)
        {
            OnFinishEvent(e);
        }      
    }

    /// <summary>
    /// 获取任务奖励
    /// </summary>
    /// <param name="e"></param>
    public void GetReward(TaskEventArgs e)
    {
        if (currentTaskDic.ContainsKey(e.taskID))
        {
            Task t = currentTaskDic[e.taskID];
            for (int i = 0; i < t.taskRewards.Count; i++)
            {
                TaskEventArgs a = new TaskEventArgs();
                a.id = t.taskRewards[i].id;
                a.amount = t.taskRewards[i].amount;
                a.taskID = e.taskID;
                OnRewardEvent(a);

                currentTaskDic.Remove(e.taskID);
            }
        } 
    }

    /// <summary>
    /// 取消任务
    /// </summary>
    /// <param name="e"></param>
    public void CancelTask(TaskEventArgs e)
    {
        if (currentTaskDic.ContainsKey(e.taskID))
        {
            OnCancelEvent(e);

            currentTaskDic.Remove(e.taskID);
        }
    } 
}
