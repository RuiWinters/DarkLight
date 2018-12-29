using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text Hp, MaxHp, Attack, Speed;
    public GameObject GoodsPrefab;//物品预设物

    public Transform Grid;//背包空格
    public GameObject[] GridArray;

    void Start()
    {
        RefreshNature();
        ShowBag();
    }

    /// <summary>
    /// 刷新属性界面数据
    /// </summary>
    public void RefreshNature()
    {
        //Hp.text = Nature.Instance.Hp + "";
        //MaxHp.text = Nature.Instance.MaxHp + "";
        //Attack.text = Nature.Instance.Attack + "";
        //Speed.text = Nature.Instance.Speed + "";
        //// 吃药的方法   使用物品后 属性改变
        //Nature.Instance.Eat();
    }

    /// <summary>
    /// 点击背包按钮
    /// </summary>
    int temp = 0;
    //public void BagBtnClick()
    //{
    //    if (temp % 2 == 0)
    //    {
    //        //显示背包数据
    //        ShowBag();
    //    }
    //    else
    //    {
    //        // 清除背包数据
    //        ClearBag();
    //    }
    //    temp++;
    //}
    
    /// <summary>
    /// 显示背包数据
    /// </summary>
    public void ShowBag()
    {
        //清除背包
       ClearBag();

        //遍历物品信息
        //int j = 0;
        //foreach (GoodsModel item in Save.goodList)
        //{
        //    // if (Save.SaveGoods.GoodsList[j].Num !=0)
        //    if (item.Num != 0)//物品数量不等于零时
        //    {
        //        //创建物品 NGUITools.AddChild(父物体，预设物);
        //        GameObject go = GameObject.Instantiate(GoodsPrefab);
        //        go.transform.SetParent(Grid.GetChild(j));
        //        go.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
        //        go.transform.localPosition = Vector3.zero;
        //        go.transform.localScale = Vector3.one;
                
                
        //        //显示物体的图片及数量
        //        Sprite tempSprite = Resources.Load<Sprite>(item.Nature);
        //        go.GetComponent<Image>().sprite = tempSprite;
        //        go.transform.GetChild(0).GetComponent<Text>().text = item.Num + "";
        //        go.GetComponent<ItemButton>().Init(item, tempSprite);
        //        //go.GetComponent<ItemButton>().Sprite = tempSprite;
        //        //go.GetComponent<ItemButton>().CurrentGoods = item;
        //        j++;
        //    }
        //}
        //for (int i = 0; i < Save.SaveGoods.GoodsList.Count; i++)
        //{
        //    if (Save.SaveGoods.GoodsList[i].Num != 0)//物品数量不等于零时
        //    {
        //        //创建物品 NGUITools.AddChild(父物体，预设物);
        //        GameObject go = NGUITools.AddChild(GameObject.Find("Grid").transform.GetChild(i).gameObject, GoodsPrefab);
        //        go.GetComponent<UISprite>().spriteName = Save.SaveGoods.GoodsList[i].Nature;
        //        go.transform.GetChild(0).GetComponent<UILabel>().text = Save.SaveGoods.GoodsList[i].Num + "";
        //    }
        //}

    }
    /// <summary>
    /// 清除背包数据
    /// </summary>
    public void ClearBag()
    {
     //删除之前创建物品的预设物
        for (int i = 0; i < GridArray.Length; i++)
        {
            if (GridArray[i].transform.childCount !=0)
            {
                Transform t = GridArray[i].transform.GetChild(0);
                t.parent = null;
                Destroy(t.gameObject);
            }
        }
    }
    /// <summary>
    ///提示框的返回按钮
    /// </summary>
    public void ShowInfo_BackBtnClick()
    {
        //倒放 提示框动画
        //ShowInfoAnimation.PlayReverse();
    }
    /// <summary>
    /// 提示框中的使用物品按钮方法
    /// </summary>
    //当前使用的物品
    public ItemModel CurrentGoods;

    /// <summary>
    /// 使用物品
    /// </summary>
    /// <param name="id"></param>
    public void ShowInfo_UseGoods(int id)
    {
        //id = BagItem.CurrentGoodsId;
        //for (int i = 0; i <Save.goodList.Count ; i++)
        //{
        //    if (id== Save.goodList[i].Id)
        //    {
        //        CurrentGoods = Save.goodList[i];
        //    }
        //}
        //使用物品  类型
        //switch (id)
        //{
        //    case 0:
        //        Nature.Instance.Hp += CurrentGoods.Value;
        //        if (Nature.Instance.Hp>= Nature.Instance.MaxHp)
        //        {
        //            Nature.Instance.Hp = Nature.Instance.MaxHp;
        //        }
        //        break;
        //    case 1:
        //        Nature.Instance.MaxHp += CurrentGoods.Value;
        //        break;
        //    case 2:
        //        Nature.Instance.Attack += CurrentGoods.Value;
        //        break;
        //    case 3:
        //        Nature.Instance.Speed += CurrentGoods.Value;
        //        break;
        //    default:
        //        break;      
        //}
        CurrentGoods.Num--;
        if (CurrentGoods.Num <= 0)
        {
            //ShowInfoAnimation.PlayReverse();
            CurrentGoods.Num = 0;
        }
        //刷新属性界面数据
        RefreshNature();
        //刷新背包界面数据
        ShowBag();

        //for (int i = 0; i < Save.goodList.Count; i++)
        //{
        //    if (Save.goodList[i].Id ==CurrentGoods.Id)
        //    {
        //        Save.goodList[i] = CurrentGoods;
        //    }
        //}
    }

    /// <summary>
    /// 点击保存按钮
    /// </summary>
    public void SaveBtnClick()
    {
        string path = Application.dataPath + @"/Resources/Setting/UserJson.txt";
        FileInfo info = new FileInfo(path);
        StreamWriter sw = info.CreateText();
        //string json = JsonMapper.ToJson(Save.SaveUser);
        //sw.Write(json);
        sw.Close();
        sw.Dispose();
        AssetDatabase.Refresh();

        string path1 = Application.dataPath + @"/Resources/Setting/GoodsList.json";
        FileInfo info1 = new FileInfo(path1);
        StreamWriter sw1 = info1.CreateText();
        //string json1 = JsonMapper.ToJson(Save.SaveGoods);
        //sw1.Write(json1);
        sw1.Close();
        sw1.Dispose();
        AssetDatabase.Refresh();
    }
}
