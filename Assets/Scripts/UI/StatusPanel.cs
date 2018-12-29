using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TinyTeam.UI;

public class StatusPanel : TTUIPage
{
    //用于显示属性的文字
    private Text textAtk, textDef, textSpd;

    public StatusPanel():base(UIType.Normal,UIMode.HideOther,UICollider.None)
    {
        uiPath = "UIPrefab/StatusPanel";
    }

    public override void Awake(GameObject go)
    {
        base.Awake(go);
        //初始化UI
        textAtk = transform.Find("ImageBG/Atk/TextContent").GetComponent<Text>();
        textDef = transform.Find("ImageBG/Def/TextContent").GetComponent<Text>();
        textSpd = transform.Find("ImageBG/Spd/TextContent").GetComponent<Text>();
    }

    public override void Refresh()
    {
        base.Refresh();
        //更新属性
        textAtk.text = Nature.Instance.Attack.ToString();
        textDef.text = Nature.Instance.Defend.ToString();
        textSpd.text = Nature.Instance.Speed.ToString();
    }
}
