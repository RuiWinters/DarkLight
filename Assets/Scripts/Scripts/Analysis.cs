using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;

/// <summary>
/// 解析数据
/// </summary>
public class Analysis : MonoBehaviour
{
	void Awake () {
        // 用户数据解析
        UserAnalysis();
        // 物品数据解析
        BagAnalysis();
        //装备数据解析
        EquipAnalysis();
    }

    /// <summary>
    /// 装备栏数据解析
    /// </summary>
    private void EquipAnalysis()
    {
        TextAsset goodsTA = Resources.Load("Setting/EquipItemList") as TextAsset;
        if (!goodsTA)
        {
            Debug.Log("EquipItemList文件不存在！");
            return;
        }
        Save.EquipItemList = JsonConvert.DeserializeObject<List<GoodsModel>>(goodsTA.text);
        print(goodsTA.text);
    }

    /// <summary>
    /// 用户数据解析
    /// </summary>
    void UserAnalysis()
    {
        TextAsset userTA = Resources.Load("Setting/UserJson") as TextAsset;
        if (!userTA)
        {
            return;
        }
        Save.UserList = JsonConvert.DeserializeObject<List<UserModel>>(userTA.text);
        //print(userTA.text);
    }

    /// <summary>
    /// 背包数据解析
    /// </summary>
    void BagAnalysis()
    {
        TextAsset goodsTA = Resources.Load("Setting/BagItemList") as TextAsset;
        if (!goodsTA)
        {
            Debug.Log("BagItemList文件不存在！");
            return;
        }
        Save.BagItemList = JsonConvert.DeserializeObject<List<GoodsModel>>(goodsTA.text);
        print(goodsTA.text);
    }

    /// <summary>
    /// 任务数据解析
    /// </summary>
    void TaskAnalysis()
    {
        TextAsset goodsTA = Resources.Load("Setting/TaskItemList") as TextAsset;
        if (!goodsTA)
        {
            Debug.Log("BagItemList文件不存在！");
            return;
        }
        Save.BagItemList = JsonConvert.DeserializeObject<List<GoodsModel>>(goodsTA.text);
    }
}
