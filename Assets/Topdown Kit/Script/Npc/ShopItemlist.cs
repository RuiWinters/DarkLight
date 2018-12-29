/// <summary>
/// Npc shop.
/// This script use to create a shop to sell item
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class ShopItemlist : MonoBehaviour {
	
    public static event Action<bool,string,List<int>> OnNPCTrigger;//��������˳�NPC��������ص��¼�

	public List<int> itemIDs = new List<int>();
    //public Button gantanhao;
	
	void Start()
	{
		if(this.gameObject.tag == "Untagged")
			this.gameObject.tag = "Npc_Shop";
	}
	
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //gantanhao.gameObject.SetActive(true);
            //gantanhao.onClick.AddListener(�̵�.�򿪣�itemID��)
            //�����ť
            //���̵�
            //�����̵��������Ʒ
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
            OnNPCTrigger(false, gameObject.tag,itemIDs);
        }
    }
}


