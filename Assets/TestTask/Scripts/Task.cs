using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System;

public class Task 
{
    /// <summary>
    /// 任务ID
    /// </summary>
    public string taskID;
    /// <summary>
    /// 任务名字
    /// </summary>
    public string taskName;
    /// <summary>
    /// 任务描述 
    /// </summary>
    public string description; 
    /// <summary>
    /// 任务目标
    /// </summary>
    public List<TaskCondition> taskConditions = new List<TaskCondition>();
    /// <summary>
    /// 任务奖励
    /// </summary>
    public List<TaskReward> taskRewards = new List<TaskReward>();
      
    public Task(string _id,string _name,string _des,List<TaskCondition> _con,List<TaskReward> _rew)
    {
        this.taskID = _id; taskName = _name; description = _des; taskConditions = _con; taskRewards = _rew;
    }
}
