﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TaskList : MonoBehaviour {

    public static event Action<bool,string, List<int>> OnNPCTrigger;//跟进入和退出NPC触发器相关的事件

    public List<int> itemIDs = new List<int>();

    void Start()
    {
        if (this.gameObject.tag == "Untagged")
            this.gameObject.tag = "Npc";
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //点击按钮
            //打开商店
            //设置商店里面的物品
            if (OnNPCTrigger != null)
            {
                OnNPCTrigger(true, gameObject.tag,itemIDs);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (OnNPCTrigger != null)
        {
            OnNPCTrigger(false, gameObject.tag, itemIDs);
        }
    }
}
