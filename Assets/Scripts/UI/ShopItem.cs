using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using TinyTeam.UI;

public class ShopItem : MonoBehaviour,IPointerDownHandler
{
    private Button buttonBuy;
    private Item itemInfo;//当前格子所代表的物品
    private Toggle toggle;

    public static event Action<Item> OnItemSelected;

    public void OnPointerDown(PointerEventData eventData)
    {
        SelectItem();
        toggle.isOn = true;
    }

    public void Init(Item _item)
    {
        itemInfo = _item;
        transform.localScale = Vector3.one;
        transform.localPosition = Vector3.zero;

        //设置商店格子的详细信息
        //图片 名称 类型 价格 购买按钮
        transform.Find("TextName").GetComponent<Text>().text = _item.item_Name;
        transform.Find("TextType").GetComponent<Text>().text = _item.item_Type;
        transform.Find("TextPrice").GetComponent<Text>().text = _item.price.ToString();
        transform.Find("ImageSlot/ImageItem").GetComponent<Image>().sprite = Resources.Load<Sprite>("Icon/" + _item.item_Img);
    }

    // Use this for initialization
    void Start ()
    {
        buttonBuy = transform.Find("ButtonBuy").GetComponent<Button>();
        toggle = transform.Find("ImageSlot").GetComponent<Toggle>();

        //购买按钮
        buttonBuy.onClick.AddListener(() => {
            Save.AddItemToBag(itemInfo);
            SoundManager.instance.PlayingSound("BuyItem");

            TTUIPage.ShowPage<TipPanel>("购买成功！");
            toggle.isOn = true;
        });
        toggle.onValueChanged.AddListener(x=> { SelectItem(); });

        Debug.LogWarning("记住调用Init方法，对物品信息进行初始化");
	}

    private void SelectItem()
    {
        if (OnItemSelected!=null)
        {
            OnItemSelected(itemInfo);
        }
    }
}
