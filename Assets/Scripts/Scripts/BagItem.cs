using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

/// <summary>
/// 挂载在预设物上  
/// </summary>
public class BagItem : MonoBehaviour,IPointerDownHandler
{
    //当前物品的图片
    private Sprite Sprite;
    //当前物品
    public GoodsModel CurrentGoods;
    public static event Action<GoodsModel> OnItemSelceted;

    public void Init(GoodsModel _Good,Sprite _sprite)
    {
        CurrentGoods = _Good;Sprite = _sprite;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (OnItemSelceted != null)
        {
            OnItemSelceted(CurrentGoods);
        }
    }
}
