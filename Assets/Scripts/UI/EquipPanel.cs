using System;
using System.Collections;
using System.Collections.Generic;
using TinyTeam.UI;
using UnityEngine;
using UnityEngine.UI;

public class EquipPanel : TTUIPage
{
    //6个格子
    private Transform head, armor, leftHand, rightHand, shoes, accessory;
    //装备格子
    private Dictionary<string, GameObject> cellDict = new Dictionary<string, GameObject>();

    //关闭按钮
    private Button buttonClose;

    //信息显示相关的UI
    private Text infoName, infoDes;
    private Transform infoParent;
    private Button buttonOff, buttonCancel;

    private GameObject itemPrefab;//物品预制
    Item currentItem;//当前所选择的物品

    public EquipPanel():base(UIType.Normal,UIMode.HideOther,UICollider.None)
    {
        uiPath = "UIPrefab/EquipPanel";
    }
    public override void Awake(GameObject go)
    {
        base.Awake(go);

        //关闭按钮
        buttonClose = transform.Find("ButtonClose").GetComponent<Button>();
        buttonClose.onClick.AddListener(Hide);


        //初始化信息框
        infoParent = transform.Find("ItemInfo");
        infoName = infoParent.Find("TextName").GetComponent<Text>();
        infoDes = infoParent.Find("TextDes").GetComponent<Text>();
        //脱装备按钮
        buttonOff = infoParent.Find("ButtonUse").GetComponent<Button>();
        buttonOff.onClick.AddListener(
            () => 
            {
                Debug.Log("脱下了装备");
                Save.TakeOffEquip(currentItem);
                Nature.Instance.TakeOffItem(currentItem);
                Refresh();
            });

        buttonCancel = infoParent.Find("ButtonCancel").GetComponent<Button>();
        //点击取消时，关闭信息框，取消物品选中效果
        buttonCancel.onClick.AddListener(() => {
            infoParent.gameObject.SetActive(false);

        });

        //初始化装备格子字典
        Transform cellParent = transform.GetChild(0);       
        for (int i = 0; i < cellParent.childCount; i++)
        {
            cellDict.Add(cellParent.GetChild(i).name, cellParent.GetChild(i).gameObject);
        }

        //初始化物品预制
        itemPrefab = Resources.Load("UIPrefab/EquipItem") as GameObject;

        //绑定点击装备格子的方法
        EquipItem.OnItemSelceted += ShowSelectedItemInfo;
    }

    /// <summary>
    /// 显示物品信息
    /// </summary>
    /// <param name="gm"></param>
    private void ShowSelectedItemInfo(GoodsModel gm)
    {
        Debug.Log(gm.Id);
        infoParent.gameObject.SetActive(true);
        currentItem = DataManager.Instance.GetItemByID(gm.Id);
        infoName.text = currentItem.item_Name;
        infoDes.text = currentItem.description;

        //修改弹出框的位置
        Vector3 worldPos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
            TTUIRoot.Instance.root as RectTransform,
            Input.mousePosition,
            TTUIRoot.Instance.uiCamera,
            out worldPos))
        {
            infoParent.position = worldPos;
        }
    }

    public override void Refresh()
    {
        base.Refresh();
        infoParent.gameObject.SetActive(false);
        ShowEquip();
    }

    /// <summary>
    /// 生成和显示装备物品
    /// </summary>
    private void ShowEquip()
    {
        //清除背包
        Clear();

        //遍历物品信息
        int j = 0;
        foreach (GoodsModel goodModel in Save.EquipItemList)
        {
            if (goodModel.Num != 0)//物品数量不等于零时
            {
                //创建物品
                GameObject go = GameObject.Instantiate(itemPrefab);
                //根据id得到对应的父物体
                Item itemInfo = DataManager.Instance.GetItemByID(goodModel.Id);
                Transform itemParent = cellDict[itemInfo.equipment_Type.ToString()].transform;

                go.transform.SetParent(itemParent);
                go.GetComponent<RectTransform>().sizeDelta = new Vector2(69, 69);
                go.transform.localPosition = Vector3.zero;
                go.transform.localScale = Vector3.one;

                //显示物体的图片及数量
                Sprite tempSprite = Resources.Load<Sprite>("Icon/" + goodModel.Id);
                go.GetComponent<Image>().sprite = tempSprite;
                ////设置数量文字
                //go.transform.GetChild(0).GetComponent<Text>().text = goodModel.Num + "";
                go.GetComponent<EquipItem>().Init(goodModel, tempSprite);
                j++;
            }
        }
    }

    /// <summary>
    /// 清除装备数据
    /// </summary>
    public void Clear()
    {
        //删除之前创建物品的预设物

        foreach (var item in cellDict.Values)
        {
            if (item.transform.childCount!=0)
            {
                Transform t = item.transform.GetChild(0);
                t.parent = null;
                GameObject.Destroy(t.gameObject);
            }
        }
    }
}
