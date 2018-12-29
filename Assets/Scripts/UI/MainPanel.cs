using System.Collections;
using System.Collections.Generic;
using TinyTeam.UI;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : TTUIPage
{
    //主界面的各种按钮
    private Button buttonStatus, buttonEquip, buttonBag, buttonSkill, buttonTishi;
    //private List<int> tempShopList = new List<int>();
    Button buttonTask;

    public MainPanel():base(UIType.Normal,UIMode.DoNothing,UICollider.None)
    {
        uiPath = "UIPrefab/MainPanel";
    }

    public override void Awake(GameObject go)
    {
        base.Awake(go);
        //找到各个按钮
        buttonStatus = transform.Find("ButtonStatus").GetComponent<Button>();
        buttonEquip = transform.Find("ButtonEquip").GetComponent<Button>();
        buttonBag = transform.Find("ButtonBag").GetComponent<Button>();
        buttonSkill = transform.Find("ButtonSkill").GetComponent<Button>();
        buttonTishi = transform.Find("ButtonTishi").GetComponent<Button>();
        buttonTask = transform.Find("ButtonTask").GetComponent<Button>();

        buttonTishi.gameObject.SetActive(false);//默认隐藏提示按钮

        ShopItemlist.OnNPCTrigger += ShowTishi;//提示按钮在人物接近商店NPC时出现,远离时隐藏
        TaskList.OnNPCTrigger += ShowTishi;

        //buttonTishi.onClick.AddListener(() => TTUIPage.ShowPage<ShopPanel>(tempShopList));
        buttonBag.onClick.AddListener(() => TTUIPage.ShowPage<BagPanel>());
        buttonEquip.onClick.AddListener(() => TTUIPage.ShowPage<EquipPanel>());
        buttonTask.onClick.AddListener(() => TTUIPage.ShowPage<TaskPanel>());
        //先加载TaskPanel的资源，防止TaskPanel里的事件不能及时绑定
        TTUIPage.ShowPage<TaskPanel>();
        TTUIPage.ClosePage<TaskPanel>();
    }

    /// <summary>
    /// 是否显示提示按钮
    /// </summary>
    /// <param name="isshow">按钮是否显示</param>
    /// <param name="npcTag">NPC的标签</param>
    /// <param name="_list">NPC传来的物品列表</param>
    public void ShowTishi(bool isshow,string npcTag,List<int> _list)
    {
        buttonTishi.gameObject.SetActive(isshow);//默认隐藏提示按钮

        if (isshow)
        {
            if (npcTag== "Npc_Shop")
            {
                buttonTishi.onClick.AddListener(() => TTUIPage.ShowPage<ShopPanel>(_list));
            }
            else if (npcTag == "Npc")
            {
                buttonTishi.onClick.AddListener(() => TTUIPage.ShowPage<TaskListPanel>(_list));
            }
        }

        if (!isshow)
        {
            if (TTUIPage.allPages.ContainsKey("ShopPanel"))
            {
                TTUIPage.ClosePage<ShopPanel>();
            }        
            buttonTishi.onClick.RemoveAllListeners();
        }
    }
}
