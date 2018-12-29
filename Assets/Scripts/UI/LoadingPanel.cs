using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyTeam.UI;
using UnityEngine.UI;

public class LoadingPanel : TTUIPage
{
    public Slider sliderLoading;
    public Text textProgress;
    public LoadingPanel():base(UIType.Normal,UIMode.DoNothing,UICollider.None)
    {
        uiPath = "UIPrefab/LoadingPanel";
    }

    public override void Awake(GameObject go)
    {
        base.Awake(go);
        sliderLoading = transform.Find("Slider").GetComponent<Slider>();
        textProgress = transform.Find("Text").GetComponent<Text>();
    }
}
