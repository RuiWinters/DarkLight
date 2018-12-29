using System;
using System.Collections;
using System.Collections.Generic;
using TinyTeam.UI;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : TTUIPage
{
    private GameObject itemPrefab;//商店的格子预制
    private Transform itemParent;
    private GameObject itemInfo;//显示信息的UI
    private ToggleGroup group;
    private Button buttonClose;

    private Text infoName, infoDes;

    public ShopPanel():base(UIType.Normal,UIMode.HideOther,UICollider.None)
    {
        uiPath = "UIPrefab/ShopPanel";
    }

    public override void Awake(GameObject go)
    {
        base.Awake(go);
        //查找各种UI
        itemPrefab = Resources.Load<GameObject>("UIPrefab/ShopItem");
        itemParent = transform.Find("Scroll View/Viewport/Content");
        //itemParent = Tools.FindInChildren<GridLayoutGroup>(go).transform;
        itemInfo = transform.Find("ItemInfo").gameObject;
        group = transform.Find("ToggleGroup").GetComponent<ToggleGroup>();
        buttonClose = transform.Find("ButtonClose").GetComponent<Button>();
        buttonClose.onClick.AddListener(Hide);

        //信息显示相关的UI
        infoName = itemInfo.transform.Find("TextName").GetComponent<Text>();
        infoDes = itemInfo.transform.Find("TextDes").GetComponent<Text>();


        //选中一个物品后，执行显示信息的方法
        ShopItem.OnItemSelected += ShowSelectedItemInfo;

        //从NPC身上传过来的物品列表
        List<int> tempList = (List<int>)data;
        //根据物品列表列出物品
        for (int i = 0; i < tempList.Count; i++)
        {
            GameObject obj = GameObject.Instantiate(itemPrefab);
            Item info = DataManager.Instance.GetItemByID(tempList[i]);//得到物品信息

            obj.transform.SetParent(itemParent);
            obj.transform.Find("ImageSlot").GetComponent<Toggle>().group = group;
           
            obj.transform.GetComponent<ShopItem>().Init(info);
        }
    }

    /// <summary>
    /// 显示物品信息
    /// </summary>
    /// <param name="obj"></param>
    private void ShowSelectedItemInfo(Item obj)
    {
        infoName.text = obj.item_Name;
        infoDes.text = obj.description;
    }

    
    public override void Hide()
    {
        base.Hide();
        Clear();
    }

    void Clear()
    {
        GameObject.Destroy(gameObject);
    }

}
