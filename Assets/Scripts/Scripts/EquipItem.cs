using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class EquipItem : MonoBehaviour,IPointerDownHandler
{
    //当前物品的图片
    private Sprite Sprite;
    //当前物品
    public ItemModel CurrentGoods;
    public static event Action<ItemModel> OnItemSelceted;

    public void Init(ItemModel _Good, Sprite _sprite)
    {
        CurrentGoods = _Good; Sprite = _sprite;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (OnItemSelceted != null)
        {
            OnItemSelceted(CurrentGoods);
        }
    }
}
