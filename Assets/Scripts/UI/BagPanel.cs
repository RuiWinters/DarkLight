using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TinyTeam.UI;
using System;

public class BagPanel : TTUIPage
{
    //信息弹出框
    //装东西
    //容量限制
    //一个格子里的物品数量
    //使用物品

    public Transform Grid;//背包空格
    private List<GameObject> gridList = new List<GameObject>();//背包格子
    private GameObject itemPrefab;//物品模板
    private Button buttonClose;//关闭按钮

    //信息显示
    private Text infoName, infoDes;
    private Transform infoParent;
    private Button buttonUse, buttonCancel;

    Item currentItem;//当前所选择的物品

    public BagPanel():base(UIType.Normal,UIMode.HideOther,UICollider.None)
    {
        uiPath = "UIPrefab/BagPanel";
    }

    public override void Awake(GameObject go)
    {
        base.Awake(go);
        //初始化物品预制
        itemPrefab = Resources.Load("UIPrefab/BagItem") as GameObject;
        //初始化格子的父物体
        Grid = Tools.FindInChildren<GridLayoutGroup>(go).transform;
        buttonClose = transform.Find("ButtonClose").GetComponent<Button>();
        buttonClose.onClick.AddListener(Hide);

        BagItem.OnItemSelceted += ShowSelectedItemInfo;

        //初始化信息框
        infoParent = transform.Find("ItemInfo");
        infoName = infoParent.Find("TextName").GetComponent<Text>();
        infoDes = infoParent.Find("TextDes").GetComponent<Text>();
        //使用物品
        buttonUse = infoParent.Find("ButtonUse").GetComponent<Button>();
        buttonUse.onClick.AddListener(Button_UseGoodsClick);

        buttonCancel = infoParent.Find("ButtonCancel").GetComponent<Button>();
        //点击取消时，关闭信息框，取消物品选中效果
        buttonCancel.onClick.AddListener(() => {
            infoParent.gameObject.SetActive(false);

        });

        //初始所有格子
        for (int i = 0; i < Grid.childCount; i++)
        {
            gridList.Add(Grid.GetChild(i).gameObject);
        }
        
    }

    /// <summary>
    /// 显示物品信息
    /// </summary>
    /// <param name="gm"></param>
    private void ShowSelectedItemInfo(GoodsModel gm)
    {
        //Debug.Log(gm.Id);
        infoParent.gameObject.SetActive(true);
        currentItem = DataManager.Instance.GetItemByID(gm.Id);
        infoName.text = currentItem.item_Name;
        infoDes.text = currentItem.description;

        //修改弹出框的位置
        Vector3 worldPos;
        if(RectTransformUtility.ScreenPointToWorldPointInRectangle(
            TTUIRoot.Instance.root as RectTransform,
            Input.mousePosition,
            TTUIRoot.Instance.uiCamera,
            out worldPos))
        {
            infoParent.position = worldPos;
        }
    }

    /// <summary>
    /// 使用物品
    /// </summary>
    /// <param name="id"></param>
    public void Button_UseGoodsClick()
    {
        //药品
        if (currentItem.item_Type=="Potion")
        {
            //从背包数据列表里找到物品，并修改数量
            Save.UsePotion(currentItem.item_ID);
            //修改人物属性
        }
        //
        else if (currentItem.item_Type!="Etc"&& currentItem.item_Type!="None")
        {
            //从背包数据列表里找到物品，并修改数量
            Save.UseEquip(currentItem.item_ID);
            Nature.Instance.WearItem(currentItem);
            //修改人物属性
            //更新装备栏的物品数据
        }

        //刷新背包界面数据
        Refresh();
    }

    /// <summary>
    /// 每次打开界面时都会调用这个方法
    /// </summary>
    public override void Refresh()
    {
        base.Refresh();
        infoParent.gameObject.SetActive(false);
        ShowBag();
    }

    /// <summary>
    /// 显示背包数据
    /// </summary>
    public void ShowBag()
    {
        //清除背包
        ClearBag();

        //遍历物品信息
        int j = 0;
        foreach (GoodsModel item in Save.BagItemList)
        {
            if (item.Num != 0)//物品数量不等于零时
            {
                //创建物品
                GameObject go = GameObject.Instantiate(itemPrefab);
                go.transform.SetParent(Grid.GetChild(j));
                go.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 80);
                go.transform.localPosition = Vector3.zero;
                go.transform.localScale = Vector3.one;

                //显示物体的图片及数量
                Sprite tempSprite = Resources.Load<Sprite>("Icon/"+item.Id);
                go.GetComponent<Image>().sprite = tempSprite;
                //设置数量文字
                go.transform.GetChild(0).GetComponent<Text>().text = item.Num + "";
                go.GetComponent<BagItem>().Init(item, tempSprite);
                j++;
            }
        }
    }

    /// <summary>
    /// 清除背包数据
    /// </summary>
    public void ClearBag()
    {
        //删除之前创建物品的预设物
        for (int i = 0; i < gridList.Count; i++)
        {
            if (gridList[i].transform.childCount != 0)
            {
                Transform t = gridList[i].transform.GetChild(0);
                t.SetParent(null);
                GameObject.Destroy(t.gameObject);
            }
        }
    }

}
