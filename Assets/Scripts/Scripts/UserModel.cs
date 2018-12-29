using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using System;

public class UserModel
{
    /*  {"UserList":[{"Hp":80,"MaxHp":120,"Attack":35,"Speed":25}]}  */
    public int Hp;
    public int MaxHp;
    public int Attack;
    public int Speed;
}

[System.Serializable]
public class ItemModel //商品信息
{
    public int Id;
    //public string Name;
    //public string Nature;//图片种类(图片名)
    //public string Function;//描述
    //public int Value;//值
    public int Num;//数量
}


public class Save
{
    /// <summary>
    /// 用户列表
    /// </summary>
    public static List<UserModel> UserList
    {
        get;set;
    }

    /// <summary>
    /// 背包里的物品s
    /// </summary>
    public static List<ItemModel> BagItemList;

    /// <summary>
    /// 装备栏里的物品s
    /// </summary>
    public static List<ItemModel> EquipItemList
    {
        get;set;
    }

    /// <summary>
    /// 往背包中添加物品
    /// </summary>
    /// <param name="_item"></param>
    public static void AddItemToBag(Item _item)
    {
        if (BagItemList == null)
        {
            BagItemList = new List<ItemModel>();
        }
        //先看看背包里有没有
        ItemModel gm = BagItemList.Find(x => x.Id == _item.item_ID);
        if (gm != null)//有，数量加1
        {
            gm.Num += 1;
        }
        else//没有，添加新的
        {
            BagItemList.Add(new ItemModel() { Id = _item.item_ID, Num = 1 });
        }
        SaveBag();
    }

    /// <summary>
    /// 使用药品
    /// </summary>
    /// <param name="_id"></param>
    public static void UsePotion(int _id)
    {
        UseItem(_id);
        //更新属性信息
    }

    /// <summary>
    /// 穿上了装备
    /// </summary>
    /// <param name="_id"></param>
    public static void UseEquip(int _id)
    {
        if (EquipItemList == null)
        {
            EquipItemList = new List<ItemModel>();//
        }
        //更新装备List信息
        //判断装备栏里有没有同类型的，如果没有，穿;如果有，换
        ItemModel good = EquipItemList.Find(x => x.Id == _id);
        if (good != null)//脱
        {
            EquipItemList.Remove(good);//把旧物品从装备栏里移除
            BagItemList.Add(good);//把旧物品加到背包里
        }
        //穿
        EquipItemList.Add(new ItemModel() { Id = _id, Num = 1 });

        SaveEquip();

        UseItem(_id);
    }

    /// <summary>
    /// 保存背包里的物品数据
    /// </summary>
    private static void SaveBag()
    {
        string path = Application.dataPath + @"/Resources/Setting/BagItemList.json";

        using (StreamWriter sw = new StreamWriter(path))
        {
            string json = JsonConvert.SerializeObject(BagItemList);
            sw.Write(json);
        }
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 保存装备栏的数据
    /// </summary>
    private static void SaveEquip()
    {
        string path = Application.dataPath + @"/Resources/Setting/EquipItemList.json";

        using (StreamWriter sw = new StreamWriter(path))
        {
            string json = JsonConvert.SerializeObject(EquipItemList);
            sw.Write(json);
        }
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 脱装备，更新装备List
    /// </summary>
    /// <param name="_item"></param>
    public static void TakeOffEquip(Item _item)
    {
        //移除指定物品
        EquipItemList.RemoveAll(x => x.Id == _item.item_ID);
        SaveEquip();
        AddItemToBag(_item);
    }

    /// <summary>
    /// 使用背包里的物品,更新List
    /// </summary>
    private static void UseItem(int _id)
    {
        ItemModel gm = BagItemList.Find(x => x.Id == _id);
        gm.Num--;
        if (gm.Num<=0)
        {
            BagItemList.Remove(gm);
        }
        SaveBag();
    }

    public static void SaveTask(Dictionary<string, Task> taskDict)
    {
        string path = Application.dataPath + @"/Resources/Setting/MyTask.json";

        using (StreamWriter sw = new StreamWriter(path))
        {
            string json = JsonConvert.SerializeObject(taskDict);
            sw.Write(json);
        }
        AssetDatabase.Refresh();
    }
}

