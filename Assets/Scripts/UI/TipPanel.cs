using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.UI;
using DG.Tweening;

public class TipPanel : TTUIPage
{
    Text textContent;//要显示的内容
    CanvasGroup cg;//统一调整UI的透明度

    public TipPanel():base(UIType.PopUp,UIMode.DoNothing,UICollider.Normal)
    {
        uiPath = "UIPrefab/TipPanel";
    }

    public override void Awake(GameObject go)
    {
        base.Awake(go);
        //查找UI组件
        textContent = transform.Find("ImageBg/TextContent").GetComponent<Text>();
        cg = go.GetComponent<CanvasGroup>();
    }

    public override void Refresh()
    {
        base.Refresh();

        //设置UI内容
        textContent.text = data.ToString();
        cg.alpha = 1;
        cg.DOFade(0, 0.5f).SetDelay(0.5f).OnComplete(() => Hide());
    }
}
