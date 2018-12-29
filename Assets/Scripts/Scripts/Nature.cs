using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
/// <summary>
/// 人物属性类
/// </summary>
public class Nature: MonoSingletion<Nature>
{
    public int Hp, MaxHp, Attack,Defend, Speed;

    void Start()
    {
        AssigNature();
    }

    /// <summary>
    /// 给属性赋值  assig分配，任务，作业，功课
    /// </summary>
    void AssigNature()
    {
        ////  for (int i = 0; i < Save.SaveUser.UserList.Count; i++)
        //  {
        Hp = Save.UserList[0].Hp;
        MaxHp = Save.UserList[0].MaxHp;
        Attack = Save.UserList[0].Attack;
        Speed = Save.UserList[0].Speed;
        //  }
    }

    public void WearItem(Item currentItem)
    {
        Save.UserList[0].Hp += currentItem.hp;
        Save.UserList[0].MaxHp += currentItem.hp;
        Save.UserList[0].Attack += currentItem.atk;
        Save.UserList[0].Speed += currentItem.spd;
        AssigNature();
    }

    public void TakeOffItem(Item currentItem)
    {
        Save.UserList[0].Hp -= currentItem.hp;
        Save.UserList[0].MaxHp -= currentItem.hp;
        Save.UserList[0].Attack -= currentItem.atk;
        Save.UserList[0].Speed -= currentItem.spd;
        AssigNature();
    }
}
