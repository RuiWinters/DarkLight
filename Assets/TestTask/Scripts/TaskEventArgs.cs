using UnityEngine;
using System.Collections;
using System;

public class TaskEventArgs : EventArgs {

    /// <summary>
    /// 当前任务的ID
    /// </summary>
    public string taskID;//当前任务的ID
    /// <summary>
    /// 发生事件的对象的ID(例如敌人,商品)
    /// </summary>
    public string id;//发生事件的对象的ID(例如敌人,商品)
    /// <summary>
    /// 数量
    /// </summary>
    public int amount;//数量
}
